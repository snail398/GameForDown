using UnityEngine;

namespace Assets.Scripts.Coins
{
    public class Rotator : MonoBehaviour
    {
        void FixedUpdate()
        {
            transform.Rotate(transform.forward,5);
        }
    }
}
