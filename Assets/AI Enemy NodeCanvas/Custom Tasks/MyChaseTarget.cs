using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions{

	public class MyChaseTarget : ActionTask<Transform>{

		[RequiredField]
		public BBParameter<GameObject> target;
		public BBParameter<float> speed = 4;

		private Vector3 lastRequest;

        protected override void OnExecute()
        {
            if (target.value == null) {
                EndAction(false);
                return;
            }
            Vector2 difference = target.value.transform.position - agent.position;
            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            Debug.Log(rotationZ);
        }

        protected override void OnUpdate()
        {
            if (target.value == null)
            {
                EndAction(false);
                return;
            }

            var pos = target.value.transform.position;
            //if (lastRequest != pos)
            //{
            //    // Weapon direction follow Charactor
            //    Vector2 difference = pos - agent.position;
            //    float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            //    // Atan2: góc của vector difference với trục x 
            //    // *Rad2Deg để chuyển từ radian sang độ
            //    // -90 vì hướng của target là hướng lên trên
            //    agent.rotation = Quaternion.Euler(0f, 0f, -90 + rotationZ);

            //}
            agent.position = Vector2.MoveTowards(agent.position, pos, speed.value * Time.deltaTime);
            lastRequest = pos;
            EndAction(true);
        }
    }
}