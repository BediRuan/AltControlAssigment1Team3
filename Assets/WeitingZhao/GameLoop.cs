using UnityEngine;

public enum ToolMode { Scissor, Curler }

public class GameLoop : MonoBehaviour
{
    [Header("State")]
    public ToolMode mode = ToolMode.Scissor;
    [Range(0, 3)] public int currLeftLen = 0;
    [Range(0, 3)] public int currRightLen = 0;
    [Range(0, 3)] public int currCurl = 0;

    [Header("Order & View")]
    public HairStyle currentOrder;
    public HairVisuals visuals;

    void Start()
    {
        LoadOrder(currentOrder);
    }

    public void LoadOrder(HairStyle order)
    {
        currentOrder = order;
        currLeftLen = currRightLen = currCurl = 0;
        visuals.data = order;
        visuals.Apply(currLeftLen, currRightLen, currCurl);
        mode = ToolMode.Scissor;
    }

    public void SetLeftLength(int level)
    {
        currLeftLen = Mathf.Clamp(level, 0, 3);
        visuals.Apply(currLeftLen, currRightLen, currCurl);
    }
    public void SetRightLength(int level)
    {
        currRightLen = Mathf.Clamp(level, 0, 3);
        visuals.Apply(currLeftLen, currRightLen, currCurl);
    }
    public void SetCurl(int level)
    {
        currCurl = Mathf.Clamp(level, 0, 3);
        visuals.Apply(currLeftLen, currRightLen, currCurl);
    }

    public void ToggleTool()
    {
        mode = (mode == ToolMode.Scissor) ? ToolMode.Curler : ToolMode.Scissor;
        
    }

    public int Score()
    {
        int dL = Mathf.Abs(currentOrder.targetLeftLength - currLeftLen);
        int dR = Mathf.Abs(currentOrder.targetRightLength - currRightLen);
        int dC = Mathf.Abs(currentOrder.targetCurl - currCurl);
        return 20 - 5 * (dL + dR + dC);
    }
}
