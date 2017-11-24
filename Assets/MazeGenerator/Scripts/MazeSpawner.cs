using UnityEngine;
using System.Collections;

//<summary>
//Game object, that creates maze and instantiates it in scene
//</summary>
public class MazeSpawner : MonoBehaviour
{
	public enum MazeGenerationAlgorithm
	{
		PureRecursive,
		RecursiveTree,
		RandomTree,
		OldestTree,
		RecursiveDivision,
	}
	public Material targetMaterial;
	public MazeGenerationAlgorithm Algorithm = MazeGenerationAlgorithm.PureRecursive;
	public bool FullRandom = false;
	public int RandomSeed = 12345;
	public GameObject Floor = null;
	public GameObject Wall = null;
	public GameObject Pillar = null;
	public int Rows = 5;
	public int Columns = 5;
	public float CellWidth = 5;
	public float CellHeight = 5;
	public bool AddGaps = true;
	public GameObject[] GoalPrefab = null;

	private BasicMazeGenerator mMazeGenerator = null;

	public delegate void OnMazeGenerated ();
	public OnMazeGenerated onMazeGenerated;

	void Start ()
	{
		if ( !FullRandom )
		{
			Random.seed = RandomSeed;
		}
		switch ( Algorithm )
		{
			case MazeGenerationAlgorithm.PureRecursive:
				mMazeGenerator = new RecursiveMazeGenerator(Rows, Columns);
				break;
			case MazeGenerationAlgorithm.RecursiveTree:
				mMazeGenerator = new RecursiveTreeMazeGenerator(Rows, Columns);
				break;
			case MazeGenerationAlgorithm.RandomTree:
				mMazeGenerator = new RandomTreeMazeGenerator(Rows, Columns);
				break;
			case MazeGenerationAlgorithm.OldestTree:
				mMazeGenerator = new OldestTreeMazeGenerator(Rows, Columns);
				break;
			case MazeGenerationAlgorithm.RecursiveDivision:
				mMazeGenerator = new DivisionMazeGenerator(Rows, Columns);
				break;
		}
		mMazeGenerator.GenerateMaze();
		for ( int row = 0 ; row < Rows ; row++ )
		{
			for ( int column = 0 ; column < Columns ; column++ )
			{
				float x = column*(CellWidth+(AddGaps?.2f:0));
				float z = row*(CellHeight+(AddGaps?.2f:0));
				MazeCell cell = mMazeGenerator.GetMazeCell(row,column);
				GameObject tmp;
				tmp = Instantiate(Floor, new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0)) as GameObject;
				tmp.transform.parent = transform;
				tmp.name = "Floor" + new Vector3(x, 0, z);
				if ( cell.WallRight )
				{
					tmp = Instantiate(Wall, new Vector3(x + CellWidth / 2, 0, z) + Wall.transform.position, Quaternion.Euler(0, 90, 0)) as GameObject;// right
					tmp.transform.parent = transform;
					tmp.name = "Wall" + new Vector3(x + CellWidth / 2, 0, z);

				}
				if ( cell.WallFront )
				{
					tmp = Instantiate(Wall, new Vector3(x, 0, z + CellHeight / 2) + Wall.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;// front
					tmp.transform.parent = transform;
					tmp.name = "Wall" + new Vector3(x, 0, z + CellHeight / 2);

				}
				if ( cell.WallLeft )
				{
					tmp = Instantiate(Wall, new Vector3(x - CellWidth / 2, 0, z) + Wall.transform.position, Quaternion.Euler(0, 270, 0)) as GameObject;// left
					tmp.transform.parent = transform;
					tmp.name = "Wall" + new Vector3(x - CellWidth / 2, 0, z);

				}
				if ( cell.WallBack )
				{
					tmp = Instantiate(Wall, new Vector3(x, 0, z - CellHeight / 2) + Wall.transform.position, Quaternion.Euler(0, 180, 0)) as GameObject;// back
					tmp.transform.parent = transform;
					tmp.name = "Wall" + new Vector3(x, 0, z - CellHeight / 2);

				}
				if ( cell.IsGoal && GoalPrefab.Length > 0 && Random.Range(1,3) == 2)
				{
					tmp = Instantiate(GoalPrefab[Random.Range(0, GoalPrefab.Length)], new Vector3(x, 1, z), Quaternion.Euler(0, 0, 0)) as GameObject;
					tmp.transform.parent = transform;
				}

			}
		}
		if ( Pillar != null )
		{
			for ( int row = 0 ; row < Rows + 1 ; row++ )
			{
				for ( int column = 0 ; column < Columns + 1 ; column++ )
				{
					float x = column*(CellWidth+(AddGaps?.2f:0));
					float z = row*(CellHeight+(AddGaps?.2f:0));
					GameObject tmp = Instantiate(Pillar,new Vector3(x-CellWidth/2,0,z-CellHeight/2),Quaternion.identity) as GameObject;
					tmp.transform.parent = transform;
				}
			}
		}
		SelectTarget();
	}

	private void SelectTarget ()
	{
		var floorPos = new Vector3(Rows * CellWidth - CellWidth, 0, Columns * CellHeight - CellHeight);
		string gameObjectName = "Floor" + floorPos;
		var floorTarget = GameObject.Find(gameObjectName);
		Debug.Log(floorTarget.name);
		Debug.Log("aa");

		floorTarget.tag = "Target";
		floorTarget.GetComponent<Renderer>().material = targetMaterial;
		floorTarget.AddComponent<Target>();
	}
}
