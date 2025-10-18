using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine.InputSystem;


//[System.Serializable]
//public class HairPiece
//{
//    public KeyCode input;
//    public string strandSection; // left or right (which strand this piece belongs to) (might be redundant
//    public int length; // 1, 2, or 3 (3 is longest)
//    //public float curlAmount = 0;
//}
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
    public static PlayerInputManager instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    public enum Tools { None, Scissors, CurlingIron }
    public Tools currentTool = Tools.None;

    //public Dictionary<int, HairPiece> leftPart { get; private set; } = new Dictionary<int, HairPiece>();

    //public Dictionary<int, HairPiece> rightPart { get; private set; } = new Dictionary<int, HairPiece>();

    //void PopulateStrand(Dictionary<int, HairPiece> dict, string side) // defaults
    //{
    //    if (side == "left")
    //    {        
    //        dict.Add(1, new HairPiece { input = KeyCode.Q, strandSection = side, length =  1});
    //        dict.Add(2, new HairPiece { input = KeyCode.W, strandSection = side, length = 2 });
    //        dict.Add(3, new HairPiece { input = KeyCode.E, strandSection = side, length = 3 });
    //    }
    //    else if (side == "right") 
    //    {
    //        dict.Add(1, new HairPiece { input = KeyCode.R, strandSection = side, length = 1 });
    //        dict.Add(2, new HairPiece { input = KeyCode.T, strandSection = side, length = 2 });
    //        dict.Add(3, new HairPiece { input = KeyCode.Y, strandSection = side, length = 3 });
    //    }
    //}

    public HairSection currentSectionL { get; private set; }
    public HairSection currentSectionR { get; private set; }

    private Keyboard keyboard;

    public float CurlRate = 5f;

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
                currentSectionL.currentLength = 3;
                Debug.Log("Cut left strand to length " + currentSectionL.currentLength);
            }
        }
        if (keyboard.wKey.wasPressedThisFrame) // medium length on the left side
        {
            if (currentSectionL.currentLength > 2)
            {
                currentSectionL.currentLength = 2;
                Debug.Log("Cut left strand to length " + currentSectionL.currentLength);
            }
        }
        if (keyboard.qKey.wasPressedThisFrame) // shortest length on the left side
        {
            if (currentSectionL.currentLength > 1)
            {
                currentSectionL.currentLength = 1;
                Debug.Log("Cut left strand to length " + currentSectionL.currentLength);
            }
        }

        // right hair section
        if (keyboard.yKey.wasPressedThisFrame) // longest length on the right side
        {
            if (currentSectionR.currentLength > 3)
            {
                currentSectionR.currentLength = 3;
                Debug.Log("Cut right strand to length " + currentSectionR.currentLength);
            }
        }
        if (keyboard.tKey.wasPressedThisFrame) // medium length on the right side
        {
            if (currentSectionR.currentLength > 2)
            {
                currentSectionR.currentLength = 2;
                Debug.Log("Cut right strand to length " + currentSectionR.currentLength);
            }
        }
        if (keyboard.rKey.wasPressedThisFrame) // shortest length on the right side
        {
            if (currentSectionR.currentLength > 1)
            {
                currentSectionR.currentLength = 1;
                Debug.Log("Cut right strand to length " + currentSectionR.currentLength);
            }
        }
    }
    public void UpdateCurlingIron()
    {
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
