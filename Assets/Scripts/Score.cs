using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    public static Score inst;
    public int contadorMonedas;
    public TMP_Text ScoreTxt;

    
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        ScoreTxt.text = contadorMonedas.ToString();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            contadorMonedas++;
            Debug.Log(contadorMonedas);
        }
    }
}
