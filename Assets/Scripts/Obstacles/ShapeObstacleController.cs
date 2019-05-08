using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Obstacles
{
    public class ShapeObstacleController : ObstacleController
    {
        public override void Awake()
        {
            base.Awake();
            _filter = GetComponent<MeshFilter>();
        }

        void OnEnable()
        {
            SetMesh();
        }

        void OnTriggerEnter(Collider col)
        {
            if (col.gameObject.tag.Equals("Player"))
                if (!CheckShape(col))
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
    }
}
