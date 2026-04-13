using UnityEngine;
using TMPro;
public class LoadDeck : MonoBehaviour
{
    public TMP_Text[] Amount;

    public TMP_Text info;
    void Start()
    {
        DeckData data = SaveSystem.LoadDeck();

        if (data != null)
        {
            for (int i = 0; i < Amount.Length; i++)
            {
                Amount[i].text = data.cardAmountStatus[i].ToString();
            }
        }
    }

    public void ResetDeck()
    {
            int[] startingDeck = new int[] { 3, 1, 1, 3, 1, 1 };
            SaveSystem.SaveDeck(startingDeck);
            for (int i = 0; i < Amount.Length; i++)
            {
                Amount[i].text = startingDeck[i].ToString();
            }
    }
    public void SaveDeck()
    {
            int ttcard = 0;
            int[] newDeck = new int[] { 0, 0, 0, 0, 0, 0 };
            for (int i = 0; i < Amount.Length; i++)
            {
                newDeck[i] = int.Parse(Amount[i].text);
                ttcard += newDeck[i];
                
            }
            Debug.Log(ttcard);
            if(ttcard == 10)
        {
            SaveSystem.SaveDeck(newDeck);
            info.text = "saved";
        }
        else
        {
            info.text = "not 10";
        }
            

    }

    private int GetCurrentTotal()
    {
        int total = 0;
        for (int i = 0; i < Amount.Length; i++)
        {
            int val;
            if (int.TryParse(Amount[i].text, out val))
            {
                total += val;
            }
        }
        return total;
    }
    
    private void UpdateTotalUI()
    {
        int currentTotal = GetCurrentTotal();
        info.text = "Total: " + currentTotal + " / 10";

        if (currentTotal == 10) info.color = Color.green;
        else info.color = Color.white;
    }

public void Increase(int index)
{
    if (GetCurrentTotal() >= 10)
        {
            info.text = "Full Deck (Max 10)";
            info.color = Color.red;
            return;
        } 
    int value;
    if (int.TryParse(Amount[index].text, out value))
    {
        Amount[index].text = (value + 1).ToString();
        UpdateTotalUI();
    }
}

public void Decrease(int index)
{
    int value;
    if (int.TryParse(Amount[index].text, out value))
    {
        value = Mathf.Max(0, value - 1); // prevent negative
        Amount[index].text = value.ToString();
        UpdateTotalUI();
    }
}

   
}
