using UnityEngine;
using System.Collections;

public class Banana : Pickable, IPickUp
{
	public GameObject particles;

	public float speedBoost = 2;
	public float timeSpeedBoost = 4;

	private float normalSpeed;

	public override void PickUp ( PlayerStats st )
	{
		DisableMeshes();

		var part = Instantiate(particles, transform.position, transform.rotation);

		normalSpeed = st.Speed;
		st.Speed *= speedBoost;

		StartCoroutine(ReturnBaseStats(st, part));
	}

	private IEnumerator ReturnBaseStats ( PlayerStats st, GameObject parts )
	{
		yield return StartCoroutine(DestroyObject(parts, 2));

		st.Speed = normalSpeed;
	}
}
