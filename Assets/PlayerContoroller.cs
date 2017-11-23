using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoroller : MonoBehaviour
{

	public float playerSpeed = 2;
	public float timeToPickUp = 3;

	[SerializeField]
	public GameObject pickUpCanvas;

	[SerializeField]
	private float walkSpeed = 5;
	private Rigidbody2D rb;
	private SpriteRenderer spriteRenderer;
	private Animator animator;

	private bool isWalking = false;
	private bool canPickUp = false;

	private bool pressed = false;
	private float timePassed = 0;
	private Animator pickUpAnimator;

	private void Start ()
	{
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		pickUpAnimator = pickUpCanvas.GetComponentInChildren<Animator>();

		pickUpAnimator.speed = 1/timeToPickUp;
	}

	private void Update ()
	{
		if ( pressed )
		{
			timePassed += Time.deltaTime;
			if(timePassed > timeToPickUp)
			{
				pickUpAnimator.SetBool("fill", false);
				pickUpAnimator.SetBool("interupt", false);
				pressed = false;
				Debug.Log("Picked Up!");
			}
		}

		if ( canPickUp )
		{
			if ( Input.GetKeyDown("e") )
			{
				timePassed = 0;
				pressed = true;
				pickUpAnimator.SetBool("fill", true);
				pickUpAnimator.SetBool("interupt", false);
			}
			else if ( Input.GetKeyUp("e"))
			{
				pressed = false;
				if(timePassed < timeToPickUp)
				{
					pickUpAnimator.SetBool("fill", false);
					pickUpAnimator.SetBool("interupt", true);
				}
			}
		}
	}

	void FixedUpdate ()
	{
		var curSpeed = walkSpeed;
		var maxSpeed = curSpeed;


		// Move senteces
		float xAxis = Input.GetAxis("Horizontal");
		float yAxis = Input.GetAxis("Vertical");


		PlayAnimation(xAxis, yAxis);

		var xLerp = Mathf.Lerp(0, xAxis * curSpeed, 0.8f);

		if ( xAxis < 0 && transform.position.x < 5 ) xLerp = 0;

		var yLerp = Mathf.Lerp(0, yAxis * curSpeed, 0.8f);
		rb.velocity = new Vector2(xLerp, yLerp);

		FlipSprite(xLerp);
	}

	private void FlipSprite ( float xLerp )
	{
		if ( xLerp > 0 )
		{
			spriteRenderer.flipX = false;
		}
		else if ( xLerp < 0 )
		{
			spriteRenderer.flipX = true;
		}
	}

	private void PlayAnimation ( float xAxis, float yAxis )
	{
		if ( (xAxis != 0 || yAxis != 0) && !isWalking )
		{
			isWalking = true;
			animator.SetBool("isWalking", isWalking);
		}
		if ( xAxis == 0 && yAxis == 0 && isWalking )
		{
			isWalking = false;
			animator.SetBool("isWalking", isWalking);
		}
	}

	private void OnTriggerEnter2D ( Collider2D collision )
	{
		canPickUp = true;
		pickUpCanvas.SetActive(true);
		UI.MoveUIToGameObjectPosition(pickUpCanvas.transform.GetChild(0).gameObject, transform.position, -10, 70);
	}

	private void OnTriggerExit2D ( Collider2D collision )
	{
		pickUpAnimator.SetBool("fill", false);
		pickUpAnimator.SetBool("interupt", false);

		pickUpCanvas.SetActive(false);
		Debug.Log("Exited.");
		canPickUp = false;
	}
}
