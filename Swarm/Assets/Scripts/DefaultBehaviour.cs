using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultBehaviour : VehicleBehaviour {


	private Vector3 target;
	private Vehicle vehicle;


	public DefaultBehaviour(Vehicle vehicle){
		target = vehicle.getPosition();
		this.vehicle = vehicle;
	}

	public void setTaget(Vector3 newTarget){
		target = newTarget;
	}

	//calculate all active steering behaviours and return resulting force
	public override Vector3 calculate(){
		Vector3 steeringForce = Vector3.zero;
		//steeringForce += seek (target);
		steeringForce += arrive (target);
		return steeringForce;
	}




	//seek on target point
	public Vector3 seek(Vector3 targetPos){
		Vector3 desiredV = vehicle.getMaxSpeed() * (Vector3.Normalize (targetPos - vehicle.getPosition()));
		return desiredV - vehicle.getCurrentVelocity ();
	}

//	//flee from target
//	public Vector3 flee(){
//
//		return null;
//	}

	//similar to seek but decelerates close to target
	public Vector3 arrive(Vector3 targetPos){
		Vector3 currentPos = vehicle.getPosition ();

		//reduce force when close
		float desiredSpeed = Mathf.Min((targetPos - currentPos).magnitude,  vehicle.getMaxSpeed());

		//stop within threshold distance
		desiredSpeed = desiredSpeed < 3 ? 0.0f : desiredSpeed;

		Vector3 desiredV =desiredSpeed * (Vector3.Normalize (targetPos - currentPos));
		return desiredV - vehicle.getCurrentVelocity ();
	}

	//pursuit
	//evade
	//wander
	//obsticle avoidance
	//
}
