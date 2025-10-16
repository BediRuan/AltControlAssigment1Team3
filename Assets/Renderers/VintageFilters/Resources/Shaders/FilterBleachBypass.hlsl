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

float _BleachBypassDesaturation;
float _BleachBypassContrast;

inline float3 FilterBleachBypass(float3 pixel, float2 uv)
{
  float luma = dot(pixel, float3(0.299, 0.587, 0.114));
  
  float3 desaturated = lerp(pixel, luma.xxx, _BleachBypassDesaturation);
  
  desaturated = (desaturated - 0.5) * (1.0 + _BleachBypassContrast) + 0.5;
  
  float highlightMask = smoothstep(0.4, 0.8, luma);
  desaturated += highlightMask * 0.1;
  
  desaturated.r *= 0.98;
  desaturated.g *= 0.99;
  desaturated.b *= 1.02;
  
  float2 offset = TEXEL_SIZE.xy;
  float3 neighbor1 = SAMPLE_MAIN(uv + float2(offset.x, 0)).rgb;
  float3 neighbor2 = SAMPLE_MAIN(uv + float2(0, offset.y)).rgb;
  float3 neighbor3 = SAMPLE_MAIN(uv - float2(offset.x, 0)).rgb;
  float3 neighbor4 = SAMPLE_MAIN(uv - float2(0, offset.y)).rgb;
  
  float3 avgNeighbor = (neighbor1 + neighbor2 + neighbor3 + neighbor4) * 0.25;
  float edgeDetect = length(pixel - avgNeighbor);
  
  desaturated += edgeDetect * _BleachBypassContrast * 0.1;
  
  return clamp(desaturated, 0.0, 1.0);
}
