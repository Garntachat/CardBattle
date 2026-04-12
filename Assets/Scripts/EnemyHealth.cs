using UnityEngine;
using System.Collections;
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

    public void TakeDamage(float damage, string deathAnimation = "DoDie")
    {   
        if (isDead) return;
        currentHealth -= damage;
        Debug.Log(gameObject.name + " โดนโจมตี เลือดเหลือ: " + currentHealth);

        if (currentHealth <= 0)
        {
            isDead = true;
            Die(deathAnimation);
        }
    }
    
    void Die(string deathAnimation = "DoDie")
    {
        isDead = true;
        
        EnemyFist fist = GetComponent<EnemyFist>();
        if (fist != null) fist.enabled = false;
        EnemyKnife knife = GetComponent<EnemyKnife>();
        if (knife != null) knife.enabled = false;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null) rb.linearVelocity = Vector3.zero;

        Animator anim = GetComponentInChildren<Animator>();
        if (anim != null) anim.SetFloat("Speed", 0f);

        StartCoroutine(PlayDeathAnimation(anim, deathAnimation));
    }

    IEnumerator PlayDeathAnimation(Animator anim, string deathAnimation)
    {
        yield return new WaitForSecondsRealtime(0.5f);  // delay ก่อน animate — ปรับได้
        if (anim != null) anim.SetTrigger(deathAnimation);
        yield return new WaitForSecondsRealtime(2f);
        Destroy(gameObject);
    }
}
