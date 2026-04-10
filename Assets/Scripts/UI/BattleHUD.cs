using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class BattleHUD : MonoBehaviour
{
    [Header("Player HP")]
	[SerializeField] Slider playerHPBar;
	[SerializeField] PlayerController playerController;

	[Header("Center Info")]
	[SerializeField] TextMeshProUGUI stageNameText;
	[SerializeField] TextMeshProUGUI roundText;

	[Header("Pause")]
	[SerializeField] Button btnPause;
	[SerializeField] GameObject pausePanel;
	[SerializeField] Button btnResume;
	[SerializeField] Button btnMainMenu;
	[SerializeField] Button btnQuit;

	int currentRound = 1;
	bool isPaused = false;

	void Start()
	{
		if (GameManager.Instance?.CurrentStage != null) 
			stageNameText.text = GameManager.Instance.CurrentStage.stageName;

		playerHPBar.minValue = 0f;
		playerHPBar.maxValue = Mathf.Max(playerController.maxHP, 1f);
		playerHPBar.value = playerController.maxHP;

		UpdateRoundText();

		pausePanel.SetActive(false);
		btnPause.onClick.AddListener(TogglePause);
		btnResume.onClick.AddListener(TogglePause);
		btnMainMenu.onClick.AddListener(GoToMainMenu);
		btnQuit.onClick.AddListener(() => {
			if (GameManager.Instance != null)
				GameManager.Instance.QuitGame();
			else
				Application.Quit();
		});

	}

	void Update()
	{
		playerHPBar.value = playerController.currentHP;

		if (Input.GetKeyDown(KeyCode.Escape))
			TogglePause();
	}

	public void NextRound()
	{
		currentRound++;
		UpdateRoundText();
	}

	void UpdateRoundText()
	{
		roundText.text = $"Round: {currentRound}";
	}

	void TogglePause()
	{
		isPaused = !isPaused;
		pausePanel.SetActive(isPaused);
		Time.timeScale = isPaused ? 0f : 1f;
	}

	void GoToMainMenu()
	{
		Time.timeScale = 1f;
		if (GameManager.Instance != null)
			SceneTransition.Instance.TransitionTo(GameManager.Instance.mainMenuSceneIndex);
		else
			SceneManager.LoadScene(0);
		}
}
