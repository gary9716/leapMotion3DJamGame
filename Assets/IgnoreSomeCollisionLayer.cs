using UnityEngine;
using System.Collections;

public class IgnoreSomeCollisionLayer : MonoBehaviour {

	public string[] layerNamesToIgnore = new string[10];
	
	void Awake() {
		int selfLayerId = transform.gameObject.layer;
		Debug.Log ("self collision layer:" + LayerMask.LayerToName(selfLayerId));
		//Physics.IgnoreLayerCollision(selfLayerId,LayerMask.NameToLayer("Shootable"));
		foreach(string name in layerNamesToIgnore) {
			//Debug.Log (name);
			if(name == null || name == "") {
				break;
			}
			else if(name == "selfLayer") {
				Physics.IgnoreLayerCollision(selfLayerId,selfLayerId);
			}
			else {
				Physics.IgnoreLayerCollision(selfLayerId,LayerMask.NameToLayer(name));
			}
		}
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
