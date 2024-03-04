using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class RotationSpeedAuthoring : MonoBehaviour
{
    public float DegreesPerSecond = 360.0f;
    private class RotationSpeedBaker : Baker<RotationSpeedAuthoring>
    {
        public override void Bake(RotationSpeedAuthoring authoring)
        {
            var entity = GetEntity(authoring, TransformUsageFlags.Dynamic);
            var rotationSpeed = new RotationSpeed { RadiansPerSecond = math.radians(authoring.DegreesPerSecond) };
            AddComponent(entity, rotationSpeed);
        }
    }
}