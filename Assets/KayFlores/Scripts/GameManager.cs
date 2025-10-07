using UnityEngine;

public class GameManager : MonoBehaviour
{
    // game structure is basically:
    // get customer order
    // display order type & allow player to go in whatever order they want
    // swap between modes (cut, curl) 

    // when in scissors mode, each hair strand will be separated into 3 sections
    // while in curling iron mode, every section in each strand will do the same thing

    public enum Tools { None, Scissors, CurlingIron }
    public Tools currentTool = Tools.None;

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
                // No tool selected
                break;
        }
    }

    public void UpdateScissors()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            currentTool = Tools.CurlingIron;
            Debug.Log("Switched to Curling Iron");
        }
    }
    public void UpdateCurlingIron()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            currentTool = Tools.Scissors;
            Debug.Log("Switched to Scissors");
        }
    }
}
