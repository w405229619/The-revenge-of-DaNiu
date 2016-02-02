using UnityEngine;
using System.Collections;

public class ObjectiveScript : MonoBehaviour {
	public bool BoxRight ;
	// Use this for initialization
	void Start () {
		BoxRight = false;	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(transform.position.x,0.5f,transform.position.z);
	}
	void OnTriggerEnter(Collider other){
		if (other.name == "Box" || other.name == "Box1")
			BoxRight = true;
	}
	
	void OnTriggerStay(Collider other){
		if (other.name == "Box" || other.name == "Box1")
			BoxRight = true;
			
	}
	
	void OnTriggerExit(Collider other){
		if (other.name == "Box" || other.name == "Box1")
			BoxRight = false;
	}
}
