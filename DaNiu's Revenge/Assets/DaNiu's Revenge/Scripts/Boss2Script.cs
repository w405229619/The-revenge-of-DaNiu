using UnityEngine;
using System.Collections;

public class Boss2Script : MonoBehaviour {
	private bool pause;
	private GameObject objCamera;
	private GameObject up;
	private GameObject down;
	private GameObject left;
	private GameObject right;
	private Boss2SingleScript AI;
	private Vector3 upPo;
	private Vector3 downPo;
	private Vector3 leftPo;
	private Vector3 rightPo;
	public GameObject Hp;
	public float healthMax = 500f;
	public float health = 300f;
	public bool DamageOn = false; 
	private VariableScript ptrScriptVariable;
	private gameScript ptrGameScript;
	public float rescuerTimeMax = 5f;
	public float upRescuerTime = 5f;
	public float downRescuerTime = 5f;
	public float leftRescuerTime = 5f;
	public float rightRescuerTime = 5f;
	private Quaternion angle = Quaternion.identity;		
	
	public GameObject portal;
	// Use this for initialization
	void Start () {
		objCamera = (GameObject) GameObject.FindWithTag ("MainCamera");
		ptrGameScript = (gameScript)objCamera.GetComponent( typeof(gameScript) );
		ptrScriptVariable = (VariableScript) objCamera.GetComponent( typeof(VariableScript) );
		angle.eulerAngles = new Vector3(0, 100*Time.deltaTime, 0);
		upPo = transform.position+new Vector3(0,0,3.2f);
		downPo = transform.position+new Vector3(3.2f,0,0);
		leftPo = transform.position+new Vector3(0,0,-3.2f);
		rightPo = transform.position+new Vector3(-3.2f,0,0);
		up = (GameObject) Instantiate(ptrScriptVariable.Boss2Single,upPo,Quaternion.identity);
		up.transform.Rotate(new Vector3(0,180,0));
		down = (GameObject) Instantiate(ptrScriptVariable.Boss2Single,downPo,Quaternion.identity);
		down.transform.Rotate(new Vector3(0,180,0));
		left = (GameObject) Instantiate(ptrScriptVariable.Boss2Single,leftPo,Quaternion.identity);
		left.transform.Rotate(new Vector3(0,180,0));
		right = (GameObject) Instantiate(ptrScriptVariable.Boss2Single,rightPo,Quaternion.identity);
		right.transform.Rotate(new Vector3(0,180,0));
	}
	// Update is called once per frame
	void Update () {
		pause = ptrGameScript.pause;
		if(!pause)
		{
			HpHandle();
			SonHandle();
			ProcessMovement();
			if (health <= 0)
			{
				removeMe();
			}
			if(up|| down|| left|| right)
				DamageOn = false;
			else
				DamageOn = true;
			if(!up)
			{
				upRescuerTime-=Time.deltaTime;
				if(upRescuerTime <= 0)
				{
					upRescuerTime = rescuerTimeMax;
					up = (GameObject) Instantiate(ptrScriptVariable.Boss2Single,upPo,Quaternion.identity);
					up.transform.Rotate(new Vector3(0,180,0));
					AI = (Boss2SingleScript) up.GetComponent(typeof ( Boss2SingleScript ) );
					AI.health = AI.healthMax;
				}
			}
			if(!down)
			{
				downRescuerTime-=Time.deltaTime;
				if(downRescuerTime <= 0)
				{
					downRescuerTime = rescuerTimeMax;
					down = (GameObject) Instantiate(ptrScriptVariable.Boss2Single,downPo,Quaternion.identity);
					down.transform.Rotate(new Vector3(0,180,0));
					AI = (Boss2SingleScript) down.GetComponent(typeof ( Boss2SingleScript ) );
					AI.health = AI.healthMax;
				}
			}
			if(!left)
			{
				leftRescuerTime-=Time.deltaTime;
				if(leftRescuerTime <= 0)
				{
					leftRescuerTime = rescuerTimeMax;
					left = (GameObject) Instantiate(ptrScriptVariable.Boss2Single,leftPo,Quaternion.identity);
					left.transform.Rotate(new Vector3(0,180,0));
					AI = (Boss2SingleScript) left.GetComponent(typeof ( Boss2SingleScript ) );
					AI.health = AI.healthMax;
				}
			}
			if(!right)
			{
				rightRescuerTime-=Time.deltaTime;
				if(rightRescuerTime <= 0)
				{
					rightRescuerTime = rescuerTimeMax;
					right = (GameObject) Instantiate(ptrScriptVariable.Boss2Single,rightPo,Quaternion.identity);
					right.transform.Rotate(new Vector3(0,180,0));
					AI = (Boss2SingleScript) right.GetComponent(typeof ( Boss2SingleScript ) );
					AI.health = AI.healthMax;
				}
			}
		}
	}
	
	void HpHandle(){
		float length = health*1.5f/healthMax;
		Hp.transform.localScale = new Vector3(length,0.05f,0.05f);
		Hp.transform.localPosition = new Vector3(0.75f-length/2,Hp.transform.localPosition.y,Hp.transform.localPosition.z);
	}
	
	void removeMe () // removes the character
	{
		Instantiate(ptrScriptVariable.parPlayerDeath, transform.position, Quaternion.identity );
		portal.SetActive(true);
		DestroyTemp();
		Destroy(gameObject);
	}
	
	void DestroyTemp(){
		GameObject[] all = GameObject.FindGameObjectsWithTag("temp");
		foreach (GameObject a in all) {
			Destroy(a);
		}
	}
	
	void SonHandle(){
		upPo = angle * (upPo-transform.position)+transform.position;
		downPo = angle * (downPo-transform.position)+transform.position;
		leftPo = angle * (leftPo-transform.position)+transform.position;
		rightPo = angle * (rightPo-transform.position)+transform.position;
	}
	
	void ProcessMovement(){
		if(up)
			up.transform.localPosition = upPo;
		if(down)
			down.transform.localPosition = downPo;
		if(left)
			left.transform.localPosition = leftPo;
		if(right)
			right.transform.localPosition = rightPo;
	}
}
