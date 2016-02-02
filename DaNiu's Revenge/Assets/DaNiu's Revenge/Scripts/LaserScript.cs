using UnityEngine;
using System.Collections;

public class LaserScript : MonoBehaviour {
	private bool pause;
	private GameObject objCamera;
	private gameScript ptrGameScript;
	public bool LaserOn = false;
	public float moveSpeed = 3f;
	public GameObject LaserLight;
	public bool moveUp = true;
	public float UpDis = 5f;
	public float DownDis = 5f;
	public float interval = 2f;
	private float dure = 1f;
	public float time = 0f;
	public bool timeRandom;
	// Use this for initialization
	void Start () {
		if(timeRandom)
		{
			time = Random.Range(0,interval);
		}
		objCamera = (GameObject) GameObject.FindWithTag ("MainCamera");
		ptrGameScript = (gameScript)objCamera.GetComponent( typeof(gameScript) );
	}
	
	// Update is called once per frame
	void Update () {
		pause = ptrGameScript.pause;
		if(!pause)
		{
			if(interval>0)
			{
				time += Time.deltaTime;
				if(time >= interval && !LaserOn)
				{
					time=0f;
					LaserOn = true;
				}
				if(time >= dure && LaserOn)
				{
					time=0f;
					LaserOn = false;
				}
			}
			transform.eulerAngles = new Vector3(0,transform.eulerAngles.y,0);
			transform.position = new Vector3(transform.position.x,0,transform.position.z);
			if(LaserOn)
				LaserLight.SetActive(true);
			else
				LaserLight.SetActive(false);
			if(UpDis<=0 || DownDis<=0)
				moveUp = !moveUp;
			if(UpDis>0 && moveUp)
			{
				UpDis -= moveSpeed * Time.deltaTime;
				DownDis += moveSpeed * Time.deltaTime;
				transform.Translate(0, 0,moveSpeed * Time.deltaTime);
			}
			if(DownDis>0 && !moveUp)
			{
				UpDis += moveSpeed * Time.deltaTime;
				DownDis -= moveSpeed * Time.deltaTime;
				transform.Translate(0, 0,-moveSpeed * Time.deltaTime);
			}
		}
	}
	public void setProperty(bool LaserOn2,float time2,bool timeRandom2,float moveSpeed2,bool moveUp2,float UpDis2,float DownDis2,float interval2,float px,float py,float pz,float rx,float ry,float rz){
		LaserOn = LaserOn2;
		time = time2;
		timeRandom = timeRandom2;
		moveSpeed = moveSpeed2;
		moveUp = moveUp2;
		UpDis = UpDis2;
		DownDis = DownDis2;
		interval = interval2;
		gameObject.transform.position = new Vector3(px,py,pz);
		gameObject.transform.Rotate(new Vector3(rx,ry,rz));
			
	}
}
