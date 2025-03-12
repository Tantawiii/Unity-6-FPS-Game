using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 100f;
    const string PLAYER_STRING = "Player";
    void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
    void OnTriggerEnter(Collider other)
    {
        ActiveWeapon activeWeapon = other.GetComponentInChildren<ActiveWeapon>();
        OnPickUp(activeWeapon);
        Destroy(this.gameObject);
    }
    
    protected abstract void OnPickUp(ActiveWeapon activeWeapon);
}
