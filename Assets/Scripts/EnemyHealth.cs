using UnityEngine;
using System.Collections;
public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public bool isBoss = false; 
    public bool isDead = false;

    public Material normalMaterial;
    public Material damageMaterial;
    private Renderer[] rends;
    void Start()
    {
        currentHealth = maxHealth;
        rends = GetComponentsInChildren<Renderer>();
    }

    public void TakeDamage(float damage, string animationName = "DoDie")
    {   
        if (isDead) return;
        currentHealth -= damage;
        Debug.Log(gameObject.name + " โดนโจมตี เลือดเหลือ: " + currentHealth);
        StartCoroutine(FlashDamage()); 
        if (currentHealth <= 0)
        {
            isDead = true;
            Die(animationName);
        } else
        {
            if (animationName != "DoDie" && animationName != "")
            {
                Animator anim = GetComponentInChildren<Animator>();
                if (anim != null) anim.SetTrigger(animationName);
                if (animationName == "DoPunched")
                {
                    EnemyFist fist = GetComponent<EnemyFist>();
                    if (fist != null) fist.Knockdown(0.5f);

                    EnemyKnife knife = GetComponent<EnemyKnife>();
                    if (knife != null) knife.Knockdown(0.5f);

                    BossNew bossNew = GetComponent<BossNew>();
                    if (bossNew != null) bossNew.Knockdown(0.5f); 
                }
            }
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
        yield return new WaitForSecondsRealtime(1.9f);
        Destroy(gameObject);
    }
   
    IEnumerator FlashDamage()
    {
        yield return new WaitForSeconds(0.25f);
        if (rends != null && damageMaterial != null)
        {
        // change all materials
        foreach (Renderer r in rends)
        {
            r.material = damageMaterial;
        }

        yield return new WaitForSeconds(0.1f);

        // revert back
        foreach (Renderer r in rends)
        {
            r.material = normalMaterial;
        }
    }
    }

}
