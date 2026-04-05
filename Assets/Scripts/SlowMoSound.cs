using UnityEngine;

public class SlowMoSound : MonoBehaviour
{
	public AudioClip whooshClip;
	public AudioClip tickClip;
	public AudioClip whooshReverseClip;

    private AudioSource whooshSource;
	private AudioSource tickSource;
	private AudioSource whooshReverseSource;

	void Start() {
		whooshSource = gameObject.AddComponent<AudioSource>();
		whooshSource.clip = whooshClip;

		tickSource = gameObject.AddComponent<AudioSource>();
		tickSource.clip = tickClip;
		tickSource.loop = true;

		whooshReverseSource = gameObject.AddComponent<AudioSource>();
		whooshReverseSource.clip = whooshReverseClip;
	}

	public void OnSlowMoStart() {
		whooshSource.Play();
		tickSource.Play();
	}

	public void OnSlowMoEnd() {
		tickSource.Stop();
		whooshReverseSource.Play();
	}
}
