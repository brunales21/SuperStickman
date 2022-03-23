using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApuntadoAutomatico : MonoBehaviour
{
    [SerializeField] private GameObject playerASeguir;
    [SerializeField] GameObject bulletPrefab;

    public float fireRate = 0.5f;
    private float nextFire = 0.0f;

    void Update()
    {
        transform.up = playerASeguir.transform.position - transform.position;
        float distanceToObject = Vector2.Distance(playerASeguir.transform.position, transform.position);

        if(distanceToObject < 10 && Time.time > nextFire)
        {            
            nextFire = Time.time + fireRate;

            GameObject gameObjectBala = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            gameObjectBala.GetComponent<RobotBullet>().setDirection(transform.up);
            
            Destroy(gameObjectBala, 10f);  
        }
    }










    IEnumerator getDisparoCadencia()
    {
        GameObject gameObjectBala = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        gameObjectBala.GetComponent<RobotBullet>().setDirection(transform.up);
        yield return new WaitForSecondsRealtime(1f);
    }
}
