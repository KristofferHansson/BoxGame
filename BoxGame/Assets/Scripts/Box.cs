using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private bool follow = false;

    void FixedUpdate()
    {
        if (follow)
        {
            print("Following");
        }
    }

    public void SetFollow(bool set)
    {
        follow = set;
    }
}
