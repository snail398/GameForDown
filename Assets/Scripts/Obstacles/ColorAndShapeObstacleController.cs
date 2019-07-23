using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Obstacles
{
    public class ColorAndShapeObstacleController : ObstacleController
    {
        public override void Awake()
        {
            base.Awake();
            _filter = GetComponent<MeshFilter>();
            _material = GetComponent<Renderer>().material;
        }

        void OnEnable()
        {
            SetMesh();
            SetColor();
        }

        void OnTriggerEnter(Collider col)
        {
            if (col.gameObject.tag.Equals("Player"))
                if (!CheckShape(col) || !CheckColor(col))
                    HeroDamage();
        }
        public bool CheckShape(Collider col)
        {
            Mesh colMesh = col.gameObject.GetComponent<MeshFilter>().sharedMesh;
            Mesh targetMesh = _configSource.MeshesAccordances
                .FirstOrDefault(x => x.obstacleMesh == _filter.sharedMesh)
                ?.playerMesh;
            return colMesh.Equals(targetMesh);
        }

        public void SetMesh()
        {
            _filter.sharedMesh = _configSource.ObstacleMeshes[Random.Range(0, _configSource.ObstacleMeshes.Count)];
        }

        public bool CheckColor(Collider col)
        {
            return col.gameObject.GetComponent<Renderer>().material.color.Equals(_material.color);
        }

        public void SetColor()
        {
            _material.color = _configSource.AvailableColors[Random.Range(0, _configSource.AvailableColors.Count)];
        }
        public void SetMesh(int i)
        {
            _filter.sharedMesh = _configSource.ObstacleMeshes[i];
        }
        public void SetColor(int i)
        {
            _material.color = _configSource.AvailableColors[i];
        }

    }
}
