using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHouseDoorTriggerEnter : MonoBehaviour
{
    [SerializeField] private LevelScript level;

    void OnTriggerEnter(Collider other)
    {
        level.HandleHouseEntry();
    }
}
