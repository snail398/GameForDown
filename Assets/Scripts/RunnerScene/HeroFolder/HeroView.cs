using System;
using System.Collections.Generic;
using Lean.Touch;
using NUnit.Framework;
using UniRx;
using UnityEngine;

namespace Assets.Scripts.HeroFolder
{
    public class HeroView : MonoBehaviour
    {
        [SerializeField] private float moveSmooth;
        [SerializeField] private List<LeanFingerSwipe> _swipes;
        public struct Ctx
        {
            public int strafe;
            public int lastLeftCoord;
            public int lastRightCoord;
            public int lineCount;
            public List<Color> availableColors;
            public List<Mesh> availableMeshes;
            public Action continueGame;
            public ReactiveProperty<int> blockSwipeCommand;
            public ReactiveCommand hideTutorialView;
        }

        private Ctx _ctx;
        private Material _heroMaterial;
        private MeshFilter _filter;
        private MeshCollider _collider;
        private int _colorVar;
        private int _meshVar;
        private Vector3 _newPos;

        void Awake()
        {
            _heroMaterial = GetComponent<Renderer>().material;
            _filter = GetComponent<MeshFilter>();
            _collider = GetComponent<MeshCollider>();
            enabled = false;
        }

        void Update()
        {
            if (!transform.position.Equals(_newPos))
                transform.position = Vector2.Lerp(transform.position,_newPos,moveSmooth);
            if (Mathf.Abs((transform.position - _newPos).magnitude) < 0.01) transform.position = _newPos;
        }
        public void MoveLeft()
        {
            if (CanMoveLeft())
            {
                _newPos = new Vector2(_newPos.x - _ctx.strafe, transform.position.y);
                _ctx.continueGame?.Invoke();
                _ctx.hideTutorialView?.Execute();

            }
        }

        public void MoveRight()
        {
            if (CanMoveRight())
            {
                _newPos = new Vector2(_newPos.x + _ctx.strafe, transform.position.y);
                _ctx.continueGame?.Invoke();
                _ctx.hideTutorialView?.Execute();
            }
        }

        public void ChangeColor()
        {
            _colorVar++;
            if (_colorVar >= _ctx.availableColors.Count) _colorVar = 0;
            _heroMaterial.color = _ctx.availableColors[_colorVar];
            _ctx.continueGame?.Invoke();
            _ctx.hideTutorialView?.Execute();
        }
        
        public void ChangeShape()
        {
            _meshVar++;
            if (_meshVar >= _ctx.availableMeshes.Count) _meshVar = 0;
            _filter.sharedMesh = _ctx.availableMeshes[_meshVar];
            _collider.sharedMesh = _ctx.availableMeshes[_meshVar];
            _ctx.continueGame?.Invoke();
            _ctx.hideTutorialView?.Execute();
        }
        private bool CanMoveLeft()
        {
            return !(_newPos.x <= _ctx.lastLeftCoord);
        }
        private bool CanMoveRight()
        {
            return !(_newPos.x >= _ctx.lastRightCoord);
        }
        
        public void SetCtx(Ctx ctx)
        {
            _ctx = ctx;
            Initialize();
        }

        private void Initialize()
        {
            transform.position = _ctx.lineCount % 2 != 0
                ? new Vector2(0, transform.position.y)
                : new Vector2(-1, transform.position.y);
            _heroMaterial.color = _ctx.availableColors[_colorVar];
            _filter.sharedMesh = _ctx.availableMeshes[_meshVar];
            _collider.sharedMesh = _ctx.availableMeshes[_meshVar];
            enabled = true;
            _ctx.blockSwipeCommand.Subscribe(ControlSwipesEnable).AddTo(this);
        }

        //0 - все доступны, -1 - никто, 1 - верх, 2-право,3 - вниз , 4 - влево
        public void ControlSwipesEnable(int command)
        {
            foreach (LeanFingerSwipe swipe in _swipes)
            {
                swipe.enabled = false;
            }
            switch (command)
            {
                case 0:
                    foreach (LeanFingerSwipe swipe in _swipes)
                    {
                        swipe.enabled = true;
                    }
                    break;
                case -1:
                    break;
                case 1:
                case 2:
                case 3:
                case 4:
                    _swipes[command - 1].enabled = true;
                    break;
                default:
                    break;
            }
        }
    }
}
