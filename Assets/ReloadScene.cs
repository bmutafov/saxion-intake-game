using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour {

	public string scene;

	public void Reload()
	{
		SceneManager.LoadScene(scene);
	}
}
