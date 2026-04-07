using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 0.15f;
	public float shakeMagnitude = 0.05f;

	private Vector3 originalPos;

	public void TriggerShake()
	{
		StartCoroutine(Shake(shakeDuration, shakeMagnitude));
	}

	public void TriggerCustomShake(float duration, float magnitude) {
		StartCoroutine(Shake(duration, magnitude));
	}

	private IEnumerator Shake(float duration, float magnitude)
	{
		Vector3 originalPos = transform.localPosition;
		float elapsed = 0f;

		while (elapsed < shakeDuration)
		{
			float damping = 1.0f - (elapsed/duration);

			float x = Random.Range(-1f, 1f) * damping;
			float y = Random.Range(-1f, 1f) * damping;
			float z = Random.Range(-1f, 1f) * damping;

			transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z + z);
			elapsed += Time.unscaledDeltaTime;
			yield return null;
		}

		transform.localPosition = originalPos;
	}
}
