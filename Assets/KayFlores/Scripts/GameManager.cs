using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
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
        if (CurrentCustomerOrder == null)
        {
            GenerateNewOrder();
        }
    }

    public void GenerateNewOrder()
    {
        CurrentCustomerOrder = new HairSection() // applies to both sides of hair
        {
            curlAmount = Random.Range(0f, 1f) > 0.4f ? Random.Range(20f, 70f) : 0f, // 60% chance of having curls (of any amount)
            currentLength = Random.Range(1, 4), // length between 1 and 3 (default is 4)
        };
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
            Debug.Log("Customer is satisfied!");
        }
        else
        {
            Debug.Log("Customer is not satisfied.");
        }

        CurrentCustomerOrder = null; // reset order
    }
}
