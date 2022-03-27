using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    public static AudioScript inst;

    void Awake()
    {
        if (AudioScript.inst == null)
        {
            //primera vez
            AudioScript.inst = this;
            DontDestroyOnLoad(gameObject);
        } else {
            //ya hay una instancia. Eliminar esta.
            Destroy(gameObject);
        }
    }
}
