using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botton : MonoBehaviour
{
    private const int LAYER_BOTON = 1 << 11;


    public PlayerController playerController;
    [SerializeField] PuertasAutomaticas puertasAuto;
    public AudioSource sonidoBoton;
    public Animator playerAnim;
    public Animator buttonAnim;
    public Collider2D botonCollider;
    public Transform botonCheck;

    public bool pressing;
    public bool isInBoton;
    public bool pressedBoton;




    void Start()
    {
        buttonAnim = GetComponent<Animator>();

    }

    void Update()
    {

        isInBoton = Physics2D.OverlapCircle(botonCheck.position, 0.1f, LAYER_BOTON);

        if (Input.GetKey("e"))
        {
            playerAnim.SetBool("isPressing", true);
            pressing = true;

        } else {
            playerAnim.SetBool("isPressing", false);
            pressing = false;

        }

        if (pressing && isInBoton)
        {
            Debug.Log("tocando boton");
            buttonAnim.SetBool("isPressed", true);
            pressedBoton = true;
            sonidoBoton.Play();
            StartCoroutine("setButon");
            
            StartCoroutine("BotonTimeWorking");
        }
    }

    
    IEnumerator setButon()
    {
        yield return new WaitForSecondsRealtime(1f);
        buttonAnim.SetBool("isPressed", false);

    }

    IEnumerator BotonTimeWorking()
    {   
        Debug.Log("dentro");
        yield return new WaitUntil(() => puertasAuto.cerrado);
        pressedBoton = false;
    
        
    }


    /*
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (pressing == true && collision.gameObject.CompareTag("Personaje"))
        {
            Debug.Log("tocando boton");
            buttonAnim.SetBool("isPressed", true);
            StartCoroutine("setButon");

        }
    }
    */
}
