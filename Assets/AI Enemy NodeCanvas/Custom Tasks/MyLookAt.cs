using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions{

	public class MyLookAt : ActionTask<Transform>{

		[RequiredField]
		public BBParameter<GameObject> lookTarget;


        protected override void OnExecute()
        {
            DoLook();
        }


        protected override void OnUpdate()
        {
            DoLook();
        }

        void DoLook()
        {
            var pos = lookTarget.value.transform.position;
            Vector2 difference = pos - agent.position;
            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            agent.rotation = Quaternion.Euler(0f, 0f, -90 + rotationZ);

            EndAction(true);
        }
    }
}