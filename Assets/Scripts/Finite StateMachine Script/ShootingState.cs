using UnityEngine;

public class ShootingState : IEnemyState
{
    private EvadePlayer enemy;

    public ShootingState(EvadePlayer enemyAI)
    {
        enemy = enemyAI;
    }

    public void EnterState()
    {
        Debug.Log("🔫 Entering Shooting State");
        enemy.StopMoving();
        enemy.PlayAnimation("Shooting"); // ✅ Trigger Shooting Animation
    }

    public void UpdateState()
    {
        float distanceToPlayer = Vector3.Distance(enemy.transform.position, enemy.player.position);

        if (distanceToPlayer < enemy.evadeDistance)
        {
            enemy.SetState(new EvadingState(enemy));
        }
        else
        {
            RotateTowardsPlayer();
            enemy.FireBullet();
        }
    }

    public void ExitState()
    {
        Debug.Log("🚀 Exiting Shooting State");
    }

    private void RotateTowardsPlayer()
    {
        Vector3 directionToPlayer = (enemy.player.position - enemy.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z)); 
        enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
