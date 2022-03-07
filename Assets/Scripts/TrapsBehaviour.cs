using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TrapsBehaviour : MonoBehaviour
{   
    [SerializeField] GameObject ObjetoAMover;

    [SerializeField] float speed;    


    [SerializeField] Transform StartPoint;
    [SerializeField] Transform EndPoint; 

    private Vector3 MoverHacia;   
    
    void Start()
    {
        MoverHacia = EndPoint.position;

    }
     void Update() 
    {
        ObjetoAMover.transform.position = Vector3.MoveTowards(ObjetoAMover.transform.position, MoverHacia, speed * Time.deltaTime);
        
        if (ObjetoAMover.transform.position == EndPoint.position)
        {
            MoverHacia = StartPoint.position;
        }

        if (ObjetoAMover.transform.position == StartPoint.position)
        {
            MoverHacia = EndPoint.position;
        }
    }

    
    
    
    
}

