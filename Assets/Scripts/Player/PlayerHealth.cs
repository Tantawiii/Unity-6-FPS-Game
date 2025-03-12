using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Range(1,10)]
    [SerializeField] int startingHealth = 10;
    [SerializeField] CinemachineVirtualCamera deathVirtualCamera;
    [SerializeField] Transform weaponCam;
    [SerializeField] Image[] healthBars;
    ActiveWeapon activeWeapon;
    int currentHealth;
    int priorityGameOver = 20;
    void Awake()
    {
        currentHealth = startingHealth;
        activeWeapon = GetComponent<ActiveWeapon>();
    }

    public void TakeDamage(int amount){
        currentHealth -= amount;
        AdjustHealthUi();
        if(currentHealth <= 0){
            weaponCam.parent = null;
            activeWeapon.SetZoomVigette(false);
            deathVirtualCamera.Priority = priorityGameOver;
            Destroy(this.gameObject);
        }
    }

    void AdjustHealthUi(){
        for(int i = 0; i < healthBars.Length; i++){
            if(i<currentHealth){
                healthBars[i].gameObject.SetActive(true);
            }
            else{
                healthBars[i].gameObject.SetActive(false);
            }
        }
    }
}
