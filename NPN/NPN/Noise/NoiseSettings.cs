using Stride.Core;

namespace NPN.Noise
{
  public class NoiseSettings
  {
    [DataMember]
    public FastNoiseLite.NoiseType NoiseType { get; set; } = FastNoiseLite.NoiseType.OpenSimplex2;
    [DataMember]
    public float Frequency { get; set; } = 0.01f;
    [DataMember]
    public int Seed { get; set; } = 1337;
  }
}