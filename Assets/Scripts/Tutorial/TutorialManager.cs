using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class TutorialManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject fullScreenBlackPanel; 
    public CanvasGroup blackPanelCanvasGroup; 
    public TextMeshProUGUI instructionText;

    [Header("Cinematic Text Animation")]
    public float targetTopYPosition = 400f; // How high up the screen the text moves (adjust in Inspector!)
    public float targetTextScale = 0.6f;    // How small the text gets (0.6 = 60% size)

    [Header("Game References")]
    public GameObject enemyMeleePrefab;
    public Transform spawnPointRight; 
    
    private GameObject currentEnemy;
    private bool playerHasRotated = false;

    void Start()
    {
        StartCoroutine(TutorialSequence());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E))
        {
            playerHasRotated = true;
        }
    }

    IEnumerator TutorialSequence()
    {
        // 1. START WITH THE BLACK SCREEN AND BIG TEXT IN THE CENTER
        fullScreenBlackPanel.SetActive(true);
        blackPanelCanvasGroup.alpha = 1f; 
        
        ShowText("Welcome to the Tutorial.\nPrepare yourself.");
        yield return new WaitForSeconds(2f);

        // 2. FADE THE BLACK SCREEN & MOVE THE TEXT UP
        yield return StartCoroutine(FadeBlackScreenAndMoveText());

        // 3. SPAWN ENEMY & TEACH Q/E
        ShowText("An enemy approaches! Press [Q] to turn Left, or [E] to turn Right. Face the enemy!");
        
        currentEnemy = Instantiate(enemyMeleePrefab, spawnPointRight.position, Quaternion.identity);
        currentEnemy.GetComponent<EnemyFist>().enabled = false; 
        
        playerHasRotated = false;
        yield return new WaitUntil(() => playerHasRotated == true);
        
        ShowText("Great! Wait for them to enter your range...");
        currentEnemy.GetComponent<EnemyFist>().enabled = true;

        CardUIManager cardUI = FindObjectOfType<CardUIManager>();
        yield return new WaitUntil(() => cardUI != null && cardUI.cardPanel.activeInHierarchy == true);

        ShowText("Select a Card!");
        
        EnemyHealth enemyHealth = currentEnemy.GetComponent<EnemyHealth>();
        yield return new WaitUntil(() => currentEnemy == null || enemyHealth.isDead == true);

        ShowText("Tutorial Complete!");
        yield return new WaitForSeconds(3f);
        
        PlayerPrefs.SetInt("TutorialDone", 1);
        PlayerPrefs.Save();
        
        UnityEngine.SceneManagement.SceneManager.LoadScene(0); 
    }

    IEnumerator FadeBlackScreenAndMoveText()
    {
        float fadeDuration = 2.0f; 
        float timeElapsed = 0f;

        // Get the starting position and size of the text
        RectTransform textRect = instructionText.rectTransform;
        Vector2 startPos = textRect.anchoredPosition;
        Vector3 startScale = textRect.localScale;

        // Calculate the final position and size
        Vector2 targetPos = new Vector2(startPos.x, targetTopYPosition);
        Vector3 targetScale = new Vector3(targetTextScale, targetTextScale, targetTextScale);

        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            float progress = timeElapsed / fadeDuration;

            // 1. Fade the black screen out
            blackPanelCanvasGroup.alpha = Mathf.Lerp(1f, 0f, progress);

            // 2. Slide the text up
            textRect.anchoredPosition = Vector2.Lerp(startPos, targetPos, progress);

            // 3. Shrink the text down
            textRect.localScale = Vector3.Lerp(startScale, targetScale, progress);

            yield return null; 
        }

        // Ensure everything is perfectly set at the end of the timer
        blackPanelCanvasGroup.alpha = 0f; 
        fullScreenBlackPanel.SetActive(false); 
        textRect.anchoredPosition = targetPos;
        textRect.localScale = targetScale;
    }

    void ShowText(string message)
    {
        instructionText.text = message;
    }
}