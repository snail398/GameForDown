using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.HeroFolder
{
    public class HeroDeathView : MonoBehaviour
    {
        public void RestartScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
