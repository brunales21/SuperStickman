using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class CoinCollector : MonoBehaviour
{

    [SerializeField] private ParticleSystem CoinBurst;
    private AudioSource CoinSound;
    private SpriteRenderer CoinRenderer;
    public GameObject Coin;
   


    // Start is called before the first frame update
    void Start()
    {
        CoinSound = GetComponent<AudioSource>();
    }

    
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Personaje"))
        {  
            Instantiate(CoinBurst, transform.position, Quaternion.identity);
            CoinSound.Play();
            SetObjectToDisabled(Coin);
        }
    }
/*
    IEnumerator DestroyObject()
    {
        //yield return new WaitForSecondsRealtime(0.35f);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        //gameObject.SetActive(false);


    }
    */

    public void SetObjectToDisabled(GameObject gameObject)
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false; 
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
    }
}
