using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApuntadoAutomatico : MonoBehaviour
{
    [SerializeField] private GameObject playerASeguir;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float shootDistance;
    [SerializeField] float bulletLifeTime;

    AudioSource FireSound;

    public float fireRate = 0.5f;
    private float nextFire = 0.0f;

    void Start()
    {
        FireSound = GetComponent<AudioSource>();
    }
    void Update()
    {
        transform.up = playerASeguir.transform.position - transform.position;
        float distanceToObject = Vector2.Distance(playerASeguir.transform.position, transform.position);

        if(distanceToObject < shootDistance && Time.time > nextFire)
        {            
            nextFire = Time.time + fireRate;

            GameObject gameObjectBala = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            FireSound.Play();
            gameObjectBala.GetComponent<RobotBullet>().setDirection(transform.up);

            Destroy(gameObjectBala, bulletLifeTime);  
        }
    }


  







    IEnumerator getDisparoCadencia()
    {
        GameObject gameObjectBala = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        gameObjectBala.GetComponent<RobotBullet>().setDirection(transform.up);
        yield return new WaitForSecondsRealtime(1f);
    }
}
