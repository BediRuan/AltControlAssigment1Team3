using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TargetHairStyleUI : MonoBehaviour
{
    [Header("UI Components")]
    public Image targetLeftHairImage;
    public Image targetRightHairImage;
    public TextMeshProUGUI hairLabel;
    
    [Header("Hair Sprites")]
    [SerializeField] private Sprite[] leftHairSprites; 
    [SerializeField] private Sprite[] rightHairSprites; 
    
    [Header("Default Sprites")]
    [SerializeField] private Sprite defaultLeftHairSprite;
    [SerializeField] private Sprite defaultRightHairSprite;
    
    private void Start()
    {
        // 初始化时隐藏图像
        if (targetLeftHairImage != null)
            targetLeftHairImage.gameObject.SetActive(false);
        if (targetRightHairImage != null)
            targetRightHairImage.gameObject.SetActive(false);
    }
    

    /// <param name="targetLength">target length (1-3)</param>
    /// <param name="targetCurl">target curl (0-100)</param>
    public void UpdateTargetHairStyle(int targetLength, float targetCurl)
    {
        // 显示左半片发型
        if (targetLeftHairImage != null)
        {
            targetLeftHairImage.gameObject.SetActive(true);
            targetLeftHairImage.sprite = GetLeftHairSprite(targetLength, targetCurl);
        }
        
        // 显示右半片发型
        if (targetRightHairImage != null)
        {
            targetRightHairImage.gameObject.SetActive(true);
            targetRightHairImage.sprite = GetRightHairSprite(targetLength, targetCurl);
        }
        
        // 更新标签
        if (hairLabel != null)
        {
            hairLabel.text = GetHairDescription(targetLength, targetCurl);
        }
    }
    
    /// <summary>
    /// 获取左半片发型图像
    /// </summary>
    private Sprite GetLeftHairSprite(int length, float curl)
    {
        int spriteIndex = GetHairSpriteIndex(length, curl);
        if (leftHairSprites != null && spriteIndex >= 0 && spriteIndex < leftHairSprites.Length)
        {
            return leftHairSprites[spriteIndex];
        }
        return defaultLeftHairSprite;
    }
    
    /// <summary>
    /// 获取右半片发型图像
    /// </summary>
    private Sprite GetRightHairSprite(int length, float curl)
    {
        int spriteIndex = GetHairSpriteIndex(length, curl);
        if (rightHairSprites != null && spriteIndex >= 0 && spriteIndex < rightHairSprites.Length)
        {
            return rightHairSprites[spriteIndex];
        }
        return defaultRightHairSprite;
    }
    
    /// <summary>
    /// convert curl value to index
    /// </summary>
    private int GetCurlIndex(float curl)
    {
        if (curl <= 0) return 1;      // straight hair (index 1)
        if (curl <= 30) return 2;     // light curl (index 2)
        if (curl <= 70) return 3;     // medium curl (index 3)
        return 3;                     // default medium curl
    }
    
    /// <summary>
    /// get hair sprite index by length and curl
    /// </summary>
    private int GetHairSpriteIndex(int length, float curl)
    {
        int curlIndex = GetCurlIndex(curl);
        // new index system: length-curl
        // long hair(1): 1-1, 1-2, 1-3
        // medium hair(2): 2-1, 2-2, 2-3  
        // short hair(3): 3-1, 3-2, 3-3
        // index calculation: length(1-3) * 3 + curl(1-3) - 1 = 0-8
        return (length - 1) * 3 + (curlIndex - 1);
    }
    
    /// <summary>
    /// get hair description
    /// </summary>
    private string GetHairDescription(int length, float curl)
    {
        string lengthDesc = GetLengthDescription(length);
        string curlDesc = GetCurlDescription(curl);
        return $"{lengthDesc} hair - {curlDesc}";
    }
    
    /// <summary>
    /// get length description
    /// </summary>
    private string GetLengthDescription(int length)
    {
        switch (length)
        {
            case 1: return "long";
            case 2: return "medium";
            case 3: return "short";
            default: return "none";
        }
    }
    
    /// <summary>
    /// get curl description
    /// </summary>
    private string GetCurlDescription(float curl)
    {
        if (curl <= 0) return "straight";
        if (curl <= 30) return "light";
        if (curl <= 70) return "medium";
        return "medium"; // default medium curl
    }
    
    /// <summary>
    /// 隐藏目标发型显示
    /// </summary>
    public void HideTargetHairStyle()
    {
        if (targetLeftHairImage != null)
            targetLeftHairImage.gameObject.SetActive(false);
        if (targetRightHairImage != null)
            targetRightHairImage.gameObject.SetActive(false);
    }
}
