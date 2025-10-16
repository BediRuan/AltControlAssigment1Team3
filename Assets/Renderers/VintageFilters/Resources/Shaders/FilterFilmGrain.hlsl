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

float _FilmGrainIntensity;
float _FilmGrainSize;

inline float3 FilterFilmGrain(float3 pixel, float2 uv)
{
  float2 grainUV = uv * _ScreenParams.xy * _FilmGrainSize * 0.001;
  
  float grain = 0.0;
  grain += snoise(grainUV) * 0.5;
  grain += snoise(grainUV * 2.0) * 0.25;
  grain += snoise(grainUV * 4.0) * 0.125;
  
  grain = grain * 0.5 + 0.5;
  
  float luma = dot(pixel, float3(0.299, 0.587, 0.114));
  float grainMask = 1.0 - abs(luma - 0.5) * 2.0; // Peak at 0.5 luminance
  grainMask = smoothstep(0.0, 0.8, grainMask);
  
  float grainEffect = (grain - 0.5) * _FilmGrainIntensity * grainMask * 0.1;
  pixel += grainEffect;
  
  pixel.r += grainEffect * 0.1;
  pixel.g += grainEffect * 0.05;
  pixel.b += grainEffect * 0.15;
  
  return clamp(pixel, 0.0, 1.0);
}
