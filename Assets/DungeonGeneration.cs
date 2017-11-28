using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGeneration : MonoBehaviour {
	public GameObject cube;
	public GameObject rayCastPoint;
	public int roomMinimum;
	public int roomMaximum;
	public Node node;
	public List<Node> nodes = new List<Node>();
	public Vector3 startPos = new Vector3(0, 0, 0);
	void Start () {
		
	}
	
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			CreateRoom ();
		}
		if (Input.GetKey (KeyCode.E)) {
			CheckSpace ();
		}
	}
	public void CreateRoom(){
		nodes.Clear ();
		int randomLength = Random.Range (roomMinimum, roomMaximum);
		int randomWidth = Random.Range (roomMinimum, roomMaximum);
		if (randomLength % 2 == 0 || randomWidth % 2 == 0) {
			CreateRoom ();
			return;
		}
		for (int x = 0; x < randomWidth; x++) {
			for (int y = 0; y < randomLength; y++) {
				GameObject obj = Instantiate (cube, new Vector3 (x + startPos.x, y + startPos.y, 0), Quaternion.identity);
				if (x == 0 && y == 0) {
					startPos = obj.transform.position;
				}
			}
		}
		Node down = new Node (new Vector3 (randomWidth / 2 + startPos.x, 0 - 1 + startPos.y, 0), Vector3.down, "down");
		Node left = new Node (new Vector3 (0 - 1 + startPos.x, randomLength / 2 + startPos.y, 0), Vector3.left, "left"); 
		Node up = new Node (new Vector3 (randomWidth / 2 + startPos.x, randomLength + startPos.y, 0), Vector3.up, "up");
		Node right = new Node (new Vector3 (randomWidth + startPos.x, randomLength / 2 + startPos.y, 0), Vector3.right, "right");
		nodes.Add (down);
		nodes.Add (left);
		nodes.Add (up);
		nodes.Add (right);
	}
	public void CheckSpace(){
		List<Node> availableSpots = new List<Node> ();
		for (int i = 0; i < nodes.Count; i++) {
			Vector3 dir = transform.TransformDirection (nodes [i].GetDirection ());
			if (Physics.Raycast (nodes [i].GetPosition (), dir, 10)) {
				print ("Node " + nodes [i].GetName () + "hit something");
			} else {
				availableSpots.Add (nodes [i]);
			}
		}
		int randPlace = Random.Range (0, availableSpots.Count - 1);
		startPos = nodes [randPlace].GetPosition ();
		CreateRoom ();
	}
}
