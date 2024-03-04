using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Tanks
{
    public partial struct PlayerSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var movement = new float3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            movement *= SystemAPI.Time.DeltaTime;
            
            foreach (var transform in SystemAPI.Query<RefRW<LocalTransform>>()
                         .WithAll<PlayerComponent>())
            {
                transform.ValueRW.Position += movement * 5;
                transform.ValueRW.Rotation = quaternion.LookRotation(movement, math.up());
            }
        }
    }
}