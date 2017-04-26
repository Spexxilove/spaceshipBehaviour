using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// contains physical attributes of the vehicle
public class Vehicle {

	protected GameObject vehicleObj;

	protected float maxSpeed;
	protected float maxForce;
	protected float maxRollRate;
	protected float maxPitchRate;

	protected VehicleBehaviour behaviour;

	public Vehicle(){
	}

	public Vehicle(GameObject go){
		vehicleObj = go;

	}

	public Vector3 getPosition(){
		return vehicleObj.transform.position;
	}

	public Transform getTransform(){
		return vehicleObj.transform;
	}

	public float getMaxSpeed(){
		return maxSpeed;
	}

	public Vector3 getCurrentVelocity(){
		Rigidbody rb = vehicleObj.GetComponent (typeof(Rigidbody)) as Rigidbody;
		return rb.velocity;
	}

	public virtual Vector3 updatePosition(float timestep){
		return Vector3.zero;
	}
}
