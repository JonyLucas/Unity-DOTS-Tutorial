using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Tanks
{
    public partial struct CannonBallSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
            var cannonJob = new CannonBallJob
            {
                ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged),
                DeltaTime = SystemAPI.Time.DeltaTime
            };

            cannonJob.Schedule();
        }
    }

    [BurstCompile]
    public partial struct CannonBallJob : IJobEntity
    {
        public EntityCommandBuffer ECB;
        public float DeltaTime;

        public void Execute(Entity entity, ref CannonBall cannonBall, ref LocalTransform transform)
        {
            var gravity = new float3(0f, -10f, 0);
            transform.Position += cannonBall.Velocity * DeltaTime;

            if (transform.Position.y < 0)
            {
                ECB.DestroyEntity(entity);
            }
            else
            {
                cannonBall.Velocity += gravity * DeltaTime;
            }
        }
    }
}