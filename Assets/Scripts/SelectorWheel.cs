using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SelectorOptions {
	public WheelOption option_Q;
	public WheelOption option_W;
	public WheelOption option_E;
	public WheelOption option_A;
	public WheelOption option_S;
	public WheelOption option_D;
	public WheelOption centerOption;
}

public class SelectorWheel : MonoBehaviour {

	public GameObject[] possibleBridgePieces;
	public GameObject splashEffect;
	public Material invalidPlacementMat, validPlacementMat, selectorMat, linkedMat, standardMat;

	private WheelOption option_Q, option_W, option_E, option_A, option_S, option_D, centerOption;
	public SelectorOptions wheelOptions;

	private char holeLocation = 'x';
	private Dictionary<char, WheelOption> optionDict = new Dictionary<char, WheelOption>();
	private Dictionary<string, char> nameDict = new Dictionary<string, char>();
	
	void Awake () {
		nameDict.Add ("Option_Q", 'q');
		nameDict.Add ("Option_W", 'w');
		nameDict.Add ("Option_E", 'e');
		nameDict.Add ("Option_A", 'a');
		nameDict.Add ("Option_S", 's');
		nameDict.Add ("Option_D", 'd');

		optionDict.Add ('q', wheelOptions.option_Q);
		optionDict.Add ('w', wheelOptions.option_W);
		optionDict.Add ('e', wheelOptions.option_E);
		optionDict.Add ('a', wheelOptions.option_A);
		optionDict.Add ('s', wheelOptions.option_S);
		optionDict.Add ('d', wheelOptions.option_D);

		centerOption = wheelOptions.centerOption;
	}

	void Start () {
		foreach (char thisChar in "qweasd") {
			optionDict[thisChar].CreateBridgePiece(possibleBridgePieces[Random.Range(0, possibleBridgePieces.Length)]);
			optionDict[thisChar].ChangeMaterial(selectorMat);
		}
	}


	void Update () {
		// Rotate the block
		if (Input.GetMouseButtonDown (1) && centerOption.GetCurrentPiece () != null)
			centerOption.RotatePiece ();
		
		// Place the block
		if (Input.GetMouseButtonDown (0) && centerOption.GetCurrentPiece () != null) {
			bool canPlace = centerOption.GetCurrentPiece().GetComponent<TemporaryPieceController> ().CanPlace ();
			if (canPlace) {
				// Set current center selection to null (drop the piece).
				RemoveTemporaryPieceController(centerOption);
				centerOption.ChangeMaterial (standardMat);
				centerOption.DropPiece();
				// Get random piece to fill the hole in the option that this was originally pulled from.
				optionDict[holeLocation].CreateBridgePiece(possibleBridgePieces[Random.Range(0, possibleBridgePieces.Length)]);
				optionDict[holeLocation].ChangeMaterial(selectorMat);
			}
		}
		
		// Switch out current selection for a different piece.
		if (Input.GetKeyDown ("q")) {
			SwapCurrentPiece('q');
		}
		if (Input.GetKeyDown ("w")) {
			SwapCurrentPiece('w');
		}
		if (Input.GetKeyDown ("e")) {
			SwapCurrentPiece('e');
		}
		if (Input.GetKeyDown ("a")) {
			SwapCurrentPiece('a');
		}
		if (Input.GetKeyDown ("s")) {
			SwapCurrentPiece('s');
		}
		if (Input.GetKeyDown ("d")) {
			SwapCurrentPiece('d');
		}
	}

	void SwapCurrentPiece (char newChar) {
		// Set foo piece with the new selection
		GameObject fooPiece = optionDict [newChar].GetCurrentPiece ();

		// Set option piece with current center piece
		optionDict [newChar].SetCurrentPiece (centerOption.GetCurrentPiece ());
		if (optionDict [newChar].GetCurrentPiece () != null) {
			RemoveTemporaryPieceController (optionDict [newChar]);
			optionDict [newChar].ChangeMaterial (selectorMat);
		}
		else
			holeLocation = newChar;

		// Set center piece with foo piece
		centerOption.SetCurrentPiece (fooPiece);
		if (centerOption.GetCurrentPiece () != null)
			AttachTemporaryPieceController (centerOption);
		else
			holeLocation = 'x';
	}


	// Attach the temporary materials and script to the current selection piece.
	void AttachTemporaryPieceController (WheelOption option) {
		option.GetCurrentPiece().AddComponent <TemporaryPieceController>();
		option.GetCurrentPiece().GetComponent <TemporaryPieceController>().invalidPlacementMat = invalidPlacementMat;
		option.GetCurrentPiece().GetComponent <TemporaryPieceController>().validPlacementMat = validPlacementMat;
		option.GetCurrentPiece().GetComponent <TemporaryPieceController>().selectorMat = selectorMat;
		option.GetCurrentPiece().GetComponent <TemporaryPieceController>().linkedMat = linkedMat;
	}

	void RemoveTemporaryPieceController(WheelOption option) {
		Destroy(option.GetCurrentPiece ().GetComponent <TemporaryPieceController> ());
	}
}
