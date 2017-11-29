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
	public List<Vector3> corridorPositions = new List<Vector3> ();
	public GameObject corridor;
	public Vector3 startPos = new Vector3(0, 0, 0);
	public bool dungeonEnd = false;
	void Start () {
		
	}
	
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			CreateRoom ();
		}
		if (Input.GetKey (KeyCode.E)) {
			CheckSpace ();
		}
		if (Input.GetKeyDown (KeyCode.D)) {
			CreateCorridor ();
		}
	}
	public void CreateRoom(){
		if (!dungeonEnd) {
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
						corridorPositions.Add (new Vector3(startPos.x + randomWidth / 2, startPos.y + randomLength / 2, 0));
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
			//CheckSpace ();
		}
	}
	public void CheckSpace(){
		List<Node> availableSpots = new List<Node> ();
		for (int i = 0; i < nodes.Count; i++) {
			Vector3 dir = transform.TransformDirection (nodes [i].GetDirection ());
			Debug.DrawRay (nodes [i].GetPosition (), dir, Color.green);
			if (!Physics.Raycast (nodes [i].GetPosition (), dir, roomMaximum)) {
				availableSpots.Add (nodes [i]);
			}
		}
		if (availableSpots.Count == 0) {
			dungeonEnd = true;
			print ("Done!");
		}
		int randPlace = Random.Range (0, availableSpots.Count);
		switch (availableSpots [randPlace].GetName()) {
		case "right":
			startPos = startPos + new Vector3 (roomMaximum, 0, 0);
			break;
		case "left":
			startPos = startPos - new Vector3 (roomMaximum, 0, 0);
			break;
		case "down":
			startPos = startPos - new Vector3 (0, roomMaximum, 0);
			break;
		case "up":
			startPos = startPos + new Vector3 (0, roomMaximum, 0);
			break;
		default:
			break;
		}
		CreateRoom ();
	}
	public void CreateCorridor(){
		float t = 0;
		for (int i = 1; i < corridorPositions.Count; i++) {
			while (t <= 1) {
				t += 0.15f;
				print ("asdsds");
				Vector3 dir = Vector3.Lerp (corridorPositions [i], corridorPositions [i - 1], t);
				Instantiate (corridor, dir, Quaternion.identity);
			}
			t = 0;
		}
	}
}
