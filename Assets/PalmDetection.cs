using UnityEngine;
using System.Collections;

public class PalmDetection : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log (other.name);
	}

//	void OnColliderEnter(Collision collision) {
//		Debug.Log("collision " + collision.collider.name);
//	}
}
