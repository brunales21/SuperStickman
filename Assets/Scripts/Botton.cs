using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botton : MonoBehaviour
{
    private const int LAYER_BOTON = 1 << 11;


    public PlayerController playerController;
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

        if (pressing == true && isInBoton)
        {
            Debug.Log("tocando boton");
            buttonAnim.SetBool("isPressed", true);
            pressedBoton = true;
            sonidoBoton.Play();
            StartCoroutine("setButon");

        }
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
    IEnumerator setButon()
    {
        yield return new WaitForSecondsRealtime(1f);
        buttonAnim.SetBool("isPressed", false);

    }
}
