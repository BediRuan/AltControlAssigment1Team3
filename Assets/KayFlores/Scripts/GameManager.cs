using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    // game structure is basically:
    // get customer order
    // display order type & allow player to go in whatever order they want
    // swap between modes (cut, curl) 

    //this will handle game loop stuff + spawning
}
