﻿using System.Collections;
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
    private bool playerInHouse = false;
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
    private int phase = 0;

    [SerializeField] private Text phaseLbl;
    [SerializeField] private Text countdownLbl;
    private const float TOTALTIME = 60.00f;
    private float timeRemaining = 60.00f;
    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("DimSunlight", 1.0f, 1.0f);
    }

    void FixedUpdate()
    {
        if (playerInHouse && timeRemaining > 0)
        {
            timeRemaining = TOTALTIME - Time.time + startTime;
            countdownLbl.text = ((int)timeRemaining).ToString();
            if (timeRemaining <= 0)
                policeLights.SetActive(true);
        }
    }

    void DimSunlight()
    {
        sun.intensity -= 0.01f;
        if (sun.intensity < 0.1)
        {
            CancelInvoke();
        }
    }

    public void HandleHouseEntry()
    {
        Destroy(houseEntryTriggers);
        frontDoorOp.SetActive(false);
        frontDoor.SetActive(true);
        phaseLbl.text = "Phase 1";
        phase++;
        startTime = Time.time;
        playerInHouse = true;
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
            phase++;
        }
    }

    public void HandleDeposit(Box deposited)
    {
        deposited.transform.Find("Trigger").gameObject.SetActive(false);
        deposited.SetFollow(false);
        numDep++;
        scorePtLbl.text = (++points).ToString();
        if (numDep >= 6 && phase == 1)
        {
            hgDoor.SetActive(false);
            hgDoorOp.SetActive(true);
            phaseLbl.text = "Phase 2";
            phase++;
        }
        if (numDep >= 11)
        {
            frontDoorOp.SetActive(true);
            frontDoor.SetActive(false);
            phaseLbl.text = "Level Complete!";
            phase++;
        }
    }
}
