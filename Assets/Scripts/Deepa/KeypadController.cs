using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;


public class KeypadController : MonoBehaviour
{

    public DoorController doorController;
    public TMP_Text textMeshProText;
    public int maxCharacterLimit = 5;
    private string currentInput = ""; 
    public Transform player;
    public float interactionDistance = 5f; 
    private bool playerNearKeypad = false;
    private bool isDoorOpen = false;

    private void Start()
    {
        //TextMeshPro Text component reference
        textMeshProText = GetComponentInChildren<TMP_Text>();
        
    }

    private void Update()
    {

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
       // Debug.Log("Distance to Player" + distanceToPlayer);
        if (distanceToPlayer <= interactionDistance)
        {
            playerNearKeypad = true;
        }
        else
        {
            playerNearKeypad = false;
            currentInput = "";
        }

        if (playerNearKeypad && !isDoorOpen)
        {

            // Check for keyboard input 

            Keyboard keyboard = Keyboard.current;

            if (keyboard != null)
            {

                if (textMeshProText.text.Length < maxCharacterLimit)
                {

                    if (keyboard.digit0Key.wasPressedThisFrame)
                    {
                        currentInput += "0";

                    }
                    else if (keyboard.digit1Key.wasPressedThisFrame)
                    {
                        currentInput += "1";
                    }
                    else if (keyboard.digit2Key.wasPressedThisFrame)
                    {
                        currentInput += "2";
                    }
                    else if (keyboard.digit3Key.wasPressedThisFrame)
                    {
                        currentInput += "3";
                    }
                    else if (keyboard.digit4Key.wasPressedThisFrame)
                    {
                        currentInput += "4";
                    }
                    else if (keyboard.digit5Key.wasPressedThisFrame)
                    {
                        currentInput += "5";
                    }
                    else if (keyboard.digit6Key.wasPressedThisFrame)
                    {
                        currentInput += "6";
                    }
                    else if (keyboard.digit7Key.wasPressedThisFrame)
                    {
                        currentInput += "7";
                    }
                    else if (keyboard.digit8Key.wasPressedThisFrame)
                    {
                        currentInput += "8";
                    }
                    else if (keyboard.digit9Key.wasPressedThisFrame)
                    {
                        currentInput += "9";
                    }
                }
                if (keyboard.backspaceKey.wasPressedThisFrame)
                {

                    // backspace to remove the last character
                    if (currentInput.Length > 0)
                    {
                        currentInput = currentInput.Substring(0, currentInput.Length - 1);
                    }
                }

                if (keyboard.enterKey.wasPressedThisFrame)
                {
                    //Debug.Log("Length is" + textMeshProText.text.Length);
                    if (textMeshProText.text.Length == maxCharacterLimit && validateCode())
                    {
                        isDoorOpen = true;
                        currentInput = "OPEN";
                        doorController.OpenDoor();
                        
                    }
                    else
                    {
                        currentInput = "WRONG";
                    }

                }

            }
        }

        // Update TextMeshPro Text component
        textMeshProText.text = currentInput;
        
        
    }


    public bool validateCode()
    {
        if (currentInput == "12345")
        {
            return true;
        }
        return false;
    }



}



