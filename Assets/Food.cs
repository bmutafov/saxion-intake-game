using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {

	public GameObject particles;
	public float healAmount = 20;

	private void OnTriggerEnter ( Collider other )
	{
		if(other.CompareTag("Player"))
		{
			StartCoroutine(DestroyThis(other.GetComponent<PlayerStats>()));
		}
	}

	private IEnumerator DestroyThis(PlayerStats st)
	{
		foreach ( var child in transform.GetComponentsInChildren<MeshRenderer>() )
		{
			child.enabled = false;
		}
		GetComponent<BoxCollider>().enabled = false;
		st.AddHp(healAmount);
		var part = Instantiate(particles, transform.position, transform.rotation);
		yield return new WaitForSeconds(2);
		

		Destroy(part.gameObject);
		Destroy(gameObject);
	}
}
