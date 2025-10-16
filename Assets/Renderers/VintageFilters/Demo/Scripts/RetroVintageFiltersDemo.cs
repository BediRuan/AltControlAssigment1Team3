using UnityEngine;
using FronkonGames.Retro.VintageFilters;

#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(RetroVintageFiltersDemo))]
public class RetroVintageFiltersDemoWarning : Editor
{
  private GUIStyle Style => style ??= new GUIStyle(GUI.skin.GetStyle("HelpBox")) { richText = true, fontSize = 14, alignment = TextAnchor.MiddleCenter };
  private GUIStyle style;
  public override void OnInspectorGUI()
  {
    EditorGUILayout.TextArea($"\nThis code is only for the demo\n\n<b>DO NOT USE</b> it in your projects\n\nIf you have any questions,\ncheck the <a href='{Constants.Support.Documentation}'>online help</a> or use the <a href='mailto:{Constants.Support.Email}'>support email</a>,\n<b>thanks!</b>\n", Style);
    DrawDefaultInspector();
  }
}
#endif

/// <summary> Retro: Vintage Filters demo. </summary>
/// <remarks>
/// This code is designed for a simple demo, not for production environments.
/// </remarks>
public class RetroVintageFiltersDemo : MonoBehaviour
{
  [Space]
  
  [SerializeField]
  private Transform floor;

  [SerializeField, Range(0.0f, 10.0f)]
  private float angularVelocity;
  
  private VintageFilters.Settings settings;

  private int filterIndex = 0;
  
  private GUIStyle styleFont;
  private GUIStyle styleButton;
  private GUIStyle styleLogo;
  private Vector2 scrollView;

  private const float BoxHeight = 75.0f;
  private const float Margin = 20.0f;
  private const float LabelSize = 250.0f;
  private const float OriginalScreenWidth = 1920.0f;

  private int Slider(string label, int value, int left, int right)
  {
    GUILayout.BeginHorizontal();
    {
      GUILayout.Space(Margin);
    
      GUILayout.Label(label, styleFont, GUILayout.Width(LabelSize));
    
      value = (int)GUILayout.HorizontalSlider(value, left, right, GUILayout.ExpandWidth(true));
    
      GUILayout.Space(Margin);
    }
    GUILayout.EndHorizontal();

    return value;
  }

  private float Slider(string label, float value, float left, float right)
  {
    GUILayout.BeginHorizontal();
    {
      GUILayout.Space(Margin);
    
      GUILayout.Label(label, styleFont, GUILayout.Width(LabelSize));
    
      value = GUILayout.HorizontalSlider(value, left, right, GUILayout.ExpandWidth(true));
    
      GUILayout.Space(Margin);
    }
    GUILayout.EndHorizontal();

    return value;
  }

  private bool Toggle(string label, bool value)
  {
    GUILayout.BeginHorizontal();
    {
      GUILayout.Space(Margin);
    
      GUILayout.Label(label, styleFont, GUILayout.Width(LabelSize));
    
      value = GUILayout.Toggle(value, string.Empty);
    
      GUILayout.Space(Margin);
    }
    GUILayout.EndHorizontal();

    return value;
  }

  private void Awake()
  {
    if (VintageFilters.IsInRenderFeatures() == false)
    {
      Debug.LogWarning($"Effect '{Constants.Asset.Name}' not found. You must add it as a Render Feature.");
#if UNITY_EDITOR
      if (EditorUtility.DisplayDialog($"Effect '{Constants.Asset.Name}' not found", $"You must add '{Constants.Asset.Name}' as a Render Feature.", "Quit") == true)
        EditorApplication.isPlaying = false;
#endif
    }

    this.enabled = VintageFilters.IsInRenderFeatures();
  }

  private void Start()
  {
    settings = VintageFilters.Instance.settings;
    settings?.ResetDefaultValues();
    filterIndex = (int)settings.filter;
  }

  private void Update()
  {
    if (floor != null && angularVelocity > 0.0f)
      floor.rotation = Quaternion.Euler(0.0f, floor.rotation.eulerAngles.y + Time.deltaTime * angularVelocity * 10.0f, 0.0f);
  }

  private void OnGUI()
  {
    Matrix4x4 guiMatrix = GUI.matrix;
    GUI.matrix = Matrix4x4.Scale(Vector3.one * (Screen.width / OriginalScreenWidth));

    styleFont ??= new GUIStyle(GUI.skin.label)
      {
        alignment = TextAnchor.UpperLeft,
        fontStyle = FontStyle.Bold,
        fontSize = 34
      };

    styleButton ??= new GUIStyle(GUI.skin.button)
      {
        fontStyle = FontStyle.Bold,
        fontSize = 34
      };

    styleLogo ??= new GUIStyle(GUI.skin.label)
      {
        alignment = TextAnchor.MiddleCenter,
        fontStyle = FontStyle.Bold,
        fontSize = 34
      };

    if (settings != null)
    {
      GUILayout.BeginHorizontal("box", GUILayout.Height(BoxHeight), GUILayout.Width(Screen.width));
      {
        GUILayout.Space(Margin);

        if (GUILayout.Button("<<", styleButton) == true)
        {
          filterIndex = filterIndex > 0 ? filterIndex - 1 : (int)Filters.XProII;
          settings.filter = (Filters)filterIndex;
        }

        GUILayout.Label(settings.filter.ToString().Replace("_", ""), styleLogo, GUILayout.Width(200.0f));
        
        if (GUILayout.Button(">>", styleButton) == true)
        {
          filterIndex = filterIndex < (int)Filters.XProII ? filterIndex + 1 : 0;
          settings.filter = (Filters)filterIndex;
        }

        GUILayout.Space(Margin);

        string description = settings.filter switch
        {
          Filters._70s =>      "Looks like it's on a old 70's TV",
          Filters.Aden =>      "Makes games look pastel shades",
          Filters.Amaro =>     "This effect adds more light to the centre of the screen and darkens around the edges",
          Filters.Brannan =>   "This low-key effect brings out the grays and greens in your game",
          Filters.Crema =>     "Crema makes games look creamy and smooth",
          Filters.Earlybird => "Use Earlybird to get a retro 'Polaroid' feel with soft faded colors and a hint of yellow",
          Filters.Hefe =>      "Hefe slightly increases saturation and gives a warm fuzzy tone to your game",
          Filters.Hudson =>    "Hudson emphasizes light and gives your game a bluish, colder feel",
          Filters.Inkwell =>   "Inkwell adds high contrast and also makes black and white",
          Filters.Juno =>      "It tints cool tones green, amps up warm tones, and makes whites glow",
          Filters.Lomofi =>    "The Lomofi efect gives your game a dreamy, blurry effect and saturated colors",
          Filters.LordKevin => "Gives a retro look by boosting the earth tones green, brown and orange and adds brightness",
          Filters.Nashville => "Nashville gives your game a warm retro fell and adds a soft purple-pink hue",
          Filters.Reyes =>     "Desaturates your game, brightens it up, and gives it an old-time feel",
          Filters.Rise =>      "Rise gives your game a nice glow and warmth by adding yellow tones",
          Filters.Sierra =>    "Sierra makes the game appear softer by adding bluish tones while emphasizing darks and yellows",
          Filters.Slumber =>   "Slumber desaturate the game and makes them hazy and dreamy look",
          Filters.Sutro =>     "Sutro gives you Sepia-like tones, with an emphasis on purple and brown",
          Filters.Toaster =>   "Gives your game a burnt, aged look. It also adds a slight texture plus vignetting",
          Filters.Valencia =>  "Gives your game a slight faded, 1980’s touch by adding a light brown and gray tint",
          Filters.Walden =>    "Gives your game washed-out, bluish colors and adds a slight corner vignetting",
          Filters.XProII =>    "This effect gives your game a warm vintage feeling and saturated tones",
          _ => string.Empty
        };

        GUILayout.Label(description, styleFont);
        
        GUILayout.FlexibleSpace();
      }
      GUILayout.EndHorizontal();
      
      GUILayout.FlexibleSpace();
    }
    else
      GUILayout.Label($"URP not available or '{Constants.Asset.Name}' is not correctly configured, please consult the documentation", styleLogo);

    GUI.matrix = guiMatrix;
  }
}