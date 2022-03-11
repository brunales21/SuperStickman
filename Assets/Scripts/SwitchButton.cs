using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchButton : MonoBehaviour
{
    private const int LAYER_SWITCHBUTTON = 1 << 12;
    [SerializeField] PlayerBehaviour playerBehaviour;

    [SerializeField] GameObject ObjetoAMover;
    [SerializeField] Transform StartPoint;
    [SerializeField] Transform EndPoint;
    private Vector3 MoverHacia;
    [SerializeField] float speed;



    public AudioSource sonidoBoton;
    public Animator playerAnim;
    public Animator buttonAnim;
    public Collider2D botonCollider;

    public Transform botonCheck;
    public bool isInButton; //if the player is near it
    public bool pressedButton; //if the player has pressed the button
    public bool abierto;


    void Start()
    {
        buttonAnim = GetComponent<Animator>();
        abierto = false;
        MoverHacia = EndPoint.position;

    }

    void Update()
    {
        isInButton = Physics2D.OverlapCircle(botonCheck.position, 0.1f, LAYER_SWITCHBUTTON);

        if (playerBehaviour.pressing && isInButton) // Sí estando cerca hace la animacion de pulsar el boton
        {
            buttonAnim.SetBool("isPressed", true); //Se activa la animacion del boton
            sonidoBoton.Play();
            pressedButton = true; //El boton se dispara
            Debug.Log("TOCANDO BOTON");
        }





        if (pressedButton)
        {
                ObjetoAMover.transform.position = Vector3.MoveTowards(ObjetoAMover.transform.position, MoverHacia, speed * Time.deltaTime);
                Debug.Log("PUERTA ABRIENDOSE");

                if (ObjetoAMover.transform.position == EndPoint.position)
                {
                    MoverHacia = StartPoint.position;
                    pressedButton = false;

                    Debug.Log("PUERTA CERRANDOSE");

                } 
                if (ObjetoAMover.transform.position == StartPoint.position)
                {
                    MoverHacia = EndPoint.position;
                    pressedButton = false;

                    Debug.Log("PUERTA CERRANDOSE");

                }   
        }
    }
}