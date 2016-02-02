using UnityEngine;
using System.Collections;

public class BoxGameTriggerScript : MonoBehaviour {
	public bool trig = false;
	public GameObject father;
	private BoxGameManagerScript bgmScript;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider other){
		if (!trig && other.name == "Player")
		{
			trig = true;
			bgmScript = (BoxGameManagerScript) father.GetComponent( typeof(BoxGameManagerScript) );	
			bgmScript.Trigger();
		}
	}
}
