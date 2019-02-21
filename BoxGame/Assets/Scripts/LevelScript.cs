using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelScript : MonoBehaviour
{
    [SerializeField] private Light sun;
    [SerializeField] private GameObject policeLights;
    [SerializeField] private Transform cam;
    private bool topView = true;
    [SerializeField] private Transform cop;
    private int copMoveCount = 0;

    [SerializeField] private Transform player;
    [SerializeField] private PlayerController playerCt;
    [SerializeField] private PCStart playerCtS;
    [SerializeField] private AudioSource mainAudio;

    [SerializeField] private GameObject frontDoor;
    [SerializeField] private GameObject frontDoorOp;
    [SerializeField] private GameObject houseEntryTriggers;
    private bool playerInHouse = false;
    [SerializeField] private GameObject houseExitTrigger;
    [SerializeField] private Box houseGuy;
    [SerializeField] private GameObject hgDoor;
    [SerializeField] private GameObject hgDoorOp;
    [SerializeField] private GameObject officeTrigger;
    [SerializeField] private GameObject divDoor;
    [SerializeField] private GameObject divDoorOp;
    [SerializeField] private GameObject bwayDoor;
    [SerializeField] private GameObject bwayDoorOp;
    private bool hgInCloset = false;
    private bool closClosed = false;
    [SerializeField] private GameObject closetDoor;
    [SerializeField] private GameObject roof;

    [SerializeField] private GameObject mainDepTrigger;
    [SerializeField] private Text scorePtLbl;
    private int points = 0;
    private int phase = 0;
    private List<Box> collectedBoxes = new List<Box>();

    [SerializeField] private GameObject failPanel;
    [SerializeField] private GameObject successPanel;
    [SerializeField] private Text phaseLbl;
    [SerializeField] private Text countdownLbl;
    [SerializeField] private Text timeTakenLbl;
    private const float TOTALTIME = 90.00f;
    private float timeRemaining;
    private float startTime;
    private float timeTaken;
    private bool gameInProgress = true;

    [SerializeField] private Camera mainCam;
    [SerializeField] private Camera closetCam;
    [SerializeField] private Camera stolenGoodsCam;
    [SerializeField] private Camera overviewCam;
    [SerializeField] private Camera closeupCam;

    // Start is called before the first frame update
    void Start()
    {
        playerCt.enabled = false;
        timeRemaining = TOTALTIME;
        InvokeRepeating("DimSunlight", 1.0f, 1.0f);
    }

    void FixedUpdate()
    {
        if (playerInHouse && timeRemaining > 0 && gameInProgress)
        {
            timeRemaining = TOTALTIME - Time.time + startTime;
            countdownLbl.text = ((int)timeRemaining).ToString();
            if (timeRemaining <= 0) // Win condition
                HandleEndGame();
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
        playerCtS.enabled = false;
        playerCt.enabled = true;
    }

    public void HandleOfficeEntry()
    {
        Destroy(officeTrigger);
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
            bwayDoor.SetActive(false);
            bwayDoorOp.SetActive(true);
            phaseLbl.text = "Phase 3";
            phase++;
        }
    }

    public void HandleDeposit(Box deposited)
    {
        deposited.transform.Find("Trigger").gameObject.SetActive(false);
        deposited.SetFollow(false);
        collectedBoxes.Add(deposited);
        scorePtLbl.text = (++points).ToString();
        if (collectedBoxes.Count >= 6 && phase == 1)
        {
            hgDoor.SetActive(false);
            hgDoorOp.SetActive(true);
            phaseLbl.text = "Phase 2";
            phase++;
        }
        if (collectedBoxes.Count >= 11 && gameInProgress) // Win condition
        {
            mainDepTrigger.SetActive(false);
            timeTaken = TOTALTIME - (timeRemaining);
            phase++;
            HandleEndGame();
        }
    }

    public void HandleEndGame()
    {
        if (phase == 4) // Level has been completed
        {
            gameInProgress = false;
            timeTakenLbl.text = "Time: " + timeTaken.ToString() + "s";

            // Open front door
            frontDoorOp.SetActive(true);
            frontDoor.SetActive(false);
            phaseLbl.text = "Level Complete!";

            // Set all collected boxes to follow
            foreach (Box b in collectedBoxes)
            {
                b.transform.Find("Trigger").gameObject.SetActive(true);
                b.SetFollow(true);
            }

            // Upon exiting house, player will enter trigger
            houseExitTrigger.SetActive(true);
        }
        else // Level failed
        {
            // Level failed, must restart
            playerCt.SetMoveSpeed(0.0f);
            failPanel.SetActive(true);
        }
    }

    public void HandleHouseExit()
    {
        roof.SetActive(true);
        // Set all collected boxes to not follow
        foreach (Box b in collectedBoxes)
        {
            b.transform.Find("Trigger").gameObject.SetActive(false);
            b.SetFollow(false);
        }
        playerCt.SetMoveSpeed(0.01f);
        RotateCam();
        policeLights.SetActive(true);
        InvokeRepeating("MoveCop", 0.05f, 0.05f);
    }

    private void MoveCop()
    {
        cop.position = Vector3.MoveTowards(cop.position, player.position, 5.0f * 0.05f);
        if (++copMoveCount > 50.0f)
        {
            CancelInvoke();
            Invoke("PlayCinematic", 2.0f);
        }
    }

    private void PlayCinematic()
    {
        closetDoor.SetActive(false);
        mainCam.enabled = false;
        closetCam.enabled = true;
        Invoke("SwitchToStolenGoods", 4.0f);
    }
    private void SwitchToStolenGoods()
    {
        closetDoor.SetActive(true);
        closetCam.enabled = false;
        stolenGoodsCam.enabled = true;
        Invoke("SwitchToOverview", 4.0f);
    }
    private void SwitchToOverview()
    {
        stolenGoodsCam.enabled = false;
        overviewCam.enabled = true;
        Invoke("SwitchToCloseup", 4.0f);
    }
    private void SwitchToCloseup()
    {
        overviewCam.enabled = false;
        closeupCam.enabled = true;
        Invoke("ShowSuccessPanel", 4.0f);
    }

    private void ShowSuccessPanel()
    {
        successPanel.SetActive(true);
    }

    private void RotateCam()
    {
        if (topView)
        {
            cam.eulerAngles = new Vector3(0, 45, -70);
        }
        else
        {
            cam.eulerAngles = new Vector3(0, 0, 0);
        }

        topView = !topView;
    }

    public void EHToggleMute()
    {
        if (mainAudio.volume == 0)
            mainAudio.volume = 1;
        else
            mainAudio.volume = 0;
    }

    public void EHRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void EHQuit()
    {
        Application.Quit();
    }
}
