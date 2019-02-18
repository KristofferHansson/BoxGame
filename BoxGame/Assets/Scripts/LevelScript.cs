using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelScript : MonoBehaviour
{
    [SerializeField] private Light sun;
    [SerializeField] private GameObject policeLights;

    [SerializeField] private GameObject frontDoor;
    [SerializeField] private GameObject frontDoorOp;
    [SerializeField] private GameObject houseEntryTriggers;
    [SerializeField] private Box houseGuy;
    [SerializeField] private GameObject hgDoor;
    [SerializeField] private GameObject hgDoorOp;
    [SerializeField] private GameObject divDoor;
    [SerializeField] private GameObject divDoorOp;
    private bool hgInCloset = false;
    private bool closClosed = false;
    [SerializeField] private GameObject closetDoor;

    [SerializeField] private Text scorePtLbl;
    private int points = 0;
    private int numDep = 0;

    [SerializeField] private Text phaseLbl;

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

    public void HandleHouseEntry()
    {
        Destroy(houseEntryTriggers);
        frontDoorOp.SetActive(false);
        frontDoor.SetActive(true);
        phaseLbl.text = "Phase 1";
    }

    public void HandleOfficeEntry()
    {
        Destroy(houseEntryTriggers);
        houseGuy.SetFlee(true);
    }

    public void HandleHouseGuyInCloset()
    {
        Destroy(houseGuy.transform.Find("Trigger").gameObject);
        houseGuy.SetFollow(false);
        hgInCloset = true;
    }

    public void HandlePlayerLeaveCloset()
    {
        if (hgInCloset && !closClosed)
        {
            closetDoor.transform.Translate(new Vector3(-3, 0, 0), Space.Self);
            closClosed = true;

            divDoor.SetActive(false);
            divDoorOp.SetActive(true);
            phaseLbl.text = "Phase 3";
        }
    }

    public void HandleDeposit(Box deposited)
    {
        deposited.transform.Find("Trigger").gameObject.SetActive(false);
        deposited.SetFollow(false);
        numDep++;
        scorePtLbl.text = (++points).ToString();
        if (numDep >= 5)
        {
            hgDoor.SetActive(false);
            hgDoorOp.SetActive(true);
            phaseLbl.text = "Phase 2";
        }
    }
}
