using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform turretHead;
    [SerializeField] Transform playerTargetted;
    [SerializeField] Transform bulletSpawnLocation;
    [SerializeField] float fireRate = 2f;
    [SerializeField] int damage = 2;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    ActiveWeapon activeWeapon;

    void Start()
    {
        playerHealth = FindFirstObjectByType<PlayerHealth>();
        enemyHealth = GetComponentInParent<EnemyHealth>();
        activeWeapon = FindFirstObjectByType<ActiveWeapon>();
        StartCoroutine(FireRoutine());
    }

    void Update()
    {
        if(enemyHealth.GetEnemyHealth() != 0)
            turretHead.LookAt(playerTargetted);
    }

    IEnumerator FireRoutine(){
        while (activeWeapon.GetWeapons().Count == 0) // Keep checking until the player picks up a weapon
        {
            yield return null;
        }
        while(playerHealth && enemyHealth.GetEnemyHealth() != 0){
            yield return new WaitForSeconds(fireRate);
            Bullet bullet = Instantiate(bulletPrefab, bulletSpawnLocation.position, Quaternion.identity).GetComponent<Bullet>();
            bullet.transform.LookAt(playerTargetted);
            bullet.Initalize(damage);
        }
    }
}
