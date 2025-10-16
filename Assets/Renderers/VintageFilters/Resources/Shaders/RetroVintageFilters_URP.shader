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
Shader "Hidden/Fronkon Games/Retro/Vintage Filters URP"
{
  Properties
  {
    _MainTex("Main Texture", 2D) = "white" {}
  }

  SubShader
  {
    Tags
    {
      "RenderType" = "Opaque"
      "RenderPipeline" = "UniversalPipeline"
    }
    LOD 100
    ZTest Always ZWrite Off Cull Off

    Pass
    {
      Name "Fronkon Games Retro Vintage Filters"

      HLSLPROGRAM
      #include "Retro.hlsl"

      #pragma vertex RetroVert
      #pragma fragment frag
      #pragma fragmentoption ARB_precision_hint_fastest
      #pragma exclude_renderers d3d9 d3d11_9x ps3 flash
      #pragma multi_compile ___ ENABLE_LUT_3D ENABLE_LUT_2D

      int _Filter;

      uniform TEXTURE2D(_LevelsTex);
      SAMPLER(sampler_LevelsTex);
      uniform TEXTURE2D(_BlowoutTex);
      SAMPLER(sampler_BlowoutTex);
      uniform TEXTURE2D(_CurvesTex);
      SAMPLER(sampler_CurvesTex);
      uniform TEXTURE2D(_EdgeBurnTex);
      SAMPLER(sampler_EdgeBurnTex);
      uniform TEXTURE2D(_GradientTex);
      SAMPLER(sampler_GradientTex);
      uniform TEXTURE2D(_SoftLightTex);
      SAMPLER(sampler_SoftLightTex);

      #include "FilterLevels.hlsl"
      #include "FilterBlowout.hlsl"
      #include "Filter70s.hlsl"
      #include "FilterBrannan.hlsl"
      #include "FilterEarlybird.hlsl"
      #include "FilterHefe.hlsl"
      #include "FilterInkwell.hlsl"
      #include "FilterLordKevin.hlsl"
      #include "FilterSutro.hlsl"
      #include "FilterToaster.hlsl"
      #include "FilterValencia.hlsl"
      #include "FilterLUT.hlsl"
      #include "FilterSepia.hlsl"
      #include "FilterKodachrome.hlsl"
      #include "FilterPolaroid.hlsl"
      #include "FilterCrossProcess.hlsl"
      #include "FilterBleachBypass.hlsl"
      #include "FilterVintage80s.hlsl"
      #include "FilterFilmGrain.hlsl"
      
      float4 frag(const RetroVaryings input) : SV_Target 
      {
        UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);
        const float2 uv = UnityStereoTransformScreenSpaceTex(input.texcoord.xy);

        const float4 color = SAMPLE_MAIN(uv);
        float4 pixel = color;

        switch (_Filter)
        {
          case 10: // Lomofi.
          case 12: // Nashville.
          case 20: // Walden.
          case 21: // XPro II.
            pixel.rgb = FilterLevels(pixel.rgb, uv); break; 
          case 2:  // Amaro.
          case 7:  // Hudson.
          case 14: // Rise.
          case 15: // Sierra.
            pixel.rgb = FilterBlowout(pixel.rgb, uv); break;
          case 0:  // 70s.
            pixel.rgb = Filter70s(pixel.rgb, uv); break; 
          case 1:  // Aden.
          case 4:  // Crema.
          case 9: // Juno.
          case 13: // Reyes.
          case 16: // Slumber.
            pixel.rgb = FilterLUT(pixel.rgb, uv); break; 
          case 3:  // Brannan.
            pixel.rgb = FilterBrannan(pixel.rgb, uv); break; 
          case 5:  // Earlybird.
            pixel.rgb = FilterEarlybird(pixel.rgb, uv); break; 
          case 6:  // Hefe.
            pixel.rgb = FilterHefe(pixel.rgb, uv); break; 
          case 8:  // Inkwell.
            pixel.rgb = FilterInkwell(pixel.rgb, uv); break; 
          case 11: // LordKevin.
            pixel.rgb = FilterLordKevin(pixel.rgb, uv); break; 
          case 17: // Sutro.
            pixel.rgb = FilterSutro(pixel.rgb, uv); break; 
          case 18: // Toaster.
            pixel.rgb = FilterToaster(pixel.rgb, uv); break; 
          case 19: // Valencia.
            pixel.rgb = FilterValencia(pixel.rgb, uv); break; 
          case 22: // Sepia.
            pixel.rgb = FilterSepia(pixel.rgb, uv); break;
          case 23: // Kodachrome.
            pixel.rgb = FilterKodachrome(pixel.rgb, uv); break;
          case 24: // Polaroid.
            pixel.rgb = FilterPolaroid(pixel.rgb, uv); break;
          case 25: // CrossProcess.
            pixel.rgb = FilterCrossProcess(pixel.rgb, uv); break;
          case 26: // BleachBypass.
            pixel.rgb = FilterBleachBypass(pixel.rgb, uv); break;
          case 27: // Vintage80s.
            pixel.rgb = FilterVintage80s(pixel.rgb, uv); break;
          case 28: // FilmGrain.
            pixel.rgb = FilterFilmGrain(pixel.rgb, uv); break;
        }

        pixel = pow(abs(pixel), 1.2);

        // Color adjust.
        pixel.rgb = ColorAdjust(pixel.rgb, _Contrast, _Brightness, _Hue, _Gamma, _Saturation);
        
        return lerp(color, pixel, _Intensity);
      }

      ENDHLSL
    }
  }
  
  FallBack "Diffuse"
}
