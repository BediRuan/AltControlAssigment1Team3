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
using UnityEngine.Rendering.Universal;

namespace FronkonGames.Retro.VintageFilters
{
  ///------------------------------------------------------------------------------------------------------------------
  /// <summary> Settings. </summary>
  /// <remarks> Only available for Universal Render Pipeline. </remarks>
  ///------------------------------------------------------------------------------------------------------------------
  public sealed partial class VintageFilters
  {
    /// <summary>
    /// Settings.
    /// </summary>
    [System.Serializable]
    public sealed class Settings
    {
#region Common settings.
      /// <summary> Controls the intensity of the effect [0, 1]. Default 1. </summary>
      /// <remarks> An effect with Intensity equal to 0 will not be executed. </remarks>
      public float intensity = 1.0f;
#endregion

#region Vintage Filters settings.
      /// <summary> Current filter. </summary>
      public Filters filter = Filters.Hefe;

      /// <summary> Amaro filter settings. </summary>
      public Amaro amaro = new();

      /// <summary> Hefe filter settings. </summary>
      public Hefe hefe = new();
      
      /// <summary> Hudson filter settings. </summary>
      public Hudson hudson = new();
      
      /// <summary> Rise filter settings. </summary>
      public Rise rise = new();
      
      /// <summary> Sierra filter settings. </summary>
      public Sierra sierra = new();
      
      /// <summary> Toaster filter settings. </summary>
      public Toaster toaster = new();
      
      /// <summary> Sepia filter settings. </summary>
      public Sepia sepia = new();
      
      /// <summary> Kodachrome filter settings. </summary>
      public Kodachrome kodachrome = new();
      
      /// <summary> Polaroid filter settings. </summary>
      public Polaroid polaroid = new();
      
      /// <summary> Cross Process filter settings. </summary>
      public CrossProcess crossProcess = new();
      
      /// <summary> Bleach Bypass filter settings. </summary>
      public BleachBypass bleachBypass = new();
      
      /// <summary> Vintage 80s filter settings. </summary>
      public Vintage80s vintage80s = new();
      
      /// <summary> Film Grain filter settings. </summary>
      public FilmGrain filmGrain = new();
#endregion

#region Color settings.
      /// <summary> Brightness [-1.0, 1.0]. Default 0. </summary>
      public float brightness = 0.0f;

      /// <summary> Contrast [0.0, 10.0]. Default 1. </summary>
      public float contrast = 1.0f;

      /// <summary>Gamma [0.1, 10.0]. Default 1. </summary>      
      public float gamma = 1.0f;

      /// <summary> The color wheel [0.0, 1.0]. Default 0. </summary>
      public float hue = 0.0f;

      /// <summary> Intensity of a colors [0.0, 2.0]. Default 1. </summary>      
      public float saturation = 1.0f;
      #endregion

      #region Advanced settings.
      /// <summary> Does it affect the Scene View? </summary>
      public bool affectSceneView = false;

#if !UNITY_6000_0_OR_NEWER
      /// <summary> Enable render pass profiling. </summary>
      public bool enableProfiling = false;

      /// <summary> Filter mode. Default Bilinear. </summary>
      public FilterMode filterMode = FilterMode.Bilinear;
#endif

      /// <summary> Render pass injection. Default BeforeRenderingPostProcessing. </summary>
      public RenderPassEvent whenToInsert = RenderPassEvent.BeforeRenderingPostProcessing;

      /// <summary> Use, if available, 3D textures for LUTs </summary>
      public bool use3DTextures = true;
#endregion

      /// <summary> Reset to default values. </summary>
      public void ResetDefaultValues()
      {
        intensity = 1.0f;
        
        amaro.ResetDefaultValues();
        hudson.ResetDefaultValues();
        rise.ResetDefaultValues();
        sierra.ResetDefaultValues();
        toaster.ResetDefaultValues();
        sepia.ResetDefaultValues();
        kodachrome.ResetDefaultValues();
        polaroid.ResetDefaultValues();
        crossProcess.ResetDefaultValues();
        bleachBypass.ResetDefaultValues();
        vintage80s.ResetDefaultValues();
        filmGrain.ResetDefaultValues();
        
        brightness = 0.0f;
        contrast = 1.0f;
        gamma = 1.0f;
        hue = 0.0f;
        saturation = 1.0f;

        affectSceneView = false;
#if !UNITY_6000_0_OR_NEWER
        enableProfiling = false;
        filterMode = FilterMode.Bilinear;
#endif
        whenToInsert = RenderPassEvent.BeforeRenderingPostProcessing;
      }
    }    
  }
  
  [System.Serializable]
  public sealed class Amaro
  {
    /// <summary> Overlay strength [0, 1]. Default 0.5. </summary>
    public float overlay = 0.5f;

    /// <summary> Reset to default values. </summary>
    public void ResetDefaultValues() => overlay = 0.5f;
  }

  [System.Serializable]
  public sealed class Hefe
  {
    /// <summary> Edge burn strength [0, 1]. Default 1.0. </summary>
    public float edgeBurn = 1.0f;

    /// <summary> Gradient strength [0, 1]. Default 1.0. </summary>
    public float gradient = 1.0f;

    /// <summary> Gradient strength [0, 1]. Default 1.0. </summary>
    public float softLight = 1.0f;

    /// <summary> Reset to default values. </summary>
    public void ResetDefaultValues()
    {
      edgeBurn = 1.0f;
      gradient = 1.0f;
      softLight = 1.0f;
    }
  }
  
  [System.Serializable]
  public sealed class Hudson
  {
    /// <summary> Overlay strength [0, 1]. Default 0.25. </summary>
    public float overlay = 0.25f;

    /// <summary> Reset to default values. </summary>
    public void ResetDefaultValues() => overlay = 0.25f;
  }
  
  [System.Serializable]
  public sealed class Rise
  {
    /// <summary> Overlay strength [0, 1]. Default 0.25. </summary>
    public float overlay = 0.25f;

    /// <summary> Reset to default values. </summary>
    public void ResetDefaultValues() => overlay = 0.25f;
  }
  
  [System.Serializable]
  public sealed class Sierra
  {
    /// <summary> Overlay strength [0, 1]. Default 0.25. </summary>
    public float overlay = 0.25f;

    /// <summary> Reset to default values. </summary>
    public void ResetDefaultValues() => overlay = 0.25f;
  }
  
  [System.Serializable]
  public sealed class Toaster
  {
    /// <summary> Soft light strength [0, 1]. Default 0.25. </summary>
    public float overlayWarm = 0.25f;

    /// <summary> Reset to default values. </summary>
    public void ResetDefaultValues() => overlayWarm = 0.25f;
  }

  [System.Serializable]
  public sealed class Sepia
  {
    /// <summary> Sepia intensity [0, 1]. Default 0.8. </summary>
    public float intensity = 0.8f;

    /// <summary> Reset to default values. </summary>
    public void ResetDefaultValues() => intensity = 0.8f;
  }

  [System.Serializable]
  public sealed class Kodachrome
  {
    /// <summary> Color enhancement [0, 1]. Default 0.6. </summary>
    public float enhancement = 0.6f;

    /// <summary> Warmth [0, 1]. Default 0.3. </summary>
    public float warmth = 0.3f;

    /// <summary> Reset to default values. </summary>
    public void ResetDefaultValues()
    {
      enhancement = 0.6f;
      warmth = 0.3f;
    }
  }

  [System.Serializable]
  public sealed class Polaroid
  {
    /// <summary> Overexposure [0, 1]. Default 0.2. </summary>
    public float overexposure = 0.2f;

    /// <summary> Softness [0, 1]. Default 0.4. </summary>
    public float softness = 0.4f;

    /// <summary> Reset to default values. </summary>
    public void ResetDefaultValues()
    {
      overexposure = 0.2f;
      softness = 0.4f;
    }
  }

  [System.Serializable]
  public sealed class CrossProcess
  {
    /// <summary> Color shift intensity [0, 1]. Default 0.7. </summary>
    public float colorShift = 0.7f;

    /// <summary> Contrast boost [0, 1]. Default 0.5. </summary>
    public float contrastBoost = 0.5f;

    /// <summary> Reset to default values. </summary>
    public void ResetDefaultValues()
    {
      colorShift = 0.7f;
      contrastBoost = 0.5f;
    }
  }

  [System.Serializable]
  public sealed class BleachBypass
  {
    /// <summary> Desaturation amount [0, 1]. Default 0.6. </summary>
    public float desaturation = 0.6f;

    /// <summary> Contrast increase [0, 1]. Default 0.4. </summary>
    public float contrast = 0.4f;

    /// <summary> Reset to default values. </summary>
    public void ResetDefaultValues()
    {
      desaturation = 0.6f;
      contrast = 0.4f;
    }
  }

  [System.Serializable]
  public sealed class Vintage80s
  {
    /// <summary> Neon intensity [0, 1]. Default 0.5. </summary>
    public float neonIntensity = 0.5f;

    /// <summary> Color pop [0, 1]. Default 0.6. </summary>
    public float colorPop = 0.6f;

    /// <summary> Reset to default values. </summary>
    public void ResetDefaultValues()
    {
      neonIntensity = 0.5f;
      colorPop = 0.6f;
    }
  }

  [System.Serializable]
  public sealed class FilmGrain
  {
    /// <summary> Grain intensity [0, 1]. Default 0.3. </summary>
    public float intensity = 0.3f;

    /// <summary> Grain size [0.5, 2.0]. Default 1.0. </summary>
    public float size = 1.0f;

    /// <summary> Reset to default values. </summary>
    public void ResetDefaultValues()
    {
      intensity = 0.3f;
      size = 1.0f;
    }
  }
}
