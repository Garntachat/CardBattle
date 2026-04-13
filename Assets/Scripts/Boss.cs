using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour
{
    [Header("Settings")]
	public Transform target;
	public float speed = 3f;
	public float attackRange = 2f;
	public float attackRate = 2f;

	[Header("States")]
	public bool isFarming = false;

	private Animator anim;
	private float nextAttackTime = 0f;

	public bool isKnockedDown = false;

	void Start()
	{
		anim = GetComponent<Animator>();

		if (target == null)
		{
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			if (player != null) target = player.transform;
		}
	}

	void Update()
	{
		if (target == null) return;

		if (isKnockedDown) return;

		if (isFarming)
		{
			StopMoving();
			anim.SetBool("IsFarming", true);
			return;
		}
		else
		{
			anim.SetBool("IsFarming", false);
		}

		float distance = Vector3.Distance(transform.position, target.position);

		if (distance <= attackRange)
		{
			StopMoving();

			if (Time.time >= nextAttackTime)
			{
				Attack();
				nextAttackTime = Time.time + attackRate;
			}
			
		}
		else
		{
			MoveToTarget();
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

	void MoveToTarget()
	{
		Vector3 direction = (target.position - transform.position).normalized;
		Vector3 moveDir = new Vector3(direction.x, 0, direction.z);

		transform.position += moveDir * speed * Time.deltaTime;

		transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));

		anim.SetFloat("Speed", speed);
	}

	void StopMoving()
	{
		anim.SetFloat("Speed", 1f);
	}

	void Attack()
	{
		anim.SetTrigger("Attack");
		transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
	}
}
