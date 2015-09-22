using UnityEngine;
using System.Collections;

public class WheelOption : MonoBehaviour {
	
	private GameObject currentPiece;
	private GameObject splashEffect;


	void Start () {
		splashEffect = gameObject.GetComponentInParent<SelectorWheel>().splashEffect;
	}


	void FixedUpdate () {
		if (currentPiece != null) {
			currentPiece.transform.position = transform.position;
		}
	}


	// Create an instance of a piece at this quad location
	public void CreateBridgePiece (GameObject piece) {
		currentPiece = Instantiate (piece, transform.position, Quaternion.identity) as GameObject;
	}


	// Rotate this piece 90 degrees
	public void RotatePiece () {
		currentPiece.transform.Rotate (0, 90, 0);
		currentPiece.GetComponent<AudioSource> ().Play();
	}

	public void DropPiece () {
		currentPiece.transform.position = new Vector3 (transform.position.x, 0.0f, transform.position.z);
		Instantiate (splashEffect, new Vector3 (transform.position.x, 0.0f, transform.position.z), Quaternion.identity);
		currentPiece = null;
	}

	public GameObject GetCurrentPiece () {
		return currentPiece;
	}


	public void SetCurrentPiece(GameObject piece) {
		currentPiece = piece;
	}


	// Given a Vector3 position, round to integer values and return the rounded value.
	Vector3 SnapToGrid(Vector3 pos) {
		return new Vector3 (Mathf.Round (pos.x), Mathf.Round (pos.y), Mathf.Round (pos.z));
	}


	public void ChangeMaterial (Material newMat) {
		Component[] renderers = currentPiece.GetComponentsInChildren<Renderer> ();
		foreach (Renderer rend in renderers) {
			rend.sharedMaterial = newMat;
		}
	}

}
