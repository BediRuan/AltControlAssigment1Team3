////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Martin Bustos @FronkonGames <fronkongames@gmail.com>. All rights reserved.
//
// THIS FILE CAN NOT BE HOSTED IN PUBLIC REPOSITORIES.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using UnityEngine;
using UnityEditor;
using static FronkonGames.Retro.VintageFilters.Inspector;

namespace FronkonGames.Retro.VintageFilters.Editor
{
  /// <summary> Retro Vintage Filters inspector. </summary>
  [CustomPropertyDrawer(typeof(VintageFilters.Settings))]
  public class VintageFiltersFeatureSettingsDrawer : Drawer
  {
    private VintageFilters.Settings settings;

    protected override void ResetValues() => settings?.ResetDefaultValues();

    protected override void InspectorGUI()
    {
      settings ??= GetSettings<VintageFilters.Settings>();

      /////////////////////////////////////////////////
      // Common.
      /////////////////////////////////////////////////
      settings.intensity = Slider("Intensity", "Controls the intensity of the effect [0, 1]. Default 1.", settings.intensity, 0.0f, 1.0f, 1.0f);        

      /////////////////////////////////////////////////
      // Vintage Filters.
      /////////////////////////////////////////////////
      Separator();

      settings.filter = (Filters)EnumPopup("Filter", "Current filter", settings.filter, Filters.Hefe);

      string info = string.Empty;
      switch (settings.filter)
      {
        case Filters._70s:
          info = "Looks like it's on a old 70's TV";
          break;
        case Filters.Aden:
          info = "Makes games look pastel shades";
          break;
        case Filters.Amaro:
          info = "This effect adds more light to the centre of the screen and darkens around the edges";
          settings.amaro.overlay = Slider("Overlay", "Overlay strength [0, 1]. Default 0.5.", settings.amaro.overlay, 0.0f, 1.0f, 0.5f);        
          break;
        case Filters.Brannan:
          info = "This low-key effect brings out the grays and greens in your game";
          break;
        case Filters.Crema:
          info = "Crema makes games look creamy and smooth";
          break;
        case Filters.Earlybird:
          info = "Use Earlybird to get a retro 'Polaroid' feel with soft faded colors and a hint of yellow";
          break;
        case Filters.Hefe:
          info = "Hefe slightly increases saturation and gives a warm fuzzy tone to your game";
          settings.hefe.edgeBurn = Slider("Edge burn", "Edge burn strength [0, 1]. Default 1.0.", settings.hefe.edgeBurn, 0.0f, 1.0f, 1.0f);        
          settings.hefe.gradient = Slider("Gradient", "Gradient strength [0, 1]. Default 1.0.", settings.hefe.gradient, 0.0f, 1.0f, 1.0f);        
          settings.hefe.softLight = Slider("Soft light", "Gradient strength [0, 1]. Default 1.0.", settings.hefe.softLight, 0.0f, 1.0f, 1.0f);        
          break;
        case Filters.Hudson:
          info = "Hudson emphasizes light and gives your game a bluish, colder feel";
          settings.hudson.overlay = Slider("Overlay", "Overlay strength [0, 1]. Default 0.25.", settings.hudson.overlay, 0.0f, 1.0f, 0.25f);        
          break;
        case Filters.Inkwell:
          info = "Inkwell adds high contrast and also makes black and white";
          break;
        case Filters.Juno:
          info = "It tints cool tones green, amps up warm tones, and makes whites glow";
          break;
        case Filters.Lomofi:
          info = "The Lomofi efect gives your game a dreamy, blurry effect and saturated colors";
          break;
        case Filters.LordKevin:
          info = "Gives a retro look by boosting the earth tones green, brown and orange and adds brightness";
          break;
        case Filters.Nashville:
          info = "Nashville gives your game a warm retro fell and adds a soft purple-pink hue";
          break;
        case Filters.Reyes:
          info = "Desaturates your game, brightens it up, and gives it an old-time feel";
          break;
        case Filters.Rise:
          info = "Rise gives your game a nice glow and warmth by adding yellow tones";
          settings.rise.overlay = Slider("Overlay", "Overlay strength [0, 1]. Default 0.25.", settings.rise.overlay, 0.0f, 1.0f, 0.25f);        
          break;
        case Filters.Sierra:
          info = "Sierra makes the game appear softer by adding bluish tones while emphasizing darks and yellows";
          settings.sierra.overlay = Slider("Overlay", "Overlay strength [0, 1]. Default 0.25.", settings.sierra.overlay, 0.0f, 1.0f, 0.25f);        
          break;
        case Filters.Slumber:
          info = "Slumber desaturate the game and makes them hazy and dreamy look";
          break;
        case Filters.Sutro:
          info = "Sutro gives you Sepia-like tones, with an emphasis on purple and brown";
          break;
        case Filters.Toaster:
          info = "Gives your game a burnt, aged look. It also adds a slight texture plus vignetting";
          settings.toaster.overlayWarm = Slider("Overlay warm", "Overlay warm strength [0, 1]. Default 0.25.", settings.toaster.overlayWarm, 0.0f, 1.0f, 0.25f);        
          break;
        case Filters.Valencia:
          info = "Gives your game a slight faded, 1980’s touch by adding a light brown and gray tint";
          break;
        case Filters.Walden:
          info = "Gives your game washed-out, bluish colors and adds a slight corner vignetting";
          break;
        case Filters.XProII:
          info = "This effect gives your game a warm vintage feeling and saturated tones";
          break;
        case Filters.Sepia:
          info = "Classic sepia tone effect that gives your game a warm, aged brown tint reminiscent of old photographs";
          settings.sepia.intensity = Slider("Intensity", "Sepia intensity [0, 1]. Default 0.8.", settings.sepia.intensity, 0.0f, 1.0f, 0.8f);
          break;
        case Filters.Kodachrome:
          info = "Emulates the rich, vibrant colors of Kodachrome film with enhanced reds and warm tones";
          settings.kodachrome.enhancement = Slider("Enhancement", "Color enhancement [0, 1]. Default 0.6.", settings.kodachrome.enhancement, 0.0f, 1.0f, 0.6f);
          settings.kodachrome.warmth = Slider("Warmth", "Warmth [0, 1]. Default 0.3.", settings.kodachrome.warmth, 0.0f, 1.0f, 0.3f);
          break;
        case Filters.Polaroid:
          info = "Recreates the instant camera aesthetic with soft colors, slight overexposure, and warm highlights";
          settings.polaroid.overexposure = Slider("Overexposure", "Overexposure [0, 1]. Default 0.2.", settings.polaroid.overexposure, 0.0f, 1.0f, 0.2f);
          settings.polaroid.softness = Slider("Softness", "Softness [0, 1]. Default 0.4.", settings.polaroid.softness, 0.0f, 1.0f, 0.4f);
          break;
        case Filters.CrossProcess:
          info = "Cross-processing effect with shifted colors and increased contrast for a surreal, artistic look";
          settings.crossProcess.colorShift = Slider("Color Shift", "Color shift intensity [0, 1]. Default 0.7.", settings.crossProcess.colorShift, 0.0f, 1.0f, 0.7f);
          settings.crossProcess.contrastBoost = Slider("Contrast Boost", "Contrast boost [0, 1]. Default 0.5.", settings.crossProcess.contrastBoost, 0.0f, 1.0f, 0.5f);
          break;
        case Filters.BleachBypass:
          info = "High-contrast bleach bypass effect that retains silver while removing color for a dramatic, desaturated look";
          settings.bleachBypass.desaturation = Slider("Desaturation", "Desaturation amount [0, 1]. Default 0.6.", settings.bleachBypass.desaturation, 0.0f, 1.0f, 0.6f);
          settings.bleachBypass.contrast = Slider("Contrast", "Contrast increase [0, 1]. Default 0.4.", settings.bleachBypass.contrast, 0.0f, 1.0f, 0.4f);
          break;
        case Filters.Vintage80s:
          info = "Captures the neon-soaked, vibrant aesthetic of the 1980s with enhanced magentas and cyans";
          settings.vintage80s.neonIntensity = Slider("Neon Intensity", "Neon intensity [0, 1]. Default 0.5.", settings.vintage80s.neonIntensity, 0.0f, 1.0f, 0.5f);
          settings.vintage80s.colorPop = Slider("Color Pop", "Color pop [0, 1]. Default 0.6.", settings.vintage80s.colorPop, 0.0f, 1.0f, 0.6f);
          break;
        case Filters.FilmGrain:
          info = "Adds authentic film grain texture to give your game the organic feel of analog photography";
          settings.filmGrain.intensity = Slider("Intensity", "Grain intensity [0, 1]. Default 0.3.", settings.filmGrain.intensity, 0.0f, 1.0f, 0.3f);
          settings.filmGrain.size = Slider("Size", "Grain size [0.5, 2.0]. Default 1.0.", settings.filmGrain.size, 0.5f, 2.0f, 1.0f);
          break;
      }

      BeginHorizontal();
      {
        Separator(LabelWidth);
        
        EditorGUILayout.HelpBox("  " + info, MessageType.Info);
      }
      EndHorizontal();
      
      /////////////////////////////////////////////////
      // Color.
      /////////////////////////////////////////////////
      Separator();

      if (Foldout("Color") == true)
      {
        IndentLevel++;

        settings.brightness = Slider("Brightness", "Brightness [-1.0, 1.0]. Default 0.", settings.brightness, -1.0f, 1.0f, 0.0f);
        settings.contrast = Slider("Contrast", "Contrast [0.0, 10.0]. Default 1.", settings.contrast, 0.0f, 10.0f, 1.0f);
        settings.gamma = Slider("Gamma", "Gamma [0.1, 10.0]. Default 1.", settings.gamma, 0.01f, 10.0f, 1.0f);
        settings.hue = Slider("Hue", "The color wheel [0.0, 1.0]. Default 0.", settings.hue, 0.0f, 1.0f, 0.0f);
        settings.saturation = Slider("Saturation", "Intensity of a colors [0.0, 2.0]. Default 1.", settings.saturation, 0.0f, 2.0f, 1.0f);

        IndentLevel--;
      }
      
      /////////////////////////////////////////////////
      // Advanced.
      /////////////////////////////////////////////////
      Separator();

      if (Foldout("Advanced") == true)
      {
        IndentLevel++;

#if !UNITY_6000_0_OR_NEWER
        settings.filterMode = (FilterMode)EnumPopup("Filter mode", "Filter mode. Default Bilinear.", settings.filterMode, FilterMode.Bilinear);
#endif
        settings.affectSceneView = Toggle("Affect the Scene View?", "Does it affect the Scene View?", settings.affectSceneView);
        settings.whenToInsert = (UnityEngine.Rendering.Universal.RenderPassEvent)EnumPopup("RenderPass event",
          "Render pass injection. Default BeforeRenderingPostProcessing.",
          settings.whenToInsert,
          UnityEngine.Rendering.Universal.RenderPassEvent.BeforeRenderingPostProcessing);
#if !UNITY_6000_0_OR_NEWER
        settings.enableProfiling = Toggle("Enable profiling", "Enable render pass profiling", settings.enableProfiling);
#endif

        IndentLevel--;
      }
    }
  }
}
