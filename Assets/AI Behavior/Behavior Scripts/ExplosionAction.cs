using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ExplosionAI", story: "Update [Explode] Range and assign [Target]", category: "Action", id: "056c4f451eeadd4d0181db0d2802cd0e")]
public partial class ExplosionAction : Action
{
    [SerializeReference] public BlackboardVariable<ExplosionAI> Explode;
    [SerializeReference] public BlackboardVariable<GameObject> Target;

    // protected override Status OnStart()
    // {
    //     return Status.Running;
    // }

    protected override Status OnUpdate()
    {
        if (Explode.Value == null)
        {
            Debug.LogError("ExplosionAction: No explosion object assigned!");
            return Status.Failure;
        }

        // Perform explosion if the player is in range
        Collider[] hitColliders = Physics.OverlapSphere(Explode.Value.transform.position, Explode.Value.GetRadius());
        foreach (var hitCollider in hitColliders)
        {
            PlayerHealth playerHealth = hitCollider.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                Explode.Value.Explosive();
                return Status.Success;
            }
        }
        
        return Status.Failure;
    }


}

