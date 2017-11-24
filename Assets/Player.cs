using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]

public class Player: MonoBehaviour
{

	public float speed = 10.0f;
	public float gravity = 10.0f;
	public float maxVelocityChange = 10.0f;
	public bool canJump = true;
	public float jumpHeight = 2.0f;
	private bool grounded = false;
	private Rigidbody rb;
	private Animator animator;

	private void Start ()
	{
		rb = GetComponent<Rigidbody>();
		animator = GetComponent<Animator>();
	}

	void FixedUpdate ()
	{
		if ( grounded )
		{
			// Calculate how fast we should be moving
			float xInput = Input.GetAxis("Horizontal");
			float zInput = Input.GetAxis("Vertical");
			if(xInput != 0 || zInput != 0)
			{
				animator.SetBool("isRunning", true);
			}
			else
			{
				animator.SetBool("isRunning", false);
			}
			Vector3 targetVelocity = new Vector3(xInput, 0, zInput);
			targetVelocity = transform.TransformDirection(targetVelocity);
			targetVelocity *= speed;

			// Apply a force that attempts to reach our target velocity
			Vector3 velocity = rb.velocity;
			Vector3 velocityChange = (targetVelocity - velocity);
			velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
			velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
			velocityChange.y = 0;
			rb.AddForce(velocityChange, ForceMode.VelocityChange);

			// Jump
			if ( canJump && Input.GetButton("Jump") )
			{
				rb.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
			}
		}

		// We apply gravity manually for more tuning control
		rb.AddForce(new Vector3(0, -gravity * rb.mass, 0));

		grounded = false;
	}

	void OnCollisionStay ()
	{
		grounded = true;
	}

	float CalculateJumpVerticalSpeed ()
	{
		// From the jump height and gravity we deduce the upwards speed 
		// for the character to reach at the apex.
		return Mathf.Sqrt(2 * jumpHeight * gravity);
	}
}