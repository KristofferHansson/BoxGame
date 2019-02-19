using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHouseExit : MonoBehaviour
{
    [SerializeField] private LevelScript level;

    void OnTriggerEnter(Collider other)
    {
        level.HandleHouseExit();
    }
}
