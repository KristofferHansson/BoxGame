using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour
{
    [SerializeField] private Light sun;
    [SerializeField] private GameObject policeLights;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("DimSunlight", 1.0f, 1.0f);
    }

    void DimSunlight()
    {
        sun.intensity -= 0.05f;
        if (sun.intensity < 0.1)
        {
            CancelInvoke();
            policeLights.SetActive(true);
        }
    }
}
