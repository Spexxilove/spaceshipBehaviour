using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour {

	[SerializeField]
	private GameObject targetObj;

	Vehicle vehicle;

	// Use this for initialization
	void Start () {
		vehicle = new Vehicle_Plane (this.gameObject, targetObj);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){
		vehicle.updatePosition (Time.fixedDeltaTime);
	}
}

