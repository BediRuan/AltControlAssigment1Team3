using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_HairVisManager : MonoBehaviour
{
    public bool trueIfLeft;
    public PlayerInputManager playerInfo;
    public Image hairIM;
    public List<Sprite> hairImages = new List<Sprite>();

    // Update is called once per frame
    void Update()
    {
        if (trueIfLeft)
        {
            if (playerInfo.currentSectionL.currentLength == 4)
            {
                if (playerInfo.currentSectionL.curlAmount <= 30)
                {
                    hairIM.sprite = hairImages[0];
                }
                else if (playerInfo.currentSectionL.curlAmount <= 60)
                {
                    hairIM.sprite = hairImages[1];
                }
                else
                {
                    hairIM.sprite = hairImages[2];
                }
            }
            if (playerInfo.currentSectionL.currentLength == 3)
            {
                //hairIM.sprite = hairImages[0];
                if (playerInfo.currentSectionL.curlAmount <= 30)
                {
                    hairIM.sprite = hairImages[0];
                }
                else if (playerInfo.currentSectionL.curlAmount <= 60)
                {
                    hairIM.sprite = hairImages[1];
                }
                else
                {
                    hairIM.sprite = hairImages[2];
                }
            }
            if (playerInfo.currentSectionL.currentLength == 2)
            {
                //hairIM.sprite = hairImages[1];
                if (playerInfo.currentSectionL.curlAmount <= 30)
                {
                    hairIM.sprite = hairImages[3];
                }
                else if (playerInfo.currentSectionL.curlAmount <= 60)
                {
                    hairIM.sprite = hairImages[4];
                }
                else
                {
                    hairIM.sprite = hairImages[5];
                }
            }
            if (playerInfo.currentSectionL.currentLength == 1)
            {
                //hairIM.sprite = hairImages[2];
                if (playerInfo.currentSectionL.curlAmount <= 30)
                {
                    hairIM.sprite = hairImages[6];
                }
                else if (playerInfo.currentSectionL.curlAmount <= 60)
                {
                    hairIM.sprite = hairImages[7];
                }
                else
                {
                    hairIM.sprite = hairImages[8];
                }
            }
        }
        else
        {
            if (playerInfo.currentSectionR.currentLength == 4)
            {
                if (playerInfo.currentSectionR.curlAmount <= 30)
                {
                    hairIM.sprite = hairImages[0];
                }
                else if (playerInfo.currentSectionR.curlAmount <= 60)
                {
                    hairIM.sprite = hairImages[1];
                }
                else
                {
                    hairIM.sprite = hairImages[2];
                }
            }
            if (playerInfo.currentSectionR.currentLength == 3)
            {
                //hairIM.sprite = hairImages[0];
                if (playerInfo.currentSectionR.curlAmount <= 30)
                {
                    hairIM.sprite = hairImages[0];
                }
                else if (playerInfo.currentSectionR.curlAmount <= 60)
                {
                    hairIM.sprite = hairImages[1];
                }
                else
                {
                    hairIM.sprite = hairImages[2];
                }
            }
            if (playerInfo.currentSectionR.currentLength == 2)
            {
                //hairIM.sprite = hairImages[1];
                if (playerInfo.currentSectionR.curlAmount <= 30)
                {
                    hairIM.sprite = hairImages[3];
                }
                else if (playerInfo.currentSectionR.curlAmount <= 60)
                {
                    hairIM.sprite = hairImages[4];
                }
                else
                {
                    hairIM.sprite = hairImages[5];
                }
            }
            if (playerInfo.currentSectionR.currentLength == 1)
            {
                //hairIM.sprite = hairImages[2];
                if (playerInfo.currentSectionR.curlAmount <= 30)
                {
                    hairIM.sprite = hairImages[6];
                }
                else if (playerInfo.currentSectionR.curlAmount <= 60)
                {
                    hairIM.sprite = hairImages[7];
                }
                else
                {
                    hairIM.sprite = hairImages[8];
                }
            }
        }
    }
}
