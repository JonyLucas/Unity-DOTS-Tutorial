using Unity.Entities;
using UnityEngine;

public class TankAuthoring : MonoBehaviour
{
    [SerializeField] private GameObject turret;
    [SerializeField] private GameObject cannonBall;

    class Baker : Baker<TankAuthoring>
    {
        public override void Bake(TankAuthoring authoring)
        {
            var entity = GetEntity(authoring, TransformUsageFlags.Dynamic);
            AddComponent(entity, new Tank
            {
                Turret = GetEntity(authoring.turret, TransformUsageFlags.Dynamic),
                CannonBall = GetEntity(authoring.cannonBall, TransformUsageFlags.Dynamic)
            });
        }
    }
}

public struct Tank : IComponentData
{
    public Entity Turret;
    public Entity CannonBall;
}