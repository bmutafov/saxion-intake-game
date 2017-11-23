using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
	public GameObject mainCamera;

	private static Camera cameraD;

	private void Start ()
	{
		cameraD = mainCamera.GetComponent<Camera>();
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
        Vector3 screenPos = cameraD.WorldToScreenPoint(position);
        screenPos = new Vector2(screenPos.x + x, screenPos.y + y);
        toMove.GetComponent<RectTransform>().position = screenPos;
    }
}