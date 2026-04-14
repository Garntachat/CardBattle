using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    public Transform target;
    public float attackRange = 3f;
    public float auraRange = 6f; // ระยะที่จะเริ่มใช้ท่า Aura
    public float auraCooldown = 10f; // คูลดาวน์ท่าไม้ตาย
    
    private NavMeshAgent agent;
    private Animator anim;
    private float nextAuraTime = 0f;
    private bool isUsingAura = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        if (target == null) target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (target == null || isUsingAura) return; // ถ้ากำลังใช้ท่า Aura ให้บอสหยุดทำอย่างอื่น

        float distance = Vector3.Distance(transform.position, target.position);

        // ระบบเคลื่อนที่พื้นฐาน
        if (distance > attackRange)
        {
            agent.SetDestination(target.position);
            anim.SetFloat("Speed", 1f);
        }
        else
        {
            agent.ResetPath();
            anim.SetFloat("Speed", 0f);
        }

        // เช็คการใช้ท่า Aura Farm
        if (distance <= auraRange && Time.time >= nextAuraTime)
        {
            StartCoroutine(PerformAuraSkill());
        }
    }

    System.Collections.IEnumerator PerformAuraSkill()
    {
        isUsingAura = true;
        agent.isStopped = true; // สั่งให้ NavMeshAgent หยุดเดิน
        
        anim.SetTrigger("DoAura"); // ชื่อต้องตรงกับใน Animator
        
        // รอให้ท่า Aura เล่นจบ (สมมติว่าท่ายาว 3 วินาที ปรับตามความจริงได้)
        yield return new WaitForSeconds(3f); 
        
        agent.isStopped = false;
        isUsingAura = false;
        nextAuraTime = Time.time + auraCooldown;
    }
}