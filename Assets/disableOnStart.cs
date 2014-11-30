using UnityEngine;
using System.Collections;

public class disableOnStart : MonoBehaviour {


	void Awake() {
		transform.gameObject.SetActive(false);
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
