using StarterAssets;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class RobotChaserBehavior : MonoBehaviour
{
    FirstPersonController player;
    EnemyHealth enemyHealth;
    const string PLAYER_STRING = "Player";
    bool isExploding = false; // Prevent multiple explosions

    void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();
    }

    void Start()
    {
        player = FindFirstObjectByType<FirstPersonController>();
        if (player == null)
        {
            Debug.LogError("Robot: No FirstPersonController found in the scene!", this);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER_STRING) && !isExploding)
        {
            isExploding = true;
            StartCoroutine(ExplodeAfterDelay());
        }
    }

    IEnumerator ExplodeAfterDelay()
    {
        yield return new WaitForSeconds(2f); // Wait 2 seconds before exploding

        ExplosionAI explosion = GetComponent<ExplosionAI>();
        if (explosion != null)
        {
            explosion.Explosive();
        }

        Destroy(gameObject); // Destroy robot after explosion
    }
}
