using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary {
	public float xMin, xMax, zMin, zMax;
	public float orthoMin, orthoMax;
}

public class CameraControls : MonoBehaviour {
	public float speed;
	public Boundary boundary;
	public float cameraHeight;

	void Update () {
		// Zoom camera in
		if (Input.mouseScrollDelta.y > 0)
			Camera.main.orthographicSize -= 1;
		// Zoom camera out
		else if (Input.mouseScrollDelta.y < 0)
			Camera.main.orthographicSize += 1;

		Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, boundary.orthoMin, boundary.orthoMax);
	}

	void FixedUpdate () {
		// Get camera input and move it
		float moveHorizontal = 0.0f;
		if (Input.GetKey ("right"))
			moveHorizontal = 1.0f;
		else if (Input.GetKey ("left"))
			moveHorizontal = -1.0f;
		float moveVertical = 0.0f;
		if (Input.GetKey ("up"))
			moveVertical = 1.0f;
		else if (Input.GetKey ("down"))
			moveVertical = -1.0f;
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		Rigidbody rb = GetComponent<Rigidbody> ();
		rb.velocity = movement * speed;
		rb.position = new Vector3 (
			Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax),
			cameraHeight,
			Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
			);
	}
}