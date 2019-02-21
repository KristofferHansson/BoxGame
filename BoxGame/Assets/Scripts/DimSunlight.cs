using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimSunlight : MonoBehaviour
{
    [SerializeField] private Light sun;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("DimLight", 1.0f, 1.0f);
    }

    void DimLight()
    {
        sun.intensity -= 0.01f;
        if (sun.intensity < 0.1)
        {
            CancelInvoke();
        }
    }
}
