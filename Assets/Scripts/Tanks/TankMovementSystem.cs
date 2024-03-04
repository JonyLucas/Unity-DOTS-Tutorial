using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Tanks
{
    public partial struct TankMovementSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;

            foreach (var (transform, entity) in SystemAPI.Query<RefRW<LocalTransform>>()
                         .WithAll<Tank>()
                         .WithNone<PlayerComponent>()
                         .WithEntityAccess())
            {
                var position = transform.ValueRO.Position;
                position.y = (float)entity.Index;

                var angle = (0.5f + noise.cnoise(position / 10)) * math.PI * 4;
                var direction = float3.zero;
                math.sincos(angle, out direction.x, out direction.z);

                transform.ValueRW.Position += direction * deltaTime * 5;
                transform.ValueRW.Rotation = quaternion.RotateY(angle);
            }
        }
    }
}