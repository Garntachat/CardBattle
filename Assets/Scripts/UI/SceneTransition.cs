using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition Instance { get; private set; }

	CanvasGroup fadePanel;
	[SerializeField] float fadeDuration = 0.4f;

	void Awake()
	{
		if (Instance != null) { Destroy(gameObject); return; }
		Instance = this;
		DontDestroyOnLoad(gameObject);


		var canvasGO = new GameObject("FadeCanvas");
		canvasGO.transform.SetParent(transform);
		var canvas = canvasGO.AddComponent<Canvas>();
		canvas.renderMode = RenderMode.ScreenSpaceOverlay;
		canvas.sortingOrder = 999;
		canvasGO.AddComponent<CanvasScaler>();
		canvasGO.AddComponent<GraphicRaycaster>();

		var imgGO = new GameObject("FadeImage");
		imgGO.transform.SetParent(canvasGO.transform, false);
		var img = imgGO.AddComponent<UnityEngine.UI.Image>();
		img.color = Color.black;
		img.raycastTarget = false;
		var rt = imgGO.GetComponent<RectTransform>();
		rt.anchorMin = Vector2.zero;
		rt.anchorMax = Vector2.one;
		rt.offsetMin = rt.offsetMax = Vector2.zero;

		fadePanel = imgGO.AddComponent<CanvasGroup>();
		fadePanel.alpha = 0f;
		fadePanel.blocksRaycasts = false;
	}

	public void TransitionTo(int sceneIndex)
	{
		StartCoroutine(FadeAndLoad(sceneIndex));
	}

	IEnumerator FadeAndLoad(int sceneIndex)
	{
		yield return StartCoroutine(Fade(0f, 1f));
		SceneManager.LoadScene(sceneIndex);
		yield return null;
		yield return StartCoroutine(Fade(1f, 0f));
	}

	IEnumerator Fade(float from, float to)
	{
		fadePanel.blocksRaycasts = true;
		float elapsed = 0f;
		while (elapsed < fadeDuration)
		{
			elapsed += Time.unscaledDeltaTime;
			fadePanel.alpha = Mathf.Lerp(from, to, elapsed / fadeDuration);
			yield return null;
		}
		fadePanel.alpha = to;
		if (to == 0f) fadePanel.blocksRaycasts = false;
	}
}
