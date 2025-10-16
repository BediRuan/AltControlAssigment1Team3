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
#pragma once

float _OverlayWarm;

uniform TEXTURE2D(_MetalTex);
SAMPLER(sampler_MetalTex);
uniform TEXTURE2D(_OverlayWarmTex);
SAMPLER(sampler_OverlayWarmTex);
uniform TEXTURE2D(_ColorShiftTex);
SAMPLER(sampler_ColorShiftTex);

inline float3 FilterToaster(float3 pixel, float2 uv)
{
  float3 final = pixel;

  float3 metal = SAMPLE_TEXTURE2D(_MetalTex, sampler_MetalTex, uv).rgb;

  float2 lookup = float2(metal.r, pixel.r);
  final.r = SAMPLE_TEXTURE2D(_SoftLightTex, sampler_SoftLightTex, 1.0 - lookup).r;
  lookup = float2(metal.g, pixel.g);
  final.g = SAMPLE_TEXTURE2D(_SoftLightTex, sampler_SoftLightTex, 1.0 - lookup).g;
  lookup = float2(metal.b, pixel.b);
  final.b = SAMPLE_TEXTURE2D(_SoftLightTex, sampler_SoftLightTex, 1.0 - lookup).b;

  final.r = SAMPLE_TEXTURE2D(_CurvesTex, sampler_CurvesTex, float2(final.r, 1.0 - 0.16666)).r;
  final.g = SAMPLE_TEXTURE2D(_CurvesTex, sampler_CurvesTex, float2(final.g, 0.5)).g;
  final.b = SAMPLE_TEXTURE2D(_CurvesTex, sampler_CurvesTex, float2(final.b, 1.0 - 0.83333)).b;

  float3 warn = final;
  const float2 tc = ((2.0 * uv) - 1.0);
  lookup.x = dot(tc, tc);
  lookup.y = 1.0 - warn.r;
  warn.r = SAMPLE_TEXTURE2D(_OverlayWarmTex, sampler_OverlayWarmTex, lookup).r;
  lookup.y = 1.0 - warn.g;
  warn.g = SAMPLE_TEXTURE2D(_OverlayWarmTex, sampler_OverlayWarmTex, lookup).g;
  lookup.y = 1.0 - warn.b;
  warn.b = SAMPLE_TEXTURE2D(_OverlayWarmTex, sampler_OverlayWarmTex, lookup).b;
  final = lerp(final, warn, _OverlayWarm);

  final.r = SAMPLE_TEXTURE2D(_ColorShiftTex, sampler_ColorShiftTex, float2(final.r, 1.0 - 0.16666)).r;
  final.g = SAMPLE_TEXTURE2D(_ColorShiftTex, sampler_ColorShiftTex, float2(final.g, 0.5)).g;
  final.b = SAMPLE_TEXTURE2D(_ColorShiftTex, sampler_ColorShiftTex, float2(final.b, 1.0 - 0.83333)).b;
  
  return final;
}
