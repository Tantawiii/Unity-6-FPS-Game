using UnityEngine;

public class WeaponPickUp : Pickup
{
    [SerializeField] WeaponSO weaponSO;
    // void OnTriggerEnter(Collider other)
    // {
    //     if(other.CompareTag(PLAYER_STRING)){
    //         ActiveWeapon activeWeapon = other.GetComponentInChildren<ActiveWeapon>();
    //         activeWeapon.SwitchWeapon(weaponSO);
    //         Destroy(this.gameObject);
    //     }
    // }
    protected override void OnPickUp(ActiveWeapon activeWeapon){
        activeWeapon.SwitchWeapon(weaponSO);
    }
}
