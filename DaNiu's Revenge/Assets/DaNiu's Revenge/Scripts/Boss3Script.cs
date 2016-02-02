using UnityEngine;
using System.Collections;

public class Boss3Script : MonoBehaviour {
	private bool pause;
	public GameObject Hp;
	public GameObject father;
	public GameObject HpB;
	private GameObject objPlayer;
	private GameObject objCamera;
	private VariableScript ptrScriptVariable;
	private gameScript ptrGameScript;
	
	private Vector3 inputRotation;
	private Vector3 inputMovement;
	
	public float moveSpeed = 25000f;
	public float healthMax = 1f;	
	public float health = 1f;
	
	private Vector3 tempVector;
	private Vector3 tempVector2;
	private int i;
	
	public float animationFrameRate = 11f; // how many frames to play per second
	public float walkAnimationMin = 1; // the first frame of the walk animation
	public float walkAnimationMax = 10; // the last frame of the walk animation
	public float standAnimationMin = 11; // the first frame of the stand animation
	public float standAnimationMax = 20; // the last frame of the stand animation
	public float spriteSheetTotalRow = 5; // the total number of columns of the sprite sheet
	public float spriteSheetTotalHigh = 4; // the total number of rows of the sprite sheet
	public float frameNumber = 1; // the current frame being played,
	private float animationStand = 0; // the ID of the stand animation
	private float animationWalk = 1; // the ID of the walk animation
	private float animationMelee = 2; // the ID of the melee animation
	private float currentAnimation = 1; // the ID of the current animation being played
	private float animationTime = 0f; // time to pass before playing next animation
	private Vector2 spriteSheetCount; // the X, Y position of the frame
	private Vector2 spriteSheetOffset; // the offset value of the X, Y coordinate for the texture
	public GameObject objSpriteRender;
	
	public float bulletInteval = 3f;
	public float bulletIntevalMax = 3f;
	// Use this for initialization
	void Start () {
		objPlayer = (GameObject) GameObject.FindWithTag ("Player");
		objCamera = (GameObject) GameObject.FindWithTag ("MainCamera");
		ptrScriptVariable = (VariableScript) objCamera.GetComponent( typeof(VariableScript) );
		ptrGameScript = (gameScript)objCamera.GetComponent( typeof(gameScript) );
		inputMovement = new Vector3(-1,0,0);
	}
	
	// Update is called once per frame
	void Update () {
		pause = ptrGameScript.pause;
		if(!pause)
			{
			HpHandle();
			if (health <= 0)
			{
				removeMe();
			}
			FindAIinput();
			ProcessMovement();
			HandleAnimation();
			bulletInteval -= Time.deltaTime;
			if(bulletInteval<=0)
			{
				HandleBullets();
				bulletInteval = bulletIntevalMax;
			}
		}
	}
	
	void HpHandle(){
		float length = health*0.8f/healthMax;
		Hp.transform.localScale = new Vector3(length,0.05f,0.05f);
		Hp.transform.position = transform.position+new Vector3(0,0,0.4f)-new Vector3(0.4f-length/2,-	0.1f,0);
		HpB.transform.position = transform.position+new Vector3(0,0,0.4f);
	}
	
	void FindAIinput ()
	{
		if (objPlayer == null)
		{
			inputMovement = Vector3.zero;
			inputRotation = transform.forward * -1;
			return;
		}
		if(inputMovement == new Vector3(-1,0,0))
		{
			if(transform.position.x<-966)
				inputMovement = new Vector3(0,0,-1);
		}
		if(inputMovement == new Vector3(0,0,-1))
		{
			if(transform.position.z<-298)
				inputMovement = new Vector3(1,0,0);
		}
		if(inputMovement == new Vector3(1,0,0))
		{
			if(transform.position.x>-938)
				inputMovement = new Vector3(0,0,1);
		}
		if(inputMovement == new Vector3(0,0,1))
		{
			if(transform.position.z>-282)
				inputMovement = new Vector3(-1,0,0);
		}
		inputRotation = objPlayer.transform.position - transform.position; // face the direction we are moving, towards the player
	}
	void HandleBullets (){
		tempVector = Quaternion.AngleAxis(8f, Vector3.up) * inputRotation;
		tempVector = (transform.position + (tempVector.normalized * 0.8f));
		
		GameObject objCreatedBullet = (GameObject) Instantiate(ptrScriptVariable.objBullet, tempVector, Quaternion.LookRotation(inputRotation) ); // create a bullet, and rotate it based on the vector inputRotation
		objCreatedBullet.tag = "enemyBullet";
		Physics.IgnoreCollision(objCreatedBullet.GetComponent<Collider>(), GetComponent<Collider>());

		objCreatedBullet = (GameObject) Instantiate(ptrScriptVariable.objBullet, tempVector, Quaternion.LookRotation(inputRotation) ); // create a bullet, and rotate it based on the vector inputRotation
		objCreatedBullet.transform.Translate(0.3f, 0, 0);
		objCreatedBullet.tag = "enemyBullet";

		Physics.IgnoreCollision(objCreatedBullet.GetComponent<Collider>(), GetComponent<Collider>());
		objCreatedBullet = (GameObject) Instantiate(ptrScriptVariable.objBullet, tempVector, Quaternion.LookRotation(inputRotation) ); // create a bullet, and rotate it based on the vector inputRotation
		objCreatedBullet.transform.Translate(-0.3f, 0, 0);
		objCreatedBullet.tag = "enemyBullet";
		
	}
	
	void ProcessMovement()
	{
		tempVector = GetComponent<Rigidbody>().GetPointVelocity(transform.position) * Time.deltaTime * 1000;
		GetComponent<Rigidbody>().AddForce (-tempVector.x, -tempVector.y, -tempVector.z);

		GetComponent<Rigidbody>().AddForce (inputMovement.normalized * moveSpeed * Time.deltaTime);
		transform.rotation = Quaternion.LookRotation(inputRotation);
		transform.eulerAngles = new Vector3(0,transform.eulerAngles.y + 180,0);
		transform.position = new Vector3(transform.position.x,0,transform.position.z);
	}
	void HandleAnimation () // handles all animation
	{
		FindAnimation();
		ProcessAnimation();
	}
	void FindAnimation ()
	{
		if (inputMovement.magnitude > 0)
		{
			currentAnimation = animationWalk;
		} else {
			currentAnimation = animationStand;
		}
	}
	
	void ProcessAnimation ()
	{
		animationTime -= Time.deltaTime; // animationTime -= Time.deltaTime; subtract the number of seconds passed since the last frame, if the game is running at 30 frames per second the variable will subtract by 0.033 of a second (1/30)
		if (animationTime <= 0)
		{
			frameNumber += 1;
			// one play animations (play from start to finish)
			if (currentAnimation == animationStand)
			{
				frameNumber = Mathf.Clamp(frameNumber,standAnimationMin,standAnimationMax+1);
				if (frameNumber > standAnimationMax)
				{
					frameNumber = standAnimationMin;
				}
			}
			if (currentAnimation == animationWalk)
			{
				frameNumber = Mathf.Clamp(frameNumber,walkAnimationMin,walkAnimationMax+1);
				if (frameNumber > walkAnimationMax)
				{
					frameNumber = walkAnimationMin;
				}
			}
			animationTime += (1/animationFrameRate); // if the animationFrameRate is 11, 1/11 is one eleventh of a second, that is the time we are waiting before we play the next frame.
		}
		spriteSheetCount.y = 0;
		for (i=(int)frameNumber; i > 5; i-=5) // find the number of frames down the animation is and set the y coordinate accordingly
		{
			spriteSheetCount.y += 1;
		}
		spriteSheetCount.x = i - 1; // find the X coordinate of the frame to play
		spriteSheetOffset = new Vector2(1 - (spriteSheetCount.x/spriteSheetTotalRow),1 - (spriteSheetCount.y/spriteSheetTotalHigh));  // find the X and Y coordinate of the frame to display
		objSpriteRender.GetComponent<Renderer>().material.SetTextureOffset ("_MainTex", spriteSheetOffset); // offset the texture to display the correct frame
	}
	void removeMe () // removes the character
	{
		Instantiate(ptrScriptVariable.parPlayerDeath, transform.position, Quaternion.identity );
		DestroyTemp();
		ptrGameScript.ending = true;
		Destroy(father);
	}
	
	void DestroyTemp(){
		GameObject[] all = GameObject.FindGameObjectsWithTag("temp");
		foreach (GameObject a in all) {
			Destroy(a);
		}
	}
}
