using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour {

	public string mainScene;
	public GameObject character;

	private Button button;

	private void Start ()
	{
		button = GetComponent<Button>();
		button.onClick.AddListener(() => StartCoroutine(LoadSceneAsync()));
	}

	private IEnumerator LoadSceneAsync ()
	{
		AsyncOperation async = SceneManager.LoadSceneAsync(mainScene);
		character.GetComponent<Animator>().SetBool("isRunning", true);

		yield return async;
	}
}
