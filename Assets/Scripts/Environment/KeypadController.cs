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
    [SerializeField]
    private string validCode = "12345";
    private bool _displayMessage = false;

    private void Start()
    {
        //TextMeshPro Text component reference
        textMeshProText = GetComponentInChildren<TMP_Text>();
        
    }

    private IEnumerator ValidationMessage(string msg)
    {
        WaitForSeconds wait = new WaitForSeconds(2);
        _displayMessage = true;
        currentInput = msg;
        yield return wait;
        currentInput = "";
        _displayMessage = false;
    }

    private void Update()
    {
        if (_displayMessage) return;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
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
                    if (textMeshProText.text.Length == maxCharacterLimit && validateCode())
                    {
                        isDoorOpen = true;
                        StartCoroutine(ValidationMessage("OPEN"));
                        doorController.OpenDoor();
                        
                    }
                    else
                    {
                        StartCoroutine(ValidationMessage("WRONG"));
                    }

                }

            }
        }

        // Update TextMeshPro Text component
        textMeshProText.text = currentInput;
        
        
    }


    public bool validateCode()
    {
        if (currentInput == validCode)
        {
            return true;
        }
        return false;
    }



}



