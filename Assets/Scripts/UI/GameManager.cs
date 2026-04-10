using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

	[Header("Scene Indexes")]
	public int mainMenuSceneIndex = 0;
	public int stageSelectSceneIndex = 1;

	public StageData CurrentStage { get; private set; }

	void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
			return;
		}
		Instance = this;
		DontDestroyOnLoad(gameObject);
	}

	public void LoadMainMenu()
	{
		SceneManager.LoadScene(mainMenuSceneIndex);
	}

	public void LoadStageSelect()
	{
		SceneManager.LoadScene(stageSelectSceneIndex);
	}

	public void LoadStage(StageData stage)
	{
		CurrentStage = stage;
		SceneManager.LoadScene(stage.sceneIndex);
	}

	public void QuitGame()
	{
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#else
			Application.Quit();
		#endif
	}
}
