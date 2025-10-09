using UnityEngine;
using UnityEngine.UI;

public class HairVisuals : MonoBehaviour
{
    public Image leftLenLayer;
    public Image rightLenLayer;
    public Image curlLayer;
    public HairStyle data;

    public void Apply(int leftLen, int rightLen, int curl)
    {
        leftLenLayer.sprite = data.leftLenSprites[leftLen];
        rightLenLayer.sprite = data.rightLenSprites[rightLen];
        curlLayer.sprite = data.curlSprites[curl];
    }
}
