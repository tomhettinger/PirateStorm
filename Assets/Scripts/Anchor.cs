using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Anchor : MonoBehaviour {

	private AnchorCollider maleAnchor;
	private AnchorCollider femaleAnchor;


	void Awake () {
		foreach (AnchorCollider anchorCollider in GetComponentsInChildren<AnchorCollider>()) {
			if (anchorCollider.gender == "male")
				maleAnchor = anchorCollider;
			else if (anchorCollider.gender == "female")
				femaleAnchor = anchorCollider;
		}
	}


	public AnchorCollider GetMaleAnchor () {
		return maleAnchor;
	}


	public AnchorCollider GetFemaleAnchor () {
		return femaleAnchor;
	}

	
	// Return a list of all game objects that are correctly locked with this anchor.
	public List<GameObject> GetLockedList () {
		List<GameObject> lockedList = new List<GameObject> ();
		// Find an anchor that this male is connected to
		foreach (AnchorCollider otherFemaleAnchor in maleAnchor.GetAttachedFemales()) {
			Anchor otherAnchor = otherFemaleAnchor.GetComponentInParent<Anchor>();
			// Check if this female is also connected to the other anchor's male point
			if (femaleAnchor.GetAttachedMales().Contains(otherAnchor.GetMaleAnchor())) {
				if (!lockedList.Contains(otherAnchor.gameObject))
					lockedList.Add(otherAnchor.gameObject);
			}
		}
		return lockedList;
	}
	
}


    /*
	private MaleAnchor maleAnchor;
	private FemaleAnchor femaleAnchor;


	void Start () {
		foreach (MaleAnchor anchor in GetComponentsInChildren<MaleAnchor>()) {
			maleAnchor = anchor;
		}
		foreach (FemaleAnchor anchor in GetComponentsInChildren<FemaleAnchor>()) {
			femaleAnchor = anchor;
		}
	}


	public MaleAnchor GetMaleAnchor () {
		return maleAnchor;
	}


	public FemaleAnchor GetFemaleAnchor () {
		return femaleAnchor;
	}



	// Return a list of all game objects that are correctly locked with this anchor.
	public List<GameObject> GetLockedList () {
		
		List<GameObject> lockedList = new List<GameObject> ();

		// Find an anchor that this male is connected to
		foreach (FemaleAnchor otherFemaleAnchor in maleAnchor.GetAttachedFemales()) {
			Anchor otherAnchor = otherFemaleAnchor.GetComponentInParent<Anchor>();
			
			// Check if this female is also connected to the other anchor's male point
			if (femaleAnchor.GetAttachedMales().Contains(otherAnchor.GetMaleAnchor())) {
				if (!lockedList.Contains(otherAnchor.gameObject))
					lockedList.Add(otherAnchor.gameObject);
			}
		}
		
		return lockedList;
	}
	*/




