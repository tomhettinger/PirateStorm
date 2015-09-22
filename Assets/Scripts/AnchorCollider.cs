using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnchorCollider : MonoBehaviour {
	public string gender;

	private List<AnchorCollider> collidingFemales = new List<AnchorCollider>();
	private List<AnchorCollider> collidingMales = new List<AnchorCollider>();
	private string femaleAnchorTag = "female_anchor_collision";
	private string maleAnchorTag = "male_anchor_collision";
	
	
	void OnTriggerEnter (Collider other) {
		if (other.tag == femaleAnchorTag)
			collidingFemales.Add (other.gameObject.GetComponent<AnchorCollider>());
		
		if (other.tag == maleAnchorTag)
			collidingMales.Add (other.gameObject.GetComponent<AnchorCollider>());
	}

	
	void OnTriggerExit (Collider other) {
		if (other.tag == femaleAnchorTag)
			collidingFemales.Remove (other.gameObject.GetComponent<AnchorCollider>());
		
		if (other.tag == maleAnchorTag)
			collidingMales.Remove (other.gameObject.GetComponent<AnchorCollider>());
	}
	
	
	public List<AnchorCollider> GetAttachedFemales () {
		return collidingFemales;
	}
	
	
	public List<AnchorCollider> GetAttachedMales () {
		return collidingMales;
	}
}
