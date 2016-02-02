using UnityEngine;
using System.Collections;

public class MazeScript : MonoBehaviour {
	public bool emit = true;
	public bool RandomChange = false;
	public bool playerDamage = true;
	public bool playerIsHere = false;
	public float damage = 0f;
	public int i;
	public int j;
	private float Xmax;
	private float Zmax;
	private float Xmin;
	private float Zmin;
	public GameObject objPlayer;
	public GameObject father;
	private float objPlayerX;
	private float objPlayerZ;
	private AIscript objAIScriptAI;
	private MazeMainScript mmScript;
	// Use this for initialization
	void Start () {
		Xmax = transform.position.x+1;
		Xmin = transform.position.x-1;
		Zmax = transform.position.z+1;
		Zmin = transform.position.z-1;
		objPlayer = (GameObject) GameObject.FindWithTag ("Player");
		objAIScriptAI  = (AIscript) objPlayer.GetComponent( typeof(AIscript) );
		mmScript = (MazeMainScript) father.GetComponent( typeof(MazeMainScript) );
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.GetComponent<ParticleEmitter>().emit = emit;
		if(objPlayer!=null)
		{
			objPlayerX = objPlayer.transform.position.x;
			objPlayerZ = objPlayer.transform.position.z;
			if((objPlayerX > Xmin &&  objPlayerX < Xmax && objPlayerZ > Zmin && objPlayerZ < Zmax) && emit)
			{
				objAIScriptAI.health -= 100f;
			}
			if(!(objPlayerX > Xmin &&  objPlayerX < Xmax && objPlayerZ > Zmin && objPlayerZ < Zmax))
				playerIsHere = false;
			if(playerIsHere == false && (objPlayerX > Xmin &&  objPlayerX < Xmax && objPlayerZ > Zmin && objPlayerZ < Zmax))
			{
				playerIsHere = true;
				mmScript.objNowi=i;
				mmScript.objNowj=j;
				mmScript.publicNow=true;
			}
		}
		if(RandomChange)
		{
			RandomChange = false;
			int rd = Random.Range(0,2);
			if(rd==1)
			{
				emit=true;
			}else
				emit=false;
		}
	}
}
