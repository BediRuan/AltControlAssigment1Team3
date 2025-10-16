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

inline float3 FilterLordKevin(float3 pixel, float2 uv)
{
  float2 lookup;
  lookup.y = 0.5;

  float3 final = pixel;

  lookup.x = pixel.r;
  final.r = SAMPLE_TEXTURE2D(_LevelsTex, sampler_LevelsTex, lookup).r;
  lookup.x = pixel.g;
  final.g = SAMPLE_TEXTURE2D(_LevelsTex, sampler_LevelsTex, lookup).g;
  lookup.x = pixel.b;
  final.b = SAMPLE_TEXTURE2D(_LevelsTex, sampler_LevelsTex, lookup).b;
  
  return final;
}
