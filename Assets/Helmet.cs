using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Item/Helmet")]
public class Helmet : Item
{

    public override void PickUp()
    {
        FindObjectOfType<PlayerStats>().helmet = this;
    }
}
