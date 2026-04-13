using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] Button btnStart;
    [SerializeField] Button btnStageSelect;
    [SerializeField] Button btnQuit;
    [SerializeField] Button btnTutorial; 
    
    // 1. ADD THE EDIT DECK BUTTON HERE
    [SerializeField] Button btnEditDeck; 

    [Header("Title Animation")]
    [SerializeField] RectTransform titleTransform;
    [SerializeField] float bobHeight = 8f;
    [SerializeField] float bobSpeed = 1.2f;

    [Header("ด่านแรก")]
    [SerializeField] StageData firstStage;

    [Header("Scene Settings")]
    // Put the Build Settings number for your Card Deck scene here!
    [SerializeField] int cardDeckSceneIndex = 3; 

    Vector3 titleStartPos;

    void Start()
    {
        if (titleTransform != null)
            titleStartPos = titleTransform.anchoredPosition;

        btnStart.onClick.AddListener(OnStartClicked);
        btnStageSelect.onClick.AddListener(OnStageSelectedClicked);
        btnQuit.onClick.AddListener(OnQuitClicked);
        
        if (btnTutorial != null) 
        {
            btnTutorial.onClick.AddListener(OnTutorialClicked);
        }

        // 2. TELL THE EDIT DECK BUTTON TO LISTEN FOR CLICKS
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

    void OnStageSelectedClicked()
    {
        SceneTransition.Instance.TransitionTo(2);
    }

    // 3. THIS LOADS YOUR CARD DECK SCENE!
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