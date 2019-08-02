using UnityEngine;

namespace Assets.Scripts.Background
{
    public class BackgroundRepeater : MonoBehaviour
    {
        [SerializeField] private float _startY;
        [SerializeField] private float _endY;
        
        void Update()
        {
            if (transform.position.y < _endY) transform.position = new Vector3( transform.position.x,_startY, transform.position.z);
        }
    }
}
