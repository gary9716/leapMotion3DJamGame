using UnityEngine;
using System.Collections;

public class GestureRecognizer : MonoBehaviour {

//	private HandModel currentHand; 
//	private FingerModel[] fingers;
//	public FingerModel testFinger;
	public HandController controller;
	private Leap.Controller _controller;
	private Leap.HandList hands;
	private Leap.Vector vecUpInLeapForm = new Leap.Vector(0,1,0);
	private GameObject player;
	public string monsterLayerName;
	public int monsterLayerID;

	public int gestureOption;
	
	// Use this for initialization
	void Start () {
		if(controller == null) {
			controller = GameObject.Find("HandController").GetComponent<HandController>();
		}

		player = GameObject.FindGameObjectWithTag("Player");

		//start to track swipe gesture
		_controller = controller.GetLeapController();
		if(gestureOption == 1) {
			_controller.EnableGesture(Leap.Gesture.GestureType.TYPECIRCLE);
		}

		monsterLayerID = LayerMask.NameToLayer(monsterLayerName);
	}

	bool tempDisableFeature = false;
	void getCollidersInARangeAndSendMessage() {
		if(!tempDisableFeature) {
			tempDisableFeature = true;
			Collider[] colliders = Physics.OverlapSphere(player.transform.position, 10);
			for(int i = 0;i < colliders.Length;i++) {
				//Debug.Log("Yes out");
				if(colliders[i].gameObject.layer == monsterLayerID) {
					//Debug.Log("Yes");
					FlyEffect flyEffect = colliders[i].gameObject.GetComponent<FlyEffect>();
					if(flyEffect != null) {
						flyEffect.makeItFly();
					}
				}
			}
			tempDisableFeature = false;
		}
	}
	
	//1,DISTAL(tip) 2,INTERMEDIATE 3,PROXIMAL
	// Update is called once per frame
	void Update () {
		Leap.Frame frame = controller.GetFrame();
		if(gestureOption == 0) {
			hands = frame.Hands;
			Leap.Hand hand = hands.Leftmost;
			//Debug.Log ("here");
			if(hand.IsValid && hand.IsLeft) {

				Leap.FingerList fingers = hand.Fingers;
				Leap.Vector tipV = fingers[0].TipVelocity;
				float tipVmag = tipV.Magnitude;
				//Leap.Vector tipV2 = fingers[1].TipVelocity;
				Leap.Bone bone = fingers[0].Bone (Leap.Bone.BoneType.TYPE_DISTAL);
				//Leap.Bone bone2 = fingers[1].Bone (Leap.Bone.BoneType.TYPE_DISTAL);

				float distFromBoneToPalm = bone.Center.DistanceTo(hand.PalmPosition);
				float palmDirectionSign = hand.PalmNormal.Dot(vecUpInLeapForm);

				//Debug.Log (distFromBoneToPalm + " " + palmDirectionSign + " " + tipVmag + " " + hand.Confidence);
				//Debug.Log (bone.Direction.Pitch);
				if(distFromBoneToPalm < 70 && palmDirectionSign > 0.5 && tipVmag > 30 && hand.Confidence > 0.7) {
					getCollidersInARangeAndSendMessage();	
				}

			}
		}
		else if(gestureOption == 1){
			Leap.GestureList gestures = frame.Gestures();
			for(int i = 0;i < gestures.Count;i++) {
				if(gestures[i].Type == Leap.Gesture.GestureType.TYPESWIPE) {
					if(gestures[i].IsValid && gestures[i].State == Leap.Gesture.GestureState.STATESTOP) {
						Debug.Log ("Yes");
						getCollidersInARangeAndSendMessage();
					}
				}
			}
		}






	}
}
