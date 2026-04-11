using UnityEngine;

public class EnemyFist : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;
    public float attackRange = 1.5f;
    public float attackRate = 1.5f;
    private float nextAttackTime = 0f;
    public float damage = 10f;

    // เพิ่มตัวแปร Animator
    private Animator anim;

    void Start() {
        if (target == null) target = GameObject.FindGameObjectWithTag("Player")?.transform;
        
        // หา Animator ที่อยู่ที่ตัวลูก (ร่างคน)
        anim = GetComponentInChildren<Animator>();
    }

    void Update() {
        if (target == null) return;
        float distance = Vector3.Distance(transform.position, target.position);

        if (distance > attackRange) {
            // --- ช่วงเดิน ---
            Vector3 dir = (target.position - transform.position).normalized;
            transform.position += new Vector3(dir.x, 0, dir.z) * speed * Time.deltaTime;
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));

            // สั่งให้เล่นแอนิเมชันวิ่ง (Speed > 0.1)
            if (anim != null) anim.SetFloat("Speed", speed);
        } 
        else {
            // --- ช่วงหยุดเพื่อต่อย ---
            // สั่งให้หยุดวิ่ง (Speed < 0.1) จะกลับไปท่า Idle
            if (anim != null) anim.SetFloat("Speed", 0f);

            if (Time.time >= nextAttackTime) {
                Attack();
                nextAttackTime = Time.time + attackRate;
            }
        }
        Debug.Log("TimeScale: " + Time.timeScale);
    }

    void Attack() 
    {
        // สั่งให้เล่นแอนิเมชันต่อย (Trigger DoAttack)
        if (anim != null) anim.SetTrigger("DoAttack");

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