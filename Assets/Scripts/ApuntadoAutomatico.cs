using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApuntadoAutomatico : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] RobotBullet robotBullet;
    [SerializeField] GameObject bulletPrefab;

    void Update()
    {

        transform.up = player.transform.position - transform.position;
        float distanceToObject = Vector2.Distance(player.transform.position, transform.position);

        //Instantiate(bulletPrefab, transform.position, Quaternion.identity);
       

    }
}
