using System.Collections;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
	public float characterSpeed = 0.1f;
	public float turnSpeed = 10;
	public float inMotionMultiplier = 2;
	public GameObject particles;
	public GameObject decal;
	public GameObject decalInfo;

	private bool canSkipWalls = false;
	public bool CanSkipWalls
	{
		get
		{
			return canSkipWalls;
		}

		set
		{
			decal.SetActive(value);
			decalInfo.SetActive(PlayerPrefs.HasKey("helpInfo") ? false : value);
			if ( !PlayerPrefs.HasKey("helpInfo") )
			{
				PlayerPrefs.SetInt("helpInfo", 1);
			}
			canSkipWalls = value;
		}
	}

	private CharacterController charController;
	private Animator animator;
	private Vector3 direction = Vector3.zero;
	private Quaternion targetRotation;
	private SkinnedMeshRenderer[] renderers;

	public float CharacterSpeed
	{
		set
		{
			characterSpeed = value;
			animator.SetFloat("speed", characterSpeed * 10);
		}
	}

	private void Start ()
	{
		renderers = GetComponentsInChildren<SkinnedMeshRenderer>();
		animator = GetComponent<Animator>();
		charController = GetComponent<CharacterController>();
		targetRotation = transform.rotation;
	}

	private void Update ()
	{
		direction = NormalizedDirection();
		charController.Move(direction * characterSpeed);
		transform.position.Set(transform.position.x, 10, transform.position.z);

		SkippWalls();

		RotateCharacter(direction);

		PlayAnimation();
	}

	private void SkippWalls ()
	{
		if ( CanSkipWalls && Input.GetKeyDown("e") )
		{
			CanSkipWalls = false;
			StartCoroutine(TeleportPlayer(decal.transform.position));
		}
	}

	private IEnumerator TeleportPlayer ( Vector3 position )
	{
		ChangeRenderesState(false);
		var currentPosPart = Instantiate(particles, transform.position, Quaternion.identity);

		yield return new WaitForSeconds(0.5f);

		var newPosPart = Instantiate(particles, position, Quaternion.identity);

		yield return new WaitForSeconds(0.2f);

		ChangeRenderesState(true);
		transform.position = position;

		yield return new WaitForSeconds(2f);

		Destroy(currentPosPart);
		Destroy(newPosPart);
	}

	private void ChangeRenderesState ( bool state )
	{
		for ( int i = 0 ; i < renderers.Length ; i++ )
		{
			renderers[i].enabled = state;
		}
	}

	private Vector3 NormalizedDirection ()
	{
		float xAxis = Input.GetAxis("Horizontal");
		float zAxis = -Input.GetAxis("Vertical");

		return new Vector3(zAxis, 0, xAxis).normalized;
	}

	private void RotateCharacter ( Vector3 direction )
	{
		if ( direction != Vector3.zero )
		{
			targetRotation = Quaternion.LookRotation(direction);
		}
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, (charController.velocity.magnitude > 5 ? turnSpeed * inMotionMultiplier : turnSpeed) * Time.deltaTime);
	}

	private void PlayAnimation ()
	{
		if ( charController.velocity.magnitude != 0 )
		{

			animator.SetBool("isRunning", true);
		}
		else
		{
			animator.SetBool("isRunning", false);
		}
	}
}
