using UnityEngine;

public class DecalProjector : MonoBehaviour {

	public Transform decal;

	private void Update ()
	{
		var dist = 10;
		var dir = new Vector3(0, -1, 0);
		//edit: to draw ray also//
		Debug.DrawRay(transform.position, dir * dist, Color.green);
		//end edit//
		RaycastHit hit;
		if ( Physics.Raycast(transform.position, dir, out hit, dist) )
		{
			decal.position = hit.point;
		}
	}
}
