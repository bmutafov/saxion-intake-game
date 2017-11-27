using UnityEngine;

public class Apple : Pickable, IPickUp
{

	public GameObject particles;
	public float healAmount = 20;

	public override void PickUp ( PlayerStats st )
	{
		DisableMeshes();

		st.AddHp(healAmount);
		var part = Instantiate(particles, transform.position, transform.rotation);
		StartCoroutine(DestroyObject(part, 2));
	}
}
