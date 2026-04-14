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
    public float targetTopYPosition = 400f; 
    public float targetTextScale = 0.6f;

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
        fullScreenBlackPanel.SetActive(true);
        blackPanelCanvasGroup.alpha = 1f; 
        
        ShowText("Welcome to the Tutorial.\nPrepare yourself.");
        yield return new WaitForSeconds(2f);

        yield return StartCoroutine(FadeBlackScreenAndMoveText());

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

        RectTransform textRect = instructionText.rectTransform;
        Vector2 startPos = textRect.anchoredPosition;
        Vector3 startScale = textRect.localScale;

        Vector2 targetPos = new Vector2(startPos.x, targetTopYPosition);
        Vector3 targetScale = new Vector3(targetTextScale, targetTextScale, targetTextScale);

        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            float progress = timeElapsed / fadeDuration;

            blackPanelCanvasGroup.alpha = Mathf.Lerp(1f, 0f, progress);

            textRect.anchoredPosition = Vector2.Lerp(startPos, targetPos, progress);

            textRect.localScale = Vector3.Lerp(startScale, targetScale, progress);

            yield return null; 
        }

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