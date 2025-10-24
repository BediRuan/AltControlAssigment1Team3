using UnityEngine;
using UnityEditor;
using System.IO;

#if UNITY_EDITOR
public class HairStyleImageGenerator : EditorWindow
{
    [MenuItem("Tools/generate hair style preview image")]
    public static void ShowWindow()
    {
        GetWindow<HairStyleImageGenerator>("hair style image generator");
    }
    
    private void OnGUI()
    {
        GUILayout.Label("hair style preview image generator", EditorStyles.boldLabel);
        GUILayout.Space(10);
        
        GUILayout.Label("this tool will generate simple hair style preview image");
        GUILayout.Label("images will be saved to Assets/Sprites/HairStyles/ directory");
        GUILayout.Space(10);
        
        if (GUILayout.Button("generate all hair style images"))
        {
            GenerateHairStyleImages();
        }
        
        GUILayout.Space(10);
        GUILayout.Label("note: generated images are simple geometric shape examples");
        GUILayout.Label("you can replace these example images with actual avatar images");
    }
    
    private void GenerateHairStyleImages()
    {
        string directory = "Assets/Sprites/HairStyles/";
        
        // create directory
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
        
        // generate combined hair style images
        GenerateCombinedHairImages(directory);
        
        AssetDatabase.Refresh();
        Debug.Log("hair style images generated successfully");
    }
    
    private void GenerateCombinedHairImages(string directory)
    {
        // generate 9 combinations of length and curl for both left and right halves
        string[] lengths = {"long", "medium", "short"}; // long hair, medium hair, short hair
        string[] curls = {"straight", "light", "medium"}; // straight hair, light hair, medium hair
        Color[] lengthColors = {Color.brown, Color.gray, Color.black}; // long hair brown, medium hair gray, short hair black
        Color[] curlColors = {Color.black, Color.gray, Color.brown}; // straight hair black, light hair gray, medium hair brown
        
        for (int lengthIndex = 0; lengthIndex < lengths.Length; lengthIndex++)
        {
            for (int curlIndex = 0; curlIndex < curls.Length; curlIndex++)
            {
                int spriteIndex = lengthIndex * 3 + curlIndex;
                
                // generate left half hair style image
                CreateHalfHairImage(directory + $"left_hair_{spriteIndex}.png", 
                    lengthColors[lengthIndex], curlColors[curlIndex], 
                    lengths[lengthIndex], curls[curlIndex], true);
                
                // generate right half hair style image (mirrored)
                CreateHalfHairImage(directory + $"right_hair_{spriteIndex}.png", 
                    lengthColors[lengthIndex], curlColors[curlIndex], 
                    lengths[lengthIndex], curls[curlIndex], false);
            }
        }
    }
    
    private void CreateHalfHairImage(string path, Color lengthColor, Color curlColor, string lengthDesc, string curlDesc, bool isLeft)
    {
        Texture2D texture = new Texture2D(100, 100);
        Color[] pixels = new Color[100 * 100];
        
        for (int y = 0; y < 100; y++)
        {
            for (int x = 0; x < 100; x++)
            {
                // 只绘制左半部分或右半部分
                bool shouldDraw = isLeft ? (x < 50) : (x >= 50);
                
                if (shouldDraw)
                {
                    // 检查是否在发型轮廓内
                    if (IsInsideHalfHairOutline(x, y, lengthDesc, curlDesc, isLeft))
                    {
                        // 混合长度和卷度颜色
                        Color finalColor = Color.Lerp(lengthColor, curlColor, 0.5f);
                        pixels[y * 100 + x] = finalColor;
                    }
                    else
                    {
                        pixels[y * 100 + x] = Color.clear;
                    }
                }
                else
                {
                    pixels[y * 100 + x] = Color.clear;
                }
            }
        }
        
        texture.SetPixels(pixels);
        texture.Apply();
        
        byte[] pngData = texture.EncodeToPNG();
        File.WriteAllBytes(path, pngData);
        
        DestroyImmediate(texture);
    }
    
    private bool IsInsideHalfHairOutline(int x, int y, string lengthDesc, string curlDesc, bool isLeft)
    {
        // 调整中心点位置（左半片或右半片）
        int centerX = isLeft ? 25 : 75;
        int centerY = 50;
        
        // 根据长度确定发型范围
        float hairRadius = 0;
        switch (lengthDesc)
        {
            case "short":
                hairRadius = 15;
                break;
            case "medium":
                hairRadius = 20;
                break;
            case "long":
                hairRadius = 25;
                break;
        }
        
        // 检查是否在发型范围内
        float distance = Mathf.Sqrt((x - centerX) * (x - centerX) + (y - centerY) * (y - centerY));
        if (distance > hairRadius) return false;
        
        // 根据卷度调整发型形状
        switch (curlDesc)
        {
            case "straight":
                return Mathf.Abs(x - centerX) < 8;
            case "light":
                return Mathf.Abs(x - centerX) < 10;
            case "medium":
                return Mathf.Abs(x - centerX) < 12;
            default:
                return false;
        }
    }
}
#endif
