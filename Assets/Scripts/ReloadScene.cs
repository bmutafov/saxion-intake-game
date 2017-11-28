using TMPro;
using UnityEngine;

public class ReloadScene : MonoBehaviour
{
	public TextMeshProUGUI levelUI;
	public int level = 1;
	public string scene;

	public MazeSpawner maze;

	private void Start ()
	{
		DisplayNewLevelOnUI();
	}

	public void Reload ()
	{
		level = 1;
		LoadLevel();
	}

	public void NextLevel ()
	{
		level++;
		LoadLevel();
	}

	private void LoadLevel ()
	{
		ResetPlayerStatsAndPosition();

		UpdateMazeSize();

		DestroyOldMaze();

		maze.SpawnMaze();

		DisplayNewLevelOnUI();
	}

	private void UpdateMazeSize ()
	{
		maze.Rows = maze.Columns = 10 + level;
	}

	private void DestroyOldMaze ()
	{
		for ( int i = 0 ; i < maze.transform.childCount ; i++ )
		{
			Destroy(maze.transform.GetChild(i).gameObject);
		}
	}

	private static void ResetPlayerStatsAndPosition ()
	{
		FindObjectOfType<CharacterController>().transform.position = new Vector3(1, 0, 1);
		FindObjectOfType<PlayerStats>().Reset();
	}

	private void DisplayNewLevelOnUI ()
	{
		levelUI.text = "Level " + level;
	}

	

}
