using StarterAssets;
using UnityEngine;
using UnityEngine.AI;

public class Robot : MonoBehaviour
{
    FirstPersonController player;
    NavMeshAgent agent;
    EnemyHealth enemyHealth;
    const string PLAYER_STRING = "Player";
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyHealth = GetComponent<EnemyHealth>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindFirstObjectByType<FirstPersonController>();
        if (player == null)
        {
            Debug.LogError("Robot: No FirstPersonController found in the scene!", this);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HuntPlayer();
    }

    private void HuntPlayer()
    {
        if (player != null && enemyHealth.GetEnemyHealth() > 0)
        {
            agent.SetDestination(player.transform.position);
        }
        else{
            agent.isStopped = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(PLAYER_STRING)){
            enemyHealth.SelfDestruct();
        }
    }
}
