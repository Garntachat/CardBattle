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

	[Header("Sound")]
	public AudioSource audioSource;
	public AudioClip punchSound;
	// public AudioClip blockSound;
	// public AudioClip grabSound;
	public AudioClip slamSound;
	public AudioClip pushSound;

	[Header("Voice Lines")]
	public AudioClip straightPunchVoice;
	public AudioClip dodgeVoice;
	public AudioClip guardVoice;
	public AudioClip multiPunchVoice;
	public AudioClip throwVoice;
	public AudioClip pushVoice;

    private Animator animator;

	public HitStop hitStop;
	void Start() {
		animator = GetComponent<Animator>();
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
		TriggerHitStopEvent(0.1f);
	}

	public void TriggerHitStopEvent(float magnitude)
	{
		hitStop.TriggerHitStop(magnitude);
	}

	public void PlayAttack() {
		animator.SetTrigger("DoAttack");
		if (straightPunchVoice != null) audioSource.PlayOneShot(straightPunchVoice);
	}

	public void PlayBlock() {
		animator.SetTrigger("DoBlock");
		if (guardVoice != null) audioSource.PlayOneShot(guardVoice);
	}

	public void PlayDodge() {
		animator.SetTrigger("DoDodge");
		if (dodgeVoice != null) audioSource.PlayOneShot(dodgeVoice);
	}

	public void PlayIdle() {
		animator.ResetTrigger("DoAttack");
		animator.ResetTrigger("DoThrow");
		animator.ResetTrigger("DoMultiPunch");
		animator.ResetTrigger("DoBlock");
		animator.ResetTrigger("DoDodge");
		animator.ResetTrigger("DoGodJud");
		animator.SetTrigger("DoIdle");
	}

	public void PlayThrow()
	{
		animator.SetTrigger("DoThrow");
		if (throwVoice != null) audioSource.PlayOneShot(throwVoice);
	}

	public void PlayMultiPunch()
	{
		animator.SetTrigger("DoMultiPunch");
		if (multiPunchVoice != null) audioSource.PlayOneShot(multiPunchVoice);
	}

	public void PlayGodJud()
	{
		animator.SetTrigger("DoGodJud");
		if (pushVoice != null) audioSource.PlayOneShot(pushVoice);
	}

	public void PlayPunchSound()
	{
		audioSource.PlayOneShot(punchSound);
	}

	public void PlaySlamSound()
	{
		audioSource.PlayOneShot(slamSound);
	}

	public void PlayPushSound()
	{
		audioSource.PlayOneShot(pushSound);
	}
}
