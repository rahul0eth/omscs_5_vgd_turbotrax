using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Rotator : MonoBehaviour
{
	public int rotationSpeed = 5;

    void Update()
	{
		// Rotate the game object that this script is attached to by 15 in the X axis,
		// 30 in the Y axis and 45 in the Z axis, multiplied by deltaTime in order to make it per second
		// rather than per frame.
		transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime * rotationSpeed);
	}
}
