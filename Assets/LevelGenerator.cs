using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

	[SerializeField]
	private List<GameObject> backgroundImages;
	[SerializeField]
	private int mapWidth = 10;
	[SerializeField]
	private int mapHight = 10;
	[SerializeField]
	private int textureDistance = 5;

	private void Start ()
	{
		for ( int w = 0 ; w < mapWidth ; w++ )
		{
			for ( int h = 0 ; h < mapHight ; h++ )
			{
				GameObject randomBG = backgroundImages[Random.Range(0, backgroundImages.Count)];
				Instantiate(randomBG, new Vector3(w * textureDistance, h * textureDistance, -1), Quaternion.identity);
			}
		}
	}
}
