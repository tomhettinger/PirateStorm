using UnityEngine;
using System.Collections;

public class UnitPiece : Piece {
	public int health;
	public int attack;

	void FixedUpdate () {
		if (health <= 0)
			Die();
	}

	void Die () {
		Debug.Log ("This unit is dead.");
		Destroy (gameObject);
	}
}
