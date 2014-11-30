//
//Filename: MouseCameraControl.cs
//

using UnityEngine;

[AddComponentMenu("Camera-Control/Mouse")]
public class MouseCameraControl : MonoBehaviour
{
	// Mouse buttons in the same order as Unity
	public enum MouseButton { Left = 0, Right = 1, Middle = 2, None = 3 }
	
	[System.Serializable]
	// Handles left modifiers keys (Alt, Ctrl, Shift)
	public class Modifiers
	{
		public bool leftAlt;
		public bool leftControl;
		public bool leftShift;
		
		public bool checkModifiers()
		{
			return (!leftAlt ^ Input.GetKey(KeyCode.LeftAlt)) &&
				(!leftControl ^ Input.GetKey(KeyCode.LeftControl)) &&
					(!leftShift ^ Input.GetKey(KeyCode.LeftShift));
		}
	}
	
	[System.Serializable]
	// Handles common parameters for translations and rotations
	public class MouseControlConfiguration
	{
		
		public bool activate;
		public MouseButton mouseButton;
		public Modifiers modifiers;
		public float sensitivity;
		public float upperBound;
		public float lowerBound;
		public Transform controlledTransform;
		
		public bool isActivated()
		{
			//return activate && Input.GetMouseButton((int)mouseButton) && modifiers.checkModifiers();
			return activate;
		}
	}
	
	[System.Serializable]
	// Handles scroll parameters
	public class MouseScrollConfiguration
	{
		
		public bool activate;
		public MouseButton mouseButton;
		public Modifiers modifiers;
		public float sensitivity;
		public float upperBound;
		public float lowerBound;
		public Transform controlledTransform;

		public bool isActivated()
		{
			return activate;
		}
	}

	// Default unity names for mouse axes
	public string mouseHorizontalAxisName = "Mouse X";
	public string mouseVerticalAxisName = "Mouse Y";
	public string scrollAxisName = "Mouse ScrollWheel";

	// Yaw default configuration
	public MouseControlConfiguration yaw = new MouseControlConfiguration();

	// Pitch default configuration
	public MouseControlConfiguration pitch = new MouseControlConfiguration();
	
	// Roll default configuration
	public MouseControlConfiguration roll = new MouseControlConfiguration();
	
	// Vertical translation default configuration
	public MouseControlConfiguration verticalTranslation = new MouseControlConfiguration();
	
	// Horizontal translation default configuration
	public MouseControlConfiguration horizontalTranslation = new MouseControlConfiguration();
	
	// Depth (forward/backward) translation default configuration
	public MouseControlConfiguration depthTranslation = new MouseControlConfiguration();
	
	// Scroll default configuration
	public MouseScrollConfiguration scroll = new MouseScrollConfiguration();
	
	//constraint in the interval [lowerBound,upperBound]
	void constraintRotation(Transform controlledTransform, float currentAngle, float rotationAmount,float lowerBound, float upperBound, Vector3 rotationVec) { //rotationAmount should contain sign
		if(currentAngle > 90) {
			currentAngle -= 360;
		}
		//Debug.Log("eulerX:" + currentEulerAngleX);
		float intendedAngle = currentAngle + rotationAmount;
		if(intendedAngle < pitch.lowerBound) {
			controlledTransform.Rotate((lowerBound - currentAngle) * rotationVec);
		}
		else if(intendedAngle > pitch.upperBound) {
			controlledTransform.Rotate((upperBound - currentAngle) * rotationVec);
		}
		else {
			controlledTransform.Rotate(rotationVec * rotationAmount);
		}
	}

	void LateUpdate ()
	{
		if(yaw.isActivated()) {
//			float rotationX = Input.GetAxis(mouseHorizontalAxisName) * yaw.sensitivity;
//			yaw.controlledTransform.Rotate(0, rotationX, 0);
			Transform toControllTransform = yaw.controlledTransform;
			constraintRotation(toControllTransform,
			                   toControllTransform.localEulerAngles.y,
			                   Input.GetAxis(mouseHorizontalAxisName) * yaw.sensitivity,
			                   yaw.lowerBound,
			                   yaw.upperBound,
			                   Vector3.up);

		}

		if (pitch.isActivated())
		{
			Transform toControllTransform = pitch.controlledTransform;
			constraintRotation(toControllTransform, 
			                   toControllTransform.localEulerAngles.x,
			                   -1 * Input.GetAxis(mouseVerticalAxisName) * pitch.sensitivity, 
			                   pitch.lowerBound, 
			                   pitch.upperBound,
			                   Vector3.right);
		}

		if (roll.isActivated())
		{
			float rotationZ = Input.GetAxis(mouseHorizontalAxisName) * roll.sensitivity;
			roll.controlledTransform.Rotate(0, 0, rotationZ);
		}
		
		if (verticalTranslation.isActivated())
		{
			float translateY = Input.GetAxis(mouseVerticalAxisName) * verticalTranslation.sensitivity;
			verticalTranslation.controlledTransform.Translate(0, translateY, 0);
		}
		
		if (horizontalTranslation.isActivated())
		{
			float translateX = Input.GetAxis(mouseHorizontalAxisName) * horizontalTranslation.sensitivity;
			horizontalTranslation.controlledTransform.Translate(translateX, 0, 0);
		}
		
		if (depthTranslation.isActivated())
		{
			float translateZ = Input.GetAxis(mouseVerticalAxisName) * depthTranslation.sensitivity;
			depthTranslation.controlledTransform.Translate(0, 0, translateZ);
		}
		
		if (scroll.isActivated())
		{
			float translateZ = Input.GetAxis(scrollAxisName) * scroll.sensitivity;
			scroll.controlledTransform.Translate(0, 0, translateZ);
		}
	}
	
}