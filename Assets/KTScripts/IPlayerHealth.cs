using UnityEngine;
using System.Collections;

public class IPlayerHealth : MonoBehaviour {

	private PlayerHealth playerHealth;

	// Use this for initialization
	void Awake () {
		playerHealth = gameObject.GetComponent<PlayerHealth>();
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (playerHealth.health);
	
	}
}
