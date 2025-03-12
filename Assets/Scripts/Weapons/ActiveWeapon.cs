using System.Collections.Generic;
using Cinemachine;
using StarterAssets;
using TMPro;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] WeaponSO weaponSO;
    [SerializeField] CinemachineVirtualCamera playerFollowCamera;
    [SerializeField] Camera weaponCam;
    [SerializeField] GameObject zoomImage;
    [SerializeField] GameObject ammoPanel;
    [SerializeField] TextMeshProUGUI ammoText;
    StarterAssetsInputs starterAssetsInputs;
    FirstPersonController firstPersonController;
    Animator animator;
    Dictionary<int, (Weapon weapon, WeaponSO weaponSO)> weapons = new Dictionary<int, (Weapon, WeaponSO)>(); // Store both weapon and WeaponSO
    Dictionary<int, int> weaponAmmo = new Dictionary<int, int>(); // Stores ammo per weapon
    Weapon currentWeapon;
    float timeSinceLastShot = 0f;
    float defaultFov;
    float defaultRotationSpeed;
    const string SHOOT_STRING = "Shoot";
    void Awake()
    {
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        firstPersonController = GetComponentInParent<FirstPersonController>();
        animator = GetComponent<Animator>();
        defaultFov = playerFollowCamera.m_Lens.FieldOfView;
        defaultRotationSpeed = firstPersonController.RotationSpeed;
    }
    void Start()
    {
        if(currentWeapon != null){
            currentWeapon = GetComponentInChildren<Weapon>();
        }
    }
    // Update is called once per frame
    void Update()
    {
        HandleShooting();
        HandleZoom();
        HandleWeaponSwitching();
    }
    void HandleShooting(){
        timeSinceLastShot += Time.deltaTime;
        if(!starterAssetsInputs.shoot || currentWeapon == null)
            return;
        int slot = starterAssetsInputs.gun;
        if(timeSinceLastShot >= weaponSO.FireRate && weaponAmmo.ContainsKey(slot) && weaponAmmo[slot] > 0){
            currentWeapon.Shoot(weaponSO);
            animator.Play(SHOOT_STRING, 0, 0f);
            timeSinceLastShot = 0f;
            AdjustAmmo(slot, -1); // Reduce only the active weapon's ammo
        }

        if(!weaponSO.IsAutomatic){
            starterAssetsInputs.ShootInput(false);
        }
    }

    void HandleZoom(){
        if(!weaponSO.CanZoom || currentWeapon == null) 
            return;
        if(starterAssetsInputs.zoom){
            playerFollowCamera.m_Lens.FieldOfView = weaponSO.ZoomAmount;
            weaponCam.fieldOfView = weaponSO.ZoomAmount;
            zoomImage.SetActive(true);
            firstPersonController.ChangeRotationSpeed(weaponSO.ZoomRotationSpeed);
        }
        else{
            playerFollowCamera.m_Lens.FieldOfView = defaultFov;
            weaponCam.fieldOfView = defaultFov;
            zoomImage.SetActive(false);
            firstPersonController.ChangeRotationSpeed(defaultRotationSpeed);
        }
    }

    void HandleWeaponSwitching()
    {
        if (starterAssetsInputs.gun == 0) return; // No weapon change input

        int weaponSlot = starterAssetsInputs.gun; // Number key pressed
        if (weapons.ContainsKey(weaponSlot))
        {
            EquipWeapon(weaponSlot);
        }
    }

    
    // public void SwitchWeapon(WeaponSO weaponSO){
    //     if(currentWeapon){
    //         Destroy(currentWeapon.gameObject);
    //     }
    //     Weapon newWeapon = Instantiate(weaponSO.weaponPrefab, transform).GetComponent<Weapon>();
    //     currentWeapon = newWeapon;
    //     this.weaponSO = weaponSO;
    // }

    public void SwitchWeapon(WeaponSO newWeaponSO)
    {
        int slot = newWeaponSO.weaponSlot;

        // If there's already a weapon in this slot, destroy it
        // if (weapons.ContainsKey(slot))
        // {
        //     Destroy(weapons[slot].gameObject);
        // }

        if(!ammoPanel.activeSelf){
            ammoPanel.SetActive(true);
            // currentAmmo = weaponSO.WeaponAmmo;
            // ammoText.text = currentAmmo.ToString("D2");
        }

        if (currentWeapon != null)
        {
            currentWeapon.gameObject.SetActive(false);
        }

        // Instantiate and store new weapon
        Weapon newWeapon = Instantiate(newWeaponSO.weaponPrefab, transform).GetComponent<Weapon>();
        weapons[slot] = (newWeapon, newWeaponSO);
        weaponAmmo[slot] = newWeaponSO.WeaponAmmo;

        // Equip if it's the first weapon or if switching
        if (currentWeapon == null || starterAssetsInputs.gun == slot || weapons.Count == 1)
        {
            starterAssetsInputs.gun = slot;
            EquipWeapon(slot);
        }
    }

    private void EquipWeapon(int slot)
    {
        if (!weapons.ContainsKey(slot)) return;

        // Disable ALL weapons first
        foreach (var kvp in weapons)
        {
            kvp.Value.weapon.gameObject.SetActive(false);
        }

        // Disable current weapon
        // if (currentWeapon != null)
        // {
        //     currentWeapon.gameObject.SetActive(false);
        // }

        // Enable new weapon
        (currentWeapon, weaponSO) = weapons[slot];
        currentWeapon.gameObject.SetActive(true);
        starterAssetsInputs.gun = slot;
        UpdateAmmoText(slot);
    }

    private void UpdateAmmoText(int slot)
    {
        if (weaponAmmo.ContainsKey(slot))
        {
            ammoText.text = weaponAmmo[slot].ToString("D2");
        }
        else
        {
            ammoText.text = "00"; // Default to zero if ammo isn't set
        }
    }

    public void AdjustAmmo(int slot, int amount)
    {
        if (weaponAmmo.ContainsKey(slot))
        {
            weaponAmmo[slot] += amount;

        // Ensure ammo never exceeds max limit
        int maxAmmo = weapons[slot].weaponSO.WeaponAmmo;
        if (weaponAmmo[slot] > maxAmmo)
        {
            weaponAmmo[slot] = maxAmmo;
        }

        if (weaponAmmo[slot] < 0) 
        {
            weaponAmmo[slot] = 0; // Prevent negative ammo
        }
        
            UpdateAmmoText(slot);
        }
    }

    public int GetCurrentWeaponSOSlot(){
        return weaponSO.weaponSlot;
    }

    public Dictionary<int, (Weapon weapon, WeaponSO weaponSO)> GetWeapons(){
        return weapons;
    }

    public void SetZoomVigette(bool value){
        this.zoomImage.SetActive(value);
    }
}