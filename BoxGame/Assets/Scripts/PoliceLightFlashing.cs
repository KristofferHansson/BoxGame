using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceLightFlashing : MonoBehaviour
{
    [SerializeField] private GameObject blue;
    [SerializeField] private GameObject red;

    // Start is called before the first frame update
    void Start()
    {
        blue.SetActive(true);
        red.SetActive(false);
        InvokeRepeating("Flash", 0.05f, 0.3f);
    }

    void Flash()
    {
        blue.SetActive(!blue.activeSelf);
        red.SetActive(!red.activeSelf);
    }
}
