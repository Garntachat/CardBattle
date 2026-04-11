// PlayerController.cs
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Stats")]
    public float maxHP = 100f;
    public float currentHP;

    [Header("Defense")]
    private float damageReduction = 0f;  // 守 เซ็ตค่านี้
    private bool isDodging = false;       // 避 เซ็ตค่านี้
    private Animator animator;

    [Header("Effects")]
    public SlowmoEffect slowMoEffect;
    public HitStop hitStop;

    void Start()
    {
        currentHP = maxHP;
        animator = GetComponent<Animator>();
    }

    // --- damage ---
    public void TakeDamage(float damage)
    {
        //dogging == True ? no_damage : damaged
        if (isDodging)
        {
            Debug.Log("Dodged!");
            isDodging = false;
            return;
        }

        // cal. damage after begin hit
        float finalDamage = damage * (1f - damageReduction);
        currentHP -= finalDamage;

        Debug.Log($"Player took {finalDamage} damage! HP: {currentHP}/{maxHP}");

        // reset reduction after being hit
        damageReduction = 0f;

        if (currentHP <= 0f)
        {
            Die();
        }

        // Damge Taken
        slowMoEffect.OnPlayerHit();
        hitStop.TriggerHitStop(0.05f);
    }

    // --- 守 Guard ---
    public void SetDamageReduction(float reduction)
    {
        damageReduction = reduction;
        animator.SetTrigger("DoBlock");
        Debug.Log($"Guard! Damage reduction: {reduction * 100}%");
    }

    // --- 避 Dodge ---
    public void DodgeNextAttack()
    {
        isDodging = true;
        animator.SetTrigger("DoDodge");
        Debug.Log("Ready to dodge!");
    }
    public void PlayAttackAnimation(string triggerName) 
    {
        animator.SetTrigger(triggerName);
    }
    // --- ตาย ---
    private void Die()
    {
        Debug.Log("Player died!");
        // บอกทีมให้ trigger game over ตรงนี้
    }
}