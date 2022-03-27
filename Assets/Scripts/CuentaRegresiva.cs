using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;



public class CuentaRegresiva : MonoBehaviour
{
    int segundos = 60;
    public TMP_Text contadorTxt;
    AudioSource cronoSound;
    void Start()
    {
        StartCoroutine("getCronometro");
    }

    // Update is called once per frame
    void Update()
    {
        contadorTxt.text = segundos.ToString();
    }

    IEnumerator getCronometro()
    {
        while (segundos > 0)
        {
            
            yield return new WaitForSecondsRealtime(1.1f);
            segundos--;

            if (segundos == 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            segundos = segundos + 10;
        }
    }
}
