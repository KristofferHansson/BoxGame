﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBoxTriggerEnter : MonoBehaviour
{
    [SerializeField] private Box box;

    void OnTriggerExit(Collider other)
    {
        if (other.transform.parent != null && other.transform.parent.name.Equals("Character"))
        {
            box.SetFollow(true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent != null && other.transform.parent.name.Equals("Character"))
        {
            box.SetFollow(false);
        }
    }
}
