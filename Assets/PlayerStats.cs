using UnityEngine;

public class PlayerStats : MonoBehaviour
{
	public float maxHP = 100;
	public float hpTakeSpeed = 3;

	public GameObject gameOver;

	private bool takeDamage = true;
	private float _hp;

	public float Hp
	{
		get
		{
			return _hp;
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
		if ( takeDamage )
			_hp -= Time.deltaTime * hpTakeSpeed;
		if ( _hp <= 0 )
		{
			gameOver.SetActive(true);
			takeDamage = false;
		}
	}
}
