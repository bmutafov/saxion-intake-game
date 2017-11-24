using UnityEngine;
using System.Collections;

public class TargetSpawner : MonoBehaviour
{
	public Material targetMaterial;

	private MazeSpawner maze;

	private void Start ()
	{
		maze.onMazeGenerated += SelectTarget;
	}
	private void Awake ()
	{
		maze = GetComponent<MazeSpawner>();
	}

	private void SelectTarget()
	{
		var floorPos = new Vector3(maze.Rows * maze.CellWidth - maze.CellWidth, 0, maze.Columns * maze.CellHeight - maze.CellHeight);
		string gameObjectName = "Floor" + floorPos;
		var floorTarget = GameObject.Find(gameObjectName);
		Debug.Log(floorTarget.name);
		Debug.Log("aa");

		floorTarget.tag = "Target";
		floorTarget.GetComponent<Renderer>().material = targetMaterial;
		floorTarget.AddComponent<Target>();
	}
}
