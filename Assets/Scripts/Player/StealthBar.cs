using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StealthBar : MonoBehaviour
{
  

    [SerializeField] private PlayerCharacter playerCharacter;

    [SerializeField] private Slider _stealthSlider;
    // Start is called before the first frame update
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void SetMaxStealth(float maxStealth)
    {
        _stealthSlider.maxValue = maxStealth;
        _stealthSlider.value = maxStealth;
    }
    
    public void SetStealth(float stealth)
    {
        _stealthSlider.value = stealth;
    }

    

}