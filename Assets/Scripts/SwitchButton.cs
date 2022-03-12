using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchButton : MonoBehaviour, ISwitchable
{
    private const int LAYER_SWITCHBUTTON = 1 << 12;
    [SerializeField] PlayerBehaviour playerBehaviour;

    [SerializeField] GameObject ObjetoAMover;
    [SerializeField] Transform StartPoint;
    [SerializeField] Transform EndPoint;
    private Vector3 MoverHacia;
    [SerializeField] float speed;

    public ISwitchable switchable;

    public AudioSource sonidoBoton;
    public Animator playerAnim;
    public Animator buttonAnim;
    public Collider2D botonCollider;

    public Transform botonCheck;
    public bool isInButton; //if the player is near it
    public bool pressedButton; //if the player has pressed the button



    void Start()
    {
        buttonAnim = GetComponent<Animator>();
        MoverHacia = EndPoint.position;
        switchable = null;

        foreach (Component c in GetComponentsInParent<Component>()) {
            foreach (ISwitchable s in c.GetComponentsInChildren<ISwitchable>()) {
                if (this != s) {
                    switchable = s;
                }
            }
        }        
        
    }

    void Update()
    {
        isInButton = Physics2D.OverlapCircle(botonCheck.position, 0.1f, LAYER_SWITCHBUTTON);

        if (pressedButton && isInButton) // Sí estando cerca hace la animacion de pulsar el boton
        {            
            pressedButton = false;

            if (isOn()) {
                effectOff();
            } else {
                effectOn();
            }

            //buttonAnim.SetBool("isPressed", !buttonAnim.GetBool("isPressed")); 
            //buttonAnim.SetBool("isPressed", true);
            //Debug.Log("buttonAnim: " + buttonAnim.GetBool("isPressed"));
            
        }
    }

    private void effectOn() {
        //TODO: Invocar animación ON
        buttonAnim.SetBool("isPressed", true);
        sonidoBoton.Play();
    }

    private void effectOff() {
        //TODO: Invocar animación OFF
        buttonAnim.SetBool("isPressed", false);
        sonidoBoton.Play();
    }
    
    public void on() {
        pressedButton = true;
        Debug.Log("Button: ON");
        if (switchable != null) {
            switchable.on();
        }
    }

    public void off() {
        pressedButton = true;
        Debug.Log("Button: OFF");
        if (switchable != null) {
            switchable.off();
        }
    }

    public bool isOn() {
        if (switchable == null) {
            return false;
        }
        return switchable.isOn();
    }
    public bool isOff() {
        if (switchable == null) {
            return true;
        }
        return switchable.isOff();
    }
}
