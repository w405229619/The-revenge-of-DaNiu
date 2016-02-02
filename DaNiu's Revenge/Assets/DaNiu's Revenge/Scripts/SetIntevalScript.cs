using UnityEngine;
using System.Collections;

public class SetIntevalScript : MonoBehaviour {
	public float interval = 2f;
	public float time = 0f;
	// Use this for initialization
	void Start () {
		gameObject.GetComponent<ParticleEmitter>().emit=true;
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if(time >= interval)
		{
			time=0f;
			gameObject.GetComponent<ParticleEmitter>().emit = !gameObject.GetComponent<ParticleEmitter>().emit;
		}
		
	}
}
