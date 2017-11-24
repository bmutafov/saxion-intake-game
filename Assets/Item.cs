using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject {
    public Sprite sprite;
    public string inteName;

    public abstract void PickUp ();

    public static IEnumerator DisplayMessage (string message)
    {
        DisplayPickUpMessage displayPickUpMessage = FindObjectOfType<DisplayPickUpMessage>();
        displayPickUpMessage.PopUp(message);

        yield return new WaitForSeconds(2);

        displayPickUpMessage.DeactivatePopUp();
    }
}
