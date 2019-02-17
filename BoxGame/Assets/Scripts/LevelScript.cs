using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour
{
    [SerializeField] private Light sun;
    [SerializeField] private GameObject policeLights;

    [SerializeField] private Box houseGuy;
    private bool hgInCloset = false;
    [SerializeField] private GameObject closetDoor;

    private int points = 0;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("DimSunlight", 1.0f, 1.0f);
    }

    void DimSunlight()
    {
        sun.intensity -= 0.01f;
        if (sun.intensity < 0.1)
        {
            CancelInvoke();
            policeLights.SetActive(true);
        }
    }

    public void HandleHouseGuyInCloset()
    {
        Destroy(houseGuy.transform.Find("Trigger").gameObject);
        houseGuy.SetFollow(false);
        hgInCloset = true;
    }

    public void HandlePlayerLeaveCloset()
    {
        if (hgInCloset)
            closetDoor.transform.Translate(new Vector3(-3, 0, 0), Space.Self);
    }

    public void HandleDeposit(Box deposited)
    {
        Destroy(deposited.transform.Find("Trigger").gameObject);
        deposited.SetFollow(false);
        points++;
    }
}
