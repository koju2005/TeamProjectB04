using UnityEngine;

namespace Data.Functions
{
    [CreateAssetMenu(fileName = "Print",menuName = "Scriptable/CollisionInteraction/Print")]
    public class Print : CollisionInteraction
    {
        public override void EnterCollsion(GameObject Owner, GameObject target)
        {
            Debug.Log("Enter");
        }

        public override void ExitCollsion(GameObject who, GameObject target)
        {
            Debug.Log("Exit");
        }
    }
}