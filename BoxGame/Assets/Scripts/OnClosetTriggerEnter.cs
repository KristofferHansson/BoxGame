using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClosetTriggerEnter : MonoBehaviour
{
    [SerializeField] private LevelScript level;

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent != null && other.transform.parent.name.Equals("HouseGuyObj"))
        {
            level.HandleHouseGuyInCloset();
        }
    }
}
