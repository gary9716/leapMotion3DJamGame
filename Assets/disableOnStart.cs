using UnityEngine;
using System.Collections;

public class disableOnStart : MonoBehaviour {


	void Awake() {
		//transform.gameObject.SetActive(false);
		transform.gameObject.SetActive(true);
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
