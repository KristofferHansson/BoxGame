using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClosetTriggerExit : MonoBehaviour
{
    [SerializeField] private LevelScript level;

    void OnTriggerExit(Collider other)
    {
        if (other.transform.parent != null && other.transform.parent.name.Equals("Character"))
        {
            // Close door
            level.HandlePlayerLeaveCloset();
        }
    }
}
