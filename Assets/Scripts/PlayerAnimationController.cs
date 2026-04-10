using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
	[Header("Effects")]
	public GameObject punchEffect;
	public GameObject guardEffect;
	public GameObject grabEffect;
	public GameObject slamEffect;

	[Header("Spawn Points")]
    public Transform guardPoint;
    public Transform grabPoint;

    private Animator animator;

	public HitStop hitStop;
	void Start() {
		animator = GetComponent<Animator>();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			PlayAttack();
		} else if (Input.GetKeyDown(KeyCode.B))
		{
			PlayBlock();
		} else if (Input.GetKeyDown(KeyCode.T))
		{
			PlayDodge();
		}
	}

	public void SpawnPunchEffect()
	{
		Instantiate(punchEffect, grabPoint.position, grabPoint.rotation);
	}

	public void SpawnGuardEffect()
	{
		Instantiate(guardEffect, guardPoint.position, guardPoint.rotation);
	}

	public void SpawnGrabEffect()
	{
		Instantiate(grabEffect, grabPoint.position, grabPoint.rotation);
	}

	public void SpawnSlamEffect()
	{
		Vector3 spawnPos = grabPoint.position;
		spawnPos.y = transform.position.y;
		Quaternion flatRotation = Quaternion.Euler(-90f, 0f, 0f);
		Instantiate(slamEffect, spawnPos, flatRotation);
		TriggerHitStopEvent();
	}

	public void TriggerHitStopEvent()
	{
		hitStop.TriggerHitStop();
	}

	public void PlayAttack() {
		animator.SetTrigger("DoAttack");
	}

	public void PlayBlock() {
		animator.SetTrigger("DoBlock");
	}

	public void PlayDodge() {
		animator.SetTrigger("DoDodge");
	}
}
