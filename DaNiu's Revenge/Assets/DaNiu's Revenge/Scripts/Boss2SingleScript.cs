using UnityEngine;
using System.Collections;

public class Boss2SingleScript : MonoBehaviour {
	private bool pause;
	private GameObject objPlayer;
	private GameObject objCamera;
	private gameScript ptrGameScript;
	public float healthMax = 60f;
	public float health = 60f;
	private VariableScript ptrScriptVariable;
	
	public float findDistance;
	public float attackDistance;
	private Vector3 tempVector;
	private float distance;
	public float bulletIntervalMax;
	public float bulletInterval;
	private Quaternion ANGLE;
	// Use this for initialization
	void Start () {
		ANGLE.eulerAngles = new Vector3(0,45,0);
		objPlayer = (GameObject) GameObject.FindWithTag ("Player");
		objCamera = (GameObject) GameObject.FindWithTag ("MainCamera");
		ptrGameScript = (gameScript)objCamera.GetComponent( typeof(gameScript) );
		ptrScriptVariable = (VariableScript) objCamera.GetComponent( typeof(VariableScript) );
	}
	
	// Update is called once per frame
	void Update () {
		pause = ptrGameScript.pause;
		if(!pause)
		{
			if (health <= 0)
			{
				removeMe();
			}
			tempVector = objPlayer.transform.position-transform.position;
			distance = tempVector.x*tempVector.x + tempVector.z*tempVector.z;
			if (distance<=findDistance)
			{
				FindInput();
			}
		}
	}
	void FindInput()
	{
		bulletInterval -= Time.deltaTime;
		if (bulletInterval<=0 && distance<=attackDistance)
		{
			bulletInterval = bulletIntervalMax;
			HandleBullets();
		}
	}
	void HandleBullets()
	{
		Vector3 bulletRotation = transform.forward;
		
		GameObject objCreatedBullet = (GameObject) Instantiate(ptrScriptVariable.objBullet2, transform.position, Quaternion.LookRotation(bulletRotation) ); 
		objCreatedBullet.tag = "enemyBullet";
		Physics.IgnoreCollision(objCreatedBullet.GetComponent<Collider>(), GetComponent<Collider>());	
		
		bulletRotation = ANGLE*bulletRotation;
		objCreatedBullet = (GameObject) Instantiate(ptrScriptVariable.objBullet2, transform.position, Quaternion.LookRotation(bulletRotation) ); 
		objCreatedBullet.tag = "enemyBullet";
		Physics.IgnoreCollision(objCreatedBullet.GetComponent<Collider>(), GetComponent<Collider>());	
		
		bulletRotation = ANGLE*bulletRotation;
		objCreatedBullet = (GameObject) Instantiate(ptrScriptVariable.objBullet2, transform.position, Quaternion.LookRotation(bulletRotation) ); 
		objCreatedBullet.tag = "enemyBullet";
		Physics.IgnoreCollision(objCreatedBullet.GetComponent<Collider>(), GetComponent<Collider>());
		
		bulletRotation = ANGLE*bulletRotation;
		objCreatedBullet = (GameObject) Instantiate(ptrScriptVariable.objBullet2, transform.position, Quaternion.LookRotation(bulletRotation) ); 
		objCreatedBullet.tag = "enemyBullet";
		Physics.IgnoreCollision(objCreatedBullet.GetComponent<Collider>(), GetComponent<Collider>());
		
		bulletRotation = ANGLE*bulletRotation;
		objCreatedBullet = (GameObject) Instantiate(ptrScriptVariable.objBullet2, transform.position, Quaternion.LookRotation(bulletRotation) ); 
		objCreatedBullet.tag = "enemyBullet";
		Physics.IgnoreCollision(objCreatedBullet.GetComponent<Collider>(), GetComponent<Collider>());
		
		bulletRotation = ANGLE*bulletRotation;
		objCreatedBullet = (GameObject) Instantiate(ptrScriptVariable.objBullet2, transform.position, Quaternion.LookRotation(bulletRotation) ); 
		objCreatedBullet.tag = "enemyBullet";
		Physics.IgnoreCollision(objCreatedBullet.GetComponent<Collider>(), GetComponent<Collider>());
		
		bulletRotation = ANGLE*bulletRotation;
		objCreatedBullet = (GameObject) Instantiate(ptrScriptVariable.objBullet2, transform.position, Quaternion.LookRotation(bulletRotation) ); 
		objCreatedBullet.tag = "enemyBullet";
		Physics.IgnoreCollision(objCreatedBullet.GetComponent<Collider>(), GetComponent<Collider>());
		
		bulletRotation = ANGLE*bulletRotation;
		objCreatedBullet = (GameObject) Instantiate(ptrScriptVariable.objBullet2, transform.position, Quaternion.LookRotation(bulletRotation) ); 
		objCreatedBullet.tag = "enemyBullet";
		Physics.IgnoreCollision(objCreatedBullet.GetComponent<Collider>(), GetComponent<Collider>());
	}
	
	void removeMe()
	{
		Instantiate(ptrScriptVariable.parPlayerDeath, transform.position, Quaternion.identity );
		Destroy(gameObject);
	}
}
