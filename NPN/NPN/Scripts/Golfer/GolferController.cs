using Stride.Animations;
using Stride.Engine;
using Stride.Input;
using Stride.Physics;
using System.Linq;

namespace NPN.Scripts.Golfer
{
  public class GolferController : SyncScript
  {
    public float Speed = 1.0f;

    public float HitForce = 150f;

    private AnimationComponent _animations;

    private PlayingAnimation _currentAnimation;

    public Prefab golfBallPrefab;
    public Entity golfBallReset;

    public bool GamePad { get; set; } = true;

    public override void Start()
    {
      _animations = Entity.Get<AnimationComponent>();

      _currentAnimation = _animations.Play("Drive");
    }

    public override void Update()
    {
      if(GamePad && Input.HasGamePad)
      {
        GamePadState gp = Input.DefaultGamePad.State;

        if(gp.RightTrigger > 0.5)
        {
          DriveBall();
        }
      }
    }

    private void DriveBall()
    {
      var newSphere = golfBallPrefab.Instantiate().First();

      newSphere.Transform.Position = golfBallReset.Transform.Position;
      newSphere.Transform.Rotation = golfBallReset.Transform.Rotation;

      Entity.Scene.Entities.Add(newSphere);

      var rigidBody = newSphere.Get<RigidbodyComponent>();
      var direction = golfBallReset.Transform.WorldMatrix.Forward;

      rigidBody.LinearVelocity = direction * HitForce;
    }
  }
}