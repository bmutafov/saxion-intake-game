using UnityEngine;

public class FollowDecal : MonoBehaviour
{

	public Transform decal;

	private void Update ()
	{
		UI.MoveUIToGameObjectPosition(gameObject, decal.position, 40, 40);
	}
}
