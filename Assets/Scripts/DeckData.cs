using System;

[System.Serializable]
public class DeckData
{
    public int[] cardAmountStatus;

    public DeckData(int[] cardAmount)
    {
        cardAmountStatus = cardAmount;
    }
}