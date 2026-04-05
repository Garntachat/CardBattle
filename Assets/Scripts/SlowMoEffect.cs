using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SlowmoEffect : MonoBehaviour
{
    public Volume globalVolume;
	public float vignetteIntensityOn = 0.5f;
	public float vignetteIntensityOff = 0.2f;
	public float transitionSpeed = 3f;

	private Vignette vignette;
	private float targetIntensity;

	void Start() {
		globalVolume.profile.TryGet(out vignette);
		targetIntensity = vignetteIntensityOff;
	}

	void Update() {
		if (vignette == null) return;
		vignette.intensity.value = Mathf.Lerp(
			vignette.intensity.value,
			targetIntensity,
			transitionSpeed * Time.unscaledDeltaTime
		);
	}

	public void OnSlowMoStart() {
		targetIntensity = vignetteIntensityOn;
	}

	public void OnSlowMoEnd() {
		targetIntensity = vignetteIntensityOff;
	}
}
