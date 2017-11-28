using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeRayCast : MonoBehaviour {
	Vector3 direction;
	string name;
	void Update () {
		Cast ();
	}
	public void CreateNode(GameObject obj, Vector3 position, Vector3 dir, string nameDir){
		Instantiate (obj, position, Quaternion.identity);
		direction = dir;
		name = nameDir;
	}
	public void Cast(){
		Vector3 dir = transform.TransformDirection (direction);
		if (Physics.Raycast (transform.position, direction, 10)) {
			print ("i hit something, i'm direction: " + name);
		}
	}
}
