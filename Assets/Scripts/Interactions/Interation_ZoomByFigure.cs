using System;
using Leap;
using UnityEngine;
namespace URECA
{
	public class Interation_ZoomByFigure:InteractionBase
	{
		//private HandController _handcontroller;
		private GameObject _duplicate;
		Frame currentFrame;
		Frame lastFrame = null;
		Frame thisFrame = null;
		void Start()
		{
			//_handcontroller = HandController.Main;
			HandController.Main.GetLeapController ().Config.SetFloat ("Gesture.Circle.MinArc",1.5f*Mathf.PI);
		}
		//2 condition: 1st:moving toward or outward screen, 2nd:palm face screen
		private bool isPalmPullAction(Frame frame)
		{
			this.currentFrame = HandController.Main.GetFrame();
			GestureList gestures = this.currentFrame.Gestures();

			foreach (Gesture g in gestures) {
				foreach (var f in g.Frame.Fingers) {
					if (!f.IsExtended)
						return false;
				}

				foreach (var h in g.Hands) {
					Vector palmDirection = h.PalmNormal.Normalized;
					Debug.Log (palmDirection);
					// that means the angle between moving direction and facing direction is greater than 30 degree
					if(-palmDirection.z < Mathf.Cos(40f*Mathf.PI/180f)){
						return false;
					}
					if(h.PalmVelocity.Normalized.z > Mathf.Cos(30f*Mathf.PI/180f))
					{
						Debug.Log ("move out");
						return true;
					}
					if(-h.PalmVelocity.Normalized.z > Mathf.Cos(45f*Mathf.PI/180f))
					{
						Debug.Log ("move in");
						return true;
					}						
				}
			}
			return false;
		}

		private float getScaleFactor(Frame frame)
		{
			this.currentFrame = HandController.Main.GetFrame();
			GestureList gestures = this.currentFrame.Gestures();
			Hand hand = null;
			foreach (Gesture g in gestures) {
				foreach (var h in g.Hands) {
					if (h != null && h.IsValid)
						hand = h;
					break;				
				}
				if (hand != null)
					break;
			}
			float z = hand.PalmVelocity.z;
			return z / 2000f;
		}

		private Vector3 vector2vector3(Vector v)
		{
			return new Vector3 (v.x, v.y, v.z);
		}

		void Update()
		{
			if (LeapModeController.instance ().mode == LeapMode.EditingMode)
				return;
			this.currentFrame = HandController.Main.GetFrame();
			GestureList gestures = this.currentFrame.Gestures();
			if(_duplicate != null){
				if (isPalmPullAction (this.currentFrame))
				{
					_duplicate.transform.localScale *= (1f+getScaleFactor (currentFrame));
					return;
				}
			}
			foreach (Gesture g in gestures)
			{
				if (g.Type == Gesture.GestureType.TYPECIRCLE )
				{
					foreach(var f in g.Frame.Fingers)
					{
						if (f.Type == Finger.FingerType.TYPE_INDEX && _duplicate == null)
						{
							CircleGesture cg = new CircleGesture (g);
							Vector3 tipPos = HandController.Main.ToUnitySpace (f.TipPosition);
							Vector3 centerPos = HandController.Main.ToUnitySpace (cg.Center);
							centerPos = Camera.main.WorldToScreenPoint (centerPos);
							tipPos = Camera.main.WorldToScreenPoint (tipPos);
							float radius = Vector2.Distance (tipPos, centerPos);
							//Vector3 fingerScreencenter = Camera.main.WorldToScreenPoint (new Vector3 (center.x, center.y, center.z));
							Vector3 objectScreencenter = Camera.main.WorldToScreenPoint (this.transform.position);
							float distance = Vector2.Distance (centerPos, objectScreencenter);
							if (distance < radius) {
								Debug.Log ("circle a object");
								_duplicate = GameObject.Instantiate (this.gameObject, this.transform.position+new Vector3(0,0,-1), this.transform.rotation);
								_duplicate.transform.parent = this.transform.parent;
								_duplicate.transform.localScale = this.transform.localScale;
								DuplicatedObject dobj = _duplicate.AddComponent<DuplicatedObject> ();
								dobj.OriginObject = this.gameObject;
								Destroy (_duplicate.GetComponent ("Interation_ZoomByFigure"));
								LeapModeController.instance ().mode = LeapMode.RotatingOjbect;

							}
						}
					}

				}
				if (g.Type == Gesture.GestureType.TYPESWIPE)
				{
					if (_duplicate)
					{
						Destroy(_duplicate);
						_duplicate = null;
						LeapModeController.instance ().mode = LeapMode.NoRotatingOjbectInScene;
					}
				}
			}
			foreach (var h in HandController.Main.GetFrame().Hands)
			{
				if (h.IsRight)
				{
					foreach (var f in h.Fingers)
					{
						if (f.Type == Finger.FingerType.TYPE_INDEX)
						{
							// this code converts the tip position from leap motion to unity world position
							//Leap.Vector position = f.TipPosition;
							//Vector3 unityPosition = position.ToUnityScaled(false);
							//Vector3 worldPosition = hc.transform.TransformPoint(unityPosition);

							if (_duplicate != null)
								_duplicate.transform.rotation = Quaternion.EulerRotation(h.Direction.Pitch, h.Direction.Yaw, h.Direction.Roll);
						}
					}

				}

			}
		}
	}
}

