using UnityEngine;

public class ExplosionAI : MonoBehaviour
{
    [SerializeField] float radius = 1.5f;
    [SerializeField] int damage = 3;
    
    public float GetRadius()
    {
        return radius;
    }

    public void Explosive()
    {
        Debug.Log("💥 Explosion Triggered!");

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        bool playerHit = false;

        foreach (var hitCollider in hitColliders)
        {
            PlayerHealth playerHealth = hitCollider.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                playerHit = true;
            }
        }

        if (playerHit)
        {
            Debug.Log("✅ Player took damage from explosion.");
        }
        else
        {
            Debug.Log("❌ No player in explosion radius.");
        }

        Destroy(gameObject, 3f); // Destroy explosion effect after 1 second
    }
}
