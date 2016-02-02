using UnityEngine;
using System.Collections;

public class Boss1Script : MonoBehaviour {
	private bool pause;
	private GameObject objCamera;
	public GameObject Hp;
	public float healthMax = 3000f;
	public float health = 3000f;
	private GameObject objPlayer;
	private VariableScript ptrScriptVariable;
	private gameScript ptrGameScript;
	public GameObject portal;
	
	public float findDistance;
	public float attackDistance;
	private Vector3 tempVector;
	private float distance;
	public float bulletIntervalMax;
	public float bulletInterval;
	private Quaternion angle = Quaternion.identity;
	private float angleDelta;
	
	private Vector3 inputRotation;
	// Use this for initialization
	void Start () {
		objPlayer = (GameObject) GameObject.FindWithTag ("Player");
		objCamera = (GameObject) GameObject.FindWithTag ("MainCamera");
		ptrScriptVariable = (VariableScript) objCamera.GetComponent( typeof(VariableScript) );
		ptrGameScript = (gameScript)objCamera.GetComponent( typeof(gameScript) );
		angleDelta = 0;
		inputRotation = transform.forward;
	}
	
	// Update is called once per frame
	void Update () {
		pause = ptrGameScript.pause;
		if(!pause)
		{
			angleDelta+=Time.deltaTime;
			angle.eulerAngles = new Vector3(0, 100*angleDelta, 0);
			HpHandle();
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
		GameObject objCreatedBullet = (GameObject) Instantiate(ptrScriptVariable.objBullet3, transform.position, angle); // create a bullet, and rotate it based on the vector inputRotation
		objCreatedBullet.tag = "enemyBullet";
		Physics.IgnoreCollision(objCreatedBullet.GetComponent<Collider>(), GetComponent<Collider>());
	}
	
	void HpHandle(){
		float length = health*1.5f/healthMax;
		Hp.transform.localScale = new Vector3(length,0.05f,0.05f);
		Hp.transform.localPosition = new Vector3(0.55f-length/2,Hp.transform.localPosition.y,Hp.transform.localPosition.z);
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
}
