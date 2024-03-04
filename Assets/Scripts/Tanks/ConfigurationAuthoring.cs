using Unity.Entities;
using UnityEngine;

namespace Tanks
{
    public class ConfigurationAuthoring : MonoBehaviour
    {
        public GameObject TankPrefab;
        public GameObject CannonBallPrefab;
        public int TankCount = 10;
        
        private class ConfigurationBaker : Baker<ConfigurationAuthoring>
        {
            public override void Bake(ConfigurationAuthoring authoring)
            {
                var entity = GetEntity(authoring, TransformUsageFlags.None);
                AddComponent(entity, new Configuration
                {
                    TankPrefab = GetEntity(authoring.TankPrefab, TransformUsageFlags.Dynamic),
                    CannonBallPrefab = GetEntity(authoring.CannonBallPrefab, TransformUsageFlags.Dynamic),
                    TankCount = authoring.TankCount
                });
            }
        }
    }
    
    public struct Configuration : IComponentData
    {
        public Entity TankPrefab;
        public Entity CannonBallPrefab;
        public int TankCount;
    }
}