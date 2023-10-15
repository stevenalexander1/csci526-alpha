    using System.Collections;
using System.Collections.Generic;
    using TMPro;
    using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    [SerializeField] private int itemValue = 0;
    [SerializeField] private GameObject itemValueText;
    private bool _isGrabbed = false;

    public int ItemValue
    {
        get => itemValue;
        set => itemValue = value;
    }
    
    public bool IsGrabbed
    {
        get => _isGrabbed;
        set => _isGrabbed = value;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FaceCamera();
    }
    
    private void FaceCamera()
    {
        // Have the item value text face the opposite direction of the camera
        itemValueText.transform.LookAt(Camera.main.transform);
        itemValueText.transform.Rotate(0, 180, 0);
    }
}
