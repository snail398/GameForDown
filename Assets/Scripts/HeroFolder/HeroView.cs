using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace Assets.Scripts.HeroFolder
{
    public class HeroView : MonoBehaviour
    {
        public struct Ctx
        {
            public int strafe;
            public int lastLeftCoord;
            public int lastRightCoord;
            public int lineCount;
            public List<Color> availableColors;
            public List<Mesh> availableMeshes;
        }

        private Ctx _ctx;
        private Material _heroMaterial;
        private MeshFilter _filter;
        private MeshCollider _collider;
        private int _colorVar;
        private int _meshVar;

        void Awake()
        {
            _heroMaterial = GetComponent<Renderer>().material;
            _filter = GetComponent<MeshFilter>();
            _collider = GetComponent<MeshCollider>();
            enabled = false;
        }
        public void MoveLeft()
        {
            if (CanMoveLeft()) transform.position = new Vector2(transform.position.x - _ctx.strafe, transform.position.y);
        }

        public void MoveRight()
        {
            if (CanMoveRight()) transform.position = new Vector2(transform.position.x + _ctx.strafe, transform.position.y);
        }

        public void ChangeColor()
        {
            _colorVar++;
            if (_colorVar >= _ctx.availableColors.Count) _colorVar = 0;
            _heroMaterial.color = _ctx.availableColors[_colorVar];
        }
        
        public void ChangeShape()
        {
            _meshVar++;
            if (_meshVar >= _ctx.availableMeshes.Count) _meshVar = 0;
            _filter.sharedMesh = _ctx.availableMeshes[_meshVar];
            _collider.sharedMesh = _ctx.availableMeshes[_meshVar];
        }
        private bool CanMoveLeft()
        {
            return !(transform.position.x <= _ctx.lastLeftCoord);
        }
        private bool CanMoveRight()
        {
            return !(transform.position.x >= _ctx.lastRightCoord);
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
        }
    }
}
