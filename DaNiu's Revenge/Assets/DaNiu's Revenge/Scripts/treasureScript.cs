using UnityEngine;
using System.Collections;

public class treasureScript : MonoBehaviour {
	public int treasureType;
	private GameObject objPlayer;
	private AIscript objAIScriptAI;
	// Use this for initialization
	void Start () {
		objPlayer = (GameObject) GameObject.FindWithTag ("Player");
		objAIScriptAI  = (AIscript) objPlayer.GetComponent( typeof(AIscript) );
	}
	
	// Update is called once per frame
	void Update () {
		if ( Vector3.Distance(objPlayer.transform.position,transform.position) < 1.2f )
		{
			getSomeThing();
			Destroy(gameObject);
		}
	}
	
	void getSomeThing(){
		if( treasureType == 1)
		{
			objAIScriptAI.GetGun();
		}
	}
}
