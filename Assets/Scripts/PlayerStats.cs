using UnityEngine;

public class PlayerStats : MonoBehaviour
{
	public float maxHP = 100;
	public float hpTakeSpeed = 3;

	public GameObject gameOver;
	public GameObject winScreen;

	private bool takeDamage = true;
	private float _hp;
	public float Speed
	{
		get
		{
			return GetComponent<CharacterMove>().characterSpeed;
		}

		set
		{
			GetComponent<CharacterMove>().CharacterSpeed = value;
		}
	}

	public float Hp
	{
		get
		{
			return _hp;
		}
	}

	public bool TakeDamage
	{
		set
		{
			takeDamage = value;
		}
	}

	public bool CanSkipWalls
	{
		get
		{
			return GetComponent<CharacterMove>().CanSkipWalls;
		}
		set
		{
			GetComponent<CharacterMove>().CanSkipWalls = value;
		}
	}

	public void AddHp ( float hp )
	{
		_hp = Mathf.Clamp(_hp + hp, 0, maxHP);
	}

	private void Start ()
	{
		_hp = maxHP;
	}

	private void Update ()
	{
		TakeDamageOverTime();

		if ( _hp <= 0 )
		{
			gameOver.SetActive(true);
			takeDamage = false;
		}
	}

	private void TakeDamageOverTime ()
	{
		if ( takeDamage )
		{
			_hp -= Time.deltaTime * hpTakeSpeed;
		}
	}

	public void Reset ()
	{
		Speed = 0.1f;
		_hp = maxHP;
		TakeDamage = true;
	}
}
