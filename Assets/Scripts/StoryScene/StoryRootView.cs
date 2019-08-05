using UnityEngine;
using UnityEngine.UI;

namespace Story
{
    public class StoryRootView : MonoBehaviour
    {
        [SerializeField] private IntroView introView;

        public IntroView IntroView => introView;
    }
}
