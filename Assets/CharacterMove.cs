using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
	public float characterSpeed = 0.1f;

	private CharacterController cc;
	private Animator animator;

	private void Start ()
	{
		animator = GetComponent<Animator>();
		cc = GetComponent<CharacterController>();
	}

	private void Update ()
	{
		float xAxis = Input.GetAxis("Horizontal");
		float zAxis = -Input.GetAxis("Vertical");
		Vector3 normalized = new Vector3(zAxis, 0, xAxis);
		normalized.Normalize();
		Vector3 direction = normalized * characterSpeed;
		cc.Move(direction);
		transform.position = new Vector3(transform.position.x, 0, transform.position.z);
		if ( direction != Vector3.zero )
			transform.rotation = Quaternion.LookRotation(direction * Time.deltaTime);

		if ( cc.velocity.magnitude != 0 )
		{
			
			animator.SetBool("isRunning", true);
		}
		else
		{
			animator.SetBool("isRunning", false);
		}
	}
}
