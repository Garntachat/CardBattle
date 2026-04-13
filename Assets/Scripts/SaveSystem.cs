using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveDeck(int[] cardAmount)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.fun";

        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            DeckData data = new DeckData(cardAmount);
            formatter.Serialize(stream, data);
        }

        Debug.Log("deck saved at: " + path);
    }

    public static DeckData LoadDeck()
    {
        string path = Application.persistentDataPath + "/player.fun";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                DeckData data = formatter.Deserialize(stream) as DeckData;
                return data;
            }
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

}
