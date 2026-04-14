using UnityEngine;
using System.Collections;

public class EnemyKnife : MonoBehaviour
{
    public Transform target;
    public float speed = 4f;
    public float attackRange = 2.5f; 
    public float attackRate = 2f;
    private float nextAttackTime = 0f;
    public float damage = 20f; 

    public bool isKnockedDown = false;

    [Header("SoundEffects")]
    public AudioSource audioSource;
    public AudioClip stabSound;
    public AudioClip footStepSound;

    // เพิ่มตัวแปรเหล่านี้
    Animator anim;
    Rigidbody rb;

    void Start() {
        // รวม Start ไว้ที่เดียว
        if (target == null) target = GameObject.FindGameObjectWithTag("Player")?.transform;
        
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        if (target == null) return;

        if (isKnockedDown) return;

        float distance = Vector3.Distance(transform.position, target.position);

        // --- ส่วนของการเคลื่อนที่ ---
        if (distance > attackRange) {
            Vector3 dir = (target.position - transform.position).normalized;
            transform.position += new Vector3(dir.x, 0, dir.z) * speed * Time.deltaTime;
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
        } 
        // --- ส่วนของการโจมตี ---
        else if (Time.time >= nextAttackTime) {
            Attack();
            nextAttackTime = Time.time + attackRate;
        }

        // --- ส่วนของการส่งค่าให้ Animator (ยุบรวมมาไว้ที่นี่) ---
        if (anim != null) {
            // คำนวณความเร็ว: ถ้าใช้การย้าย position ตรงๆ แบบข้างบน 
            // ให้เช็คว่าระยะห่าง > attackRange ไหม ถ้าใช่ให้ความเร็วเป็น speed ถ้าไม่ใช่เป็น 0
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
        // 1. สั่งเล่นท่าแอนิเมชันโจมตี
        if (anim != null) {
            anim.SetTrigger("DoAttack"); 
        }

        // 2. ส่งดาเมจ
        if (target != null)
        {
            PlayerController pc = target.GetComponent<PlayerController>();
            if (pc != null)
            {
                pc.TakeDamage(damage);
                Debug.Log("ศัตรูมีดฟันเข้า " + damage);
            }
        }
    }

    public void PlayStabSound()
    {
        audioSource.PlayOneShot(stabSound, 0.3f);
    }

    public void PlayFootStepSound()
    {
        audioSource.PlayOneShot(footStepSound, 0.03f);
    }
}