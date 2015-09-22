using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Piece : MonoBehaviour {
	private List<Anchor> anchorList = new List<Anchor>();
	private GraphNode graphNode;
	private Component[] renderers;

	public List<Anchor> GetAnchorList() {
		return anchorList;
	}
	
	
	public GraphNode GetGraphNode() {
		return graphNode;
	}
	
	
	void Awake () {
		foreach (Anchor anchor in GetComponentsInChildren<Anchor>()) {
			anchorList.Add(anchor);
		}
		graphNode = GetComponentInChildren<GraphNode> ();
		renderers = GetComponentsInChildren<Renderer> ();
	}
	
	
	// Return a list of all game objects that are currently locked with this object.
	public List<GameObject> FindListOfLockedPieces () {
		List<GameObject> lockedList = new List<GameObject> ();
		foreach (Anchor anchor in anchorList) {
			lockedList.AddRange (anchor.GetLockedList ());
		}
		return lockedList;
	}
	
	
	// Return true if this bridge piece is currently locked to another piece.
	public bool CurrentlyLockedWithOther () {
		if (FindListOfLockedPieces ().Count > 0)
			return true;
		else
			return false;
	}
	
	
	// Return true if this piece is colliding with another bridge
	public bool InvalidLocation () {
		foreach (PieceCollider collider in GetComponentsInChildren<PieceCollider>()) {
			if (collider.IsCollidingWithPiece ())
				return true;
		}
		return false;
	}
	
	
	// Change the material for all child objects that have a renderer to the requested material.
	public void ChangeMaterial (Material newMat) {
		foreach (Renderer rend in renderers) {
			rend.sharedMaterial = newMat;
		}
	}
	
}