using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections.Generic;
public class CardUIManager : MonoBehaviour
{
    [Header("References")]
    public CardManager cardManager;
    public GameObject cardPanel;
    public PlayerController playerController;
    public ThinkingRate thinkingRate;
    public CardResolver cardResolver;

    [Header("Card UI Elements")]
    public Button[] cardButtons = new Button[3];
    public Image[] cardBackgrounds = new Image[3];  
    public Image[] cardArtworks = new Image[3]; 
    public TextMeshProUGUI[] cardNameTexts = new TextMeshProUGUI[3];
    // public TextMeshProUGUI[] cardStatTexts = new TextMeshProUGUI[3]; // For Damage/Guard numbers
    public TextMeshProUGUI[] cardDescTexts = new TextMeshProUGUI[3]; // For the effect text
    
    private List<CardData> currentCards;

    void Start()
    {
        if (cardPanel == null)
        {
            Debug.LogError("CardPanel is not assigned!");
            return;
        }
        for (int i = 0; i < cardButtons.Length; i++)
        {
            int index = i;
            
            EventTrigger trigger = cardButtons[i].gameObject.GetComponent<EventTrigger>();

            if (trigger == null)
                trigger = cardButtons[i].gameObject.AddComponent<EventTrigger>();
             
            trigger.triggers.Clear();

            EventTrigger.Entry entryEntry = new EventTrigger.Entry();
            entryEntry.eventID = EventTriggerType.PointerEnter;
            entryEntry.callback.AddListener((e) => 
            {
                cardArtworks[index].gameObject.SetActive(false);
                cardDescTexts[index].gameObject.SetActive(true);
            });
            trigger.triggers.Add(entryEntry);

            EventTrigger.Entry exitEntry = new EventTrigger.Entry();
            exitEntry.eventID = EventTriggerType.PointerExit;
            exitEntry.callback.AddListener((e) =>
            {
                Debug.Log($"Hover card {index}");
                cardArtworks[index].gameObject.SetActive(true);
                cardDescTexts[index].gameObject.SetActive(false);
                
            });
            trigger.triggers.Add(exitEntry);
        }

        cardPanel.SetActive(false);
    }

    public void ShowCards()
    {
        cardPanel.SetActive(true);
        currentCards = cardManager.DrawCards();

        for (int i = 0; i < cardButtons.Length; i++)
        {
            int index = i;
            CardData data = currentCards[i];

            cardNameTexts[i].text = data.englishName;
            cardBackgrounds[i].color = data.cardColor;

            cardDescTexts[i].text = data.description;
            cardDescTexts[i].gameObject.SetActive(false);
            cardArtworks[i].gameObject.SetActive(true);

            if (data.cardArtwork != null) 
            {
                cardArtworks[i].sprite = data.cardArtwork; 
                cardArtworks[i].color = new Color(1, 1, 1, 1); // Make it visible (Alpha 1)
            }
            else 
            {
                cardArtworks[i].sprite = null; 
                cardArtworks[i].color = new Color(1, 1, 1, 0); // Make the empty box invisible (Alpha 0)
            }

            cardButtons[i].onClick.RemoveAllListeners();
            cardButtons[i].onClick.AddListener(() => OnCardSelected(index));
        }
    }

    void OnCardSelected(int index)
    {
        CardData selected = cardManager.SelectCard(index);
        Debug.Log($"Selected: {selected.englishName}");
        
        // trigger animation ตาม card
        playerController.PlayAttackAnimation(selected.animationTrigger);
        cardResolver.ResolveCard(selected);
        thinkingRate.OnCardSelected();
        Time.timeScale = 1f;
        cardPanel.SetActive(false);
    }
}
