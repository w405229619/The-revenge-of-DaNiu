using UnityEngine;
using System.Collections;

public class boxMoveScript : MonoBehaviour {
	private GameObject objPlayer;
	public Collider[] back;
	public GameObject father;
	private BoxGameManagerScript bgmScript;
	// Use this for initialization
	void Start () {
		objPlayer = (GameObject) GameObject.FindWithTag ("Player");
		bgmScript = (BoxGameManagerScript) father.GetComponent( typeof(BoxGameManagerScript) );	
	}
	
	// Update is called once per frame
	void Update () {
		float x = Mathf.Round(transform.position.x/2)*2;
		float y = 0;
		float z = Mathf.Round(transform.position.z/2)*2;
		transform.position = new Vector3(x,y,z);
	}
	
	void OnCollisionEnter(Collision Other) {
		if(Other.gameObject == objPlayer)
		{
			bgmScript.startState = false;
			Vector3 relative = transform.position - objPlayer.transform.position;
			if (Mathf.Abs(relative.x)>Mathf.Abs(relative.z))
				relative = new Vector3((relative.x/Mathf.Abs(relative.x)),0,0);
			else
				relative = new Vector3(0,0,(relative.z/Mathf.Abs(relative.z)));
			Vector3 pos = relative*2 + transform.position;
			back = Physics.OverlapSphere(pos,0.1f);
			if(back.Length==0)
				transform.position = pos;
		}
	}
}
