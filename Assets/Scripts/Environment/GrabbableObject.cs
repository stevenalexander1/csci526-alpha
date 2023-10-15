    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    [SerializeField] private int itemValue = 0;
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
        
    }
}
