using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace Assets.Scripts.HeroFolder
{
    public class HeroController : MonoBehaviour
    {

        private HeroView _heroView;
        private Vector2 fp;
        private Vector2 lp;
        void Awake()
        {
            _heroView = GetComponent<HeroView>();
        }

        void Update()
        {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _heroView?.MoveRight();
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _heroView?.MoveLeft();
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _heroView?.ChangeShape();
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                _heroView?.ChangeColor();
            }
#endif
#if UNITY_IOS || UNITY_ANDROID
            foreach (var touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    fp = touch.position;
                    lp = touch.position;
                }
                if (touch.phase == TouchPhase.Moved )
                {
                    lp = touch.position;
                }
                if(touch.phase == TouchPhase.Ended)
                { 
                    if((fp.x - lp.x) > 80) // left swipe
                        _heroView?.MoveLeft();
                    else if((fp.x - lp.x) < -80) // right swipe
                        _heroView?.MoveRight();
                    else if((fp.y - lp.y) < -80 ) // up swipe
                        _heroView?.ChangeShape();
                    else if((fp.y - lp.y) > 80 ) // down swipe
                        _heroView?.ChangeColor();
                }
            }       
#endif
        }


    }
}
