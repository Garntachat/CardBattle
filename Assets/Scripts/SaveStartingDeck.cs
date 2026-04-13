using UnityEngine;

public class SaveStartingDeck : MonoBehaviour
{
    void Start()
    {
        DeckData data = SaveSystem.LoadDeck();

        // Only create starting deck if NO save exists
        if (data == null)
        {
            int[] startingDeck = new int[] { 3, 1, 1, 3, 1, 1 };
            SaveSystem.SaveDeck(startingDeck);
        }
    }
}