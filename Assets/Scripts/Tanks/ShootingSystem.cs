using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;

namespace Tanks
{
    [UpdateBefore(typeof(TransformSystemGroup))]
    public partial struct ShootingSystem : ISystem
    {
        private float timer;

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            timer -= SystemAPI.Time.DeltaTime;
            if (timer > 0)
            {
                return;
            }

            timer = 0.3f;
            var config = SystemAPI.GetSingleton<Configuration>();
            var ballTransform = state.EntityManager.GetComponentData<LocalTransform>(config.CannonBallPrefab);

            var commandBuffer = new EntityCommandBuffer(Allocator.TempJob);

            foreach (var (tank, transform, color) in SystemAPI
                         .Query<RefRO<Tank>, RefRO<LocalTransform>, RefRO<URPMaterialPropertyBaseColor>>())
            {
                var cannonBallEntity = commandBuffer.Instantiate(config.CannonBallPrefab);
                commandBuffer.SetComponent(cannonBallEntity, color.ValueRO);

                var cannonTransform = state.EntityManager.GetComponentData<LocalToWorld>(tank.ValueRO.Cannon);
                ballTransform.Position = cannonTransform.Position;
                commandBuffer.SetComponent(cannonBallEntity, ballTransform);
                commandBuffer.AddComponent(cannonBallEntity, new CannonBall
                {
                    Velocity = math.normalize(cannonTransform.Up) * 12.0f
                });
            }

            commandBuffer.Playback(state.EntityManager);
            commandBuffer.Dispose();
        }
    }
}