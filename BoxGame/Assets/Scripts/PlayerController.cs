using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LevelScript lvl;
    [SerializeField] private float move_Speed = 1.0f;
    [SerializeField] private Transform cam;
    [SerializeField] private GameObject mesh;
    private Rigidbody m_Rigidbody;
    private Vector3 move;
    private bool topView = true;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = 0.0f, z = 0.0f;

        // Check for input
        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
            x += -1.0f;
        else if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
            x += 1.0f;

        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            z += -1.0f;
        else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
            z += 1.0f;

        if (Input.GetKeyDown(KeyCode.Escape))
            lvl.EHQuit();
        if (Input.GetKeyDown(KeyCode.BackQuote))
            lvl.EHRestart();
        if (Input.GetKeyDown(KeyCode.M))
            lvl.EHToggleMute();

        // Update movement vector
        move.x = x;
        move.z = z;
        move.Normalize();

        Move();
    }

    private void Move()
    {
        m_Rigidbody.velocity = move * move_Speed;
        if (Mathf.Abs(move.x) > 0.707)
            mesh.transform.eulerAngles = new Vector3(-90,0,90);
        else if (Mathf.Abs(move.z) > 0.707)
            mesh.transform.eulerAngles = new Vector3(-90, 0, 0);
    }

    public void SetMoveSpeed(float spd)
    {
        if (spd < 0)
            move_Speed = 0;
        else
            move_Speed = spd;
    }
}
