using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public partial struct SpawnSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<Spawner>(); // Only runs the update when there are entities with the Spawner component
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        state.Enabled = false; // Prevents the system from running every frame
        var prefab =
            SystemAPI.GetSingleton<Spawner>()
                .Prefab; // Get the prefab entity from the Spawner component, it'll return an exception if the component is not found or more than one entity has the component
        var instances =
            state.EntityManager.Instantiate(prefab, 10, Allocator.Temp); // Instantiate 10 entities from the prefab

        var random = new Random(123);
        foreach (var entity in instances)
        {
            var transform = SystemAPI.GetComponentRW<LocalTransform>(entity);
            transform.ValueRW.Position = random.NextFloat3(new float3(10, 10, 10));
        }
    }
}