using System.Collections;
using UnityEngine;

public class SpawnGate : MonoBehaviour
{
    [SerializeField] GameObject robotPrefab;
    [SerializeField] float spawnTime = 50f;
    [SerializeField] Transform spawnPosition;
    ActiveWeapon activeWeapon;
    PlayerHealth playerHealth;
    void Awake()
    {
        activeWeapon = FindFirstObjectByType<ActiveWeapon>();
        playerHealth = FindFirstObjectByType<PlayerHealth>();
    }
    
    void Start() {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine(){
        while (activeWeapon.GetWeapons().Count == 0) // Keep checking until the player picks up a weapon
        {
            yield return null;
        }
        while(playerHealth){
            Instantiate(robotPrefab, spawnPosition.position, transform.rotation);
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
