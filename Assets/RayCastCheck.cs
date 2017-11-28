using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastCheck : MonoBehaviour {

	void Start () {
		
	}
	
	void CheckRay (Vector3 dir) {
		if (Physics.Raycast (transform.position, dir, 10)) {
			print ("hitting");
		}
	}
}
