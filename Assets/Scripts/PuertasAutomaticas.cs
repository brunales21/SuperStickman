using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertasAutomaticas : MonoBehaviour, ISwitchable
{
    private Vector3 targetPosition;
    public Transform startPosition;
    public Transform endPosition;
    [SerializeField] float speed;
    private bool opened;
    private bool moving;
    bool pressedButton;
    void Start()
    {
        opened = false;
        targetPosition = endPosition.position;

    }
     void Update() 
    {

        if (moving)
        {
            
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            Debug.Log("PUERTA ABRIENDOSE");


            if (transform.position == endPosition.position)
            {
                targetPosition = startPosition.position;
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                Debug.Log("PUERTA CERRANDOSE");

            } 
            if (transform.position == startPosition.position)
            {
                targetPosition = endPosition.position; // Para que a la siguiente pulsada se abra
                moving = false;
                opened = false;
                Debug.Log("PUERTA CERRADA");
        
            } 
        }
    }

    public void on() {
        Debug.Log("Door: ON");
        targetPosition = endPosition.position;
        moving = true;
        opened = true;
    }

    public void off() {
        Debug.Log("Door: OFF");
        targetPosition = startPosition.position;
        moving = true;
        opened = false;
    }

    public bool isOn() {
        return opened;
    }

    public bool isOff() {
        return !isOn();
    }
}
