using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOfficeTriggerEntry : MonoBehaviour
{
    [SerializeField] private LevelScript level;

    void OnTriggerEnter(Collider other)
    {
        level.HandleOfficeEntry();
    }
}
