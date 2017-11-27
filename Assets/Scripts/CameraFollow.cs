using UnityEngine;

public class CameraFollow : MonoBehaviour
{

	public Transform target;
	public float distance = 10;
	public float xOffset = 5;
	public float smoothTime = 0.5f;
	private Vector3 velocity = Vector3.zero;


	private void LateUpdate ()
	{
		Vector3 newPosition = target.position + new Vector3(xOffset, distance, 0);
		transform.position = Vector3.Lerp(transform.position, newPosition, smoothTime * Time.deltaTime);
	}
}
