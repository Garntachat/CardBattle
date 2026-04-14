using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] Button btnStart;
    [SerializeField] Button btnEndless;
    [SerializeField] Button btnQuit;
    [SerializeField] Button btnTutorial; 
	[SerializeField] Button btnSelectStage;
    
    [SerializeField] Button btnEditDeck; 

    [Header("Title Animation")]
    [SerializeField] RectTransform titleTransform;
    [SerializeField] float bobHeight = 8f;
    [SerializeField] float bobSpeed = 1.2f;

    [Header("ด่านแรก")]
    [SerializeField] StageData firstStage;

    [Header("Scene Settings")]
    [SerializeField] int cardDeckSceneIndex = 3; 

    Vector3 titleStartPos;

    void Start()
    {
        if (titleTransform != null)
            titleStartPos = titleTransform.anchoredPosition;

        btnStart.onClick.AddListener(OnStartClicked);
        btnEndless.onClick.AddListener(OnEndlessClicked);
        btnQuit.onClick.AddListener(OnQuitClicked);
		btnSelectStage.onClick.AddListener(OnStageSelect);
        
        if (btnTutorial != null) 
        {
            btnTutorial.onClick.AddListener(OnTutorialClicked);
        }
        if (btnEditDeck != null)
        {
            btnEditDeck.onClick.AddListener(OnEditDeckClicked);
        }
    }

    void Update()
    {
        if (titleTransform == null) return;
        float offsetY = Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        titleTransform.anchoredPosition = titleStartPos + new Vector3(0f, offsetY, 0f);
    }

    void OnStartClicked()
    {
        int tutorialFinished = PlayerPrefs.GetInt("TutorialDone", 0);

        if (tutorialFinished == 0)
        {
            Debug.Log("First time! Going to Tutorial...");
            SceneTransition.Instance.TransitionTo(1); 
        }
        else
        {
            Debug.Log("Tutorial already done! Going to Level 1...");
            SceneTransition.Instance.TransitionTo(3); 
        }
    }

    void OnTutorialClicked()
    {
        SceneTransition.Instance.TransitionTo(1); 
    }

    void OnEndlessClicked()
    {
        SceneTransition.Instance.TransitionTo(7);
    }
	void OnStageSelect()
    {
        SceneTransition.Instance.TransitionTo(2); 
    }

    void OnEditDeckClicked()
    {
        Debug.Log("Going to Edit Deck Scene!");
        SceneTransition.Instance.TransitionTo(4);
    }

    void OnQuitClicked()
    {
        GameManager.Instance.QuitGame();
    }
}