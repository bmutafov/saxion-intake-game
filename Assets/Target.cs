using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour
{
	private void Start ()
	{
		SphereCollider sphc = gameObject.AddComponent<SphereCollider>();
		sphc.isTrigger = true;
		sphc.radius = 0.5f;
	}

	private void OnTriggerEnter ( Collider other )
	{
		Debug.Log("Won!");
	}
}
