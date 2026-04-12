using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public bool isBoss = false; 
    public bool isDead = false;
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {   
        if (isDead) return;
        currentHealth -= damage;
        Debug.Log(gameObject.name + " โดนโจมตี เลือดเหลือ: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die(string deathAnimation = "DoDie")
    {
        Debug.Log(gameObject.name + " ตายแล้ว");

        // หยุด movement ทันที
        EnemyFist fist = GetComponent<EnemyFist>();
        if (fist != null) fist.enabled = false;
        EnemyKnife knife = GetComponent<EnemyKnife>();
        if (knife != null) knife.enabled = false;

        // หยุด rigidbody ถ้ามี
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null) rb.linearVelocity = Vector3.zero;

        Animator anim = GetComponentInChildren<Animator>();
        if (anim != null) anim.SetTrigger(deathAnimation);

        Destroy(gameObject, 2f);
    }
}