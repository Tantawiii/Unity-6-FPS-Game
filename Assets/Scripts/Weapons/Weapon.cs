using Cinemachine;
using UnityEngine;

public class Weapon : MonoBehaviour
{   
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] LayerMask ineteractionLayers;
    CinemachineImpulseSource cinemachineImpulseSource;
    void Awake()
    {
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }
    public void Shoot(WeaponSO weaponSO){
        muzzleFlash.Play();
        cinemachineImpulseSource.GenerateImpulse();
        RaycastHit hit;
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, weaponSO.WeaponRange, ineteractionLayers, QueryTriggerInteraction.Ignore)){
            Instantiate(weaponSO.hitVFX, hit.point, Quaternion.identity);
            EnemyHealth enemyHealth = hit.collider.GetComponentInParent<EnemyHealth>();
            enemyHealth?.TakeDamage(weaponSO.Damage);
        }
    }
}
