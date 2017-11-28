using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{

	private static Camera mainCamera;

	private void Start ()
	{
		mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
	}

	public static void MoveUIToGameObjectPosition ( GameObject toMove, Vector3 position )
	{
		MoveUIToGameObjectPosition(toMove, position, 0, 0);
	}

	/// <summary>
	/// Moves a RectTransform to a gameObjects position
	/// </summary>
	/// <param name="toMove">UI's GameObject</param>
	/// <param name="position">Position of the GameObject</param>
	/// <param name="x">X offset</param>
	/// <param name="y">Y offset</param>
	public static void MoveUIToGameObjectPosition (GameObject toMove, Vector3 position, float x, float y)
    {
        Vector3 screenPos = mainCamera.WorldToScreenPoint(position);
        screenPos = new Vector2(screenPos.x + x, screenPos.y + y);
        toMove.GetComponent<RectTransform>().position = screenPos;
    }
}