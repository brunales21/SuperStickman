using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertasAutomaticas : MonoBehaviour
{
    [SerializeField] float speed;

    [SerializeField] GameObject ObjetoAMover;
    [SerializeField] Transform StartPoint;
    [SerializeField] Transform EndPoint;
    [SerializeField] Botton botton;
    private Vector3 MoverHacia;   
    
    void Start()
    {
       
        MoverHacia = EndPoint.position;

    }
     void Update() 
    {
        if (botton.pressedBoton)
        {
        
            ObjetoAMover.transform.position = Vector3.MoveTowards(ObjetoAMover.transform.position, MoverHacia, speed * Time.deltaTime);

            if (ObjetoAMover.transform.position == StartPoint.position)
            {
                MoverHacia = EndPoint.position;
            }
        }
    }

    
    
    
    
}
