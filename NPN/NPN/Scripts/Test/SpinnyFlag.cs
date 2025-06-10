using Stride.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;

namespace NPN.Scripts.Test
{
    public class SpinnyFlag : SyncScript
    {
        public override void Update()
        {
            var incrementalRotation = Quaternion.RotationYawPitchRoll(0, 0.01f, 0);
            Entity.Transform.Rotation = incrementalRotation * Entity.Transform.Rotation;
        }
    }
}
