using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmBehaviour : MonoBehaviour
{
    Animator alarmAnim;
    int numberIOfAlarms = 3;

    async void Start()
    {
        for (int i = 0; i < numberIOfAlarms; i++)
        {
            alarmAnim = GetComponentInChildren<Animator>();

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Personaje"))
        {
            Debug.Log("DETECTADO");
            alarmAnim.SetBool("isAlarmaOn", true);
        }
    }
}
