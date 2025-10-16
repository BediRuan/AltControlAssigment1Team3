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

float _PolaroidOverexposure;
float _PolaroidSoftness;

inline float3 FilterPolaroid(float3 pixel, float2 uv)
{
  float3 overexposed = pixel + _PolaroidOverexposure * 0.2;
  
  overexposed.r += 0.05;
  overexposed.g += 0.02;
  overexposed.b -= 0.03;
  
  float2 center = uv - 0.5;
  float vignette = 1.0 - dot(center, center) * 0.8;
  vignette = smoothstep(0.3, 1.0, vignette);
  
  float3 softened = overexposed;
  float softnessFactor = _PolaroidSoftness * 0.1;
  
  float luma = dot(overexposed, float3(0.299, 0.587, 0.114));
  if (luma > 0.5)
    softened = lerp(overexposed, luma.xxx, softnessFactor);
  
  softened *= vignette;
  
  float gray = dot(softened, float3(0.299, 0.587, 0.114));
  softened = lerp(gray.xxx, softened, 0.9);
  
  return clamp(softened, 0.0, 1.0);
}
