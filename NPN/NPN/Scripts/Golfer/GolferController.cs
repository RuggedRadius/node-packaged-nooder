using Stride.Animations;
using Stride.Engine;

namespace NPN.Scripts.Golfer
{
  public class GolferController : SyncScript
  {
    public float Speed = 1.0f;

    private AnimationComponent _animations;

    private PlayingAnimation _currentAnimation;

    public override void Start()
    {
      _animations = Entity.Get<AnimationComponent>();

      _currentAnimation = _animations.Play("Drive");
    }

    public override void Update()
    {

    }
  }
}