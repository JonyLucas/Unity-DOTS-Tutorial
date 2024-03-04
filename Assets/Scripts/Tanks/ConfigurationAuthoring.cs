using Unity.Entities;
using UnityEngine;

namespace Tanks
{
    public class ConfigurationAuthoring : MonoBehaviour
    {
        [SerializeField] private GameObject tankPrefab;
        [SerializeField] private GameObject cannonBallPrefab;
        [SerializeField] private int tankCount = 10;
        
        
        private class ConfigurationBaker : Baker<ConfigurationAuthoring>
        {
            public override void Bake(ConfigurationAuthoring authoring)
            {
                var entity = GetEntity(authoring, TransformUsageFlags.None);
                AddComponent(entity, new Configuration
                {
                    TankPrefab = GetEntity(authoring.tankPrefab, TransformUsageFlags.Dynamic),
                    CannonBallPrefab = GetEntity(authoring.cannonBallPrefab, TransformUsageFlags.Dynamic),
                    TankCount = authoring.tankCount
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