using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGeneration : MonoBehaviour {
	public GameObject cube;
	public GameObject rayCastPoint;
	public int roomMinimum;
	public int roomMaximum;
	public Node node;
	public List<Node> tempNodes = new List<Node>();
	public List<Node> nodes = new List<Node> ();
	public List<Vector3> roomPositions = new List<Vector3> ();
	public GameObject corridor;
	public Vector3 startPos = new Vector3(0, 0, 0);
	public bool dungeonEnd = false;
	public int randomLength;
	public int randomWidth;
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
			tempNodes.Clear ();
			 randomLength = Random.Range (roomMinimum, roomMaximum);
			 randomWidth = Random.Range (roomMinimum, roomMaximum);
			if (randomLength % 2 == 0 || randomWidth % 2 == 0) {
				CreateRoom ();
				return;
			}
			for (int x = 0; x < randomWidth; x++) {
				for (int y = 0; y < randomLength; y++) {
					GameObject obj = Instantiate (cube, new Vector3 (x + startPos.x, y + startPos.y, 0), Quaternion.identity);
					if (x == 0 && y == 0) {
						startPos = obj.transform.position;
						roomPositions.Add (startPos);
					}
				}
			}
			Node down = new Node (new Vector3 (randomWidth / 2 + startPos.x, 0 - 1 + startPos.y, 0), Vector3.down, "down");
			Node left = new Node (new Vector3 (0 - 1 + startPos.x, randomLength / 2 + startPos.y, 0), Vector3.left, "left"); 
			Node up = new Node (new Vector3 (randomWidth / 2 + startPos.x, randomLength + startPos.y, 0), Vector3.up, "up");
			Node right = new Node (new Vector3 (randomWidth + startPos.x, randomLength / 2 + startPos.y, 0), Vector3.right, "right");
			tempNodes.Add (down);
			tempNodes.Add (left);
			tempNodes.Add (up);
			tempNodes.Add (right);
			CheckSpace ();
		}
	}
	public void CheckSpace(){
		List<Node> availableSpots = new List<Node> ();
		float randomSpawnDistance = Random.Range(roomMaximum, roomMaximum * 1.3f);
		for (int i = 0; i < tempNodes.Count; i++) {
			Vector3 dir = transform.TransformDirection (tempNodes [i].GetDirection ());
			if (!Physics.Raycast (tempNodes [i].GetPosition (), dir, randomSpawnDistance)) {
				availableSpots.Add (tempNodes [i]);
			}
		}
		if (availableSpots.Count == 0) {
			dungeonEnd = true;
			CreateCorridor ();
			return;
		}
		int randPlace = Random.Range (0, availableSpots.Count);
		switch (availableSpots [randPlace].GetName()) {
		case "right":
			startPos = startPos + new Vector3 (randomSpawnDistance, 0, 0);
			break;
		case "left":
			startPos = startPos - new Vector3 (randomSpawnDistance, 0, 0);
			break;
		case "down":
			startPos = startPos - new Vector3 (0, randomSpawnDistance, 0);
			break;
		case "up":
			startPos = startPos + new Vector3 (0, randomSpawnDistance, 0);
			break;
		default:
			break;
		}
		nodes.Add (availableSpots [randPlace]);
		CreateRoom ();
	}
	public void CreateCorridor(){
		for (int i = 1; i < nodes.Count; i++) {
			float t = 0;
			while (t <= 1) {
				switch (nodes [i - 1].GetName ()) {
				case "down":
					Vector3 dir = Vector3.Lerp (nodes [i - 1].GetPosition (), new Vector3(roomPositions [i].x + randomWidth / 2, roomPositions[i].y + randomLength, 0), t);
					Instantiate (corridor, dir, Quaternion.identity);

					break;
				case "left":
					Vector3 dir3 = Vector3.Lerp (nodes [i - 1].GetPosition (), new Vector3(roomPositions [i].x + randomWidth, roomPositions[i].y + randomLength / 2, 0), t);
					Instantiate (corridor, dir3, Quaternion.identity);
					break;
				case "up":
					Vector3 dir2 = Vector3.Lerp (nodes [i - 1].GetPosition (), new Vector3(roomPositions [i].x + randomWidth / 2, roomPositions[i].y, 0), t);
					Instantiate (corridor, dir2, Quaternion.identity);
					break;
				case "right":
					Vector3 dir1 = Vector3.Lerp (nodes [i - 1].GetPosition (), new Vector3(roomPositions [i].x, roomPositions[i].y + randomLength / 2, 0), t);
					Instantiate (corridor, dir1, Quaternion.identity);
					break;
				default:
					break;
				}
				t += 0.1f;
			}
		}
	}
}
