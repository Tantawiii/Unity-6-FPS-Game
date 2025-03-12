using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Bullet", story: "Update [Self] attack [Target] with [ShotBullet] from [BulletSpawnLocation]", category: "Action", id: "670339fbb021490525284e3a60d40b09")]
public partial class BulletAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<GameObject> ShotBullet;
    [SerializeReference] public BlackboardVariable<GameObject> BulletSpawnLocation;

    protected override Status OnUpdate()
    {
        if (Target.Value == null || ShotBullet.Value == null || BulletSpawnLocation.Value == null)
        {
            Debug.LogError("❌ BulletAction: One of the required variables is NULL!");
            return Status.Failure;
        }
        Self.Value.transform.LookAt(Target.Value.transform.position);

        // Instantiate bullet at the spawn location
        Bullet bulletObj = GameObject.Instantiate(ShotBullet.Value, BulletSpawnLocation.Value.transform.position, Quaternion.identity).GetComponent<Bullet>();
        bulletObj.transform.LookAt(Target.Value.transform);

        if (bulletObj == null)
        {
            Debug.LogError("❌ BulletAction: Instantiation failed!");
            return Status.Failure;
        }

        // Ensure bullet moves toward the target
        Rigidbody rb = bulletObj.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.linearVelocity = bulletObj.transform.forward * 15f; // ✅ Bullet moves forward
        }

        // Initialize damage if the bullet has a script
        // Bullet bullet = bulletObj.GetComponent<Bullet>();
        if (bulletObj != null)
        {
            bulletObj.Initalize(5); // Set damage
        }

        return Status.Success;
    }
}
