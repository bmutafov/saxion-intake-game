using TMPro;
using UnityEngine;

public class DisplayPickUpMessage : MonoBehaviour
{
    public GameObject popUpCanvas;
    public TextMeshProUGUI text;

    public void PopUp(string message)
    {
        popUpCanvas.SetActive(true);
        text.text = message;
    }

    public void DeactivatePopUp()
    {
        popUpCanvas.SetActive(false);
    }
}