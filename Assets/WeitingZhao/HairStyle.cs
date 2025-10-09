using UnityEngine;

[CreateAssetMenu(menuName = "Salon/HairStyle")]
public class HairStyle : ScriptableObject
{
    [Header("Targets")]
    [Range(0, 3)] public int targetLeftLength;   //left 0-3
    [Range(0, 3)] public int targetRightLength;  // right 0-3
    [Range(0, 3)] public int targetCurl;         // curl whole head 0-3

    [Header("Sprites")]
    // 4 cut levels
    public Sprite[] leftLenSprites = new Sprite[4];
    public Sprite[] rightLenSprites = new Sprite[4];
    // 4 curl levels
    public Sprite[] curlSprites = new Sprite[4];
}
