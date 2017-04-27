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
		//steeringForce += arrive (target);
		//steeringForce += flee (target);
		steeringForce = wander ();
		return steeringForce;
	}




	//seek on target point
	public Vector3 seek(Vector3 targetPos){
		Vector3 desiredV = vehicle.getMaxSpeed() * (Vector3.Normalize (targetPos - vehicle.getPosition()));
		return desiredV - vehicle.getCurrentVelocity ();
	}

	//flee from target
	public Vector3 flee(Vector3 targetPos){
		Vector3 desiredV = vehicle.getMaxSpeed() * (Vector3.Normalize ( vehicle.getPosition()-targetPos));
		return desiredV - vehicle.getCurrentVelocity ();
	}

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



	//second atempt at wander
	private Vector3 wanderTarget = Vector3.zero;
	private float setNewTargetProbability = 0.005f;
	private int maxTurnTime  = 3000; // max amount of updates to turn in new direction
	private int currentTurnTime=0; // remaining updates to turn
	private int turnTime  = 0; // time for this  turn
	public Vector3 wander(){
		if(wanderTarget == Vector3.zero){ // first call of wander
			wanderTarget= vehicle.getTransform().forward;
		}

		if (Random.value <= setNewTargetProbability) {// set new target direction
			wanderTarget = Random.onUnitSphere;
			turnTime = Random.Range(0,maxTurnTime);
			currentTurnTime = turnTime;
		}
			
		// seek current target direction
		if (currentTurnTime > 1) {
			currentTurnTime--;
			return ( vehicle.getCurrentVelocity ()* currentTurnTime - wanderTarget * vehicle.getMaxSpeed () * (turnTime - currentTurnTime))/turnTime;
		} else {
			return (wanderTarget * vehicle.getMaxSpeed ()  - vehicle.getCurrentVelocity ());
		}
	}

	//pursuit
	//evade
	//wander
	//obsticle avoidance
	//
}
