using UnityEngine;
using System.Collections;

public class FlyEffect : MonoBehaviour {

	Rigidbody rigidbody;
	bool navAgentEnableFlag = false;
	
	public NavMeshAgent agent;
	// Use this for initialization
	void Start () {
		rigidbody = gameObject.rigidbody;
		agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.y < -1) {
			DestroyImmediate(gameObject);
		}
	}

	public void makeItFly() {
		if(agent.enabled) {
			Invoke("fly",0.1f);
			navAgentEnableFlag = false;
			agent.enabled = false;
			Invoke("enableNavAgentFlag",1);
		}
	}

	void enableNavAgentFlag() {
		navAgentEnableFlag = true;
	}
	
	void fly() {
		rigidbody.drag = 0;
		rigidbody.angularDrag = 0.05f;
		rigidbody.AddForce(new Vector3(0,20,0),ForceMode.VelocityChange);
	}
	
	//public LayerMask floorMask;
	
	void OnCollisionEnter(Collision collision) {
		
		if(collision.gameObject.name=="Floor" && navAgentEnableFlag) {
			agent.enabled = true;
			rigidbody.drag = Mathf.Infinity;
			rigidbody.angularDrag = Mathf.Infinity;
		}
		
	}
	
	void enableAgent()
	{
		agent.enabled = true;
	}
}
