using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomWalk : MonoBehaviour {

	public Transform target;
	UnityEngine.AI.NavMeshAgent agent;
	 

	// Use this for initialization
	void Start () {
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.deltaTime % 15 == 0) {
			target.position = new Vector3 (Random.value * 10, 0, Random.value * 10);
			agent.SetDestination (target.position);
		}
	}
}
