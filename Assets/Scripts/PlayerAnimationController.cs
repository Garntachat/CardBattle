using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
	public ParticleSystem punchEffect;

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
		}
	}

	public void SpawnPunchEffect()
	{
		punchEffect.Stop();
		punchEffect.Clear();
		punchEffect.Play();
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
