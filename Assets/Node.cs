using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node{
	Vector3 position;
	Vector3 direction;
	string name;
	public Node(Vector3 pos, Vector3 dir, string nm){
		this.position = pos;
		this.direction = dir;
		this.name = nm;
	}
	public Vector3 GetDirection(){
		return direction;
	}
	public Vector3 GetPosition(){
		return position;
	}
	public string GetName(){
		return name;
	}
}