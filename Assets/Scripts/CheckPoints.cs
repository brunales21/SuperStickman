using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{

    public Transform CheckPoint;
    public Transform StartPos;
    Animator anim;
    public bool OnCheckpoint;
    AudioSource CheckPointSound;
    bool firstTimeInCheckPoint;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        CheckPointSound = GetComponent<AudioSource>();

        firstTimeInCheckPoint = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        
        if (collision.gameObject.CompareTag("Personaje"))
        {
            if (firstTimeInCheckPoint)
            {
                CheckPointSound.Play();
                anim.SetBool("CheckPointOn", true);
                firstTimeInCheckPoint = false;
            }
                OnCheckpoint = true;
                StartPos.position = CheckPoint.position;
        }
    }
}
