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

inline float3 FilterSutro(float3 pixel, float2 uv)
{
  float3 final = pixel;

  const float3 rgbPrime = float3(0.1019, 0.0, 0.0);
  const float m = dot(float3(0.3, 0.59, 0.11), final.rgb) - 0.03058;
  final = lerp(final, rgbPrime + m, 0.32);
    
  final *= SAMPLE_TEXTURE2D(_EdgeBurnTex, sampler_EdgeBurnTex, uv).rgb;

  final.r = SAMPLE_TEXTURE2D(_CurvesTex, sampler_CurvesTex, float2(final.r, 1.0 - 0.16666)).r;
  final.g = SAMPLE_TEXTURE2D(_CurvesTex, sampler_CurvesTex, float2(final.g, 0.5)).g;
  final.b = SAMPLE_TEXTURE2D(_CurvesTex, sampler_CurvesTex, float2(final.b, 1.0 - 0.83333)).b;
  
  return final;
}
