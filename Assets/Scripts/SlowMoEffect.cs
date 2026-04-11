using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class SlowmoEffect : MonoBehaviour
{
    public Volume globalVolume;
	public float vignetteIntensityOn = 0.5f;
	public float vignetteIntensityOff = 0.2f;
	public float transitionSpeedOn = 3f;
	public float fadeOutDuration = 0.8f;

	public Color damageColor = Color.red;
	public float damageDuration = 0.15f;

	private Vignette vignette;
	private float targetIntensity;
	private float currentSpeed;

	void Start() {
		globalVolume.profile.TryGet(out vignette);
		targetIntensity = vignetteIntensityOff;
	}

	void Update() {
		if (vignette == null) return;
		vignette.intensity.value = Mathf.Lerp(
			vignette.intensity.value,
			targetIntensity,
			currentSpeed * Time.unscaledDeltaTime
		);
	}

	public void OnSlowMoStart() {
		targetIntensity = vignetteIntensityOn;
		currentSpeed = transitionSpeedOn;
	}

	public void OnSlowMoEnd() {
		targetIntensity = vignetteIntensityOff;
		float delta = vignetteIntensityOn - vignetteIntensityOff;
		currentSpeed = delta / fadeOutDuration;
	}

	public void OnPlayerHit()
	{
		StartCoroutine(DamageFlash());
	}

	private IEnumerator DamageFlash()
	{
		vignette.color.Override(damageColor);
		targetIntensity = 0.6f;
		yield return new WaitForSecondsRealtime(damageDuration);
		vignette.color.Override(Color.black);
	}
}
