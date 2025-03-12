using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{   
    [SerializeField] int startEnemyHealth = 3;
    [SerializeField] Slider enemyHealthSlider;
    [SerializeField] GameObject explosionOfBot;
    int currentEnemyHealth;
    const string DEATH_STRING = "Death";
    Animator animator;
    void Awake()
    {
        currentEnemyHealth = startEnemyHealth;
        if (enemyHealthSlider != null)
        {
            enemyHealthSlider.maxValue = startEnemyHealth;
            enemyHealthSlider.value = currentEnemyHealth;
        }
        animator = GetComponent<Animator>();
    }
    public void TakeDamage(int damage){
        currentEnemyHealth -= damage;
        if (enemyHealthSlider != null)
        {
            enemyHealthSlider.value = currentEnemyHealth;
        }
        if(currentEnemyHealth <= 0){
            if(explosionOfBot != null)
                SelfDestruct();
        }
    }

    public void SelfDestruct(){
        if(enemyHealthSlider.value != 0)
            enemyHealthSlider.value = 0;
            currentEnemyHealth = 0;
        Vector3 explosionPosition = transform.position;
        explosionPosition.y += 1.0f;
        Instantiate(explosionOfBot, explosionPosition, Quaternion.identity);
        if(animator!= null)
            animator.Play(DEATH_STRING, 0, 0f);
        Destroy(this.gameObject,3f);
    }
    
    public int GetEnemyHealth(){
        return currentEnemyHealth;
    }
}
