using UnityEngine;
using UnityEngine.AI;

public class EvadePlayer : MonoBehaviour
{
    public Transform player;
    public float evadeDistance = 5f;
    public float shootingRange = 12f;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float fireRate = 2f;
    public float moveSpeed = 3f;

    private NavMeshAgent agent;
    private IEnemyState currentState;
    private float nextFireTime;
    private Animator animator; // ✅ Add Animator

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>(); // ✅ Get Animator component
        SetState(new ShootingState(this)); // Start with shooting
    }

    private void Update()
    {
        currentState?.UpdateState();
    }

    public void SetState(IEnemyState newState)
    {
        currentState?.ExitState();
        currentState = newState;
        currentState.EnterState();
    }

    public void FireBullet()
    {
        if (Time.time >= nextFireTime)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            bullet.transform.LookAt(player.position);
            bullet.GetComponent<Rigidbody>().linearVelocity = bullet.transform.forward * 15f;
            nextFireTime = Time.time + fireRate;
        }
    }

    public void MoveAwayFromPlayer()
    {
        Vector3 directionAway = transform.position - player.position;
        Vector3 newPos = transform.position + directionAway.normalized * moveSpeed;
        agent.SetDestination(newPos);
    }

    public void StopMoving()
    {
        agent.SetDestination(transform.position);
    }

    public void PlayAnimation(string animParam)
    {
        animator.ResetTrigger("Shooting");
        animator.ResetTrigger("Running");
        animator.SetTrigger(animParam);
    }
}
