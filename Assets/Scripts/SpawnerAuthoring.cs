using Unity.Entities;
using UnityEngine;

public class SpawnerAuthoring : MonoBehaviour
{
    public GameObject prefab;
    private class SpawnerBaker : Baker<SpawnerAuthoring>
    {
        public override void Bake(SpawnerAuthoring authoring)
        {
            var entity = GetEntity(authoring, TransformUsageFlags.None); // For invisible object, that won't move, you can use TransformUsageFlags.None
            var spawner = new Spawner
            {
                Prefab = GetEntity(authoring.prefab, TransformUsageFlags.Dynamic)
            };
            AddComponent(entity, spawner);
        }
    }
}

struct Spawner : IComponentData
{
    public Entity Prefab;
}