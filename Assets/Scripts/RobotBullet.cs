using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBullet : MonoBehaviour
{
    ApuntadoAutomatico apuntadoAutomatico;
    Rigidbody2D rbBotBullet;
    public GameObject robotBullet;
    public float bulletSpeed;

    void Start()
    {
        rbBotBullet = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        rbBotBullet.velocity = apuntadoAutomatico.transform.up * bulletSpeed;
        //transform.Translate(apuntadoAutomatico.transform.up * bulletSpeed);

    }
}
