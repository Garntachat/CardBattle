using UnityEngine;
using System.Collections;

public class HitStop : MonoBehaviour
{
    public float stopDuration = 0.05f;
	public CameraShake cameraShake;
	public void TriggerHitStop()
	{
		StartCoroutine(DoHitStop());
	}

	private IEnumerator DoHitStop()
	{
		Time.timeScale = 0f;
		yield return new WaitForSecondsRealtime(stopDuration);
		Time.timeScale = 1f;
		cameraShake.TriggerShake();
	}
}
