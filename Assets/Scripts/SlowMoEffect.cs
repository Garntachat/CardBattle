using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SlowmoEffect : MonoBehaviour
{
    public Volume globalVolume;
	public float vignetteIntensityOn = 0.5f;
	public float vignetteIntensityOff = 0.2f;
	public float transitionSpeedOn = 3f;
	public float fadeOutDuration = 0.8f;

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
}
