using UnityEngine;
using UnityEngine.UI;

public class AltControlInput : MonoBehaviour
{
    public GameLoop gm;
    public Slider heatBar;

    bool heating;

    void Update()
    {
        // switch tools
        if (Input.GetKeyDown(KeyCode.G)) gm.ToggleTool();

        if (gm.mode == ToolMode.Scissor)
        {
            // left ABC ----- 1/2/3
            if (Input.GetKeyDown(KeyCode.A)) gm.SetLeftLength(1);
            if (Input.GetKeyDown(KeyCode.B)) gm.SetLeftLength(2);
            if (Input.GetKeyDown(KeyCode.C)) gm.SetLeftLength(3);
            // right DEF ----- 1/2/3
            if (Input.GetKeyDown(KeyCode.D)) gm.SetRightLength(1);
            if (Input.GetKeyDown(KeyCode.E)) gm.SetRightLength(2);
            if (Input.GetKeyDown(KeyCode.F)) gm.SetRightLength(3);
        }



        // curl£∫long press A to F == heating
        else
        {
            bool anyAFDown = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.B) || Input.GetKey(KeyCode.C) ||
                              Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.F);

            if (anyAFDown && !heating) { heating = true; }
            if (!anyAFDown && heating)
            {
                heating = false;
                int curlLevel = HeatToCurl(heatBar ? heatBar.value : 0f);
                gm.SetCurl(curlLevel);
            }

            if (heatBar)
            {
                if (anyAFDown) heatBar.value = Mathf.Clamp01(heatBar.value + Time.deltaTime * 0.6f);
                else heatBar.value = Mathf.Clamp01(heatBar.value - Time.deltaTime * 0.9f); // ¿‰»¥ªÿ¬‰
            }
        }

        // H = Compelete! 
        if (Input.GetKeyDown(KeyCode.H))
        {
            int score = gm.Score();
            Debug.Log($"Score: {score} (L:{gm.currLeftLen}/{gm.currentOrder.targetLeftLength}, " +
                      $"R:{gm.currRightLen}/{gm.currentOrder.targetRightLength}, " +
                      $"C:{gm.currCurl}/{gm.currentOrder.targetCurl})");

        }
    }

    int HeatToCurl(float v)
    {
        if (v < 0.20f) return 0;
        if (v < 0.45f) return 1;
        if (v < 0.75f) return 2;
        return 3; // highest = burnt hair
    }
}
