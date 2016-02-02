using UnityEngine;
using System.Collections;

public class gameScript : MonoBehaviour {
	public bool pause = true;
	public GameObject gui;
	
	public int life = 5;
	private float healthLength;
	
	private GameObject objPlayer;
	private GameObject objCamera;
	private AIscript objAIScriptAI;
	private VariableScript ptrScriptVariable;
	
	private GameObject newLaser;
	private LaserScript newLaserScript;
	
	private GameObject newTrashCan;
	private GameObject maze;
	private GameObject boxGame;
	private GameObject standEnemy;
	private GameObject Enemy;
	public GameObject Boss1;
	public GameObject Boss2;
	public GameObject Boss3;
	public GameObject AllEnemy;
	public GameObject endinglazer;
	
	public bool ending = false;
	
	private bool one = false;
	private bool two = false;
	private bool three = false;
	
	public bool menuShow=false;
	public int pillNumber;
	
	public float time = 0f;
	
	public int level = 1;
	// Use this for initialization
	
	public Texture2D Gun1;
	public Texture2D Gun2;
	public Texture2D pill0;
	public Texture2D pill1;
	public Texture2D pill2;
	public Texture2D pill3;
	
	public GUIStyle healthBox;
	public GUIStyle button;
	public GUIStyle label;
	public float volSound = 0.5f;
	public float volMusic = 0.5f;
	
	public void Resurrect(){
		life --;
		if(level == 1)
			level1Initial();
		else if (level == 2)
			level2Initial();
		else if (level == 3)
			level3Initial();
		else if (level == 4)
			level4Initial();
		else if (level == 5)
			level5Initial();
		else if (level == 6)
			level6Initial();
		objAIScriptAI.health = objAIScriptAI.healthMax;
		objPlayer.SetActive(true);
	}
	void Start () {	
		objPlayer = (GameObject) GameObject.FindWithTag ("Player");
		objCamera = (GameObject) GameObject.FindWithTag ("MainCamera");
		ptrScriptVariable = (VariableScript) objCamera.GetComponent( typeof(VariableScript) );
		objAIScriptAI  = (AIscript) objPlayer.GetComponent( typeof(AIscript) );
		pause = true;
		musicOn();
	}
	
	// Update is called once per frame
	void OnGUI () {
		if(Camera.main.depth > 0)
		{
			if(menuShow)
			{
				if(life > 0)
				{
					if (GUI.Button (new Rect(Screen.width/2-150, Screen.height/3, 300, 30), "Restart the Level",button))
					{
						Resurrect();
						menuShow=false;
						pause = false;
						musicOn();
					}
				}else
				{
					if(objPlayer.activeSelf)
						GUI.Button (new Rect(Screen.width/2-150, Screen.height/3, 300, 30), "Can not restart",button);
					else
						GUI.Label (new Rect(Screen.width/2-250, Screen.height/3, 500, 80), "Game Over",label);
				}
				if(objPlayer.activeSelf)
				{
					if(GUI.Button (new Rect(Screen.width/2-150, Screen.height/2, 300, 30), "Continue",button))
					{
						menuShow=false;
						pause = false;
						musicOn();
					}
				}
				if(GUI.Button (new Rect(Screen.width/2-150, Screen.height*2/3, 300, 30), "Quit",button))
				{
					Application.Quit();
				}
			}
			GUI.Label (new Rect(0,0,50,20),"Level "+level);
			GUI.Box (new Rect(0,20,healthLength,20),"",healthBox);
			GUI.Label (new Rect(0,20,100,20),"Hp "+objAIScriptAI.health+"/"+objAIScriptAI.healthMax);
			GUI.Label (new Rect(0,40,50,20),"Life "+life);
			if(objAIScriptAI.gunType>0)
			{
				GUI.DrawTexture(new Rect(0,Screen.height-60,60,60), Gun1, ScaleMode.ScaleToFit, true, 0);
				GUI.DrawTexture(new Rect(60,Screen.height-60,60,60), Gun2, ScaleMode.ScaleToFit, true, 0);
			}
			if(level >= 4)
			{
				if(pillNumber == 0)
					GUI.DrawTexture(new Rect(120,Screen.height-60,60,60), pill0, ScaleMode.ScaleToFit, true, 0);
				if(pillNumber == 1)
					GUI.DrawTexture(new Rect(120,Screen.height-60,60,60), pill1, ScaleMode.ScaleToFit, true, 0);
				if(pillNumber == 2)
					GUI.DrawTexture(new Rect(120,Screen.height-60,60,60), pill2, ScaleMode.ScaleToFit, true, 0);
				if(pillNumber == 3)
					GUI.DrawTexture(new Rect(120,Screen.height-60,60,60), pill3, ScaleMode.ScaleToFit, true, 0);
			}
		}
	}
	void Update () {
		//cheat
		if(Input.GetKeyDown(KeyCode.M)){
			objAIScriptAI.health = 100000;
			objAIScriptAI.gunType = 1;
			objAIScriptAI.gunLevel = 3;
			LevelUp ();
			//if(level == 1)
				//objPlayer.transform.position = new Vector3(-958.9012f,0,-904.2126f);
			//if(level == 4)
				//objPlayer.transform.position = new Vector3(-956.3852f,0,-549.0717f);
			//if(level == 5)
				//objPlayer.transform.position = new Vector3(-977.537f,0,-413.4832f);
		}
		if(Camera.main.depth > 0)
		{
			healthLength = objAIScriptAI.health/objAIScriptAI.healthMax*100;
			if(ending)
			{
				objAIScriptAI.health = objAIScriptAI.healthMax;
				objPlayer.SetActive(true);
				AIscript tempAi = (AIscript) objPlayer.GetComponent(typeof(AIscript));
				tempAi.enabled = false;
				EndingScript tempend = (EndingScript) objPlayer.GetComponent(typeof(EndingScript));
				tempend.enabled = true;
				tempend = (EndingScript) endinglazer.GetComponent(typeof(EndingScript));
				tempend.enabled = true;
			}
			if(!pause)
			{
				if(level == 4)
				{
					if(objPlayer.transform.position.z>-555 && Boss1)
					{
						time+=Time.deltaTime;
						if(time > 10)
						{
							time = 0;
							
							Enemy = (GameObject)Instantiate(ptrScriptVariable.Enemy,new Vector3(-970f,0,-554f),Quaternion.identity);
							Enemy.tag = "temp";
							Enemy = (GameObject)Instantiate(ptrScriptVariable.Enemy,new Vector3(-970f,0,-544f),Quaternion.identity);
							Enemy.tag = "temp";
							Enemy = (GameObject)Instantiate(ptrScriptVariable.Enemy,new Vector3(-952f,0,-540f),Quaternion.identity);
							Enemy.tag = "temp";
						}
					}
				}
				if(level == 5)
				{
					if(objPlayer.transform.position.z>-381 && objPlayer.transform.position.x>-950 && !one)
					{
						one = true;
						AllEnemy = (GameObject)Instantiate(ptrScriptVariable.EnemyGroup1,new Vector3(-982.4306f,0,-371.5335f),Quaternion.identity);
						AllEnemy.tag = "temp";
					}
					if(objPlayer.transform.position.z<-400 && objPlayer.transform.position.x>-931 && !two)
					{
						two = true;
						AllEnemy = (GameObject)Instantiate(ptrScriptVariable.EnemyGroup2,new Vector3(-918.3665f,0,-365.1376f),Quaternion.identity);
						AllEnemy.tag = "temp";
					}
					if(objPlayer.transform.position.z<-418 && objPlayer.transform.position.x<-950 && !three)
					{
						three = true;
						AllEnemy = (GameObject)Instantiate(ptrScriptVariable.EnemyGroup1,new Vector3(-915.2975f,0,-429.8957f),Quaternion.identity);
						AllEnemy.tag = "temp";
					}
				}
			}
			FindInput();
		}
	}
	void FindInput ()
	{
		if(!ending)
		{
			if(Input.GetKeyDown(KeyCode.Escape)){
				if(!menuShow)
				{
					menuShow = true;
					pause = true;
					musicOff();
				}else
				{
					menuShow = false;
					pause = false;
					musicOn();
				}
			}
			if(!pause)
			{
				if(objAIScriptAI.gunType>0)
				{
					if(Input.GetKeyDown(KeyCode.Alpha1))
					{
						objAIScriptAI.gunType = 1;
					}
					if(Input.GetKeyDown(KeyCode.Alpha2))
					{
						objAIScriptAI.gunType = 2;
					}
				}
				if(level >= 4 && pillNumber > 0)
				{
					if(Input.GetKeyDown(KeyCode.Alpha3))
					{
						pillNumber --;
						objAIScriptAI.health = objAIScriptAI.healthMax;
					}
				}
			}
		}	
	}
	public void LevelUp(){
		pause = true;
		Camera.main.depth = -1;
		gui.SetActive(true);
		gui.GetComponent<GUIControl>().Transition();
	}
	public void LoadLevel() 
	{
		pause = false;
		musicOn();
		if(level == 1)
		{
			level2Initial();
			objAIScriptAI.healthMax=100;
			objAIScriptAI.health=100;
		}
		else if(level == 2)
		{
			level3Initial();
			objAIScriptAI.gunType = 1;
			objAIScriptAI.gunLevel = 1;
		}
		else if(level == 3)
		{
			level4Initial();
			objAIScriptAI.gunType = 1;
			objAIScriptAI.gunLevel = 1;
			pillNumber = 3;
		}else if(level == 4)
		{
			level5Initial();
			objAIScriptAI.gunType = 1;
			objAIScriptAI.gunLevel = 2;
		}else if(level == 5)
		{
			level6Initial();
			objAIScriptAI.gunType = 1;
			objAIScriptAI.gunLevel = 3;
		}else if(level == 6)
		{
		}
		level++;
	}
	void DestroyTemp(){
		GameObject[] all = GameObject.FindGameObjectsWithTag("temp");
		foreach (GameObject a in all) {
			Destroy(a);
		}
	}
	
	void level1Initial(){
		DestroyTemp();
		
		objPlayer.transform.position = new Vector3(-960,0,-970);
		
		newLaser = (GameObject)Instantiate(ptrScriptVariable.Laser);
		newLaser.tag = "temp";
		newLaserScript = (LaserScript) newLaser.GetComponent( typeof(LaserScript) );
		newLaserScript.setProperty(true,0,true,0,true,0,0,1f,-966.2775f,0,-957.5f,0,0,0);
		
		newLaser = (GameObject)Instantiate(ptrScriptVariable.Laser);
		newLaser.tag = "temp";
		newLaserScript = (LaserScript) newLaser.GetComponent( typeof(LaserScript) );
		newLaserScript.setProperty(true,0,true,0,true,0,0,1f,-953.6655f,0,-955f,0,180,0);
		
		newLaser = (GameObject)Instantiate(ptrScriptVariable.Laser);
		newLaser.tag = "temp";
		newLaserScript = (LaserScript) newLaser.GetComponent( typeof(LaserScript) );
		newLaserScript.setProperty(true,0,true,0,true,0,0,1f,-966.2775f,0,-952.5f,0,0,0);
		
		newLaser = (GameObject)Instantiate(ptrScriptVariable.Laser);
		newLaser.tag = "temp";
		newLaserScript = (LaserScript) newLaser.GetComponent( typeof(LaserScript) );
		newLaserScript.setProperty(true,0,true,0,true,0,0,1f,-953.6655f,0,-950f,0,180,0);
		
		newLaser = (GameObject)Instantiate(ptrScriptVariable.Laser);
		newLaser.tag = "temp";
		newLaserScript = (LaserScript) newLaser.GetComponent( typeof(LaserScript) );
		newLaserScript.setProperty(true,0,true,3,true,5,5,0,-966.2775f,0,-945f,0,0,0);
		
		newLaser = (GameObject)Instantiate(ptrScriptVariable.Laser);
		newLaser.tag = "temp";
		newLaserScript = (LaserScript) newLaser.GetComponent( typeof(LaserScript) );
		newLaserScript.setProperty(true,0,true,3,false,5,5,0,-953.6655f,0,-935f,0,180,0);
		
		newLaser = (GameObject)Instantiate(ptrScriptVariable.Laser);
		newLaser.tag = "temp";
		newLaserScript = (LaserScript) newLaser.GetComponent( typeof(LaserScript) );
		newLaserScript.setProperty(true,0,true,0,false,0,0,0,-974.5f,0,-921.5f,0,315,0);
		
		newLaser = (GameObject)Instantiate(ptrScriptVariable.Laser);
		newLaser.tag = "temp";
		newLaserScript = (LaserScript) newLaser.GetComponent( typeof(LaserScript) );
		newLaserScript.setProperty(true,0,true,0,false,0,0,0,-945.0286f,0,-921.5f,0,225,0);
		
		newLaser = (GameObject)Instantiate(ptrScriptVariable.Laser);
		newLaser.tag = "temp";
		newLaserScript = (LaserScript) newLaser.GetComponent( typeof(LaserScript) );
		newLaserScript.setProperty(true,0,true,0,false,0,0,0,-974.5f,0,-906.1869f,0,45,0);
		
		newLaser = (GameObject)Instantiate(ptrScriptVariable.Laser);
		newLaser.tag = "temp";
		newLaserScript = (LaserScript) newLaser.GetComponent( typeof(LaserScript) );
		newLaserScript.setProperty(true,0,true,0,false,0,0,0,-945.0286f,0,-906.1869f,0,135,0);
		
		newTrashCan = (GameObject)Instantiate(ptrScriptVariable.TrashCan,new Vector3(-962.6769f,0,-926.5178f),Quaternion.identity);
		newTrashCan.tag = "temp";
		newTrashCan = (GameObject)Instantiate(ptrScriptVariable.TrashCan,new Vector3(-957.4157f,0,-926.5178f),Quaternion.identity);
		newTrashCan.tag = "temp";
	}
	void level2Initial(){
		DestroyTemp();
		
		objPlayer.transform.position = new Vector3(-896,0,-774);
		
		boxGame = (GameObject)Instantiate(ptrScriptVariable.BoxGame);
		boxGame.tag = "temp";
		
		standEnemy = (GameObject)Instantiate(ptrScriptVariable.StandEnemy);
		standEnemy.tag = "temp";
	}
	void level3Initial(){
		DestroyTemp();	
		
		objPlayer.transform.position = new Vector3(-972,0,-624);
		
		maze = (GameObject)Instantiate(ptrScriptVariable.Maze,new Vector3(-972f,0,-620f),Quaternion.identity);
		maze.tag = "temp";
	}
	void level4Initial(){
		DestroyTemp();	
		
		objPlayer.transform.position = new Vector3(-928,0,-581);
		
		AllEnemy = (GameObject)Instantiate(ptrScriptVariable.Enemy,new Vector3(-967.318f,0,-578.18f),Quaternion.identity);
		AllEnemy.tag = "temp";
		AllEnemy = (GameObject)Instantiate(ptrScriptVariable.Enemy,new Vector3(-964.4136f,0,-576.5463f),Quaternion.identity);
		AllEnemy.tag = "temp";
		AllEnemy = (GameObject)Instantiate(ptrScriptVariable.Enemy,new Vector3(-964.4136f,0,-576.5463f),Quaternion.identity);
		AllEnemy.tag = "temp";
		AllEnemy = (GameObject)Instantiate(ptrScriptVariable.Enemy,new Vector3(-968.6003f,0,-586.5452f),Quaternion.identity);
		AllEnemy.tag = "temp";
		AllEnemy = (GameObject)Instantiate(ptrScriptVariable.Enemy,new Vector3(-967.318f,0,-574.731f),Quaternion.identity);
		AllEnemy.tag = "temp";
		AllEnemy = (GameObject)Instantiate(ptrScriptVariable.Enemy,new Vector3(-949.9711f,0,-584.0666f),Quaternion.identity);
		AllEnemy.tag = "temp";
		
		AllEnemy = (GameObject)Instantiate(ptrScriptVariable.Enemy1,new Vector3(-944.7692f,0,-557.6914f),Quaternion.identity);
		AllEnemy.tag = "temp";
		AllEnemy = (GameObject)Instantiate(ptrScriptVariable.Enemy1,new Vector3(-944.7692f,0,-573.4426f),Quaternion.identity);
		AllEnemy.tag = "temp";
		AllEnemy = (GameObject)Instantiate(ptrScriptVariable.Enemy1,new Vector3(-967.947f,0,-566.2607f),Quaternion.identity);
		AllEnemy.tag = "temp";
		AllEnemy = (GameObject)Instantiate(ptrScriptVariable.Enemy1,new Vector3(-944.7692f,0,-570.423f),Quaternion.identity);
		AllEnemy.tag = "temp";
		AllEnemy = (GameObject)Instantiate(ptrScriptVariable.Enemy1,new Vector3(-944.7692f,0,-586.3815f),Quaternion.identity);
		AllEnemy.tag = "temp";
		
		AllEnemy = (GameObject)Instantiate(ptrScriptVariable.Enemy2,new Vector3(-958.1597f,0,-570.3895f),Quaternion.identity);
		AllEnemy.tag = "temp";
		
		AllEnemy = (GameObject)Instantiate(ptrScriptVariable.Enemy3,new Vector3(-957.6459f,0,-579.8266f),Quaternion.identity);
		AllEnemy.tag = "temp";
		
		AllEnemy = (GameObject)Instantiate(ptrScriptVariable.Enemy5,new Vector3(-939.4366f,0,-577.6955f),Quaternion.identity);
		AllEnemy.tag = "temp";
		AllEnemy = (GameObject)Instantiate(ptrScriptVariable.Enemy5,new Vector3(-968.5752f,0,-585.39f),Quaternion.identity);
		AllEnemy.tag = "temp";
		AllEnemy = (GameObject)Instantiate(ptrScriptVariable.Enemy5,new Vector3(-967.7392f,0,-586.4278f),Quaternion.identity);
		AllEnemy.tag = "temp";
		AllEnemy = (GameObject)Instantiate(ptrScriptVariable.Enemy5,new Vector3(-933.9837f,0,-564.527f),Quaternion.identity);
		AllEnemy.tag = "temp";
	}
	void level5Initial(){
		one = false;
		two = false;
		three = false;
		DestroyTemp();	
		
		objPlayer.transform.position = new Vector3(-979,0,-396);
		
		AllEnemy = (GameObject)Instantiate(ptrScriptVariable.EnemyGroup1,new Vector3(-981.653f,0,-365.5337f),Quaternion.identity);
		AllEnemy.tag = "temp";
		AllEnemy = (GameObject)Instantiate(ptrScriptVariable.EnemyGroup2,new Vector3(-980.3098f,0,-372.9951f),Quaternion.identity);
		AllEnemy.tag = "temp";
		AllEnemy = (GameObject)Instantiate(ptrScriptVariable.EnemyGroup3,new Vector3(-957.7065f,0,-430.8052f),Quaternion.identity);
		AllEnemy.tag = "temp";
		AllEnemy.transform.Rotate(new Vector3(0,90,0));
		AllEnemy = (GameObject)Instantiate(ptrScriptVariable.EnemyGroup3,new Vector3(-941.2521f,0,-371.9264f),Quaternion.identity);
		AllEnemy.tag = "temp";
		AllEnemy.transform.Rotate(new Vector3(0,90,0));
		AllEnemy = (GameObject)Instantiate(ptrScriptVariable.EnemyGroup4,new Vector3(-918.3335f,0,-370.7191f),Quaternion.identity);
		AllEnemy.tag = "temp";
		AllEnemy = (GameObject)Instantiate(ptrScriptVariable.EnemyGroup5,new Vector3(-980.3339f,0,-424.6867f),Quaternion.identity);
		AllEnemy.tag = "temp";
		AllEnemy = (GameObject)Instantiate(ptrScriptVariable.EnemyGroup6,new Vector3(0,0,0),Quaternion.identity);
		AllEnemy.tag = "temp";
		AllEnemy = (GameObject)Instantiate(ptrScriptVariable.EnemyGroup7,new Vector3(-917.7324f,0,-426.7218f),Quaternion.identity);
		AllEnemy.tag = "temp";
		
	}
	void level6Initial(){
		DestroyTemp();	
		
		objPlayer.transform.position = new Vector3(-966,0,-298);
		
		newLaser = (GameObject)Instantiate(ptrScriptVariable.Laser);
		newLaser.tag = "temp";
		newLaserScript = (LaserScript) newLaser.GetComponent( typeof(LaserScript) );
		newLaserScript.setProperty(false,0,false,0,false,0,0,25,-966,0,-280,0,90,0);
		
		newLaser = (GameObject)Instantiate(ptrScriptVariable.Laser);
		newLaser.tag = "temp";
		newLaserScript = (LaserScript) newLaser.GetComponent( typeof(LaserScript) );
		newLaserScript.setProperty(false,1,false,0,false,0,0,25,-962,0,-280,0,90,0);
		
		newLaser = (GameObject)Instantiate(ptrScriptVariable.Laser);
		newLaser.tag = "temp";
		newLaserScript = (LaserScript) newLaser.GetComponent( typeof(LaserScript) );
		newLaserScript.setProperty(false,2,false,0,false,0,0,25,-958,0,-280,0,90,0);
		
		newLaser = (GameObject)Instantiate(ptrScriptVariable.Laser);
		newLaser.tag = "temp";
		newLaserScript = (LaserScript) newLaser.GetComponent( typeof(LaserScript) );
		newLaserScript.setProperty(false,3,false,0,false,0,0,25,-954,0,-280,0,90,0);
		
		newLaser = (GameObject)Instantiate(ptrScriptVariable.Laser);
		newLaser.tag = "temp";
		newLaserScript = (LaserScript) newLaser.GetComponent( typeof(LaserScript) );
		newLaserScript.setProperty(false,4,false,0,false,0,0,25,-950,0,-280,0,90,0);
		
		newLaser = (GameObject)Instantiate(ptrScriptVariable.Laser);
		newLaser.tag = "temp";
		newLaserScript = (LaserScript) newLaser.GetComponent( typeof(LaserScript) );
		newLaserScript.setProperty(false,5,false,0,false,0,0,25,-946,0,-280,0,90,0);
		
		newLaser = (GameObject)Instantiate(ptrScriptVariable.Laser);
		newLaser.tag = "temp";
		newLaserScript = (LaserScript) newLaser.GetComponent( typeof(LaserScript) );
		newLaserScript.setProperty(false,6,false,0,false,0,0,25,-942,0,-280,0,90,0);
		
		newLaser = (GameObject)Instantiate(ptrScriptVariable.Laser);
		newLaser.tag = "temp";
		newLaserScript = (LaserScript) newLaser.GetComponent( typeof(LaserScript) );
		newLaserScript.setProperty(false,7,false,0,false,0,0,25,-938,0,-280,0,90,0);
		
		newLaser = (GameObject)Instantiate(ptrScriptVariable.Laser);
		newLaser.tag = "temp";
		newLaserScript = (LaserScript) newLaser.GetComponent( typeof(LaserScript) );
		newLaserScript.setProperty(false,8,false,0,false,0,0,25,-936,0,-282,0,180,0);
		
		newLaser = (GameObject)Instantiate(ptrScriptVariable.Laser);
		newLaser.tag = "temp";
		newLaserScript = (LaserScript) newLaser.GetComponent( typeof(LaserScript) );
		newLaserScript.setProperty(false,9,false,0,false,0,0,25,-936,0,-286,0,180,0);
		
		newLaser = (GameObject)Instantiate(ptrScriptVariable.Laser);
		newLaser.tag = "temp";
		newLaserScript = (LaserScript) newLaser.GetComponent( typeof(LaserScript) );
		newLaserScript.setProperty(false,10,false,0,false,0,0,25,-936,0,-290,0,180,0);
		
		newLaser = (GameObject)Instantiate(ptrScriptVariable.Laser);
		newLaser.tag = "temp";
		newLaserScript = (LaserScript) newLaser.GetComponent( typeof(LaserScript) );
		newLaserScript.setProperty(false,11,false,0,false,0,0,25,-936,0,-294,0,180,0);
		
		newLaser = (GameObject)Instantiate(ptrScriptVariable.Laser);
		newLaser.tag = "temp";
		newLaserScript = (LaserScript) newLaser.GetComponent( typeof(LaserScript) );
		newLaserScript.setProperty(false,12,false,0,false,0,0,25,-936,0,-298,0,180,0);
		
		newLaser = (GameObject)Instantiate(ptrScriptVariable.Laser);
		newLaser.tag = "temp";
		newLaserScript = (LaserScript) newLaser.GetComponent( typeof(LaserScript) );
		newLaserScript.setProperty(false,13,false,0,false,0,0,25,-938,0,-300,0,270,0);
		
		newLaser = (GameObject)Instantiate(ptrScriptVariable.Laser);
		newLaser.tag = "temp";
		newLaserScript = (LaserScript) newLaser.GetComponent( typeof(LaserScript) );
		newLaserScript.setProperty(false,14,false,0,false,0,0,25,-942,0,-300,0,270,0);
		
		newLaser = (GameObject)Instantiate(ptrScriptVariable.Laser);
		newLaser.tag = "temp";
		newLaserScript = (LaserScript) newLaser.GetComponent( typeof(LaserScript) );
		newLaserScript.setProperty(false,15,false,0,false,0,0,25,-946,0,-300,0,270,0);
		
		newLaser = (GameObject)Instantiate(ptrScriptVariable.Laser);
		newLaser.tag = "temp";
		newLaserScript = (LaserScript) newLaser.GetComponent( typeof(LaserScript) );
		newLaserScript.setProperty(false,16,false,0,false,0,0,25,-950,0,-300,0,270,0);
		
		newLaser = (GameObject)Instantiate(ptrScriptVariable.Laser);
		newLaser.tag = "temp";
		newLaserScript = (LaserScript) newLaser.GetComponent( typeof(LaserScript) );
		newLaserScript.setProperty(false,17,false,0,false,0,0,25,-954,0,-300,0,270,0);
		
		newLaser = (GameObject)Instantiate(ptrScriptVariable.Laser);
		newLaser.tag = "temp";
		newLaserScript = (LaserScript) newLaser.GetComponent( typeof(LaserScript) );
		newLaserScript.setProperty(false,18,false,0,false,0,0,25,-958,0,-300,0,270,0);
		
		newLaser = (GameObject)Instantiate(ptrScriptVariable.Laser);
		newLaser.tag = "temp";
		newLaserScript = (LaserScript) newLaser.GetComponent( typeof(LaserScript) );
		newLaserScript.setProperty(false,19,false,0,false,0,0,25,-962,0,-300,0,270,0);
		
		newLaser = (GameObject)Instantiate(ptrScriptVariable.Laser);
		newLaser.tag = "temp";
		newLaserScript = (LaserScript) newLaser.GetComponent( typeof(LaserScript) );
		newLaserScript.setProperty(false,20,false,0,false,0,0,25,-966,0,-300,0,270,0);
		
		newLaser = (GameObject)Instantiate(ptrScriptVariable.Laser);
		newLaser.tag = "temp";
		newLaserScript = (LaserScript) newLaser.GetComponent( typeof(LaserScript) );
		newLaserScript.setProperty(false,21,false,0,false,0,0,25,-968,0,-298,0,0,0);
		
		newLaser = (GameObject)Instantiate(ptrScriptVariable.Laser);
		newLaser.tag = "temp";
		newLaserScript = (LaserScript) newLaser.GetComponent( typeof(LaserScript) );
		newLaserScript.setProperty(false,22,false,0,false,0,0,25,-968,0,-294,0,0,0);
		
		newLaser = (GameObject)Instantiate(ptrScriptVariable.Laser);
		newLaser.tag = "temp";
		newLaserScript = (LaserScript) newLaser.GetComponent( typeof(LaserScript) );
		newLaserScript.setProperty(false,23,false,0,false,0,0,25,-968,0,-290,0,0,0);
		
		newLaser = (GameObject)Instantiate(ptrScriptVariable.Laser);
		newLaser.tag = "temp";
		newLaserScript = (LaserScript) newLaser.GetComponent( typeof(LaserScript) );
		newLaserScript.setProperty(false,24,false,0,false,0,0,25,-968,0,-286,0,0,0);
		
		newLaser = (GameObject)Instantiate(ptrScriptVariable.Laser);
		newLaser.tag = "temp";
		newLaserScript = (LaserScript) newLaser.GetComponent( typeof(LaserScript) );
		newLaserScript.setProperty(false,25,false,0,false,0,0,25,-968,0,-282,0,0,0);
		
	}
	
	public void musicOn()
	{
		ptrScriptVariable.objCamera.GetComponent<AudioSource>().clip = ptrScriptVariable.BackGround;
		ptrScriptVariable.objCamera.GetComponent<AudioSource>().loop = true;
		ptrScriptVariable.objCamera.GetComponent<AudioSource>().volume = volMusic;
		ptrScriptVariable.objCamera.GetComponent<AudioSource>().Play();
	}
	
	public void adjustVolMusic()
	{
		ptrScriptVariable.objCamera.GetComponent<AudioSource>().volume = volMusic;
	}
	
	public void musicOff()
	{
		ptrScriptVariable.objCamera.GetComponent<AudioSource>().Pause();
	}
	
	public void gameStart() {
		pause = false;
		level1Initial();
	}
	
}
