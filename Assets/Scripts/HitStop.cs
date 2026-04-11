using UnityEngine;
using System.Collections;

public class HitStop : MonoBehaviour
{
    public float stopDuration = 0.05f;
	public CameraShake cameraShake;
	public void TriggerHitStop(float magnitude)
	{
		StartCoroutine(DoHitStop(magnitude));
	}

	private IEnumerator DoHitStop(float magnitude)
	{
		Time.timeScale = 0f;
		yield return new WaitForSecondsRealtime(stopDuration);
		Time.timeScale = 1f;
		cameraShake.TriggerShake(magnitude);
	}
}
