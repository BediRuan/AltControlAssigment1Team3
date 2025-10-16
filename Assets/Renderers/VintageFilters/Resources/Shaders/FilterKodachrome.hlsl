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

float _KodachromeEnhancement;
float _KodachromeWarmth;

inline float3 FilterKodachrome(float3 pixel, float2 uv)
{
  float3 enhanced = pixel;
  
  enhanced.r = pow(enhanced.r, 0.9) * (1.0 + _KodachromeEnhancement * 0.3);
  
  float warmth = _KodachromeWarmth * 0.1;
  enhanced.r += warmth;
  enhanced.g += warmth * 0.5;
  enhanced.b -= warmth * 0.2;
  
  float luma = dot(enhanced, float3(0.299, 0.587, 0.114));
  enhanced = lerp(luma.xxx, enhanced, 1.0 + _KodachromeEnhancement * 0.2);
  
  enhanced = (enhanced - 0.5) * (1.0 + _KodachromeEnhancement * 0.1) + 0.5;
  
  return clamp(enhanced, 0.0, 1.0);
}
