using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	public float distance = 10;
	public float xOffset = 5;
	public float smoothTime = 0.5f;
	private Vector3 velocity = Vector3.zero;


	private void LateUpdate ()
	{
		transform.position = Vector3.Lerp(transform.position, target.position + new Vector3(xOffset, distance, 0), smoothTime * Time.deltaTime);
		//transform.position = Vector3.SmoothDamp(transform.position, target.position + new Vector3(xOffset, distance, 0), ref velocity, smoothTime);
		//transform.position = target.position + new Vector3(xOffset, distance, 0);
	}
}
