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

float _CrossProcessColorShift;
float _CrossProcessContrastBoost;

inline float3 FilterCrossProcess(float3 pixel, float2 uv)
{
  float3 shifted = pixel;
  
  float luma = dot(pixel, float3(0.299, 0.587, 0.114));
  
  if (luma < 0.5)
  {
    shifted.r *= (1.0 - _CrossProcessColorShift * 0.2);
    shifted.g *= (1.0 + _CrossProcessColorShift * 0.1);
    shifted.b *= (1.0 + _CrossProcessColorShift * 0.3);
  }
  else
  {
    shifted.r *= (1.0 + _CrossProcessColorShift * 0.2);
    shifted.g *= (1.0 + _CrossProcessColorShift * 0.1);
    shifted.b *= (1.0 - _CrossProcessColorShift * 0.1);
  }
  
  shifted = (shifted - 0.5) * (1.0 + _CrossProcessContrastBoost) + 0.5;
  
  float gray = dot(shifted, float3(0.299, 0.587, 0.114));
  shifted = lerp(gray.xxx, shifted, 1.0 + _CrossProcessColorShift * 0.3);
  
  shifted.r = pow(shifted.r, 0.95);
  shifted.g = pow(shifted.g, 1.0);
  shifted.b = pow(shifted.b, 1.05);
  
  return clamp(shifted, 0.0, 1.0);
}
