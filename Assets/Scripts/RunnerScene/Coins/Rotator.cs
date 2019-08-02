using UnityEngine;

namespace Assets.Scripts.Coins
{
    public class Rotator : MonoBehaviour
    {
        void Update()
        {
            transform.Rotate(transform.forward,5);
        }
    }
}
