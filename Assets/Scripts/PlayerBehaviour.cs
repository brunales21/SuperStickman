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
    [SerializeField] AudioSource fireSound;
    public AudioSource propulsorSound;

    private ISwitchable switchable;    

    bool lHeadInSpike;
    public bool rHeadInSpike;
    bool lFootInSpike;
    bool rFootInSpike;
    bool rCrouchCheck;


    public bool enPiso;
    bool isCrouch;
    public bool inSpike;
    bool canStand;
    bool isDead;
    public bool boostOutOfPower;

    bool isHeadInTecho;
    bool isInDoor;
    bool isShooting;
    bool canDoubleJump;


    public Transform StartPos;
    public Transform refHeadCheckInTecho;
    public Transform refHeadCheckL;
    public Transform refHeadCheckR;
    public Transform refRightFootCheck;
    public Transform refLeftFootCheck;
    public Transform refCrouchCheck;


    public Collider2D CrouchCollider;
    public Collider2D StandCollider;
    public UnityEngine.UI.Image TelaNegra; //muerte

    [SerializeField] GameObject screenMessagePrefab;
    [SerializeField] private ParticleSystem playerBurst;



    float fuerzaY = 7f;
    public float jumpForce;
    public float doubleJumpForce;
    float VelocidadPersonaje = 10f;

    

    float ValorAlfaDeseadoTelaNegra;
    bool primeraVez;
    public float fuerzaSalto;
    public bool pressing = false;

    float fireRate = 0.5f;
    float nextFire = 0f;

    bool canJump;
    bool canFly;
    bool noBoostPrimeraVez;




    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        dieSound = GetComponent<AudioSource>();

        canJump = false;
        canFly = true;
        noBoostPrimeraVez = true;

        //StartCoroutine("getSaludo");
    }

    void Update()
    {
        if (enPiso)
        {
            anim.SetBool("isJumping", false);
            anim.SetBool("isDoubleJumping", false);
        }

        if (isDead) 
        {
            playerController.velX = 0f;
            fuerzaSalto = 0f; 
        } else {
            primeraVez = true;
            playerController.velX = VelocidadPersonaje;
            fuerzaSalto = fuerzaY;
        }


        if (!Input.GetButton("Crouch"))
        {
            lHeadInSpike = Physics2D.OverlapCircle(refHeadCheckL.position, 0.05f, LAYER_SPIKE);
            rHeadInSpike = Physics2D.OverlapCircle(refHeadCheckR.position, 0.05f, LAYER_SPIKE);
        }

        lFootInSpike = Physics2D.OverlapCircle(refLeftFootCheck.position, 0.05f, LAYER_SPIKE);
        rFootInSpike = Physics2D.OverlapCircle(refRightFootCheck.position, 0.05F, LAYER_SPIKE);
        rCrouchCheck = Physics2D.OverlapCircle(refCrouchCheck.position, 0.05F, LAYER_SPIKE);


        //MUERTE
        if (isTouchingSpike()) //si entra en el if, significa que el player ha muerto
        {
            if (primeraVez == true)
            {
                dieSound.Play();
                Instantiate(playerBurst, transform.position, Quaternion.identity);
                
                SetObjectToDisabled(gameObject);
                inSpike = true;
                primeraVez = false;
            }

            //anim.SetBool("isDead", true);
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

        if (Input.GetKey("e") && enPiso && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            
            anim.SetBool("isShooting", true);
            disparo();
            
        } else {
            anim.SetBool("isShooting", false);

        }
        

        enPiso = Physics2D.OverlapCircle(refLeftFootCheck.position, 0.5f, LAYER_PISO); //devuelve true or false en funcion de si el personaje está tocando el suelo
        anim.SetBool("enPiso", enPiso);

        //PROPULSAR
        if (Input.GetButtonDown("Propulsar") && canStand && canFly) //si entra en el if significas que ya no está tocando el suelo
        {
            
            //propulsorSound.Play();
            propulsar();
            anim.SetBool("isCrouch", false);
            anim.SetBool("isFalling", false);
            anim.SetBool("isPropulsing", true);

            enPiso = false;
        } //CAER
        else if (rb.velocity.y < 0 ) //Si la velocidad en el eje y es menor que 0, inicia la animacion de fall
        {
            anim.SetBool("isFalling", true);            
            anim.SetBool("isPropulsing", false);
            anim.SetBool("isCrouch", false);
        }

        //SALTAR
        if (Input.GetButton("Jump") && canJump)
        {
            if (enPiso)
            {
                anim.SetBool("isJumping", true);
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                canDoubleJump = true;

            } else if (Input.GetButtonDown("Jump") && canDoubleJump)
            {
                anim.SetBool("isDoubleJumping", true);
                rb.velocity = new Vector2(rb.velocity.x, doubleJumpForce);
                canDoubleJump = false;  
            }
            
        }
        

        //AGACHARSE
        if (Input.GetButton("Crouch") && enPiso) //Para agacharse (se switchean los colliders porque sino cuenta el doble de monedas)
        {
            //StartCoroutine("controlStandCheck");
            getCrouchCollider(CrouchCollider, StandCollider);
            anim.SetBool("isCrouch", true);
                    
        } else if (!isHeadInTecho) //Si no hay un techo arriba, se levanta
        {
            getStandCollider(StandCollider, CrouchCollider);
            anim.SetBool("isCrouch", false);
        }

        //TOCAR BOTON
        if (Input.GetKeyDown("s"))
        {
            anim.SetBool("isPressing", true);

            if (switchable != null) {
                if (switchable.isOn()) {
                    switchable.off();
                } else {
                    switchable.on();
                }
            }
        } else if (Input.GetKeyUp("s")) 
        {
            anim.SetBool("isPressing", false);
        }

        float valorAlfa = Mathf.Lerp(TelaNegra.color.a, ValorAlfaDeseadoTelaNegra, .05f);
        TelaNegra.color = new Color(0, 0, 0, valorAlfa);

        //RESPAWNEAR
        if (valorAlfa > 0.99f && isDead && !isInDoor) //El personaje resucita
        {
            fadeIn();
            SetObjectToEnabled(gameObject);
            anim.SetBool("isDead", false);
            isDead = false;
            gameObject.transform.position = StartPos.position;
        }
        if (valorAlfa > 0.99f && !isDead && isInDoor) //Si entra en la puerta, carga la escena siguiente
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        } 

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

        if (collision.gameObject.CompareTag("noBoostPoint"))
        {
            if (noBoostPrimeraVez)
            {
                Instantiate(screenMessagePrefab, transform.position, Quaternion.identity);
                noBoostPrimeraVez = false;
            }
            canJump = true;
            canFly = false;
        }
    }  
/*
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("noBoostPoint"))
        {
            Debug.Log("BOOST");
            boostOutOfPower = false;

            canJump = false;
            canFly = true;
        }
    }  
*/

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
    

    void propulsar()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(new Vector2(0, fuerzaSalto), ForceMode2D.Impulse);
    }

    void disparo()
    {
        //GameObject gameObject = Instantiate(bullet, firePoint.position, firePoint.rotation); 
        ////Bullet newBullet = (Bullet) gameObject.GetComponent(typeof(Bullet));
        //Bullet newBullet = gameObject.GetComponent<Bullet>();
      
        fireSound.Play();
        Bullet newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation).GetComponent<Bullet>();        
            
        if (playerController.facingRight)
        {
            newBullet.setDirectionRight();
        } else {
            newBullet.setDirectionLeft();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switchable = collision.gameObject.GetComponent<SwitchButton>();

        if (collision.gameObject.tag == "PlataformaMovible")
        {
            transform.parent = collision.transform;
        }   
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        switchable = null;
        if (collision.gameObject.tag == "PlataformaMovible")
        {
            transform.parent = null;
        }   
    }


    public void SetObjectToDisabled(GameObject gameObject)
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false; 
        gameObject.GetComponent<Collider2D>().enabled = false;
    }

    public void SetObjectToEnabled(GameObject gameObject)
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true; 
        gameObject.GetComponent<Collider2D>().enabled = true;
    }

   

    public bool isTouchingSpike()
    {
        if (lHeadInSpike||rHeadInSpike||lFootInSpike||rFootInSpike||rCrouchCheck)
        {
            return true;
        } else {
            return false;
        }
    }
    
}


