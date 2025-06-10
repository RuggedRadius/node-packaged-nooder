using Stride.Animations;
using Stride.Engine;
using Stride.Input;

namespace NPN.Scripts.Zombies
{


  public class ZombieController : SyncScript
  {
    public float Speed = 1.0f;

    private AnimationComponent _animations;

    private PlayingAnimation _currentAnimation;

    public int Health { get; set; } = 100;
    public bool GamePad { get; set; } = true;

    public override void Start()
    {
      _animations = Entity.Get<AnimationComponent>();

      _currentAnimation = _animations.Play("Idle");
    }

    public override void Update()
    {
      if (GamePad && Input.HasGamePad)
      {
        GamePadState gp = Input.DefaultGamePad.State;

        if (gp.LeftTrigger > 0.5)
        {
          Health = 0;
        }

        if (Health <= 0)
        {
          if(!_animations.IsPlaying("Dying"))
            _currentAnimation = _animations.Play("Dying");
        }
      }
    }
  }
}