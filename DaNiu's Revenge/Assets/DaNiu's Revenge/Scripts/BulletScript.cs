using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
	private bool pause;
	private GameObject objCamera;
	private gameScript ptrGameScript;
	public float moveSpeed = 30f; // how fast the bullet moves
	private float timeSpentAlive; // how long the bullet has stayed alive for
	private GameObject objPlayer;
	public int bulletType = 1;
	public int bulletDamage = 10;
	private VariableScript ptrScriptVariable;

	// Use this for initialization
	void Start () {
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
			timeSpentAlive += Time.deltaTime;
			if (timeSpentAlive > (24/moveSpeed)) // if we have been travelling for more than one second remove the bullet
			{
				removeMe();
			}
			// move the bullet
			transform.Translate(0, 0, moveSpeed * Time.deltaTime);
			transform.position = new Vector3(transform.position.x,0,transform.position.z); // because the bullet has a rigid body we don't want it moving off it's Y axis
	
		}
	}
	void removeMe ()
	{
		if (objPlayer!=null)
		{
			if (bulletType == 1)
			{
				Instantiate(ptrScriptVariable.parBulletHit, transform.position, Quaternion.identity );
			}
			else if (bulletType == 2)
			{				
				Instantiate(ptrScriptVariable.parBullet2Hit, transform.position, Quaternion.identity );
			}
			else if (bulletType == 3)
			{
				Instantiate(ptrScriptVariable.parBullet3Hit, transform.position, Quaternion.identity );				
			}else if (bulletType == 4)
			{
				Instantiate(ptrScriptVariable.parBullet4Hit, transform.position, Quaternion.identity );				
			}
		}
		Destroy(gameObject);
	}
	void OnCollisionEnter(Collision Other)
	{
		if( Other.gameObject.tag != "bullet" && Other.gameObject.tag != "enemyBullet")
		{
			if ( Other.gameObject.GetComponent( typeof(EnemyScript) ) != null && gameObject.tag != "enemyBullet") // if we have hit an enemy
			{
				EnemyScript ptrScriptEnemy = (EnemyScript) Other.gameObject.GetComponent( typeof(EnemyScript) );
				ptrScriptEnemy.health -= bulletDamage;
			}else if ( Other.gameObject.GetComponent( typeof(Boss1Script) ) != null && gameObject.tag != "enemyBullet")//if we have hit boss1
			{
				Boss1Script ptrBoss1Script = (Boss1Script) Other.gameObject.GetComponent( typeof(Boss1Script));
				ptrBoss1Script.health -= bulletDamage;
			}else if ( Other.gameObject.GetComponent( typeof(Boss2Script) ) != null && gameObject.tag != "enemyBullet")//if we have hit boss2
			{
				Boss2Script ptrBoss2Script = (Boss2Script) Other.gameObject.GetComponent( typeof(Boss2Script));
				if(ptrBoss2Script.DamageOn)
					ptrBoss2Script.health -= bulletDamage;	
			}else if ( Other.gameObject.GetComponent( typeof(Boss3Script) ) != null && gameObject.tag != "enemyBullet")//if we have hit boss3
			{
				Boss3Script ptrBoss3Script = (Boss3Script) Other.gameObject.GetComponent( typeof(Boss3Script));
				ptrBoss3Script.health -= bulletDamage;	
			}else if ( Other.gameObject.GetComponent( typeof(Boss2SingleScript) ) != null && gameObject.tag != "enemyBullet")//if we have hit boss2Single
			{
				Boss2SingleScript ptrBoss2SingleScript = (Boss2SingleScript) Other.gameObject.GetComponent( typeof(Boss2SingleScript));
				ptrBoss2SingleScript.health -= bulletDamage;
			}else if ( Other.gameObject.GetComponent( typeof(AIscript) ) != null && gameObject.tag != "enemyBullet")//if we have hit Aiscript enemy
			{
				AIscript ptrAIscript = (AIscript) Other.gameObject.GetComponent( typeof(AIscript));
				ptrAIscript.health -= bulletDamage;
				Instantiate(ptrScriptVariable.parAlienHit, transform.position, Quaternion.identity );
			}
			else if (Other.gameObject.name == "Player")
			{
				AIscript ptrScriptAI = (AIscript) Other.gameObject.GetComponent( typeof(AIscript) );
				ptrScriptAI.health -= bulletDamage;
				Instantiate(ptrScriptVariable.parPlayerHit, transform.position, Quaternion.identity );
			}
			removeMe(); // remove the bullet if it has hit something else apart from an enemy character
		}
	}
}
