using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenMessage : MonoBehaviour
{
    [SerializeField] PlayerBehaviour playerBehaviour;
    [SerializeField] UnityEngine.UI.Image background;

    float messageDuration = 3f;
    float initTime;

    void Start()
    {
        initTime = Time.time;
        StartCoroutine("getMessage");
    }

    void update()
    {
        
    }

    IEnumerator getMessage()
    {
        while (Time.time - initTime < messageDuration)
        {
            background.gameObject.SetActive(true);
            yield return new WaitForSecondsRealtime(0.25f);

            background.gameObject.SetActive(false);
            yield return new WaitForSecondsRealtime(0.25f);

            
        }
        
    }
}


