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

const float4x4 saturateMatrix = float4x4(1.1402,  -0.0598,  -0.061,  0.0,
                                        -0.1174,   1.0826,  -0.1186, 0.0,
                                        -0.0228,  -0.0228,   1.1772, 0.0,
                                         0.0,      0.0,      0.0,    0.0);

inline float3 FilterValencia(float3 pixel, float2 uv)
{
  float3 final = pixel;

  final.r = SAMPLE_TEXTURE2D(_LevelsTex, sampler_LevelsTex, float2(pixel.r, 1.0 - 0.16666)).r;
  final.g = SAMPLE_TEXTURE2D(_LevelsTex, sampler_LevelsTex, float2(pixel.g, 0.5)).g;
  final.b = SAMPLE_TEXTURE2D(_LevelsTex, sampler_LevelsTex, float2(pixel.b, 1.0 - 0.83333)).b;

  final = mul((float3x3)saturateMatrix, final);

  float avg = 1.0 - (final.r + final.g + final.b) * 0.33;

  final.r = SAMPLE_TEXTURE2D(_GradientTex, sampler_GradientTex, float2(final.r, avg)).r;
  final.g = SAMPLE_TEXTURE2D(_GradientTex, sampler_GradientTex, float2(final.g, avg)).g;
  final.b = SAMPLE_TEXTURE2D(_GradientTex, sampler_GradientTex, float2(final.b, avg)).b;

  return final;
}
