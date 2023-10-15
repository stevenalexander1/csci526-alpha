using System.Collections;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float openDuration = 2.0f; // Duration of the door opening animation
    private Vector3 initialScale; // Initial scale of the door object
    private bool isOpening = false; // Flag to track if the door is opening

    private void Start()
    {
        initialScale = transform.localScale; 
    }

    public void OpenDoor()
    {
        if (!isOpening)
        {
            isOpening = true;
            StartCoroutine(ScaleDoor());
        }
    }

    private IEnumerator ScaleDoor()
    {
        float elapsedTime = 0f;
        Vector3 targetScale = new Vector3(initialScale.x, 0, initialScale.z);

        while (elapsedTime < openDuration)
        {
            // Calculate the scale based on the elapsed time
            float t = elapsedTime / openDuration;
            Vector3 newScale = Vector3.Lerp(initialScale, targetScale, t);

            // Set the door's scale
            transform.localScale = newScale;

            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            yield return null;
        }
        transform.localScale = Vector3.zero;
        isOpening = false;
    }
}
