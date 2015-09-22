using UnityEngine;
using System.Collections;

public class DropperController : MonoBehaviour {

	public SelectorWheel selectorWheel;
	public float wheelHeight;
	public float camRayLength = 10000f;

	private int floorMask;


	void Awake() {
		floorMask = LayerMask.GetMask ("Floor");
	}


	void Update () {
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit floorHit;
		if (Physics.Raycast (camRay, out floorHit, Mathf.Infinity, floorMask)) {
			Vector3 targetPos = new Vector3 (floorHit.point.x, wheelHeight, floorHit.point.z);
			selectorWheel.transform.position = SnapToGrid(targetPos);
		}
	}


	// Given a Vector3 position, round to integer values and return the rounded value.
	Vector3 SnapToGrid(Vector3 pos) {
		return new Vector3 (Mathf.Round (pos.x), Mathf.Round (pos.y), Mathf.Round (pos.z));
	}
}
