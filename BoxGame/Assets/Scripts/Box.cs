using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float speed = 5.0f;
    private bool follow = false;

    void FixedUpdate()
    {
        if (follow)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }

    public void SetFollow(bool set)
    {
        follow = set;
    }
}
