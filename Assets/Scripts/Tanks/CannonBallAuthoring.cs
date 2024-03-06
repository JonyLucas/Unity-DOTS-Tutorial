using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using UnityEngine;

namespace Tanks
{
    public class CannonBallAuthoring : MonoBehaviour
    {
        private class CannonBallBaker : Baker<CannonBallAuthoring>
        {
            public override void Bake(CannonBallAuthoring authoring)
            {
                var entity = GetEntity(authoring, TransformUsageFlags.Dynamic);
                AddComponent<CannonBall>(entity);
                AddComponent<URPMaterialPropertyBaseColor>(entity);
            }
        }
    }
    
    public struct CannonBall : IComponentData
    {
        public float3 Velocity;
    }
}