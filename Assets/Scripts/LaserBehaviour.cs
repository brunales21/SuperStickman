using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour
{
    [SerializeField] GameObject laser;
    Collider2D laserCollider;
    SpriteRenderer laserSpriteRen;
    bool isLaserWorking;
    [SerializeField] float speed;

    void Start()
    {
        isLaserWorking = true;
        StartCoroutine("OnOffLaser");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

   IEnumerator OnOffLaser()
   {
       while (isLaserWorking)
       {
            yield return new WaitForSecondsRealtime(speed);
            SetObjectToDisabled(laser);

            yield return new WaitForSecondsRealtime(speed);
            SetObjectToEnabled(laser);
       }
   }


    public void SetObjectToDisabled(GameObject gameObject)
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false; 
        gameObject.GetComponent<Collider2D>().enabled = false;
    }
    public void SetObjectToEnabled(GameObject gameObject)
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true; 
        gameObject.GetComponent<Collider2D>().enabled = true;
    }
   
}
