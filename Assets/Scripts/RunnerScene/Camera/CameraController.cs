using UnityEngine;

namespace Assets.Scripts.Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private LevelConfig _levelConfig;
        [SerializeField] private Transform _target;
        [SerializeField] private Transform _cameraRig;

        private Transform _heroPosition;
        private Vector3 _offsetFromHero;
        void Awake()
        {
            _offsetFromHero = transform.position - Vector3.zero;  // hero spawned at zero position
        }

        public void SetHero(Transform heroTransform)
        {
            _heroPosition = heroTransform;
            /*
            if (_heroPosition != null)
                SetCameraPosition();
                */
        }

        private void SetCameraPosition()
        {
            _cameraRig.position = _heroPosition.position + _offsetFromHero;
            transform.LookAt(_target, Vector3.back);
        }
    }
}
