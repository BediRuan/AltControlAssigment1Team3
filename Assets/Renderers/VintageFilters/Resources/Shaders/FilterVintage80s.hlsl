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

float _Vintage80sNeonIntensity;
float _Vintage80sColorPop;

inline float3 FilterVintage80s(float3 pixel, float2 uv)
{
  float3 enhanced = pixel;
  
  enhanced.r = pow(enhanced.r, 0.9);
  enhanced.g = pow(enhanced.g, 1.1);
  enhanced.b = pow(enhanced.b, 0.85);
  
  float luma = dot(enhanced, float3(0.299, 0.587, 0.114));
  float neonMask = smoothstep(0.6, 1.0, luma);
  
  if (neonMask > 0.1)
  {
    enhanced.r += neonMask * _Vintage80sNeonIntensity * 0.1;
    enhanced.b += neonMask * _Vintage80sNeonIntensity * 0.15;
  }
  
  float gray = dot(enhanced, float3(0.299, 0.587, 0.114));
  enhanced = lerp(gray.xxx, enhanced, 1.0 + _Vintage80sColorPop * 0.5);
  
  enhanced.r += 0.02;
  enhanced.b += 0.03;
  
  float2 center = uv - 0.5;
  float vignette = 1.0 - dot(center, center) * 0.3;
  enhanced *= vignette;
  
  return clamp(enhanced, 0.0, 1.0);
}
