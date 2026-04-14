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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            targetRotation *= Quaternion.Euler(0, -90f, 0);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            targetRotation *= Quaternion.Euler(0, 90f, 0);
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
}