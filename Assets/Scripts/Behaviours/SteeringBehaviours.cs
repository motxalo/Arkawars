using UnityEngine;
using System.Collections;

public class SteeringBehaviours : MonoBehaviour {

	public float speed = 2f;
	public float sterSpeed = .5f;
	private Vector3 velocity;

	public bool seek = false;
	public bool flee = false;
	public bool wander = false;
	//public bool pursue = false;
	//public bool evade = false;
	public bool avoid = false;
	public bool path = false;
	//public bool follow = false;

	//public bool avoidTarget = false;

	public bool separate = false;
	public bool align = false;
	public bool cohesion = false;

	public bool smoothArrival = false;
	private float arrivalradius = 5f;

	public Transform target;
	// Use this for initialization
	void Start () {
		velocity = transform.forward;
	}

	public Vector3 GetVelocity(){
		return velocity.normalized;
	}

	// Update is called once per frame
	void Update () {
		if (seek)
			UpdateSeek();
		if (flee)
			UpdateFlee();
		if (wander)
			UpdateWander();
		//if(pursue)
		//	UpdatePursue();
		//if(evade)
		//	UpdateEvade();
		if (avoid)
			UpdateAvoid();
		if (path)
			UpdatePath();
		//if (follow)
		//	UpdateFollow();
		//if (avoidTarget)
		//	UpdateAvoidTarget();
		if (separate)
			UpdateSeparate();
		if (align) 
			UpdateAlign();
		if (cohesion)
			UpdateCohesion();

		Debug.DrawLine(transform.position, transform.position + 10f*velocity.normalized*speed,Color.blue);
		if(smoothArrival){
			float dist = Vector3.Distance(transform.position,target.position);
			if(dist < arrivalradius){
				transform.position += velocity.normalized*speed*(dist / arrivalradius)*Time.deltaTime;
				return;
			}
		}

		velocity.y=0f;
		velocity.Normalize();
		transform.position += velocity*speed*Time.deltaTime;

	}

	//SEEK//

	void UpdateSeek(){
		Vector3 wantedDir = (target.position - transform.position).normalized;
		Vector3 steering = (wantedDir - velocity)*sterSpeed*Time.deltaTime;
		velocity += (steering);
	}

	// EVADE //

	void UpdateFlee(){
		Vector3 wantedDir = (transform.position - target.position ).normalized;
		Vector3 steering = (wantedDir - velocity)*sterSpeed*Time.deltaTime;
		velocity += (steering);
	}

	// WANDER //

	Vector3 tempTarget = Vector3.zero;
	float wanderRadius = 50f;
	void UpdateWander(){
		if(Random.Range(0f,1f) < .05f)
			tempTarget = transform.position + new Vector3(Random.Range(-wanderRadius,wanderRadius),0f,Random.Range(-wanderRadius,wanderRadius));

		Vector3 wantedDir = (tempTarget - transform.position).normalized;
		Vector3 steering = (wantedDir - velocity)*sterSpeed*Time.deltaTime;
		velocity += (steering);

	}

	// PURSUE //
	// DEPRECATED //
	/*
	void UpdatePursue(){
		BaitController bController = target.GetComponent<BaitController>();
		int anticipation = 30;
		Vector3 wantedDir = (bController.GetNextFramePos(anticipation) - transform.position).normalized;
		Vector3 steering = (wantedDir - velocity)*sterSpeed*Time.deltaTime;
		velocity += (steering);
	}

	// EVADE //

	void UpdateEvade(){
		BaitController bController = target.GetComponent<BaitController>();
		int anticipation = 30;
		Vector3 wantedDir = ( transform.position - bController.GetNextFramePos(anticipation)).normalized;
		Vector3 steering = (wantedDir - velocity)*sterSpeed*Time.deltaTime;
		velocity += (steering);
	}
  */
	// AVOID //
	private GameObject[] obstaclePool;
	private float avoidDistance = 2f;
	private float avoidStrength = 2f;

	Transform nearestObject = null;
	void UpdateAvoid(){
		if (obstaclePool == null )
			obstaclePool = GameObject.FindGameObjectsWithTag("Obstacle");
		if(obstaclePool.Length == 0)
			return;
		if(nearestObject!=null)
			nearestObject.GetComponent<Renderer>().material.color = Color.red;
		float nearestDistance = 1000000000f;
		foreach (GameObject go in obstaclePool){
			float tDist = Vector3.Distance(transform.position, go.transform.position);
			if( tDist < nearestDistance){
				nearestObject = go.transform;
				nearestDistance = tDist;
			}
		}
		if(nearestObject == null)
			return;

		nearestObject.GetComponent<Renderer>().material.color = Color.blue;
		float obstacleRadius = 2f;
		Vector3 refPointA = transform.position + velocity*avoidDistance;
		Vector3 refPointB = transform.position + velocity*avoidDistance*.5f;

		if ( Vector3.Distance(transform.position, nearestObject.position) < obstacleRadius )
			velocity += avoidStrength*sterSpeed*Time.deltaTime*(transform.position - nearestObject.position).normalized;
		
		if ( Vector3.Distance(refPointB,nearestObject.position) < obstacleRadius )
			velocity += avoidStrength*sterSpeed*Time.deltaTime*(refPointB - nearestObject.position).normalized;

		if ( Vector3.Distance(refPointA,nearestObject.position) < obstacleRadius )
			velocity += avoidStrength*sterSpeed*Time.deltaTime*(refPointA - nearestObject.position).normalized;

	}

	// FOLLOW PATH //

	GameObject[] pathNodes = null;
	int actualNode = 0;
	float arrivalMargin = 3f;

	void UpdatePath(){
		while (pathNodes == null || pathNodes.Length==0){
			pathNodes = GameObject.FindGameObjectsWithTag("Path");
			actualNode = 0;
		}
		if (Vector3.Distance(transform.position, pathNodes[actualNode].transform.position) <= arrivalMargin )
			actualNode = (actualNode+1)%pathNodes.Length;
		
		Vector3 wantedDir = (pathNodes[actualNode].transform.position - transform.position).normalized;
		Vector3 steering = (wantedDir - velocity)*sterSpeed*Time.deltaTime;
		velocity += (steering);

	}

	// FOLLOW THE LEADER //
	// DEPRECATED //
	/*
	float followDistance = 2f;

	void UpdateFollow(){
		BaitController bController = target.GetComponent<BaitController>();
		Vector3 wantedDir = (target.position - followDistance*(bController.GetVelocity().normalized) - transform.position).normalized;
		Vector3 steering = (wantedDir - velocity)*sterSpeed*Time.deltaTime;
		velocity += (steering);
	}
	
	// AVOID TARGET //

	float distanceToTarget = 3f;

	void UpdateAvoidTarget(){
		Vector3 targetDir = target.GetComponent<BaitController>().GetNextFramePos(1);
		if ( Vector3.Distance(transform.position, targetDir) <= distanceToTarget ){

			Vector3 wantedDir = (transform.position - targetDir ).normalized;
			Vector3 steering = (wantedDir - velocity)*sterSpeed*Time.deltaTime;
			velocity += distanceToTarget*(steering);
		}
	}
	*/
	// SEPARATE //

	GameObject[] friendPool;
	float neighbourDistance = 2f;

	void UpdateSeparate(){
		while (friendPool == null || friendPool.Length==0){
			friendPool = GameObject.FindGameObjectsWithTag("Runner");
		}

		int neighborPool = 0;
		Vector3 neighborForce = Vector3.zero;

		foreach( GameObject go in friendPool){
			if( Vector3.Distance(transform.position,go.transform.position) < neighbourDistance){
				neighborPool++;
				neighborForce += go.transform.position - transform.position;
			}
		}

		if(neighborPool  <= 1)
			return;

		neighborForce/=neighborPool;
		neighborForce*=-1f;

		velocity += neighbourDistance * neighborForce.normalized;
	}

	// ALIGN //

	float alignmentDistance = 15f;

	void UpdateAlign(){
		
		while (friendPool == null || friendPool.Length==0){
			friendPool = GameObject.FindGameObjectsWithTag("Runner");
		}

		int neighborPool = 0;
		Vector3 neighborAlignment = Vector3.zero;

		foreach( GameObject go in friendPool){
			if( Vector3.Distance(transform.position,go.transform.position) < alignmentDistance){
				neighborPool++;
				neighborAlignment += go.GetComponent<SteeringBehaviours>().GetVelocity();
			}
		}

		if(neighborPool <= 1)
			return;

		neighborAlignment/=neighborPool;

		velocity +=  neighborAlignment.normalized*sterSpeed*Time.deltaTime;
	}

	// COHESION //

	float cohesionStrength = 10f;

	void UpdateCohesion(){
		while (friendPool == null || friendPool.Length==0){
			friendPool = GameObject.FindGameObjectsWithTag("Runner");
		}

		int neighborPool = 0;
		Vector3 cohesionCenter = Vector3.zero;

		foreach( GameObject go in friendPool){
			if( Vector3.Distance(transform.position,go.transform.position) < alignmentDistance){
				neighborPool++;
				cohesionCenter += go.transform.position;
			}
		}

		if(neighborPool  <= 1)
			return;

		cohesionCenter/=neighborPool;
		Vector3 cohesionVector =  cohesionCenter - transform.position;
		Vector3 steering = (cohesionVector - velocity)*sterSpeed*Time.deltaTime;
		velocity += (steering);
	}


}
