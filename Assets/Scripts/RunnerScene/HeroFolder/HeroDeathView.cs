using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.HeroFolder
{
    public class HeroDeathView : MonoBehaviour
    {
        private bool _isTutorial = false;
        private void RestartScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void ChangeSceneOnHeroDeath()
        {
            if (CheckIsTutorial())
            {
                RestartScene();
            }
            else
            {
                SetSceneToStory();
            }
        }

        private bool CheckIsTutorial()
        {
            return _isTutorial;
        }

        private void SetSceneToStory()
        {
            SceneManager.LoadScene(0);
        }

        public void InTutorial()
        {
            _isTutorial = true;
        }

        public void OutTutorial()
        {
            _isTutorial = false;
        }
    }
}
