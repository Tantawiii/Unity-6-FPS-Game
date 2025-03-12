using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform turretHead;
    [SerializeField] Transform playerTargetted;
    [SerializeField] Transform bulletSpawnLocation;
    [SerializeField] float fireRate = 2f;
    PlayerHealth playerHealth;
    ActiveWeapon activeWeapon;

    void Start()
    {
        playerHealth = FindFirstObjectByType<PlayerHealth>();
        activeWeapon = FindFirstObjectByType<ActiveWeapon>();
        StartCoroutine(FireRoutine());
    }

    void Update()
    {
        turretHead.LookAt(playerTargetted);
    }

    IEnumerator FireRoutine(){
        while (activeWeapon.GetWeapons().Count == 0) // Keep checking until the player picks up a weapon
        {
            yield return null;
        }
        while(playerHealth){
            yield return new WaitForSeconds(fireRate);
            Instantiate(bulletPrefab, bulletSpawnLocation.position, turretHead.rotation);
        }
    }
}
