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

float3 _LutParams;
#ifdef ENABLE_LUT_3D
uniform TEXTURE3D(_LutTex);
SAMPLER(sampler_LutTex);
#endif

#ifdef ENABLE_LUT_2D
uniform TEXTURE2D(_LutTex);
SAMPLER(sampler_LutTex);

inline float3 LUT_2D(float3 pixel)
{
  pixel.y = 1.0 - pixel.y;
  pixel.z *= _LutParams.z;

  const float shift = floor(pixel.z);
  pixel.xy = pixel.xy * _LutParams.z * _LutParams.xy + 0.5 * _LutParams.xy;
  pixel.x += shift * _LutParams.y;
  pixel.xyz = lerp(SAMPLE_TEXTURE2D_X(_LutTex, sampler_LutTex, pixel.xy).rgb,
                SAMPLE_TEXTURE2D_X(_LutTex, sampler_LutTex, pixel.xy + float2(_LutParams.y, 0.0)).rgb,
                pixel.z - shift);
  return pixel;
}      
#endif

inline float3 FilterLUT(float3 pixel, float2 uv)
{
#ifdef ENABLE_LUT_3D
  pixel = SAMPLE_TEXTURE3D(_LutTex, sampler_LutTex, (pixel * _LutParams.x) + _LutParams.y).rgb;
#elif ENABLE_LUT_2D
  pixel = LUT_2D(pixel);
#endif
  
  return pixel;
}
