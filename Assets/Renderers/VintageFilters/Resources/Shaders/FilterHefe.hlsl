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

float _EdgeBurn;
float _Gradient;
float _SoftLight;

inline float3 FilterHefe(float3 pixel, float2 uv)
{
  float3 final = pixel;

  final *= lerp((float3)1.0, SAMPLE_TEXTURE2D(_EdgeBurnTex, sampler_EdgeBurnTex, uv).rgb, _EdgeBurn);

  final.r = SAMPLE_TEXTURE2D(_LevelsTex, sampler_LevelsTex, float2(final.r, 1.0 - 0.16666)).r;
  final.g = SAMPLE_TEXTURE2D(_LevelsTex, sampler_LevelsTex, float2(final.g, 0.5)).g;
  final.b = SAMPLE_TEXTURE2D(_LevelsTex, sampler_LevelsTex, float2(final.b, 1.0 - 0.83333)).b;

  const float3 gradSample = lerp((float3)0.0, SAMPLE_TEXTURE2D(_GradientTex, sampler_GradientTex, float2(Luminance(final), 0.5)).rgb, _Gradient);

  final *= lerp((float3)1.0,
                float3(SAMPLE_TEXTURE2D(_SoftLightTex, sampler_SoftLightTex, float2(final.r, 0.16666 - gradSample.r)).r,
                       SAMPLE_TEXTURE2D(_SoftLightTex, sampler_SoftLightTex, float2(final.g, 0.5 - gradSample.g)).g,
                       SAMPLE_TEXTURE2D(_SoftLightTex, sampler_SoftLightTex, float2(final.b, 0.83333 - gradSample.b)).b),
                _SoftLight);
  
  return final;
}
