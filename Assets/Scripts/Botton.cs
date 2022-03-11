using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botton : MonoBehaviour
{
    private const int LAYER_BOTON = 1 << 11;

    [SerializeField] PuertasAutomaticas puertasAuto;

    [SerializeField] PlayerBehaviour playerBehaviour;

    public AudioSource sonidoBoton;

    public Animator playerAnim;
    public Animator buttonAnim;
    public Collider2D botonCollider;

    public Transform botonCheck;
    public bool isInButton; //if the player is near it
    public bool pressedButton; //if the player has pressed the button

    float botonTiempoDeRestablecido = 1f;

    void Start()
    {
        buttonAnim = GetComponent<Animator>();
    }

    void Update()
    {
        isInButton = Physics2D.OverlapCircle(botonCheck.position, 0.1f, LAYER_BOTON);

        if (playerBehaviour.pressing && isInButton) // Sí estando cerca hace la animacion de pulsar el boton
        {
            buttonAnim.SetBool("isPressed", true); //Se activa la animacion del boton
            sonidoBoton.Play();
            pressedButton = true; //El boton se dispara
            Debug.Log("TOCANDO BOTON");

            StartCoroutine("setButton"); //El boton vuelve a su idle
         
        }
    }

    IEnumerator setButton()
    {
        yield return new WaitForSecondsRealtime(botonTiempoDeRestablecido);
        buttonAnim.SetBool("isPressed", false);

    }
}