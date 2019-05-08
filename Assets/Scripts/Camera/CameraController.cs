using UnityEngine;

namespace Assets.Scripts.Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private LevelConfig _levelConfig;
        void Awake()
        {
            SetCameraOrthographic();
        }

        private void SetCameraOrthographic()
        {
            UnityEngine.Camera.main.orthographicSize = ((float)_levelConfig.LineCount) * Screen.height / Screen.width;
        }
    }
}
