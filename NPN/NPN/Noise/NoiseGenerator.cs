using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPN.Noise
{
  public class NoiseGenerator
  {
    private FastNoiseLite _noise;

    public NoiseGenerator(NoiseSettings settings)
    {
      _noise = new FastNoiseLite();
      _noise.SetNoiseType(settings.NoiseType);
      _noise.SetFrequency(settings.Frequency);
      _noise.SetSeed(settings.Seed);
    }

    public float GetNoise(float x, float y)
    {
      return _noise.GetNoise(x, y);
    }

    public float GetNoise(float x, float y, float z)
    {
      return _noise.GetNoise(x, y, z);
    }
  }
}
