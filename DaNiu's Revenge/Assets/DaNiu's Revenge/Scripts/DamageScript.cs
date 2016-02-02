using UnityEngine;
using System.Collections;

public class DamageScript : MonoBehaviour {
	public bool playerDamage = true;
	//public bool enemyDamage = false;
	public float damageXRange = 2f;
	public float damageZRange = 2f;
	//public float damage = 0f;
	public float Xmax;
	public float Zmax;
	public float Xmin;
	public float Zmin;
	public GameObject objPlayer;
	private float objPlayerX;
	private float objPlayerZ;
	private AIscript objAIScriptAI;
	// Use this for initialization
	void Start () {
		Xmax = transform.position.x+damageXRange/2;
		Xmin = transform.position.x-damageXRange/2;
		Zmax = transform.position.z+damageZRange/2;
		Zmin = transform.position.z-damageZRange/2;
		objPlayer = (GameObject) GameObject.FindWithTag ("Player");
		objAIScriptAI  = (AIscript) objPlayer.GetComponent( typeof(AIscript) );
	}
	
	// Update is called once per frame
	void Update () {
		if(objPlayer!=null)
		{
			objPlayerX = objPlayer.transform.position.x;
			objPlayerZ = objPlayer.transform.position.z;
			if(gameObject.GetComponent<ParticleEmitter>().emit)
			{
				if(playerDamage && (objPlayerX > Xmin &&  objPlayerX < Xmax && objPlayerZ > Zmin && objPlayerZ < Zmax))
				{
					objAIScriptAI.health-=0.5f;
				}
			}
		}
	}
}
