using UnityEngine;
using System.Collections;

public class EndingScript : MonoBehaviour {
	public int nowObj;
	private int player = 0;
	private int brother = 1;
	private int lazer = 2;
	public float moveSpeed = 25000f;
	private GameObject objPlayer;
	private GameObject objCamera;
	private VariableScript ptrScriptVariable;
	public GameObject LaserLight;
	public GameObject emitStart;

	private gameScript ptrGameScript;
	public Vector3 inputRotation;
	public Vector3 inputMovement;
	
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
	private float length = 0f;
	private bool nolevel = true;
	// Use this for initialization
	void Start () {
		objPlayer = (GameObject) GameObject.FindWithTag ("Player");
		objCamera = (GameObject) GameObject.FindWithTag ("MainCamera");
		ptrGameScript = (gameScript) objCamera.GetComponent( typeof(gameScript) );
		ptrScriptVariable = (VariableScript) objCamera.GetComponent( typeof(VariableScript) );
		if(nowObj!=player)
			inputMovement = Vector3.zero;
		else
		{
			objPlayer.transform.position = new Vector3(0,0,-5);
			inputMovement =new Vector3(-1,0,1);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(length<=2)
			length += 0.002f;
		else
		{
			if(nowObj==player && nolevel)
			{
				nolevel = false;
				ptrGameScript.LevelUp();
			}
		}
		if(nowObj==lazer)
		{
			LaserLight.transform.position = emitStart.transform.position + new Vector3(length,0,0);
			LaserLight.transform.localScale = new Vector3(0.2f,1,length);
		}else if(nowObj==player)
		{
			HandleCamera();
			FindAIinput();
			ProcessMovement();
			HandleAnimation();
		}else
		{
			FindAIinput();
			ProcessMovement();
			HandleAnimation();		
		}
	}
	void HandleCamera()
	{
		objCamera.transform.position = new Vector3(transform.position.x,15,transform.position.z);
		objCamera.transform.eulerAngles = new Vector3(90,0,0);
	}
	void FindAIinput ()
	{
		if(nowObj==player)
		{
			inputRotation = new Vector3(-10,0,0) - transform.position;
			if(transform.position.z>0)
			{
				inputMovement = Vector3.zero;
			}
		}else
		{
			inputRotation = objPlayer.transform.position - transform.position;	
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
	}
}
