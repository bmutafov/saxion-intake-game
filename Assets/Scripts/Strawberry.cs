using UnityEngine;

public class Strawberry : Pickable
{
	public GameObject particles;

	public override void PickUp ( PlayerStats st )
	{
		DisableMeshes();
		st.CanSkipWalls = true;
		var part = Instantiate(particles, transform.position, transform.rotation);
		StartCoroutine(DestroyObject(part, 2));
	}
}
