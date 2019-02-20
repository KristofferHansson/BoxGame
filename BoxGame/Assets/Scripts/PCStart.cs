using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCStart : MonoBehaviour
{
    [SerializeField] private LevelScript lvl;
    [SerializeField] private float move_Speed = 2.0f;
    private Rigidbody m_Rigidbody;
    private Vector3 move;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float z = 0.0f;

        // Check for input
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
            z += 1.0f;

        if (Input.GetKeyDown(KeyCode.Escape))
            lvl.EHQuit();
        if (Input.GetKeyDown(KeyCode.BackQuote))
            lvl.EHRestart();
        if (Input.GetKeyDown(KeyCode.M))
            lvl.EHToggleMute();

        // Update movement vector
        move.x = 0;
        move.z = z;
        move.Normalize();

        Move();
    }

    private void Move()
    {
        m_Rigidbody.velocity = move * move_Speed;
    }
}
