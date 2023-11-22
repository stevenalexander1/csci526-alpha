using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollRangeController : MonoBehaviour
{
    public ScrollRect scrollRect;
    public float minY = 0f; // Minimum vertical scroll position
    public float maxY = 100f; // Maximum vertical scroll position

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get the current position of the content
        Vector3 contentPosition = scrollRect.content.localPosition;

        // Clamp the vertical position within the specified range
        float clampedY = Mathf.Clamp(contentPosition.y, minY, maxY);

        // Update the content position
        scrollRect.content.localPosition = new Vector3(contentPosition.x, clampedY, contentPosition.z);
    }
}
