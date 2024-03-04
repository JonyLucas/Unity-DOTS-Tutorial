using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public partial struct CubeRotationSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        // float radians = 2 * SystemAPI.Time.DeltaTime;
        // foreach (var transform in SystemAPI.Query<RefRW<LocalToWorld>>())
        // {
        //     var matrix = transform.ValueRW.Value; // Get the matrix from the component
        //     var rotationMatrix = float4x4.RotateY(radians); // Create a rotation matrix from the quaternion
        //     matrix = math.mul(matrix, rotationMatrix); // Apply the rotation to the existing matrix
        //     transform.ValueRW.Value = matrix;
        // }
        
        var deltaTime = SystemAPI.Time.DeltaTime;

        foreach (var (transform, rotationSpeed) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<RotationSpeed>>())
        {
            var radians = rotationSpeed.ValueRO.RadiansPerSecond * deltaTime;
            transform.ValueRW = transform.ValueRW.RotateY(radians);
        }
    }
}
