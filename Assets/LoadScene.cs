using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void LoadMainMenu()
    {
    SceneManager.LoadScene(0);
    }
    public void LoadEditDeck()
    {
    SceneManager.LoadScene(3);
    }
}
