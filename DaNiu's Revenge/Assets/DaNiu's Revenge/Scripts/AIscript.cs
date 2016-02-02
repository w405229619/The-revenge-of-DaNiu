using UnityEngine;
using System.Collections;

public class AIscript : MonoBehaviour {
	//game objects  (variables which point to game objects)
	private bool pause;
	private GameObject objPlayer;
	private GameObject objCamera;
	private VariableScript ptrScriptVariable;
	private gameScript ptrGameScript;

	//input variables (variables used to process and handle input)
	private Vector3 inputRotation;
	private Vector3 inputMovement;
	private bool meleeAttackState;
	
	//identity variables (variables specific to the game object)
	public float moveSpeed = 25000f;
	public float healthMax = 1f;	
	public float health = 1f;
	private bool thisIsPlayer;

	// calculation variables (variables used for calculation)
	private Vector3 tempVector;
	private Vector3 tempVector2;
	private int i;
	private bool meleeDamageState;
	
	// animation variables (variables used for processing aniamtion)
	public float animationFrameRate = 11f; // how many frames to play per second
	public float walkAnimationMin = 1; // the first frame of the walk animation
	public float walkAnimationMax = 10; // the last frame of the walk animation
	public float standAnimationMin = 11; // the first frame of the stand animation
	public float standAnimationMax = 20; // the last frame of the stand animation
	public float meleeAnimationMin = 23; // the first frame of the melee animation
	public float meleeAnimationMax = 30; // the last frame of the melee animation
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
	public Material PlayerWithGun;
	public float Damage = 10f;
	public bool canRemove = true;
	
	public int gunType = 1;
	public int gunLevel = 1;
	public float bulletInteval = 0.3f;
	public float bulletIntevalMax = 0.3f;
	
	// Use this for initialization
	void Start () {
		objPlayer = (GameObject) GameObject.FindWithTag ("Player");
		objCamera = (GameObject) GameObject.FindWithTag ("MainCamera");
		if (gameObject.tag == "Player") { thisIsPlayer = true; }
		ptrScriptVariable = (VariableScript) objCamera.GetComponent( typeof(VariableScript) );
		ptrGameScript = (gameScript)objCamera.GetComponent( typeof(gameScript) );
	}
	
	// Update is called once per frame
	void Update () {
		pause = ptrGameScript.pause;
		if(!ptrGameScript.menuShow)
		{
			if(!pause)
			{
				if (health <= 0)
				{
					if (gameObject.name == "Player")
					{
						ptrGameScript.musicOff();
						ptrScriptVariable.objCamera.GetComponent<AudioSource>().clip = ptrScriptVariable.Death;
						ptrScriptVariable.objCamera.GetComponent<AudioSource>().loop = false;
						ptrScriptVariable.objCamera.GetComponent<AudioSource>().volume = ptrGameScript.volSound;
						ptrScriptVariable.objCamera.GetComponent<AudioSource>().Play();
					}
					removeMe();
				}else
				{
					FindInput();
					if(canRemove)
						ProcessMovement();
					HandleAnimation();
					if (thisIsPlayer == true)
					{
						HandleCamera();
					}
				}
			}
		}
	}

	public void GetGun (){
		gunType = 1;
		gunLevel = 1;
		objSpriteRender.GetComponent<Renderer>().material = PlayerWithGun;
	}
	
	void FindInput ()
	{
		if (thisIsPlayer == true)
		{
			FindPlayerInput();
		} else {
			FindAIinput();
		}
	}
	void FindPlayerInput ()
	{
		// find vector to move
		inputMovement = new Vector3( Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical") );
		// find vector to the mouse
		tempVector2 = new Vector3(Screen.width * 0.5f,0,Screen.height * 0.5f); // the position of the middle of the screen
		tempVector = Input.mousePosition; // find the position of the moue on screen
		tempVector.z = tempVector.y; // input mouse position gives us 2D coordinates, I am moving the Y coordinate to the Z coorindate in temp Vector and setting the Y coordinate to 0, so that the Vector will read the input along the X (left and right of screen) and Z (up and down screen) axis, and not the X and Y (in and out of screen) axis
		tempVector.y = 0;
		inputRotation = tempVector - tempVector2; // the direction we want face/aim/shoot is from the middle of the screen to where the mouse is pointing

		if ( Input.GetMouseButtonDown(0) )
		{
			HandleBullets();
		}
		if ( Input.GetMouseButton(0) )
		{
			bulletInteval -= Time.deltaTime;
			if( bulletInteval <= 0)
			{
				HandleBullets();
				bulletInteval = bulletIntevalMax;
			}
		}
		if (  Input.GetMouseButtonUp(0) )
		{
			bulletInteval = bulletIntevalMax;
		}
	}
	
	void OnCollisionEnter(Collision Other) {
	}
	void HandleBullets ()
	{
		if ( gunType == 0 ){}
		else if (  gunType == 1 )
		{
			tempVector = Quaternion.AngleAxis(8f, Vector3.up) * inputRotation;
			tempVector = (transform.position + (tempVector.normalized * 0.8f));
			
			GameObject objCreatedBullet = (GameObject) Instantiate(ptrScriptVariable.objBullet, tempVector, Quaternion.LookRotation(inputRotation) ); // create a bullet, and rotate it based on the vector inputRotation
			objCreatedBullet.tag = "bullet";
			Physics.IgnoreCollision(objCreatedBullet.GetComponent<Collider>(), GetComponent<Collider>());
			
			GameObject objBulletHelper = (GameObject) Instantiate(ptrScriptVariable.bulletHelper, tempVector, Quaternion.LookRotation(inputRotation) ); // create a bullet helper
			objBulletHelper.GetComponent<AudioSource>().volume = ptrGameScript.volSound;
			
			objCreatedBullet = (GameObject) Instantiate(ptrScriptVariable.objBullet, tempVector, Quaternion.LookRotation(inputRotation) ); // create a bullet, and rotate it based on the vector inputRotation
			objCreatedBullet.transform.Translate(0.1f, 0, 0);
			objCreatedBullet.tag = "bullet";
			Physics.IgnoreCollision(objCreatedBullet.GetComponent<Collider>(), GetComponent<Collider>());
			objCreatedBullet = (GameObject) Instantiate(ptrScriptVariable.objBullet, tempVector, Quaternion.LookRotation(inputRotation) ); // create a bullet, and rotate it based on the vector inputRotation
			objCreatedBullet.transform.Translate(-0.1f, 0, 0);
			objCreatedBullet.tag = "bullet";
			if ( gunLevel >= 2)
			{
				objCreatedBullet = (GameObject) Instantiate(ptrScriptVariable.objBullet, tempVector, Quaternion.LookRotation(inputRotation) ); // create a bullet, and rotate it based on the vector inputRotation
				objCreatedBullet.transform.Translate(0.2f, 0, 0);
				objCreatedBullet.tag = "bullet";
				Physics.IgnoreCollision(objCreatedBullet.GetComponent<Collider>(), GetComponent<Collider>());
				objCreatedBullet = (GameObject) Instantiate(ptrScriptVariable.objBullet, tempVector, Quaternion.LookRotation(inputRotation) ); // create a bullet, and rotate it based on the vector inputRotation
				objCreatedBullet.transform.Translate(-0.2f, 0, 0);
				objCreatedBullet.tag = "bullet";
				Physics.IgnoreCollision(objCreatedBullet.GetComponent<Collider>(), GetComponent<Collider>());
				if ( gunLevel == 3 )
				{
					objCreatedBullet = (GameObject) Instantiate(ptrScriptVariable.objBullet, tempVector, Quaternion.LookRotation(inputRotation) ); // create a bullet, and rotate it based on the vector inputRotation
					objCreatedBullet.transform.Translate(0.3f, 0, 0);
					objCreatedBullet.tag = "bullet";
					Physics.IgnoreCollision(objCreatedBullet.GetComponent<Collider>(), GetComponent<Collider>());
					objCreatedBullet = (GameObject) Instantiate(ptrScriptVariable.objBullet, tempVector, Quaternion.LookRotation(inputRotation) ); // create a bullet, and rotate it based on the vector inputRotation
					objCreatedBullet.transform.Translate(-0.3f, 0, 0);
					objCreatedBullet.tag = "bullet";
				}
			}
		}else if ( gunType == 2 )
		{
			tempVector = Quaternion.AngleAxis(8f, Vector3.up) * inputRotation;
			tempVector = (transform.position + (tempVector.normalized * 0.8f));
			
			GameObject objCreatedBullet = (GameObject) Instantiate(ptrScriptVariable.objBullet, tempVector, Quaternion.LookRotation(inputRotation) ); // create a bullet, and rotate it based on the vector inputRotation
			objCreatedBullet.tag = "bullet";
			Physics.IgnoreCollision(objCreatedBullet.GetComponent<Collider>(), GetComponent<Collider>());
			
			GameObject objBulletHelper = (GameObject) Instantiate(ptrScriptVariable.bulletHelper, tempVector, Quaternion.LookRotation(inputRotation) ); // create a bullet helper
			objBulletHelper.GetComponent<AudioSource>().volume = objBulletHelper.GetComponent<AudioSource>().volume*ptrGameScript.volSound;
						
			objCreatedBullet = (GameObject) Instantiate(ptrScriptVariable.objBullet, tempVector, Quaternion.LookRotation(inputRotation) ); // create a bullet, and rotate it based on the vector inputRotation
			objCreatedBullet.transform.Rotate(Vector3.up,30);
			objCreatedBullet.tag = "bullet";
			Physics.IgnoreCollision(objCreatedBullet.GetComponent<Collider>(), GetComponent<Collider>());
			
			objCreatedBullet = (GameObject) Instantiate(ptrScriptVariable.objBullet, tempVector, Quaternion.LookRotation(inputRotation) ); // create a bullet, and rotate it based on the vector inputRotation
			objCreatedBullet.transform.Rotate(Vector3.up,-30);
			objCreatedBullet.tag = "bullet";
			Physics.IgnoreCollision(objCreatedBullet.GetComponent<Collider>(), GetComponent<Collider>());
			
			if ( gunLevel == 2)
			{
				objCreatedBullet = (GameObject) Instantiate(ptrScriptVariable.objBullet, tempVector, Quaternion.LookRotation(inputRotation) ); // create a bullet, and rotate it based on the vector inputRotation
				objCreatedBullet.transform.Rotate(Vector3.up,15);
				objCreatedBullet.tag = "bullet";
				Physics.IgnoreCollision(objCreatedBullet.GetComponent<Collider>(), GetComponent<Collider>());
				
				objCreatedBullet = (GameObject) Instantiate(ptrScriptVariable.objBullet, tempVector, Quaternion.LookRotation(inputRotation) ); // create a bullet, and rotate it based on the vector inputRotation
				objCreatedBullet.transform.Rotate(Vector3.up,-15);
				objCreatedBullet.tag = "bullet";
				Physics.IgnoreCollision(objCreatedBullet.GetComponent<Collider>(), GetComponent<Collider>());
			}
			
			if ( gunLevel == 3 )
			{
				objCreatedBullet = (GameObject) Instantiate(ptrScriptVariable.objBullet, tempVector, Quaternion.LookRotation(inputRotation) ); // create a bullet, and rotate it based on the vector inputRotation
				objCreatedBullet.transform.Rotate(Vector3.up,10);
				objCreatedBullet.tag = "bullet";
				Physics.IgnoreCollision(objCreatedBullet.GetComponent<Collider>(), GetComponent<Collider>());
				
				objCreatedBullet = (GameObject) Instantiate(ptrScriptVariable.objBullet, tempVector, Quaternion.LookRotation(inputRotation) ); // create a bullet, and rotate it based on the vector inputRotation
				objCreatedBullet.transform.Rotate(Vector3.up,20);
				objCreatedBullet.tag = "bullet";
				Physics.IgnoreCollision(objCreatedBullet.GetComponent<Collider>(), GetComponent<Collider>());
				
				objCreatedBullet = (GameObject) Instantiate(ptrScriptVariable.objBullet, tempVector, Quaternion.LookRotation(inputRotation) ); // create a bullet, and rotate it based on the vector inputRotation
				objCreatedBullet.transform.Rotate(Vector3.up,-10);
				objCreatedBullet.tag = "bullet";
				Physics.IgnoreCollision(objCreatedBullet.GetComponent<Collider>(), GetComponent<Collider>());
				
				objCreatedBullet = (GameObject) Instantiate(ptrScriptVariable.objBullet, tempVector, Quaternion.LookRotation(inputRotation) ); // create a bullet, and rotate it based on the vector inputRotation
				objCreatedBullet.transform.Rotate(Vector3.up,-20);
				objCreatedBullet.tag = "bullet";
				Physics.IgnoreCollision(objCreatedBullet.GetComponent<Collider>(), GetComponent<Collider>());
			}
		}
	}
	void FindAIinput ()
	{
		meleeAttackState = false;
		if (objPlayer == null)
		{
			inputMovement = Vector3.zero;
			inputRotation = transform.forward * -1;
			return;
		}
		inputMovement = objPlayer.transform.position - transform.position;
		inputRotation = inputMovement; // face the direction we are moving, towards the player
		if ( Vector3.Distance(objPlayer.transform.position,transform.position) < 1.2f )
		{
			meleeAttackState = true;
			inputMovement = Vector3.zero;
			if (currentAnimation != animationMelee) { frameNumber = meleeAnimationMin + 1; }
		}
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
	void HandleCamera()
	{
		objCamera.transform.position = new Vector3(transform.position.x,15,transform.position.z);
		objCamera.transform.eulerAngles = new Vector3(90,0,0);
	}

	void HandleAnimation () // handles all animation
	{
		FindAnimation();
		ProcessAnimation();
	}
	void FindAnimation ()
	{
		if (meleeAttackState == true || currentAnimation == animationMelee)
		{
			currentAnimation = animationMelee;
			return;
		}
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
				if (currentAnimation == animationMelee)
				{
					frameNumber = Mathf.Clamp(frameNumber,meleeAnimationMin,meleeAnimationMax+1);
					if (frameNumber > meleeAnimationMax)
					{
						meleeDamageState = false; // once we have finished playing our melee animation, we can detect if we have hit the player again
						if (meleeAttackState == true)
						{
							frameNumber = meleeAnimationMin;
						} else {
							currentAnimation = animationWalk;
							frameNumber = walkAnimationMin;
						}
					}

					if (meleeAttackState == true && frameNumber > walkAnimationMin + 4 && meleeDamageState == false && objPlayer != null) // if we are within 1.2 units of the player and if we are at least 4 frames into the animation, and we haven't attacked yet
					{
						meleeDamageState = true;
						AIscript ptrScriptAI = (AIscript) objPlayer.GetComponent( typeof(AIscript) );
						ptrScriptAI.health -= Damage; // damage the player
						Instantiate(ptrScriptVariable.parPlayerHit, objPlayer.transform.position, Quaternion.identity );
					}
				}
			// cyclic animations (cycle through the animation)
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
		if (thisIsPlayer == false)
		{
			Instantiate(ptrScriptVariable.parAlienDeath, transform.position, Quaternion.identity );
			Destroy(gameObject);
		} else {
			Instantiate(ptrScriptVariable.parPlayerDeath, transform.position, Quaternion.identity );
			gameObject.SetActive(false);
			GameObject[] all = GameObject.FindGameObjectsWithTag("temp");
			foreach (GameObject a in all) {
				Destroy(a);
			}
			ptrGameScript.menuShow = true;
			//Time.timeScale = 0;
		}
	}
}