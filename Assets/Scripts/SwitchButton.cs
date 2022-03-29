using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchButton : MonoBehaviour, ISwitchable
{

    [SerializeField] Transform StartPoint;
    [SerializeField] Transform EndPoint;

    public ISwitchable switchable;

    public AudioSource sonidoBoton;

    public Animator buttonAnim;
    public bool pressedButton; //if the player has pressed the button
    public string switchableName;

    void Start()
    {
        buttonAnim = GetComponent<Animator>();
        
        //El objeto que controlará
        switchable = null;

        //Busca el switchable que controlará
        //Busca por nombre
        if (!string.IsNullOrEmpty(switchableName)) {
            GameObject gameObject = GameObject.Find(switchableName);
            if (gameObject != null) {
                switchable = gameObject.GetComponent<ISwitchable>();
            }
        }/* else 
        //Busca dentro del mismo grupo (agrupados por un mismo padre)
        {
            foreach (Component c in GetComponentsInParent<Component>()) {
                foreach (ISwitchable s in c.GetComponentsInChildren<ISwitchable>()) {
                    if (this != s) {
                        switchable = s;
                        break;
                    }
                }
                if (switchable != null) {
                    break;
                }
            }
        }
        */                
    }

    void Update()
    {
        if (pressedButton) // Sí estando cerca hace la animacion de pulsar el boton...
        {            
            pressedButton = false;
            doEffect();
        }
    }

    private void doEffect() {
        buttonAnim.SetBool("isPressed", !buttonAnim.GetBool("isPressed"));
        sonidoBoton.Play();
    }
    
    public void on() {
        pressedButton = true;
        Debug.Log("Button: ON "+ switchableName + "("+switchable+")");
        if (switchable != null) {
            switchable.on();
        }
    }

    public void off() {
        pressedButton = true;
        Debug.Log("Button: OFF "+ switchableName + "("+switchable+")");
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
