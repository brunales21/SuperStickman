using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaSecreta : MonoBehaviour
{
    [SerializeField] SwitchButton switchButton;

    [SerializeField] float speed;

    [SerializeField] GameObject ObjetoAMover;
    [SerializeField] Transform StartPoint;
    [SerializeField] Transform EndPoint;

    private Vector3 MoverHacia;
    public bool abierto;
    
    void Start()
    {
        abierto = false;
        MoverHacia = EndPoint.position;

    }
     void Update() 
    {
        
        if (switchButton.pressedButton)
        {
            ObjetoAMover.transform.position = Vector3.MoveTowards(ObjetoAMover.transform.position, MoverHacia, speed * Time.deltaTime);
            Debug.Log("PUERTA ABRIENDOSE");

            if (ObjetoAMover.transform.position == EndPoint.position)
            {
                MoverHacia = StartPoint.position;
                switchButton.pressedButton = false;

                Debug.Log("PUERTA CERRANDOSE");

            } 
            if (ObjetoAMover.transform.position == StartPoint.position)
            {
                MoverHacia = EndPoint.position;
                switchButton.pressedButton = false;

                Debug.Log("PUERTA CERRANDOSE");

            } 
        }

        
            
    }
}