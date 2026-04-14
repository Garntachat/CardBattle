using UnityEngine;

public class ChangeWorldColor : MonoBehaviour
{
    public Material bossColor;
    public Material normalColor;

    public GameObject[] objectsToChangeColor;

    private bool isBossActive = false;


    void Update()
    {
        GameObject boss = GameObject.FindWithTag("Boss");

        if (boss != null && !isBossActive)
        {
            ChangeColor(bossColor);
            isBossActive = true;
        }
        else if (boss == null && isBossActive)
        {
            ChangeColor(normalColor);
            isBossActive = false;
        }
    }

    void ChangeColor(Material newMat)
    {
        foreach (GameObject obj in objectsToChangeColor)
        {
            Renderer rend = obj.GetComponent<Renderer>();
            if (rend != null)
            {
                rend.material = newMat;
            }
        }
    }
}