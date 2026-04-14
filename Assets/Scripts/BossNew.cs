using UnityEngine;
using System.Collections;

public class BossNew : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Movement")]
    public float speed = 4f;
    public float attackRange = 2.5f;

    [Header("Attack")]
    public float attackRate = 2f;
    private float nextAttackTime = 0f;
    public float damage = 20f;

    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip floatSound;

    private Animator anim;
    private Rigidbody rb;

	public bool isKnockedDown = false;

    void Start()
    {
        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player")?.transform;

        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();

        if (anim == null)
            Debug.LogWarning("Animator not found on Boss!");

    }

    void Update()
    {
        if (target == null) return;

        float distance = Vector3.Distance(transform.position, target.position);

        // --- ATTACK ---
        if (distance <= attackRange && Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + attackRate;
            return; // ✅ stop movement this frame
        }

        // --- MOVE ---
        if (distance > attackRange)
        {
            Vector3 dir = (target.position - transform.position).normalized;

            transform.position += new Vector3(dir.x, 0, dir.z) * speed * Time.deltaTime;

            transform.LookAt(new Vector3(
                target.position.x,
                transform.position.y,
                target.position.z
            ));

        }

        // --- ANIMATION (Speed) ---
        if (anim != null)
        {
            float currentSpeed = (distance > attackRange) ? speed : 0f;
            anim.SetFloat("Speed", currentSpeed);
        }
    }

	public void Knockdown(float sleepTime)
    {
        StartCoroutine(KnockdownRoutine(sleepTime));
    }

    private IEnumerator KnockdownRoutine(float sleepTime)
    {
        isKnockedDown = true;
        if (anim != null) anim.SetFloat("Speed", 0f);

        yield return new WaitForSeconds(sleepTime);

        isKnockedDown = false;
        
    }

    void Attack()
    {
        // 🎬 Play animation
        if (anim != null)
        {
            anim.SetTrigger("Attack"); // ⚠ must match Animator EXACTLY
        }

        // 💥 Deal damage
        if (target != null)
        {
            PlayerController pc = target.GetComponent<PlayerController>();
            if (pc != null)
            {
                pc.TakeDamage(damage);
                Debug.Log("Boss hit player for " + damage);
            }
        }
    }

    void PlayFloatSOund()
    {
        audioSource.PlayOneShot(floatSound, 0.02f);
    }
}