using UnityEngine;
using System.Collections;

public class BoxGameManagerScript : MonoBehaviour {
	public bool firstReset = false;
	public bool startState;
	public int boxRightNumber;
	public int totalBox;
	public int resetX;
	public int resetZ;
	public int xOffset;
	public int zOffset;
	public int startBoxx;
	public int startBoxz;
	public int resetBoxx;
	public int resetBoxz;
	public ObjectiveScript[] ObjectiveScripts;
	public BoxGameScript[] BoxGameScripts;
	private GameObject objPlayer;
	public GameObject objBoxGame1;
	public GameObject objBoxGame2;
	public GameObject objBox1;
	public GameObject objBox2;
	public GameObject reward;
	// Use this for initialization
	void Start () {
		startState = true;
		boxRightNumber = 0;
		ObjectiveScripts = GetComponentsInChildren< ObjectiveScript >();
		BoxGameScripts = GetComponentsInChildren< BoxGameScript >();
		objPlayer = (GameObject) GameObject.FindWithTag ("Player");
		objBoxGame2.SetActive(false);
		objBox1.SetActive(false);
		objBox2.SetActive(true);
		Reset();
		firstReset = true;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 tempVector = objPlayer.transform.position-new Vector3(resetX+xOffset,objPlayer.transform.position.y,resetZ+zOffset);
		if ((!startState) && (tempVector.x*tempVector.x+tempVector.z*tempVector.z < 1))
		{
			Reset();
			return;
		}
		boxRightNumber = 0;
		ObjectiveScripts = GetComponentsInChildren< ObjectiveScript >();
		foreach (ObjectiveScript os in ObjectiveScripts)
		{
			if (os.BoxRight)
			{
				boxRightNumber++;
			}
		}
		BoxGameScripts = GetComponentsInChildren< BoxGameScript >();
		if (boxRightNumber == totalBox)
		{
			foreach (BoxGameScript bgs in BoxGameScripts)
			{
				if (bgs.gameObject!=null)
				{
					if (bgs.name == "Box" || bgs.name == "Box1" || bgs.name == "Box2" || bgs.name == "Trigger")
						Destroy(bgs.gameObject);
					if (bgs.name == "Reset")
					{
						GameObject gun = (GameObject) Instantiate(reward,bgs.gameObject.transform.position,Quaternion.identity);
						gun.transform.Rotate(new Vector3(0,140,0));
						Destroy(bgs.gameObject);
					}
				}
			}
		}
	}
	
	void Reset()
	{
		if (totalBox != 3 && firstReset)
		{
			firstReset = false;
			objBox1.SetActive(true);
			objBox2.SetActive(false);
		}
		boxRightNumber = 0;
		BoxGameScripts = GetComponentsInChildren< BoxGameScript >();
		foreach (BoxGameScript bgs in BoxGameScripts)
		{
			bgs.transform.position = new Vector3(bgs.x+xOffset,0,bgs.z+zOffset);
		}
		ObjectiveScripts = GetComponentsInChildren< ObjectiveScript >();
		foreach (ObjectiveScript os in ObjectiveScripts)
		{
			os.BoxRight = false;
		}
	}
	
	public void Trigger()
	{
		objBoxGame2.SetActive(true);
		objBoxGame1.SetActive(false);
		totalBox = 3;
	}
}

