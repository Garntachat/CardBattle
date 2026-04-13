using UnityEngine;
using System.Collections.Generic;

public class CardManager : MonoBehaviour
{
    [Header("Card Pool")]
    public List<CardData> allCards = new List<CardData>();

    [Header("Settings")]
    public int drawCount = 3;

    public ThinkingRate thinkingRate;
    private List<CardData> drawnCards = new List<CardData>();
    [Header("Card References (Index = Card Type)")]
    public List<CardData> allCardsReference = new List<CardData>();

    private void Shuffle(List<CardData> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            CardData temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    public List<CardData> DrawCards()
    {   
        BuildDeckFromSave(); // ← build deck first
        drawnCards.Clear();
        List<CardData> tempPool = new List<CardData>(allCards);
        Shuffle(tempPool);
        for (int i = 0; i < drawCount && i < tempPool.Count; i++)
        {
            drawnCards.Add(tempPool[i]);
        }
        return drawnCards;
    }

    public CardData SelectCard(int index)
    {
        if (index < 0 || index >= drawnCards.Count)
        {
            Debug.LogWarning("Invalid card index!");
            return null;
        }
        CardData selected = drawnCards[index];
        thinkingRate.OnCardSelected();
        return selected;
    }

    void BuildDeckFromSave()
{
    DeckData data = SaveSystem.LoadDeck();

    if (data == null)
    {
        Debug.LogWarning("No saved deck found!");
        return;
    }

    allCards.Clear();

    for (int i = 0; i < data.cardAmountStatus.Length; i++)
    {
        int amount = data.cardAmountStatus[i];

        for (int j = 0; j < amount; j++)
        {
            allCards.Add(allCardsReference[i]); 
        }
    }
}
}
