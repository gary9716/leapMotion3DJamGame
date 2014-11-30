using UnityEngine;
using System.Collections;

public class ForceEffect : MonoBehaviour {

	public string beatableTag = "beatBone";

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

//	void OnTriggerEnter(Collider other)
//	{
//		Debug.Log (other.name);
//	}

	void OnCollisionEnter(Collision collision) {
		Debug.Log (collision.collider.name);

		//Debug.Log ("v:" + collision.relativeVelocity);
		Rigidbody rigidbody = collision.gameObject.rigidbody;
		//rigidbody.AddForce(new Vector3(0,10,0));
		if(rigidbody != null) {
			rigidbody.AddForce(new Vector3(0,10,0),ForceMode.VelocityChange);
		}
	}

}
