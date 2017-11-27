using UnityEngine;

public class Target : MonoBehaviour
{
	private void Start ()
	{
		AddEndGameTrigger();
	}

	private void OnTriggerEnter ( Collider other )
	{
		PlayerStats playerStats = FindObjectOfType<PlayerStats>();
		playerStats.TakeDamage = false;
		playerStats.winScreen.SetActive(true);
	}

	private void AddEndGameTrigger ()
	{
		SphereCollider sphereTrigger = gameObject.AddComponent<SphereCollider>();
		sphereTrigger.isTrigger = true;
		sphereTrigger.radius = 0.5f;
	}
}
