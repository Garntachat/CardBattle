using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public float rotationSpeed = 10f;
    private Quaternion targetRotation;

    void Start()
    {
        targetRotation = transform.rotation;
    }

    void Update()
    {
        // Press Q to turn 90 degrees Left
        if (Input.GetKeyDown(KeyCode.Q))
        {
            targetRotation *= Quaternion.Euler(0, -90f, 0);
        }
        // Press E to turn 90 degrees Right
        else if (Input.GetKeyDown(KeyCode.E))
        {
            targetRotation *= Quaternion.Euler(0, 90f, 0);
        }

        // Smoothly rotate the character to the new angle
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
}