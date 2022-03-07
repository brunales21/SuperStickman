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

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        CheckPointSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.CompareTag("Personaje")) {
            anim.SetBool("CheckPointOn", true);
            CheckPointSound.Play();
            OnCheckpoint = true;
            StartPos.position = CheckPoint.position;
            
        }
    }

}
