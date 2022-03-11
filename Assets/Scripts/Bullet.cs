using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Bullet : MonoBehaviour {
    
    [SerializeField] PlayerController playerController;
    private Rigidbody2D rbBullet;

    public GameObject bullet;   

    public float bulletSpeed;
    float bulletLifeTime = 5f;
    public bool dirMomentoDisparo;


    void Start()
    {
        rbBullet = GetComponent<Rigidbody2D>();
    }
    void Update()
    {   
        if (Input.GetKey("e"))
        {
            dirMomentoDisparo = playerController.facingRight;
        }

        if (dirMomentoDisparo)
        {
            rbBullet.velocity = new Vector2(+bulletSpeed * Time.deltaTime, 0);
            
        } else
        {
            rbBullet.velocity = new Vector2(-bulletSpeed * Time.deltaTime, 0);
        }

        Destroy(gameObject, bulletLifeTime);
        
    }
}

 
