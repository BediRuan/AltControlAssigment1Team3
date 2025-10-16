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

inline float3 FilterLevels(float3 pixel, float2 uv)
{
  pixel.r = SAMPLE_TEXTURE2D(_LevelsTex, sampler_LevelsTex, float2(pixel.r, 1.0 - 0.16666)).r;
  pixel.g = SAMPLE_TEXTURE2D(_LevelsTex, sampler_LevelsTex, float2(pixel.g, 0.5)).g;
  pixel.b = SAMPLE_TEXTURE2D(_LevelsTex, sampler_LevelsTex, float2(pixel.b, 1.0 - 0.83333)).b;

  return pixel;
}