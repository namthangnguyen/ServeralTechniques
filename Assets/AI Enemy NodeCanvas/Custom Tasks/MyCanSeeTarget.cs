using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Conditions{

    [Category("Of Me")]
    [Description("A combination of line of sight and view angle check")]
    public class MyCanSeeTarget : ConditionTask<Transform> {

        [RequiredField]
        public BBParameter<GameObject> target;
        public BBParameter<float> maxDistance = 6f;
        public BBParameter<float> awarnessDistance = 2f;
        [SliderField(1, 180)]
        public BBParameter<float> viewAngle = 60f;

        protected override bool OnCheck()
        {
            var t = target.value.transform;

            if (Vector2.Distance(agent.position, t.position) > maxDistance.value) {
                return false;
            }

            Collider2D agenCollider = agent.GetComponent<Collider2D>();
            agenCollider.enabled = false;
            var hit = Physics2D.Linecast((Vector2)agent.position, (Vector2)t.position);
            agenCollider.enabled = true;
            if (hit.collider != t.GetComponent<Collider2D>()) {
                return false;
            }

            if (Vector2.Angle((Vector2)t.position - (Vector2)agent.position, agent.up) < viewAngle.value) {
                return true;
            }

            if (Vector2.Distance(agent.position, t.position) < awarnessDistance.value) { 
                return true;
            }

            return false;
        }

        public override void OnDrawGizmosSelected()
        {
            if (agent != null)
            {
                Gizmos.DrawLine(agent.position, agent.position + (agent.up * maxDistance.value));
            }
        }
    }
}