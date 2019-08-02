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

        public int LineCount { get => _lineCount; set => _lineCount = value; }
        public int SpawnTime { get => _spawnTime; set => _spawnTime = value; }
        public float SpeedIncreaseTime { get => _speedIncreaseTime; set => _speedIncreaseTime = value; }
        public float SpeedIncreaseCount { get => _speedIncreaseCount; set => _speedIncreaseCount = value; }
        public float StartGameSpeed { get => _startGameSpeed; set => _startGameSpeed = value; }
        public List<Color> AvailableColors { get => _availableColors; set => _availableColors = value; }
        public List<Mesh> AvailableMeshes { get => _availableMeshes; set => _availableMeshes = value; }
        public List<Mesh> ObstacleMeshes { get => _obstacleMeshes; set => _obstacleMeshes = value; }
        public List<MeshesAccordance> MeshesAccordances { get => _meshesAccordances; set => _meshesAccordances = value; }
        public List<String> WallPrefabNames { get => _wallPrefabNames; set => _wallPrefabNames = value; }
        public int LineWidth { get => _lineWidth; set => _lineWidth = value; }

    }
}
