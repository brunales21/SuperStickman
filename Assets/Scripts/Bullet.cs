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
    float direction;

    void Start()
    {
        rbBullet = GetComponent<Rigidbody2D>();
    }
    void Update()
    {       
        rbBullet.velocity = new Vector2(direction * bulletSpeed * Time.deltaTime, 0);
        Destroy(gameObject, bulletLifeTime);
    }

    public void setDirectionLeft()
    {
        direction = -1f;        
    }

    public void setDirectionRight()
    {
        direction = 1f;        
    }
}

 
