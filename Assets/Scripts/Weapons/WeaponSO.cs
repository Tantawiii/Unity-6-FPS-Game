using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSO", menuName = "Scriptable Objects/WeaponSO", order = -300)]
public class WeaponSO : ScriptableObject {
    public GameObject weaponPrefab;
    public int weaponSlot = 0;
    public int Damage = 1;
    public float FireRate = .5f;
    public float WeaponRange = Mathf.Infinity;
    public int WeaponAmmo = 0;
    public GameObject hitVFX;
    public bool IsAutomatic = false;
    public bool CanZoom = false;
    public float ZoomAmount = 10f;
    public float ZoomRotationSpeed = .3f;
}