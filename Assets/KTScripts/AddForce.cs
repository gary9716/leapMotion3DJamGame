using UnityEngine;
using System.Collections;

public class AddForce : MonoBehaviour {

//	private HandModel currentHand; 
//	private FingerModel[] fingers;
//	public FingerModel testFinger;
	public HandController controller;
	private Leap.HandList hands;
	// Use this for initialization
	void Start () {
	
	}

	//1,DISTAL(tip) 2,INTERMEDIATE 3,PROXIMAL
	// Update is called once per frame
	void Update () {
		Leap.Frame frame = controller.GetFrame(); // controller is a Controller object
		hands = frame.Hands;
		Leap.Hand hand = hands.Leftmost;
		if(hand.IsValid && hand.IsLeft) {
			Leap.FingerList fingers = hand.Fingers;
			Leap.Vector tipV = fingers[0].TipVelocity;
			Leap.Bone bone = fingers[0].Bone (Leap.Bone.BoneType.TYPE_DISTAL);

		}
	}
}
