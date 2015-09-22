using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TemporaryPieceController : MonoBehaviour {

	public Material invalidPlacementMat, validPlacementMat, selectorMat, linkedMat;

	private bool canPlace;
	private Piece piece;
	private GraphNode gn;


	public bool CanPlace () {
		return canPlace;
	}

	void Awake() {
		piece = GetComponent<Piece> ();
		gn = GetComponentInChildren<GraphNode> ();
	}

	void Start () {
		canPlace = false;
	}


	void Update () {
		// if currently overlapping with another bridge, set to red and break
		if (piece.InvalidLocation ()) {
			piece.ChangeMaterial (invalidPlacementMat);
			canPlace = false;
			return;
		}
		// if not, check to see that we are currently locked in with at least one other piece.
		else if (piece.CurrentlyLockedWithOther ()) {
			piece.ChangeMaterial (validPlacementMat);
			// Finally, check to see if we have an island in our graph network.
			if (gn.LinkedToIsland ()) {
				piece.ChangeMaterial (linkedMat);
				canPlace = true;
			}
			return;
		}
		// otherwise, we are freely floating
		piece.ChangeMaterial (selectorMat);
		canPlace = false;
	}

}
