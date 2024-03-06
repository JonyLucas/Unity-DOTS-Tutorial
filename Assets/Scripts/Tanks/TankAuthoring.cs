using Unity.Entities;
using UnityEngine;

/*
 * TODO  Here are some gameplay tweaks you might implement:

    Give the player direct control of their turret.
    Destroy tanks when they are hit by a cannonball.
    Respawn destroyed tanks after a timer.
    Display a victory message when the player destroys all enemy tanks.
    Respawn the player but give them a limited number of lives.
 */

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
                Cannon = GetEntity(authoring.cannonBall, TransformUsageFlags.Dynamic)
            });
        }
    }
}

public struct Tank : IComponentData
{
    public Entity Turret;
    public Entity Cannon;
}