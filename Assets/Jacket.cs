using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Item/Jacket")]
public class Jacket : Item
{
    public override void PickUp ()
    {
        FindObjectOfType<PlayerStats>().jacket = this;
    }
}
