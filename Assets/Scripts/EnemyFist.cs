using UnityEngine;

public class EnemyFist : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;
    public float attackRange = 1.5f;
    public float attackRate = 1.5f;
    private float nextAttackTime = 0f;
    public float damage = 10f;
    private bool hasAttackedThisRound = false;

    // เพิ่มตัวแปร Animator
    private Animator anim;

    void Start() {
        if (target == null) target = GameObject.FindGameObjectWithTag("Player")?.transform;
        
        // หา Animator ที่อยู่ที่ตัวลูก (ร่างคน)
        anim = GetComponentInChildren<Animator>();
        Debug.Log("Animator found: " + (anim != null));
    }

    void Update() 
    {
        EnemyHealth health = GetComponent<EnemyHealth>();
        if (health != null && health.isDead) return; 
        if (target == null) return;
        float distance = Vector3.Distance(transform.position, target.position);

        if (distance > attackRange) {
            Vector3 dir = (target.position - transform.position).normalized;
            transform.position += new Vector3(dir.x, 0, dir.z) * speed * Time.deltaTime;
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
            if (anim != null) anim.SetFloat("Speed", speed);
            hasAttackedThisRound = false;  // reset เมื่อออกจาก range
        } 
        else {
            if (anim != null) anim.SetFloat("Speed", 0f);

            if (!hasAttackedThisRound && Time.unscaledTime >= nextAttackTime) {
                hasAttackedThisRound = true;  // lock ไม่ให้ attack ซ้ำ
                Attack();
                nextAttackTime = Time.unscaledTime + attackRate;
            }
        }
    }

    void Attack() 
    {
        // สั่งให้เล่นแอนิเมชันต่อย (Trigger DoAttack)
        if (anim != null) anim.SetTrigger("DoAttack");
    }

    public void DealDamage()
    {
        if (target != null)
        {
            PlayerController pc = target.GetComponent<PlayerController>();
            if (pc != null)
            {
                pc.TakeDamage(damage);
                Debug.Log("ศัตรูหมัดต่อยเข้า " + damage);
            }
        }
    }
}