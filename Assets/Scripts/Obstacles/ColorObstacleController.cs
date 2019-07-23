using UnityEngine;

namespace Assets.Scripts.Obstacles
{
    public class ColorObstacleController : ObstacleController
    {
        public override void Awake()
        {
            base.Awake();
            _material = GetComponent<Renderer>().material;
        }
        void OnEnable()
        {
            SetColor();
        }
        public void SetColor()
        {
            _material.color = _configSource.AvailableColors[Random.Range(0, _configSource.AvailableColors.Count)];
        }

        public void SetColor(int i)
        {
            _material.color = _configSource.AvailableColors[i];
        }

        void OnTriggerEnter(Collider col)
        {
            if (col.gameObject.tag.Equals("Player"))
                if (!CheckColor(col))
                    HeroDamage();
        }

        public bool CheckColor(Collider col)
        {
            return col.gameObject.GetComponent<Renderer>().material.color.Equals(_material.color);
        }
    }
}
