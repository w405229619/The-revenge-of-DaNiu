using UnityEngine;
using System.Collections;

public class DoublePortalScript : MonoBehaviour {
	public GameObject objPlayer;
	private GameObject objCamera;
	public GameObject sendTo;
	private Vector3 tempVector;
	private VariableScript ptrScriptVariable;
	private bool fromFlag = true;
	private bool toFlag = true;
	// Use this for initialization
	void Start () {
		objPlayer = (GameObject) GameObject.FindWithTag ("Player");
		objCamera = (GameObject) GameObject.FindWithTag ("MainCamera");
		ptrScriptVariable = (VariableScript) objCamera.GetComponent( typeof(VariableScript) );
	}
	
	// Update is called once per frame
	void Update () {
		if(objPlayer!=null)
			tempVector=transform.position-objPlayer.transform.position;
		if((tempVector.x*tempVector.x+tempVector.z*tempVector.z)<=0.25f && toFlag)
		{
			objPlayer.transform.position=sendTo.transform.position;
			fromFlag = false;
		}else if((tempVector.x*tempVector.x+tempVector.z*tempVector.z)>=0.25f)
			toFlag = true;
		if(objPlayer!=null)
			tempVector=sendTo.transform.position-objPlayer.transform.position;
		if((tempVector.x*tempVector.x+tempVector.z*tempVector.z)<=0.25f && fromFlag)
		{
			objPlayer.transform.position=transform.position;
			toFlag = false;
		}else if((tempVector.x*tempVector.x+tempVector.z*tempVector.z)>=0.25f)
			fromFlag = true;
	}
}
