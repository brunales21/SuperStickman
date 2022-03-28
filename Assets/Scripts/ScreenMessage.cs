using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenMessage : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Image background;

    public float messageDuration = 3f;
    public float blinkVelocity;
    float initTime;

    void Start()
    {
        initTime = Time.time;

        StartCoroutine("getMessage");
    
    }


    IEnumerator getMessage()
    {
        while (Time.time - initTime < messageDuration)
        {
            background.gameObject.SetActive(true);
            yield return new WaitForSecondsRealtime(blinkVelocity);

            background.gameObject.SetActive(false);
            yield return new WaitForSecondsRealtime(blinkVelocity);
        }
    }
}


