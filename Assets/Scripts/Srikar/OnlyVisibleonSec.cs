using UnityEngine;
using TMPro;
using Cinemachine;

public class OnlyVisibleonSec : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera gameobject1;
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private string activeText = "";
    [SerializeField] private string inactiveText = "Press 'R' to re-enter First Person View";

    void Update()
    {
        if (gameobject1.isActiveAndEnabled)
        {
            textMesh.text = activeText;
        }
        else
        {
            textMesh.text = inactiveText;
        }
    }
}