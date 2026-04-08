using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public bool isBoss = false; // ติ๊กถูกเฉพาะใน Prefab บอส

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log(gameObject.name + " โดนโจมตี! เลือดเหลือ: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // ส่งสัญญาณบอก WaveSpawner ว่าฉันตายแล้ว (ถ้าจำเป็น)
        Debug.Log(gameObject.name + " ตายแล้ว!");
        Destroy(gameObject); 
    }
}