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
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerComponent>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var cameraTransform = Camera.main.transform;
            var movement = new float3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            movement *= SystemAPI.Time.DeltaTime;

            foreach (var transform in SystemAPI.Query<RefRW<LocalTransform>>()
                         .WithAll<PlayerComponent>())
            {
                transform.ValueRW.Position += movement * 5;
                transform.ValueRW.Rotation = quaternion.LookRotation(movement, math.up());

                // Move the camera with the player
                Vector3 position = transform.ValueRO.Position;
                position -= 10.0f * (Vector3)transform.ValueRO.Forward();
                position.y += 5.0f;
                // cameraTransform.position = position;
                // cameraTransform.LookAt(transform.ValueRO.Position);
            }
        }
    }
}