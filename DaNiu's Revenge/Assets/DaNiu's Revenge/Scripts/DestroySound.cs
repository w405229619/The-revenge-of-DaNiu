using UnityEngine;
using System.Collections;

public class DestroySound : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(!gameObject.GetComponent<AudioSource>().isPlaying)
			Destroy(gameObject);
	}
}
