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

uniform TEXTURE2D(_ProcessTex);
SAMPLER(sampler_ProcessTex);
uniform TEXTURE2D(_ContrastTex);
SAMPLER(sampler_ContrastTex);
uniform TEXTURE2D(_LumaTex);
SAMPLER(sampler_LumaTex);
uniform TEXTURE2D(_ScreenTex);
SAMPLER(sampler_ScreenTex);

inline float3 FilterBrannan(float3 pixel, float2 uv)
{
  // Process.
  float2 lookup;
  lookup.y = 0.5;
  lookup.x = pixel.r;
  float3 final;
  final.r = SAMPLE_TEXTURE2D(_ProcessTex, sampler_ProcessTex, lookup).r;
  lookup.x = pixel.g;
  final.g = SAMPLE_TEXTURE2D(_ProcessTex, sampler_ProcessTex, lookup).g;
  lookup.x = pixel.b;
  final.b = SAMPLE_TEXTURE2D(_ProcessTex, sampler_ProcessTex, lookup).b;        

  // Blowout.
  const float2 tc = (2.0 * uv) - 1.0;
  const float d = dot(tc, tc);
  float3 sampled;
  lookup.x = final.r;
  sampled.r = SAMPLE_TEXTURE2D(_BlowoutTex, sampler_BlowoutTex, lookup).r;
  lookup.x = final.g;
  sampled.g = SAMPLE_TEXTURE2D(_BlowoutTex, sampler_BlowoutTex, lookup).g;
  lookup.x = final.b;
  sampled.b = SAMPLE_TEXTURE2D(_BlowoutTex, sampler_BlowoutTex, lookup).b;
  const float value = smoothstep(0.0, 1.0, d);
  final = lerp(sampled, final, value);

  // Contrast.
  lookup.x = final.r;
  final.r = SAMPLE_TEXTURE2D(_ContrastTex, sampler_ContrastTex, lookup).r;
  lookup.x = final.g;
  final.g = SAMPLE_TEXTURE2D(_ContrastTex, sampler_ContrastTex, lookup).g;
  lookup.x = final.b;
  final.b = SAMPLE_TEXTURE2D(_ContrastTex, sampler_ContrastTex, lookup).b;

  // Luma.
  lookup.x = Luminance(final);
  final = lerp(SAMPLE_TEXTURE2D(_LumaTex, sampler_LumaTex, lookup).rgb, final, 0.5f);

  // Screen.
  lookup.x = final.r;
  final.r = SAMPLE_TEXTURE2D(_ScreenTex, sampler_ScreenTex, lookup).r;
  lookup.x = final.g;
  final.g = SAMPLE_TEXTURE2D(_ScreenTex, sampler_ScreenTex, lookup).g;
  lookup.x = final.b;
  final.b = SAMPLE_TEXTURE2D(_ScreenTex, sampler_ScreenTex, lookup).b;

  return final;
}
