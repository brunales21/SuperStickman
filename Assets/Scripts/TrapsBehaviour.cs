using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class TrapsBehaviour : MonoBehaviour
{   
    private const int LAYER_PISO = 1 << 6;
    [SerializeField] float speed;

    [SerializeField] GameObject ObjetoAMover;

    [SerializeField] Transform StartPoint;
    [SerializeField] Transform EndPoint; 
    AudioSource sonido;
    [SerializeField] Transform floorDetector;
    public bool touchingFloor;


    private Vector3 MoverHacia;   

    
    void Start()
    {
        MoverHacia = EndPoint.position;
        sonido = GetComponent<AudioSource>();

    }
     void Update() 
    {

        touchingFloor = Physics2D.OverlapCircle(floorDetector.position, 0.05f, LAYER_PISO);
        if (touchingFloor)
        {
            sonido.Play();
        }

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

