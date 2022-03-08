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
    public bool cerrado;
    
    void Start()
    {
        cerrado = false;
        MoverHacia = EndPoint.position;

    }
     void Update() 
    {


        if (botton.pressedBoton)
        {
           
            cerrado = false;
            ObjetoAMover.transform.position = Vector3.MoveTowards(ObjetoAMover.transform.position, MoverHacia, speed * Time.deltaTime);
            
            if (ObjetoAMover.transform.position == EndPoint.position)
            {
                MoverHacia = StartPoint.position;
                ObjetoAMover.transform.position = Vector3.MoveTowards(ObjetoAMover.transform.position, MoverHacia, speed * Time.deltaTime);
            } 
            if (ObjetoAMover.transform.position == StartPoint.position)
            {
                MoverHacia = EndPoint.position;
                ObjetoAMover.transform.position = Vector3.MoveTowards(ObjetoAMover.transform.position, MoverHacia, speed * Time.deltaTime);
                cerrado = true;
            } 
        }
    }

    IEnumerator Bajada()
    {
    
        Debug.Log("Esta bajando");
        MoverHacia = StartPoint.position;
        ObjetoAMover.transform.position = Vector3.MoveTowards(ObjetoAMover.transform.position, MoverHacia, speed * Time.deltaTime);
        yield return new WaitWhile(() => ObjetoAMover.transform.position == StartPoint.position);

    }

    
    
    
}
