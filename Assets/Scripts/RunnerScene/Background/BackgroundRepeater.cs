using UnityEngine;

namespace Assets.Scripts.Background
{
    public class BackgroundRepeater : MonoBehaviour
    {
        [SerializeField] private float _startY;
        [SerializeField] private float _endY;
        [SerializeField] private Transform previousBackgroundPart;
        
        void Update()
        {
            if (transform.position.y < _endY)
                transform.position = previousBackgroundPart.position;
        }
    }
}
