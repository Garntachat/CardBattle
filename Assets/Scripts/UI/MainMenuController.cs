using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [Header("Buttons")]
	[SerializeField] Button btnStart;
	[SerializeField] Button btnStageSelect;
	[SerializeField] Button btnQuit;

	[Header("Title Animation")]
	[SerializeField] RectTransform titleTransform;
	[SerializeField] float bobHeight = 8f;
	[SerializeField] float bobSpeed = 1.2f;

	[Header("ด่านแรก")]
	[SerializeField] StageData firstStage;

	Vector3 titleStartPos;

	void Start()
	{
		if (titleTransform != null)
			titleStartPos = titleTransform.anchoredPosition;

		btnStart.onClick.AddListener(OnStartClicked);
		btnStageSelect.onClick.AddListener(OnStageSelectedClicked);
		btnQuit.onClick.AddListener(OnQuitClicked);
	}

	void Update()
	{
		if (titleTransform == null) return;
		float offsetY = Mathf.Sin(Time.time * bobSpeed) * bobHeight;
		titleTransform.anchoredPosition = titleStartPos + new Vector3(0f, offsetY, 0f);
	}

	void OnStartClicked()
	{
		Debug.Log("Start clicked!");
    if (firstStage == null) { Debug.LogWarning("firstStage ยังไม่ได้กำหนด!"); return; }
    Debug.Log($"Loading scene index: {firstStage.sceneIndex}");
    SceneTransition.Instance.TransitionTo(firstStage.sceneIndex);
	}

	void OnStageSelectedClicked()
	{
		SceneTransition.Instance.TransitionTo(GameManager.Instance.stageSelectSceneIndex);
	}

	void OnQuitClicked()
	{
		GameManager.Instance.QuitGame();
	}
}
