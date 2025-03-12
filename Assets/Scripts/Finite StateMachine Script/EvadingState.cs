using UnityEngine;

public class EvadingState : IEnemyState
{
    private EvadePlayer enemy;

    public EvadingState(EvadePlayer enemyAI)
    {
        enemy = enemyAI;
    }

    public void EnterState()
    {
        Debug.Log("🏃 Entering Evading State");
        enemy.PlayAnimation("Running"); // ✅ Trigger Running Animation
    }

    public void UpdateState()
    {
        float distanceToPlayer = Vector3.Distance(enemy.transform.position, enemy.player.position);

        if (distanceToPlayer > enemy.evadeDistance)
        {
            enemy.SetState(new ShootingState(enemy));
        }
        else
        {
            enemy.MoveAwayFromPlayer();
        }
    }

    public void ExitState()
    {
        Debug.Log("🛑 Exiting Evading State");
    }
}
