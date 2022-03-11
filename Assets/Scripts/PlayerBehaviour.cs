using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerBehaviour : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;

    [SerializeField] PlayerController playerController;


    public Transform firePoint;
    public GameObject bullet;
    float cadenciaDisparo;

    private const int LAYER_PISO = 1 << 6;
    private const int LAYER_SPIKE = 1 << 7;
    private const int LAYER_TECHO = 1 << 8;
    private const int LAYER_PUERTA = 1 << 9;


    AudioSource dieSound;
    public AudioSource propulsorSound;

    

    bool lHeadInSpike;
    bool rHeadInSpike;
    bool lFootInSpike;
    bool rFootInSpike;
    bool hipCheck;


    public bool enPiso;
    bool isCrouch;
    bool inSpike;
    bool canStand;
    bool isDead;

    bool isHeadInTecho;
    bool isInDoor;
    bool isShooting;


    public Transform StartPos;
    public Transform refHeadCheckInTecho;
    public Transform refHeadCheckL;
    public Transform refHeadCheckR;
    public Transform refRightFootCheck;
    public Transform refLeftFootCheck;
    public Transform HipCheck;


    public Collider2D CrouchCollider;
    public Collider2D StandCollider;


    float fuerzaY = 7f;
    float VelocidadPersonaje = 10f;

    

    public UnityEngine.UI.Image TelaNegra; //muerte
    float ValorAlfaDeseadoTelaNegra;
    bool primeraVez;
    public float fuerzaSalto;
    bool primerDisparo = true;
    public bool pressing;


    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        dieSound = GetComponent<AudioSource>();
    }

    void Update()
    {

        if (isDead) 
        {
            playerController.velX = 0f;
            fuerzaSalto = 0f; 
        } else {
            primeraVez = true;
            playerController.velX = VelocidadPersonaje;
            fuerzaSalto = fuerzaY;
        }
        
        lHeadInSpike = Physics2D.OverlapCircle(refHeadCheckL.position, 0.05f, LAYER_SPIKE);
        rHeadInSpike = Physics2D.OverlapCircle(refHeadCheckR.position, 0.05f, LAYER_SPIKE);
        lFootInSpike = Physics2D.OverlapCircle(refLeftFootCheck.position, 0.05f, LAYER_SPIKE);
        rFootInSpike = Physics2D.OverlapCircle(refRightFootCheck.position, 0.05F, LAYER_SPIKE);
        hipCheck = Physics2D.OverlapCircle(HipCheck.position, 0.05F, LAYER_SPIKE);


        //MUERTE
        if (lHeadInSpike||rHeadInSpike||lFootInSpike||rFootInSpike||hipCheck) //si entra en el if, significa que el player ha muerto
        {
            

            if (primeraVez == true)
            {
                dieSound.Play();
                anim.SetBool("inSpike", true);
                inSpike = true;
                primeraVez = false;
            }

            anim.SetBool("isDead", true);
            isDead = true;
                
            fadeOut();

        } else
        {
            anim.SetBool("inSpike", false);
            inSpike = false;
        }


        isHeadInTecho = Physics2D.OverlapCircle(refHeadCheckInTecho.position, 0.7f, LAYER_TECHO);

        if (isHeadInTecho) //Si hay techo, el player no se puede levantar
        {
            canStand = false;
            anim.SetBool("canStand", false);
        } else
        {
            canStand = true;
            anim.SetBool("canStand", true);
        }

        //DISPARAR

        if (Input.GetKey("e")) {
            
            anim.SetBool("isShooting", true);
            disparo();
            
        } else {
            anim.SetBool("isShooting", false);

        }

        
        

        enPiso = Physics2D.OverlapCircle(refLeftFootCheck.position, 1f, LAYER_PISO); //devuelve true or false en funcion de si el personaje está tocando el suelo
        anim.SetBool("enPiso", enPiso);

        //SALTAR
        if (Input.GetButtonDown("Jump") && canStand) //si entra en el if significas que ya no está tocando el suelo
        {
            
            propulsorSound.Play();
            saltar();
            anim.SetBool("isCrouch", false);
            anim.SetBool("isFalling", false);
            anim.SetBool("isJumping", true);

            enPiso = false;
        } //CAER
        else if (rb.velocity.y < 0) //Si la velocidad en el eje y es menor que 0, inicia la animacion de fall
        {
            anim.SetBool("isFalling", true);            
            anim.SetBool("isJumping", false);
            anim.SetBool("isCrouch", false);
        }


        //AGACHARSE
        if (Input.GetButton("Crouch") && enPiso) //Para agacharse (se switchean los colliders porque sino cuenta el doble de monedas)
        {
            getCrouchCollider(CrouchCollider, StandCollider);
            anim.SetBool("isCrouch", true);
            
        } else if (!isHeadInTecho) //Si no hay un techo arriba, se levanta
        {
            getStandCollider(StandCollider, CrouchCollider);
            anim.SetBool("isCrouch", false);
        }

        //TOCAR BOTON

        if (Input.GetKey("s"))
        {
            anim.SetBool("isPressing", true);
            pressing = true;
        } else {
            anim.SetBool("isPressing", false);
            pressing = false;
        }

        //SALUDAR
        if (Input.GetKey("r"))
        {
            anim.SetBool("isSaludando", true);

        } else {
            anim.SetBool("isSaludando", false);
        }

        float valorAlfa = Mathf.Lerp(TelaNegra.color.a, ValorAlfaDeseadoTelaNegra, .05f);
        TelaNegra.color = new Color(0, 0, 0, valorAlfa);

        //RESPAWNEAR
        if (valorAlfa > 0.99f && isDead && !isInDoor) //El personaje resucita
        {
            fadeIn();
            anim.SetBool("isDead", false);
            isDead = false;
            gameObject.transform.position = StartPos.position;
            //SceneManager.LoadScene("PlayScene");
        }
        if (valorAlfa > 0.99f && !isDead && isInDoor) //Si entra en la puerta, carga la escena siguiente
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        } 

    }



    public IEnumerator getFirstShot()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        Instantiate(bullet, firePoint.position, firePoint.rotation);
    }

    public IEnumerator getCadencia()
    {
        Instantiate(bullet, firePoint.position, firePoint.rotation);
        yield return new WaitForSecondsRealtime(5f);
    }


     void switchColliderOn(Collider2D collider)
   {
        collider.enabled = true;
   }

   void switchColliderOff(Collider2D collider)
   {
        collider.enabled = false;
   }

   void getCrouchCollider(Collider2D CrouchCollider, Collider2D StandCollider)
    {
        switchColliderOff(StandCollider);
        switchColliderOn(CrouchCollider);
    }

    void getStandCollider(Collider2D StandCollider, Collider2D CrouchCollider)
    {
        switchColliderOff(CrouchCollider);
        switchColliderOn(StandCollider);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Puerta"))
        {
            isInDoor = true;
            StartCoroutine("getFadeOutDelay");
            
        } else {
            isInDoor = false;
        }
    }   

    IEnumerator getFadeOutDelay()
    {
        fadeOut();
        yield return new WaitForSecondsRealtime(2f);
    }
    void fadeIn()
    {
        ValorAlfaDeseadoTelaNegra = 0; //fade in
    }

    void fadeOut()
    {
        ValorAlfaDeseadoTelaNegra = 1; //fade out
    }
    

    void saltar()
    {

        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(new Vector2(0, fuerzaSalto), ForceMode2D.Impulse);
    }

    void disparo()
    {
        Instantiate(bullet, firePoint.position, firePoint.rotation);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlataformaMovible")
        {
            transform.parent = collision.transform;
        }   
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlataformaMovible")
        {
            transform.parent = null;
        }   
    }
    
}

