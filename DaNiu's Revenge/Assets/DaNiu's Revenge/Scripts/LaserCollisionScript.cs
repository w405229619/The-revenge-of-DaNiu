using UnityEngine;
using System.Collections;

public class LaserCollisionScript : MonoBehaviour {
	private bool pause;
	private GameObject objCamera;
	private gameScript ptrGameScript;
	public Vector3 po;
	public Vector3 sc;
	public GameObject light;
	public GameObject Laser;
	private LaserScript laserscript;
	private GameObject objPlayer;
	public Collider[] nearest = new Collider[50];
	public int length=0;
	public GameObject emitStart;
	private AIscript objAIScriptAI;
	// Use this for initialization
	void Start () {
		po = light.transform.position;
		sc = light.transform.localScale;
		objPlayer = (GameObject) GameObject.FindWithTag ("Player");
		objAIScriptAI  = (AIscript) objPlayer.GetComponent( typeof(AIscript) );
		laserscript = (LaserScript) Laser.GetComponent( typeof(LaserScript) );
		objCamera = (GameObject) GameObject.FindWithTag ("MainCamera");
		ptrGameScript = (gameScript)objCamera.GetComponent( typeof(gameScript) );
	}
	
	// Update is called once per frame
	void Update () {
		pause = ptrGameScript.pause;
		if(!pause)
		{
			if(length>0)
			{
				if(nearest[0].name == "Player" && laserscript.LaserOn)
				{
					objAIScriptAI.health -= 10;
				}
			}
			light.transform.position = new Vector3(light.transform.position.x,0,light.transform.position.z);
		}
	}
	void OnTriggerExit(Collider Other){
		if(Other.name != "LaserLightCollider" && Other.name != "BulletPrefab(Clone)")
		{
			Collider now;
			for(int i=0;i<length;i++)
			{
				now=nearest[i];
				if(now != Other)
					continue;
				else
				{
					for(int j=i;j<length-1;j++)
					{
						nearest[j]=nearest[j+1];
					}
					length--;
				}
			}
			if(length>0)
			{
				now=nearest[0];
				Vector3 point = now.ClosestPointOnBounds(emitStart.transform.position);
				Vector3 relative = point-emitStart.transform.position;
				float angle = Vector3.Angle(relative,transform.forward)/180*Mathf.PI;
				Vector3 project = transform.forward*Mathf.Abs(relative.magnitude/Mathf.Cos(angle));
				Vector3 middle = project/2+emitStart.transform.position;
				float temp = project.magnitude/2;
				light.transform.localScale = new Vector3(light.transform.localScale.x,light.transform.localScale.y,temp);
				light.transform.position = middle;
			}else{
				light.transform.localScale = sc;
				light.transform.position = po;
			}
		}
	}
	void OnTriggerStay(Collider Other){
		if(nearest[0]==Other)
		{
			Vector3 point = Other.ClosestPointOnBounds(emitStart.transform.position);
			Vector3 relative = point-emitStart.transform.position;
			float angle = Vector3.Angle(relative,transform.forward)/180*Mathf.PI;
			Vector3 project = transform.forward*Mathf.Abs(relative.magnitude/Mathf.Cos(angle));
			Vector3 middle = project/2+emitStart.transform.position;
			float temp = project.magnitude/2;
			light.transform.localScale = new Vector3(light.transform.localScale.x,light.transform.localScale.y,temp);
			light.transform.position = middle;
		}
	}
	void OnTriggerEnter(Collider Other){
		if(Other.name != "LaserLightCollider" && Other.name != "BulletPrefab(Clone)")
		{
			Vector3 point = Other.ClosestPointOnBounds(emitStart.transform.position);
			Vector3 relative = point-emitStart.transform.position;
			float angle = Vector3.Angle(relative,transform.forward)/180*Mathf.PI;
			Vector3 project = transform.forward*Mathf.Abs(relative.magnitude/Mathf.Cos(angle));
			Vector3 middle = project/2+emitStart.transform.position;
			float temp = project.magnitude/2;
			if(length==0)
			{
				light.transform.localScale = new Vector3(light.transform.localScale.x,light.transform.localScale.y,temp);
				light.transform.position = middle;
				nearest[length++] = Other;
			}else if(nearest[0]==Other)
			{
				light.transform.localScale = new Vector3(light.transform.localScale.x,light.transform.localScale.y,temp);
				light.transform.position = middle;
			}else
			{
				bool flag = false;
				for(int i=0;i<length;i++)
				{
					Collider now=nearest[i];
					Vector3 point2 = now.ClosestPointOnBounds(emitStart.transform.position);
					Vector3 relative2 = point2-emitStart.transform.position;
					float angle2 = Vector3.Angle(relative2,transform.forward)/180*Mathf.PI;
					Vector3 project2 = transform.forward*Mathf.Abs(relative2.magnitude/Mathf.Cos(angle2));
					Vector3 middle2 = project2/2+emitStart.transform.position;
					float temp3 = project2.magnitude/2;
					if(temp<temp3)
					{
						if(i==0)
						{
							light.transform.localScale = new Vector3(light.transform.localScale.x,light.transform.localScale.y,temp);
							light.transform.position = middle;
							for(int t=length;t>=1;t--)
							{
								nearest[t]=nearest[t-1];
							}
							nearest[0]=Other;
							length++;
							flag = true;
							break;
						}else
						{
							for(int t=length;t>=i+1;t--)
							{
								nearest[t]=nearest[t-1];
							}
							nearest[i]=Other;
							length++;
							flag = true;
							break;
						}
					}
				}
				if(!flag)
					nearest[length++] = Other;
			}
		}
	}
}
