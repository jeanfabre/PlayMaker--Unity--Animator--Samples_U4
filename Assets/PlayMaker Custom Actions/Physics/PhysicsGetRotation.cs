﻿// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Gets the Rotation of a Physics Game Object and stores it in a Vector3 Variable or each Axis in a Float Variable. It's more efficient than getting the rotation of the gameobject")]
	public class PhysicsGetRotation : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody))]
		public FsmOwnerDefault gameObject;
		
		[UIHint(UIHint.Variable)]
		public FsmQuaternion quaternion;
		
		[UIHint(UIHint.Variable)]
		[Title("Euler Angles")]
		public FsmVector3 vector;
		
		[UIHint(UIHint.Variable)]
		public FsmFloat xAngle;
		[UIHint(UIHint.Variable)]
		public FsmFloat yAngle;
		[UIHint(UIHint.Variable)]
		public FsmFloat zAngle;
		
		public bool everyFrame;
		
		private Rigidbody rigidBody;
		
		public override void Reset()
		{
			gameObject = null;
			quaternion = null;
			vector = null;
			xAngle = null;
			yAngle = null;
			zAngle = null;

			everyFrame = false;
		}
		
		public override void Awake()
        {
            Fsm.HandleFixedUpdate = true;
        }
		
		public override void OnEnter()
		{
			
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null)
			{
				Finish();
				return;
			}
			
			rigidBody = go.GetComponent<Rigidbody>();
			if (rigidBody == null)
			{
				Finish();
				return;
			}
				
		}

		public override void OnFixedUpdate()
		{
			DoGetRotation();
			
			if (!everyFrame)
			{
				Finish();
			}	
		}

		void DoGetRotation()
		{	
			quaternion.Value = rigidBody.rotation;
			
			if (!vector.IsNone || !xAngle.IsNone || !yAngle.IsNone || !zAngle.IsNone)
			{
				var rotation = rigidBody.rotation.eulerAngles;
				
				vector.Value = rotation;
				xAngle.Value = rotation.x;
				yAngle.Value = rotation.y;
				zAngle.Value = rotation.z;
			}
		}


	}
}