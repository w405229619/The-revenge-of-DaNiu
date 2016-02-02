using UnityEngine;
using System.Collections;

public class PhysicsObjectScript : MonoBehaviour {
	//this script stops the physics object rotating on it's X and Z axis, it also stops it moving off the Y axis
	void Update ()
	{
		transform.eulerAngles = new Vector3(0,transform.eulerAngles.y,0);
		transform.position = new Vector3(transform.position.x,0,transform.position.z);
	}
}