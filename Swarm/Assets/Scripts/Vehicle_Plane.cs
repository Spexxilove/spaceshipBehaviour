using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle_Plane : Vehicle {

	GameObject targetObject;

	//max break force
	float minForce;


	public Vehicle_Plane(GameObject vehicleObject, GameObject targetObject){
		vehicleObj = vehicleObject;
		behaviour = new DefaultBehaviour (this);

		this.targetObject = targetObject;

		maxSpeed = 1000.0f;
		maxForce = 250.0f;
		maxRollRate = 60.0f;
		maxPitchRate = 45.0f;

		minForce = -100.0f;
	}

	public override Vector3 updatePosition (float timestep)
	{
		Rigidbody rb = vehicleObj.GetComponent (typeof(Rigidbody)) as Rigidbody;
		Vector3 currentVelocity = rb.velocity;

		((DefaultBehaviour)behaviour).setTaget (targetObject.transform.position);
		Vector3 steeringForce = behaviour.calculate();
	

			Vector3 localDesiredVelocity = vehicleObj.transform.InverseTransformVector (currentVelocity + steeringForce);
		if (localDesiredVelocity.sqrMagnitude > 0.0) {
			//float pitch = 90.0f - (180.0f / (Mathf.PI) * Mathf.Acos (Vector3.Dot (new Vector3 (localDesiredVelocity.x, localDesiredVelocity.y, localDesiredVelocity.z), new Vector3 (-localDesiredVelocity.x, -localDesiredVelocity.y, 0.0f)) / (new Vector3 (0.0f, localDesiredVelocity.y, localDesiredVelocity.z).magnitude * localDesiredVelocity.magnitude)));
			//float roll = 90.0f - (180.0f / (Mathf.PI) * Mathf.Acos (Vector3.Dot (new Vector3 (localDesiredVelocity.x, localDesiredVelocity.y, 0.0f), Vector3.left) / (new Vector3 (localDesiredVelocity.x, localDesiredVelocity.y, 0.0f).magnitude)));

			float pitch = Vector3.Angle (Vector3.forward, localDesiredVelocity)*Mathf.Sign(Vector3.Dot(Vector3.down, localDesiredVelocity));

			float roll = Vector3.Angle (Vector3.up, new Vector3 (localDesiredVelocity.x, localDesiredVelocity.y, 0.0f))*Mathf.Sign(Vector3.Dot(pitch>0.0f ? Vector3.right : Vector3.left, localDesiredVelocity));
				
			if (Mathf.Abs (pitch) < 1.0f) {
				pitch = 0.0f;
				roll = 0.0f;
			}

			roll = Mathf.Abs (roll) < 1.0f ? 0.0f : roll;


			pitch = Mathf.Min (maxPitchRate * timestep, Mathf.Abs (pitch)) * Mathf.Sign (pitch);
			roll = Mathf.Min (maxRollRate * timestep, Mathf.Abs (roll)) * Mathf.Sign (roll);

			vehicleObj.transform.Rotate (pitch, 0.0f, roll);

			float deltaV =  localDesiredVelocity.magnitude - currentVelocity.magnitude; //vehicleObj.transform.InverseTransformVector (steeringForce).z;
			deltaV = Mathf.Clamp (deltaV, minForce * timestep, maxForce * timestep);

			rb.AddForce (vehicleObj.transform.forward * (deltaV));
		}
		return vehicleObj.transform.position;
	}
}
