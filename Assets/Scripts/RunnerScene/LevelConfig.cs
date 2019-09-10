using System;
using System.Collections.Generic;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts
{
    public class LevelConfig : MonoBehaviour
    {
        [SerializeField] private int _lineCount = 3;
        [SerializeField] private int _spawnTime = 2;
        [SerializeField] private float _speedIncreaseTime = 2;
        [SerializeField] private float _speedIncreaseCount = 0.5f;
        [SerializeField] private float _startGameSpeed = 3;
        [SerializeField] private List<Color> _availableColors;
        [SerializeField] private List<Mesh> _availableMeshes;
        [SerializeField] private List<Mesh> _obstacleMeshes;
        [SerializeField] private List<MeshesAccordance> _meshesAccordances;
        [SerializeField] private List<String> _wallPrefabNames;
        [SerializeField] private int _lineWidth = 2;

        public int LineCount => _lineCount;
        public int SpawnTime => _spawnTime;
        public float SpeedIncreaseTime => _speedIncreaseTime;
        public float SpeedIncreaseCount => _speedIncreaseCount;
        public float StartGameSpeed => _startGameSpeed;
        public List<Color> AvailableColors => _availableColors;
        public List<Mesh> AvailableMeshes => _availableMeshes;
        public List<Mesh> ObstacleMeshes => _obstacleMeshes;
        public List<MeshesAccordance> MeshesAccordances => _meshesAccordances;
        public List<String> WallPrefabNames => _wallPrefabNames;
        public int LineWidth => _lineWidth;

    }
}
