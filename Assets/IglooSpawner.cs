using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IglooSpawner : MonoBehaviour {

	[SerializeField]
	private GameObject iglooPrefab;

	private void Start ()
	{
		int iglooCount = Random.Range(10, 20);
		for ( int i = 0 ; i < iglooCount ; i++ )
		{
			GameObject igloo = Instantiate(iglooPrefab, new Vector2(Random.Range(6, 55), Random.Range(6, 55)), Quaternion.identity);
			igloo.tag = "igloo";
		}
	}
}
