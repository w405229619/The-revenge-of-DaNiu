using UnityEngine;
using System.Collections;

public class BoxGameScript : MonoBehaviour {
	public int xOff;
	public int zOff;		
	public int x = 0;
	public int z = 0;
	private BoxGameManagerScript bgmScript;
	public GameObject father;
	// Use this for initialization
	void Start () {
		bgmScript = (BoxGameManagerScript) father.GetComponent( typeof(BoxGameManagerScript) );	
		xOff = bgmScript.xOffset;
		zOff = bgmScript.zOffset;
		transform.position = new Vector3(x+xOff,0,z+zOff);	
	}
	
	// Update is called once per frame
	void Update () {
	}
}