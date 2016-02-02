using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {
	private bool pause;
	private GameObject objCamera;
	private gameScript ptrGameScript;
	
	private GameObject objPlayer;
	private VariableScript ptrScriptVariable;
	public float healthMax;
	public float health;
	public float moveSpeed;
	public float bulletIntervalMax;
	public float bulletInterval;
	public int enemyType;
	public int bulletType;
	public float findDistance;
	public float attackDistance;
	public float rotateSpeed;
	public float moveRange;
	public float radius;
	public float rotateCenterx;
	public float rotateCenterz;
	public bool rotateDirection;
	public bool moveDirection;//0浠ｈ〃x杞达紝1浠ｈ〃z杞成	
	private Vector3 tempVector;
	private float distance;
	
	private Vector3 inputRotation;
	private Vector3 inputMovement;
	private Quaternion ANGLE = Quaternion.identity;
	private Quaternion ANGLEFORENEMY5 = Quaternion.identity;
	private float moveDistance;
	private bool movePlus;
	private float rotateAngle;
	// Use this for initialization
	void Start () {
		objPlayer = (GameObject) GameObject.FindGameObjectWithTag("Player");
		inputRotation = transform.forward;
		if (!rotateDirection)
			ANGLE.eulerAngles = new Vector3(0,rotateSpeed*Time.deltaTime,0);
		else ANGLE.eulerAngles = new Vector3(0,-rotateSpeed*Time.deltaTime,0);
		ANGLEFORENEMY5.eulerAngles = new Vector3(0,45,0);
		moveDistance = 0;
		movePlus = true;
		rotateAngle = 0;
		rotateCenterx = transform.position.x;
		rotateCenterz = transform.position.z;
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
				ProcessMovement();
			}
		}
	}
	
	void removeMe()
	{
		if (enemyType == 1)
		{
			Instantiate(ptrScriptVariable.parEnemy1Death, transform.position, Quaternion.identity );
		}
		else if (enemyType == 2)
		{
			Instantiate(ptrScriptVariable.parEnemy1Death, transform.position, Quaternion.identity );
		}
		else if (enemyType == 3)
		{
			Instantiate(ptrScriptVariable.parEnemy1Death, transform.position, Quaternion.identity );
		}
		else if (enemyType == 4)
		{
			Instantiate(ptrScriptVariable.parEnemy1Death, transform.position, Quaternion.identity );
		}
		else if (enemyType == 5)
		{
			Instantiate(ptrScriptVariable.parAlienDeath, transform.position, Quaternion.identity );
		}
		else if (enemyType == 6)
		{
			Instantiate(ptrScriptVariable.parEnemy1Death, transform.position, Quaternion.identity );
		}
		else if (enemyType == 7)
		{
			Instantiate(ptrScriptVariable.parEnemy1Death, transform.position, Quaternion.identity );
		}
		Destroy(gameObject);
	}
	
	void FindInput()
	{
		if (objPlayer == null || !objPlayer.activeSelf)
		{
			inputMovement = Vector3.zero;
			inputRotation = transform.forward * -1;
			return;
		}
		if (enemyType == 1)
		{
			inputMovement = Vector3.zero;
			inputRotation = objPlayer.transform.position-transform.position;
			bulletInterval -= Time.deltaTime;
			if (bulletInterval<=0 && distance<=attackDistance)
			{
				bulletInterval = bulletIntervalMax;
				HandleBullets();
			}
		}
		else if (enemyType == 2)
		{
			inputMovement = Vector3.zero;
			inputRotation = objPlayer.transform.position-transform.position;
			bulletInterval -= Time.deltaTime;
			if (bulletInterval<=0 && distance<=attackDistance)
			{
				bulletInterval = bulletIntervalMax;
				HandleBullets();
			}
		}
		else if (enemyType == 3)
		{
			inputMovement = objPlayer.transform.position-transform.position;
			inputRotation = objPlayer.transform.position-transform.position;
			bulletInterval -= Time.deltaTime;
			if (bulletInterval<=0 && distance<=attackDistance)
			{
				bulletInterval = bulletIntervalMax;
				HandleBullets();
			}			
		}
		else if (enemyType == 4)
		{
			inputMovement = Vector3.zero;
			inputRotation = ANGLE*inputRotation;
			bulletInterval -= Time.deltaTime;
			if (bulletInterval<=0 && distance<=attackDistance)
			{
				bulletInterval = bulletIntervalMax;
				HandleBullets();
			}			
		}
		else if (enemyType == 5)
		{
			inputMovement = Vector3.zero;
			bulletInterval -= Time.deltaTime;
			if (bulletInterval<=0 && distance<=attackDistance)
			{
				bulletInterval = bulletIntervalMax;
				HandleBullets();
			}	
		}
		else if (enemyType == 6)
		{
			if (!moveDirection)//姘村钩骞冲姩
			{
				if (movePlus)
				{
					moveDistance += moveSpeed*Time.deltaTime;
					inputMovement = new Vector3(1,0,0);
				}
				else
				{
					moveDistance -= moveSpeed*Time.deltaTime;
					inputMovement = new Vector3(-1,0,0);
				}
				if (moveDistance  >= moveRange)
					movePlus = false;
				if (moveDistance+moveRange<=0)
					movePlus = true;
			}
			else
			{
				if (movePlus)
				{
					moveDistance += moveSpeed*Time.deltaTime;
					inputMovement = new Vector3(0,0,1);
				}
				else
				{
					moveDistance -= moveSpeed*Time.deltaTime;
					inputMovement = new Vector3(0,0,-1);
				}
				if (moveDistance  >= moveRange)
					movePlus = false;
				if (moveDistance+moveRange<=0)
					movePlus = true;
			}
			bulletInterval -= Time.deltaTime;
			if (bulletInterval<=0)
			{
				bulletInterval = bulletIntervalMax;
				HandleBullets();
			}
		}
		else if (enemyType == 7)
		{
			bulletInterval -= Time.deltaTime;
			if (bulletInterval<=0 && distance<=attackDistance)
			{
				bulletInterval = bulletIntervalMax;
				HandleBullets();
			}	
		}
	}
	
	void ProcessMovement()
	{
		if (enemyType == 7)
		{
			rotateAngle += moveSpeed*Time.deltaTime;	
			transform.position = new Vector3(rotateCenterx+radius*Mathf.Cos(rotateAngle),0,rotateCenterz+radius*Mathf.Sin(rotateAngle));
		}
		else
		{
			if (distance>attackDistance)
			{
				tempVector = GetComponent<Rigidbody>().GetPointVelocity(transform.position) * Time.deltaTime * 1000;
				GetComponent<Rigidbody>().AddForce (-tempVector.x, -tempVector.y, -tempVector.z);
		
				GetComponent<Rigidbody>().AddForce (inputMovement.normalized * moveSpeed * Time.deltaTime);
			}
			transform.rotation = Quaternion.LookRotation(inputRotation);
			transform.eulerAngles = new Vector3(0,transform.eulerAngles.y + 180,0);
			transform.position = new Vector3(transform.position.x,0,transform.position.z);
		}
	}
	
	void HandleBullets ()
	{
		if ( bulletType == 0 ){}
		else if (  bulletType == 1 )
		{
			tempVector = Quaternion.AngleAxis(8f, Vector3.up) * inputRotation;
			tempVector = (transform.position + (tempVector.normalized * 0.8f));
			
			GameObject objCreatedBullet = (GameObject) Instantiate(ptrScriptVariable.objBullet1, tempVector, Quaternion.LookRotation(inputRotation) ); // create a bullet, and rotate it based on the vector inputRotation
			objCreatedBullet.transform.Translate(-0.1f, 0, 0);
			objCreatedBullet.tag = "enemyBullet";
			Physics.IgnoreCollision(objCreatedBullet.GetComponent<Collider>(), GetComponent<Collider>());
		}
		else if (bulletType == 2)
		{
			tempVector = Quaternion.AngleAxis(8f, Vector3.up) * inputRotation;
			tempVector = (transform.position + (tempVector.normalized * 0.8f));
			
			GameObject objCreatedBullet = (GameObject) Instantiate(ptrScriptVariable.objBullet7, tempVector, Quaternion.LookRotation(inputRotation) ); // create a bullet, and rotate it based on the vector inputRotation
			objCreatedBullet.transform.Translate(-0.1f, 0, -0.2f);
			objCreatedBullet.tag = "enemyBullet";
			Physics.IgnoreCollision(objCreatedBullet.GetComponent<Collider>(), GetComponent<Collider>());
				
			objCreatedBullet = (GameObject) Instantiate(ptrScriptVariable.objBullet7, tempVector, Quaternion.LookRotation(inputRotation) ); // create a bullet, and rotate it based on the vector inputRotation
			objCreatedBullet.transform.Translate(0.2f, 0, -0.45f);
			objCreatedBullet.tag = "enemyBullet";
			Physics.IgnoreCollision(objCreatedBullet.GetComponent<Collider>(), GetComponent<Collider>());
			
			objCreatedBullet = (GameObject) Instantiate(ptrScriptVariable.objBullet7, tempVector, Quaternion.LookRotation(inputRotation) ); // create a bullet, and rotate it based on the vector inputRotation
			objCreatedBullet.transform.Translate(-0.4f, 0, -0.45f);
			objCreatedBullet.tag = "enemyBullet";
			Physics.IgnoreCollision(objCreatedBullet.GetComponent<Collider>(), GetComponent<Collider>());
		}
		else if (bulletType == 3)
		{
			Vector3 bulletRotation = inputRotation;
			tempVector = transform.position;
			
			GameObject objCreatedBullet = (GameObject) Instantiate(ptrScriptVariable.objBullet2, tempVector, Quaternion.LookRotation(bulletRotation) ); // create a bullet, and rotate it based on the vector inputRotation
			objCreatedBullet.tag = "enemyBullet";
			Physics.IgnoreCollision(objCreatedBullet.GetComponent<Collider>(), GetComponent<Collider>());	
			
			bulletRotation = ANGLEFORENEMY5*bulletRotation;
			objCreatedBullet = (GameObject) Instantiate(ptrScriptVariable.objBullet2, tempVector, Quaternion.LookRotation(bulletRotation) ); // create a bullet, and rotate it based on the vector inputRotation
			objCreatedBullet.tag = "enemyBullet";
			Physics.IgnoreCollision(objCreatedBullet.GetComponent<Collider>(), GetComponent<Collider>());	
			
			bulletRotation = ANGLEFORENEMY5*bulletRotation;
			objCreatedBullet = (GameObject) Instantiate(ptrScriptVariable.objBullet2, tempVector, Quaternion.LookRotation(bulletRotation) ); // create a bullet, and rotate it based on the vector inputRotation
			objCreatedBullet.tag = "enemyBullet";
			Physics.IgnoreCollision(objCreatedBullet.GetComponent<Collider>(), GetComponent<Collider>());
			
			bulletRotation = ANGLEFORENEMY5*bulletRotation;
			objCreatedBullet = (GameObject) Instantiate(ptrScriptVariable.objBullet2, tempVector, Quaternion.LookRotation(bulletRotation) ); // create a bullet, and rotate it based on the vector inputRotation
			objCreatedBullet.tag = "enemyBullet";
			Physics.IgnoreCollision(objCreatedBullet.GetComponent<Collider>(), GetComponent<Collider>());
			
			bulletRotation = ANGLEFORENEMY5*bulletRotation;
			objCreatedBullet = (GameObject) Instantiate(ptrScriptVariable.objBullet2, tempVector, Quaternion.LookRotation(bulletRotation) ); // create a bullet, and rotate it based on the vector inputRotation
			objCreatedBullet.tag = "enemyBullet";
			Physics.IgnoreCollision(objCreatedBullet.GetComponent<Collider>(), GetComponent<Collider>());
			
			bulletRotation = ANGLEFORENEMY5*bulletRotation;
			objCreatedBullet = (GameObject) Instantiate(ptrScriptVariable.objBullet2, tempVector, Quaternion.LookRotation(bulletRotation) ); // create a bullet, and rotate it based on the vector inputRotation
			objCreatedBullet.tag = "enemyBullet";
			Physics.IgnoreCollision(objCreatedBullet.GetComponent<Collider>(), GetComponent<Collider>());
			
			bulletRotation = ANGLEFORENEMY5*bulletRotation;
			objCreatedBullet = (GameObject) Instantiate(ptrScriptVariable.objBullet2, tempVector, Quaternion.LookRotation(bulletRotation) ); // create a bullet, and rotate it based on the vector inputRotation
			objCreatedBullet.tag = "enemyBullet";
			Physics.IgnoreCollision(objCreatedBullet.GetComponent<Collider>(), GetComponent<Collider>());
			
			bulletRotation = ANGLEFORENEMY5*bulletRotation;
			objCreatedBullet = (GameObject) Instantiate(ptrScriptVariable.objBullet2, tempVector, Quaternion.LookRotation(bulletRotation) ); // create a bullet, and rotate it based on the vector inputRotation
			objCreatedBullet.tag = "enemyBullet";
			Physics.IgnoreCollision(objCreatedBullet.GetComponent<Collider>(), GetComponent<Collider>());
		}
		else if (bulletType == 4)
		{
			tempVector = Quaternion.AngleAxis(8f, Vector3.up) * inputRotation;
			tempVector = (transform.position + (tempVector.normalized * 0.8f));
			
			GameObject objCreatedBullet = (GameObject) Instantiate(ptrScriptVariable.objBullet3, tempVector, Quaternion.LookRotation(inputRotation) ); // create a bullet, and rotate it based on the vector inputRotation
			objCreatedBullet.transform.Translate(-0.1f, 0, 0);
			objCreatedBullet.tag = "enemyBullet";
			Physics.IgnoreCollision(objCreatedBullet.GetComponent<Collider>(), GetComponent<Collider>());
		}else if (bulletType == 5)
		{
			tempVector = Quaternion.AngleAxis(8f, Vector3.up) * inputRotation;
			tempVector = (transform.position + (tempVector.normalized * 0.8f));
			
			GameObject objCreatedBullet = (GameObject) Instantiate(ptrScriptVariable.objBullet5, tempVector, Quaternion.LookRotation(inputRotation) ); // create a bullet, and rotate it based on the vector inputRotation
			objCreatedBullet.transform.Translate(-0.1f, 0, 0);
			objCreatedBullet.tag = "enemyBullet";
			Physics.IgnoreCollision(objCreatedBullet.GetComponent<Collider>(), GetComponent<Collider>());
		}else if (bulletType == 6)
		{
			tempVector = Quaternion.AngleAxis(8f, Vector3.up) * inputRotation;
			tempVector = (transform.position + (tempVector.normalized * 0.8f));
			
			GameObject objCreatedBullet = (GameObject) Instantiate(ptrScriptVariable.objBullet6, tempVector, Quaternion.LookRotation(inputRotation) ); // create a bullet, and rotate it based on the vector inputRotation
			objCreatedBullet.transform.Translate(-0.1f, 0, 0);
			objCreatedBullet.tag = "enemyBullet";
			Physics.IgnoreCollision(objCreatedBullet.GetComponent<Collider>(), GetComponent<Collider>());
		}else if (bulletType == 7)
		{
			tempVector = Quaternion.AngleAxis(8f, Vector3.up) * inputRotation;
			tempVector = (transform.position + (tempVector.normalized * 0.8f));
			
			GameObject objCreatedBullet = (GameObject) Instantiate(ptrScriptVariable.objBullet7, tempVector, Quaternion.LookRotation(inputRotation) ); // create a bullet, and rotate it based on the vector inputRotation
			objCreatedBullet.transform.Translate(-0.1f, 0, 0);
			objCreatedBullet.tag = "enemyBullet";
			Physics.IgnoreCollision(objCreatedBullet.GetComponent<Collider>(), GetComponent<Collider>());
		}
	}
	
}
