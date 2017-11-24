using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillBar : MonoBehaviour {

	public GameObject player;


	private PlayerStats st;
	private Image img;

	private void Start ()
	{
		st = player.GetComponent<PlayerStats>();
		img = GetComponent<Image>();
	}

	private void LateUpdate ()
	{
		img.fillAmount = st.Hp / st.maxHP;
	}
}
