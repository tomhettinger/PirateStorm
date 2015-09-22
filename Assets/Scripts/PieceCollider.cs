using UnityEngine;
using System.Collections;

public class PieceCollider : MonoBehaviour {
	
	private int intersecting = 0;
	
	void OnTriggerEnter (Collider other) {
		if (other.tag == "piece_collision")
			intersecting++;
	}
	
	void OnTriggerExit (Collider other) {
		if (other.tag == "piece_collision")
		    intersecting--;
	}
	
	public bool IsCollidingWithPiece () {
		if (intersecting > 0)
			return true;
		else
			return false;
	}
	
}