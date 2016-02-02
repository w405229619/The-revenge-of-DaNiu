using UnityEngine;
using System.Collections;

public class portalScript : MonoBehaviour {
	private GameObject objCamera;
	private gameScript GameScript;
	private GameObject objPlayer;
	public GameObject sendTo;
	private Vector3 tempVector;
	public GameObject son1;
	public GameObject son4;
	private NcCurveAnimation son1Script;
	private NcCurveAnimation son4Script;
	private VariableScript ptrScriptVariable;
	// Use this for initialization
	void Start () {
		objPlayer = (GameObject) GameObject.FindWithTag ("Player");
		objCamera = (GameObject) GameObject.FindWithTag("MainCamera");
		GameScript = (gameScript) objCamera.GetComponent( typeof(gameScript) );
		son1Script=(NcCurveAnimation)son1.GetComponent( typeof(NcCurveAnimation) );
		son4Script=(NcCurveAnimation)son4.GetComponent( typeof(NcCurveAnimation) );
		ptrScriptVariable = (VariableScript) objCamera.GetComponent( typeof(VariableScript) );
	}
	
	// Update is called once per frame
	void Update () {
		if(objPlayer!=null)
			tempVector=son1.transform.position-objPlayer.transform.position;
		if((tempVector.x*tempVector.x+tempVector.z*tempVector.z)<=0.25)
		{
			objPlayer.transform.position=sendTo.transform.position;
			if(son1!=null)
				son1Script.enabled=true;
			if(son4!=null)
				son4Script.enabled=true;
			GameScript.LevelUp();
			Destroy(gameObject,1);
		}
	}
}
