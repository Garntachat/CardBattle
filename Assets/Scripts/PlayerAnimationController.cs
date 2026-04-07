using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
	public ParticleSystem punchEffect;
	public ParticleSystem guardEffect;

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
		}
	}

	public void SpawnPunchEffect()
	{
		punchEffect.Stop();
		punchEffect.Clear();
		punchEffect.Play();
	}

	public void SpawnGuardEffect()
	{
		guardEffect.Stop();
		guardEffect.Clear();
		guardEffect.Play();
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
