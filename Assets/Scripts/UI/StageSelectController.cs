using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class StageSelectController : MonoBehaviour
{
    [Header("Data")]
	[SerializeField] StageData[] stages;
	
	[Header("Grid")]
	[SerializeField] Transform stageGridParent;
	[SerializeField] GameObject stageCardPrefab;

	[Header("Info Panel")]
	[SerializeField] GameObject infoPanel;
	[SerializeField] TextMeshProUGUI infoName;
	[SerializeField] TextMeshProUGUI infoDesc;
	[SerializeField] TextMeshProUGUI infoDifficulty;
	[SerializeField] Button btnPlay;

	[Header("Header")]
	[SerializeField] Button btnBack;

	StageData selectedStage;

	static readonly Color colorUnlocked = new Color(0.24f, 0.05f, 0.05f);
	static readonly Color colorLocked   = new Color(0.15f, 0.15f, 0.15f);
	static readonly Color goldColor     = new Color(0.78f, 0.59f, 0.10f);

	void Start()
	{
		btnBack.onClick.AddListener(OnBackClicked);
		btnPlay.onClick.AddListener(OnPlayClciked);
		infoPanel.SetActive(false);
		BuildGrid();
	}

	void BuildGrid()
	{
		foreach (var stage in stages)
		{
			GameObject card = Instantiate(stageCardPrefab, stageGridParent);
			SetupCard(card, stage);
		}
	}

	void SetupCard(GameObject card, StageData stage)
	{
		var bg       = card.GetComponent<Image>();
		var nameText = card.transform.Find("StageName")?.GetComponent<TextMeshProUGUI>();
		var lockIcon = card.transform.Find("LockIcon")?.gameObject;
		var btn      = card.GetComponent<Button>();

		if (stage.isLocked)
		{
			bg.color = colorLocked;
			lockIcon?.SetActive(true);
			if (nameText != null) nameText.text = "???";
			btn.interactable = false;
		}
		else
		{
			bg.color = colorUnlocked;
			lockIcon?.SetActive(false);
			if (nameText != null) nameText.text = stage.stageName;
			
			var capturedStage = stage;
			btn.onClick.AddListener(() => SelectStage(capturedStage));
		}
	}

	void SelectStage(StageData stage)
	{
		selectedStage = stage;
		infoPanel.SetActive(true);
		infoName.text = stage.stageName;
		infoDesc.text = stage.description;
		infoDifficulty.text = $"Difficulty: {DifficultyLabel(stage.difficulty)}";
	}

	void OnPlayClciked()
	{
		if (selectedStage == null) return;
		if (SceneTransition.Instance != null)
			SceneTransition.Instance.TransitionTo(selectedStage.sceneIndex);
		else
			UnityEngine.SceneManagement.SceneManager.LoadScene(selectedStage.sceneIndex);
	}

	void OnBackClicked()
	{
		if (SceneTransition.Instance != null)
			SceneTransition.Instance.TransitionTo(GameManager.Instance.mainMenuSceneIndex);
		else
			UnityEngine.SceneManagement.SceneManager.LoadScene(0);
	}

	static string DifficultyLabel(int d) => d switch
	{
		1 => "Easy",
		2 => "Medium",
		3 => "Hard",
		_ => "???"
	};
}
