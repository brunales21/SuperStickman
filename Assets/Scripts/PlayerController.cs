using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    
    public bool facingRight = true;
    public float velX = 10;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        float movX;
        movX = Input.GetAxis("Horizontal");//si avanzamos hacia la derecha devuelve num positivo

        if(movX < 0 && facingRight) {
            flip();
        } else if (movX > 0 && !facingRight) {
            flip();
        }
        
        anim.SetFloat("absMovX", Mathf.Abs(movX));
        rb.velocity = new Vector2(velX * movX, rb.velocity.y); //al multiplicar la velocidad por un num positivo, avanzamos hacia la derecha

    }

    void flip() //esta funcion transforma facing right a facing left o viceversa
    {
        facingRight = !facingRight;
        transform.Rotate(Vector3.up, 180.0f, Space.World);
    }
}