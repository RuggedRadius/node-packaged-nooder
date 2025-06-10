using Stride.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPN.Scripts.Test
{
    public class BallController : SyncScript
    {
        public override void Update()
        {
            if (Input.IsKeyDown(Stride.Input.Keys.W))
            {
                Entity.Transform.Position.Z += 0.1f;
            }
            if (Input.IsKeyDown(Stride.Input.Keys.S))
            {
                Entity.Transform.Position.Z -= 0.1f;
            }
            if (Input.IsKeyDown(Stride.Input.Keys.A))
            {
                Entity.Transform.Position.X -= 0.1f;
            }
            if (Input.IsKeyDown(Stride.Input.Keys.D))
            {
                Entity.Transform.Position.X += 0.1f;
            }
        }
    }
}
