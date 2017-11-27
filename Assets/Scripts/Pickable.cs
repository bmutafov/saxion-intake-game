using UnityEngine;
using System.Collections;
using cakeslice;

public abstract class Pickable : MonoBehaviour
{
	private void OnTriggerEnter ( Collider other )
	{
		if ( other.CompareTag("Player") )
		{
			PickUp(other.GetComponent<PlayerStats>());
		}
	}

	protected void DisableMeshes ()
	{
		transform.GetChild(0).GetComponent<Outline>().enabled = false;
		foreach ( var child in transform.GetComponentsInChildren<MeshRenderer>() )
		{
			child.enabled = false;
		}
		GetComponent<BoxCollider>().enabled = false;
	}

	protected virtual IEnumerator DestroyObject ( GameObject parts, float time )
	{
		yield return new WaitForSeconds(time);

		Destroy(parts.gameObject);
		Destroy(gameObject);
	}

	public abstract void PickUp ( PlayerStats st );
}
