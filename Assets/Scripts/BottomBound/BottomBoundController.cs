using UnityEngine;

namespace Assets.Scripts.BottomBound
{
    public class BottomBoundController : MonoBehaviour
    {
        void OnTriggerEnter(Collider col)
        {
            if (col.gameObject.tag.Equals("Obstacle"))
            {
                col.gameObject.GetComponent<PoolObject>().ReturnToPool();
            }
        }
    }
}
