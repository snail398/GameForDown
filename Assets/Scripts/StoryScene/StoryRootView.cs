using UnityEngine;
using System;

namespace Story
{
    public class StoryRootView : MonoBehaviour
    {
        [SerializeField] private IntroView introView;

        public IntroView IntroView => introView;

        public event Action OnDestroyEvent;

        private void OnDestroy()
        {
            OnDestroyEvent?.Invoke();
        }
    }
}
