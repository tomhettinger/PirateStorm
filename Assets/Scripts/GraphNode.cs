using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GraphNode : MonoBehaviour {

	public bool isAnIsland;


	// List of graphNodes that are attached to this through a lock (can show up twice if connected via 2 locks.
	public List<GraphNode> Neighbors () {
		List<GraphNode> neighbors = new List<GraphNode>();
		foreach (GameObject lockedPiece in gameObject.GetComponent<Piece> ().FindListOfLockedPieces ()) {
			neighbors.Add(lockedPiece.GetComponentInParent<GraphNode>());
		}
		return neighbors;
	}


	// Search for the first island found and return true. Otherwise return false.
	// This is a breadth first search.
	public bool LinkedToIsland () {
		GraphNode start = gameObject.GetComponent<GraphNode> ();
		Queue<GraphNode> frontier = new Queue<GraphNode> ();
		frontier.Enqueue (start);
		Dictionary<GraphNode, bool> visited = new Dictionary<GraphNode, bool>();
		visited[start] = true;
		
		GraphNode current;
		while (frontier.Count > 0) {
			current = frontier.Dequeue();
			if (current.isAnIsland)
				return true;
			
			foreach (GraphNode node in current.Neighbors()) {
				//Debug.Log("Looking at neighbor: " + node + " to see if we've visited already.");
				if (!visited.ContainsKey(node)) {
					frontier.Enqueue(node);
					visited[node] = true;
				}
			}
		}
		return false;
	}

}
