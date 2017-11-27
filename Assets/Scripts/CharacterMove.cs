using System.Collections;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
	public float characterSpeed = 0.1f;
	public float turnSpeed = 10;
	public GameObject particles;

	public bool CanSkipWalls = false;

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
		if ( CanSkipWalls && Input.GetButtonDown("Fire1") )
		{
			RaycastHit hit;

			if ( Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100) )
			{
				if ( hit.transform.CompareTag("Floor") && Vector3.Distance(transform.position, hit.point) < 5 )
				{
					CanSkipWalls = false;
					StartCoroutine(TeleportPlayer(hit));
				}
			}
		}
	}

	private IEnumerator TeleportPlayer ( RaycastHit hit )
	{
		ChangeRenderesState(false);
		var currentPosPart = Instantiate(particles, transform.position, Quaternion.identity);

		yield return new WaitForSeconds(0.5f);

		var newPosPart = Instantiate(particles, hit.point, Quaternion.identity);

		yield return new WaitForSeconds(0.2f);

		ChangeRenderesState(true);
		transform.position = hit.point;

		yield return new WaitForSeconds(2f);

		Destroy(currentPosPart);
		Destroy(newPosPart);
	}

	private void ChangeRenderesState (bool state)
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
			targetRotation = Quaternion.LookRotation(direction * Time.deltaTime * 100f);
		}
		transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, (float)(charController.velocity.magnitude > 1 ? turnSpeed * 1.5 : turnSpeed) * Time.deltaTime);
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
