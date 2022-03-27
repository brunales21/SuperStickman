using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBullet : MonoBehaviour
{
    [SerializeField] ApuntadoAutomatico apuntadoAutomatico;
    Rigidbody2D rbBotBullet;
    public GameObject robotBullet;
    
    public float bulletSpeed;
    Vector2 direction;

    void Start()
    {
        rbBotBullet = GetComponent<Rigidbody2D>();
        rbBotBullet.velocity = direction.normalized * bulletSpeed * Time.deltaTime;
    }
    void Update()
    {   
    }

    public void setDirection(Vector2 direction)
    {
        this.direction = direction;
    }
}




