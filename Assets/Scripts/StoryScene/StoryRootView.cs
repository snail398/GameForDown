using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Story
{
    public class StoryRootView : MonoBehaviour
    {
        [SerializeField] private IntroView introView;

        public IntroView IntroView
        {
            get => introView;
        }
    }
}
