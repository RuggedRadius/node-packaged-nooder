using Stride.Animations;
using Stride.Engine;

namespace NPN.Scripts.Zombies
{
  public class ZombieController : SyncScript
  {
    public float Speed = 1.0f;

    private AnimationComponent _animations;

    private PlayingAnimation _currentAnimation;

    public override void Start()
    {
      _animations = Entity.Get<AnimationComponent>();

      _currentAnimation = _animations.Play("Walk");
    }

    public override void Update()
    {

    }
  }
}