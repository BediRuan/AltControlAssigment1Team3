using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine.InputSystem;
using TMPro;

[System.Serializable]
public class HairSection
{
    public string side; // left or right
    public float curlAmount = 0;
    public int currentLength;

    //public Dictionary<int, HairPiece> piecesIncluded;
}

public class PlayerInputManager : MonoBehaviour
{

    public TextMeshProUGUI currentLLength;
    public TextMeshProUGUI currentRLength;
    public TextMeshProUGUI currentLCurl;
    public TextMeshProUGUI currentRCurl;

    public static PlayerInputManager instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    public enum Tools { None, Scissors, CurlingIron }
    public Tools currentTool = Tools.None;

    public HairSection currentSectionL { get; private set; }
    public HairSection currentSectionR { get; private set; }

    private Keyboard keyboard;

    public float CurlRate = 5f;

    public GameObject hairPS;

    public GameObject selectorGO;

    public GameObject curlingIronGO;

    public GameObject scissorsGO;


    public void SetupCurrentHair()
    {
        currentSectionL = new HairSection()
        {
            side = "left",
            currentLength = 4
            //piecesIncluded = new Dictionary<int, HairPiece>(leftPart)
        };
        currentSectionR = new HairSection()
        {
            side = "right",
            currentLength = 4
            //piecesIncluded = new Dictionary<int, HairPiece>(rightPart)
        };
    }

    private void Start()
    {
        SetupCurrentHair();

        keyboard = Keyboard.current;
    }

    public void Update()
    {
        switch (currentTool)
        {
            case Tools.Scissors:
                UpdateScissors();
                break;
            case Tools.CurlingIron:
                UpdateCurlingIron();
                break;
            case Tools.None:
                UpdateNone();
                break;
        }

        currentLLength.text = "Current Left Length: " + currentSectionL.currentLength.ToString();
        currentRLength.text = "Current Right Length: " + currentSectionR.currentLength.ToString();
        currentLCurl.text = "Current Left Curl:" + currentSectionL.curlAmount.ToString();
        currentRCurl.text = "Current Right Curl:" + currentSectionR.curlAmount.ToString();

        if (keyboard.enterKey.wasPressedThisFrame)
        {
            GameManager.instance.CalculateResult(currentSectionL, currentSectionR);
            SetupCurrentHair();
        }
    }


    void UpdateNone()
    {
        if (keyboard.sKey.wasPressedThisFrame)
        {
            currentTool = Tools.CurlingIron;
        }
    }
    public void UpdateScissors()
    {
        selectorGO.transform.position = scissorsGO.transform.position;
        if (keyboard.sKey.wasPressedThisFrame)
        {
            currentTool = Tools.CurlingIron;
            Debug.Log("Switched to Curling Iron");
        }

        // left hair section
        if (keyboard.eKey.wasPressedThisFrame) // longest length on the left side
        {
            if (currentSectionL.currentLength > 3)
            {
                Instantiate(hairPS, new Vector3(-12,-35,90), Quaternion.identity);
                currentSectionL.currentLength = 3;
                Debug.Log("Cut left strand to length " + currentSectionL.currentLength);
            }
        }
        if (keyboard.wKey.wasPressedThisFrame) // medium length on the left side
        {
            if (currentSectionL.currentLength > 2)
            {
                Instantiate(hairPS, new Vector3(-12, -25, 90), Quaternion.identity);
                currentSectionL.currentLength = 2;
                Debug.Log("Cut left strand to length " + currentSectionL.currentLength);
            }
        }
        if (keyboard.qKey.wasPressedThisFrame) // shortest length on the left side
        {
            if (currentSectionL.currentLength > 1)
            {
                Instantiate(hairPS, new Vector3(-12, -15, 90), Quaternion.identity);
                currentSectionL.currentLength = 1;
                Debug.Log("Cut left strand to length " + currentSectionL.currentLength);
            }
        }

        // right hair section
        if (keyboard.yKey.wasPressedThisFrame) // longest length on the right side
        {
            if (currentSectionR.currentLength > 3)
            {
                Instantiate(hairPS, new Vector3(12, -35, 90), Quaternion.identity);
                currentSectionR.currentLength = 3;
                Debug.Log("Cut right strand to length " + currentSectionR.currentLength);
            }
        }
        if (keyboard.tKey.wasPressedThisFrame) // medium length on the right side
        {
            if (currentSectionR.currentLength > 2)
            {
                Instantiate(hairPS, new Vector3(12, -25, 90), Quaternion.identity);
                currentSectionR.currentLength = 2;
                Debug.Log("Cut right strand to length " + currentSectionR.currentLength);
            }
        }
        if (keyboard.rKey.wasPressedThisFrame) // shortest length on the right side
        {
            if (currentSectionR.currentLength > 1)
            {
                Instantiate(hairPS, new Vector3(12, -15, 90), Quaternion.identity);
                currentSectionR.currentLength = 1;
                Debug.Log("Cut right strand to length " + currentSectionR.currentLength);
            }
        }
    }
    public void UpdateCurlingIron()
    {
        selectorGO.transform.position = curlingIronGO.transform.position;
        if (keyboard.sKey.wasPressedThisFrame)
        {
            currentTool = Tools.Scissors;
            Debug.Log("Switched to Scissors");
        }

        if (keyboard.qKey.isPressed || keyboard.wKey.isPressed || keyboard.eKey.isPressed)
        {
            if (currentSectionL.curlAmount < 100)
            {
                currentSectionL.curlAmount += CurlRate;
                Debug.Log("Increased left curl to " + currentSectionL.curlAmount);
            }
        }
        if(keyboard.rKey.isPressed || keyboard.tKey.isPressed || keyboard.yKey.isPressed)
        {
            if (currentSectionR.curlAmount < 100)
            {
                currentSectionR.curlAmount += CurlRate;
                Debug.Log("Increased right curl to " + currentSectionR.curlAmount);
            }
        }
    }
}
