using Stride.Animations;
using Stride.Core.Mathematics;
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

    private CharacterComponent character;

    public override void Start()
    {
      base.Start();

      character = Entity.Get<CharacterComponent>();

      _animations = Entity.Get<AnimationComponent>();

      _currentAnimation = _animations.Play("Drive");
    }

    public override void Update()
    {
      if (character == null)
        return;

      float dt = (float)Game.UpdateTime.Elapsed.TotalSeconds;

      if (GamePad && Input.HasGamePad)
      {
        GamePadState gp = Input.DefaultGamePad.State;
        
        Vector3 forward = Entity.Transform.WorldMatrix.Forward;
        Vector3 right = Entity.Transform.WorldMatrix.Right;

        Vector3 moveDirection = (forward * gp.LeftThumb.Y + right * gp.LeftThumb.X);
        moveDirection.Y = 0;
        moveDirection.Normalize();

        float moveSpeed = 5f;
        Vector3 finalMovement = moveDirection * moveSpeed;

        character.SetVelocity(finalMovement);

        if (!_animations.IsPlaying("Walking"))
          _currentAnimation = _animations.Play("Walking");

        if (gp.RightTrigger > 0.5)
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