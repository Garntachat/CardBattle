using UnityEngine;

public class EnemyFist : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;
    public float attackRange = 1.5f;
    public float attackRate = 1.5f;
    private float nextAttackTime = 0f;

    void Start() {
        if (target == null) target = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update() {
        if (target == null) return;
        float distance = Vector3.Distance(transform.position, target.position);

        if (distance > attackRange) {
            Vector3 dir = (target.position - transform.position).normalized;
            transform.position += new Vector3(dir.x, 0, dir.z) * speed * Time.deltaTime;
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
        } else if (Time.time >= nextAttackTime) {
            Attack();
            nextAttackTime = Time.time + attackRate;
        }
    }

    void Attack() {
        Debug.Log("หมัด: ต่อย 1-2!"); 
        // อนาคตใส่ระบบ Double Punch ตรงนี้
    }
}