using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
public class CardUIManager : MonoBehaviour
{
    [Header("References")]
    public CardManager cardManager;
    public GameObject cardPanel;
    public PlayerController playerController;
    public ThinkingRate thinkingRate;

    [Header("Card Buttons")]
    public Button[] cardButtons = new Button[3];
    public TextMeshProUGUI[] cardNameTexts = new TextMeshProUGUI[3];

    private List<CardData> currentCards;

    void Start()
    {
        if (cardPanel == null)
        {
            Debug.LogError("CardPanel is not assigned!");
            return;
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
            cardNameTexts[i].text = currentCards[i].englishName;
            cardButtons[i].GetComponent<Image>().color = currentCards[i].cardColor;
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
        
        thinkingRate.OnCardSelected();
        cardPanel.SetActive(false);
    }
}
