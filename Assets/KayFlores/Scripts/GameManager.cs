using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TextMeshProUGUI orderTextLength;
    public TextMeshProUGUI orderTextCurl;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI customerText;
    public TextMeshProUGUI fuckupText;
    
    [Header("target hair style UI display")]
    public TargetHairStyleUI targetHairStyleUI;

    public float desiredLength;
    public float desiredCurl;

    public GameObject confettiPS;
    public GameObject angryPS;
    public GameObject winScreen;
    public GameObject loseScreen;

    public int successAmount;
    public int lossAmount;
    public int customerAmount;
    public float timeLimit;
    public int scoreAmount;

    public bool gameOver;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    public HairSection CurrentCustomerOrder { get; private set; } = null;

    float hairCurlAccuracy = 0;
    public float HairCurlMinAccuracy = 80f; // minimum % to be considered accurate
    public float HairCurlAccuracyThreshold = 10f;

    float hairCutAccuracy = 0;
    public float HairCutMinAccuracy = 80f; // minimum % to be considered accurate
    public float HairCutAccuracyThreshold = 1f;

    // game structure is basically:
    // get customer order
    // display order type & allow player to go in whatever order they want
    // swap between modes (cut, curl) 

    private void Start()
    {
        GenerateNewOrder();
    }

    private void Update()
    {
        customerText.text = "Remaining customers: " + customerAmount.ToString();
        fuckupText.text = "Lives left: " + (3- lossAmount).ToString();
        scoreText.text = "Current Score: " + scoreAmount.ToString();
        resultText.text = scoreAmount.ToString();
        if (CurrentCustomerOrder == null)
        {
            GenerateNewOrder();
        }
        if ((customerAmount <= 0) || (lossAmount >= 3))
        {
            if (lossAmount >= 0)
            {
                loseScreen.SetActive(true);
            }
            else if (successAmount >= 3)
            {
                winScreen.SetActive(true);
            }
            else
            {
                loseScreen.SetActive(true);
            }
        }
    }

    public void GenerateNewOrder()
    {
        CurrentCustomerOrder = new HairSection() // applies to both sides of hair
        {
            curlAmount = Mathf.RoundToInt(Random.Range(0f, 1f) > 0f ? Random.Range(20f, 80f) : 0f), // 60% chance of having curls (of any amount)
            currentLength = Random.Range(1, 4) // length between 1 and 3 (default is 4)
        };
        orderTextCurl.text = "Customer wants their hair curled to " + CurrentCustomerOrder.curlAmount.ToString() + " units";
        orderTextLength.text = "Customer wants their hair to be " + CurrentCustomerOrder.currentLength.ToString() + " units long";
        
        // update target hair style UI display
        if (targetHairStyleUI != null)
        {
            targetHairStyleUI.UpdateTargetHairStyle(CurrentCustomerOrder.currentLength, CurrentCustomerOrder.curlAmount);
        }
        
        Debug.Log($"New Customer Order: Curl Percentage is {CurrentCustomerOrder.curlAmount} | Cut Length is {CurrentCustomerOrder.currentLength}");
    }

    private void CompareOrderWithSection(HairSection playerHair)
    {
        hairCurlAccuracy = 100f - (Mathf.Abs(playerHair.curlAmount - CurrentCustomerOrder.curlAmount) / HairCurlAccuracyThreshold) * 100f;
        hairCutAccuracy = 100f - (Mathf.Abs(playerHair.currentLength - CurrentCustomerOrder.currentLength) / HairCutAccuracyThreshold) * 100f;

        // compare playerHair with CurrentCustomerOrder
        if (playerHair.curlAmount >= CurrentCustomerOrder.curlAmount - HairCurlAccuracyThreshold &&
            playerHair.curlAmount <= CurrentCustomerOrder.curlAmount + HairCurlAccuracyThreshold)
        {
            hairCurlAccuracy = 100f;
        }
        if (playerHair.currentLength == CurrentCustomerOrder.currentLength)
        {
            hairCutAccuracy = 100f;
        }

        if (hairCurlAccuracy < 0) hairCurlAccuracy = 0;
        if (hairCutAccuracy < 0) hairCutAccuracy = 0;

        Debug.Log($"{playerHair} Curl Accuracy: {hairCurlAccuracy}% | {playerHair} Cut Accuracy: {hairCutAccuracy}%");
    }

    public void CalculateResult(HairSection L, HairSection R)
    {
        CompareOrderWithSection(L);
        float leftCurlAccuracy = hairCurlAccuracy;
        float leftCutAccuracy = hairCutAccuracy;

        CompareOrderWithSection(R);
        float rightCurlAccuracy = hairCurlAccuracy;
        float rightCutAccuracy = hairCutAccuracy;

        hairCurlAccuracy = (leftCurlAccuracy + rightCurlAccuracy) / 2f; // average both sides (curl)
        hairCutAccuracy = (leftCutAccuracy + rightCutAccuracy) / 2f; // average both sides (cut)

        if (hairCurlAccuracy >= HairCurlMinAccuracy && hairCutAccuracy >= HairCutMinAccuracy)
        {
            scoreAmount = scoreAmount + ((Mathf.RoundToInt(hairCurlAccuracy - HairCurlMinAccuracy)) + ((Mathf.RoundToInt(hairCutAccuracy - HairCutMinAccuracy))*10));
            Instantiate(confettiPS, new Vector3(0, 80, 100), Quaternion.identity);
            successAmount++;
            customerAmount--;
            Debug.Log("Customer is satisfied!");
        }
        else
        {
            Instantiate(angryPS, new Vector3(0, 80, 100), Quaternion.identity);
            lossAmount++;
            customerAmount--;
            Debug.Log("Customer is not satisfied.");
        }

        CurrentCustomerOrder = null; // reset order
    }
}
