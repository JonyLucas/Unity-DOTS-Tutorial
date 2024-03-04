using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;

namespace Tanks
{
    public partial struct TankSpawnSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            // This RequireForUpdate means the system only updates if at 
            // least one entity with the Config component exists.
            // Effectively, this system will not update until the 
            // subscene with the Config has been loaded.
            state.RequireForUpdate<Configuration>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            state.Enabled = false;
            var config = SystemAPI.GetSingleton<Configuration>();
            var random = new Random(123);
            
            for (int i = 0; i < config.TankCount; i++)
            {
                var tankEntity = state.EntityManager.Instantiate(config.TankPrefab);
                var color = new URPMaterialPropertyBaseColor { Value = RandomColor(ref random) };
                var position = random.NextFloat3(new float3(10, 10, 10));
                
                if (i == 0)
                {
                    state.EntityManager.AddComponent<PlayerComponent>(tankEntity);
                }
                
                // Every root entity instantiated from a prefab has a LinkedEntityGroup component, which
                // is a list of all the entities that make up the prefab hierarchy (including the root).
                // (LinkedEntityGroup is a special kind of component called a "DynamicBuffer", which is
                // a resizable array of struct values instead of just a single struct.)
                var linkedEntities = state.EntityManager.GetBuffer<LinkedEntityGroup>(tankEntity);

                foreach (var entity in linkedEntities)
                {
                    if (state.EntityManager.HasComponent<URPMaterialPropertyBaseColor>(entity.Value))
                    {
                        state.EntityManager.SetComponentData(entity.Value, color);
                    }
                }
                
                state.EntityManager.SetComponentData(tankEntity, new LocalTransform
                {
                    Position = position,
                    Rotation = quaternion.identity,
                    Scale = 1
                });
            }
            
        }
        
        static float4 RandomColor(ref Random random)
        {
            return new float4(random.NextFloat(), random.NextFloat(), random.NextFloat(), 1);
        }
    }
}