using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertasAutomaticas : MonoBehaviour
{
    [SerializeField] Botton botton;

    [SerializeField] float speed;

    [SerializeField] GameObject ObjetoAMover;
    [SerializeField] Transform StartPoint;
    [SerializeField] Transform EndPoint;

    private Vector3 MoverHacia;
    public bool cerrado;
    
    void Start()
    {
        cerrado = false;
        MoverHacia = EndPoint.position;

    }
     void Update() 
    {
        cerrado = false;

        if (botton.pressedButton && !cerrado)
        {
            
            ObjetoAMover.transform.position = Vector3.MoveTowards(ObjetoAMover.transform.position, MoverHacia, speed * Time.deltaTime);
            Debug.Log("PUERTA ABRIENDOSE");


            if (ObjetoAMover.transform.position == EndPoint.position)
            {
                MoverHacia = StartPoint.position;
                ObjetoAMover.transform.position = Vector3.MoveTowards(ObjetoAMover.transform.position, MoverHacia, speed * Time.deltaTime);
                Debug.Log("PUERTA CERRANDOSE");

            } 
            if (ObjetoAMover.transform.position == StartPoint.position)
            {
                MoverHacia = EndPoint.position; // Para que a la siguiente pulsada se abra
                cerrado = true;
                botton.pressedButton = false;
                Debug.Log("PUERTA CERRADA");
        
            } 
        }
    }
}