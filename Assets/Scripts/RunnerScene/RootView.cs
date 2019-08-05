using Assets.Scripts;
using Assets.Scripts.Camera;
using Assets.Scripts.HeroFolder;
using Assets.Scripts.Tutorial;
using UnityEngine;

public class RootView : MonoBehaviour
{
    [SerializeField] private LevelConfig levelConfig;
    [SerializeField] private GameSpeed gameSpeed;
    [SerializeField] private HeroView heroPrefab;
    [SerializeField] private TutorialView tutorialPrefab;
    [SerializeField] private StepSuccesful stepSuccesful;
    [SerializeField] private SlowDowner slowDowner;
    [SerializeField] private IntVariable score;
    [SerializeField] private IntVariable maxScore;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private GameEvent onTutorialStart;
    [SerializeField] private GameEvent onTutorialFinish;

    public LevelConfig LevelConfig => levelConfig;
    public GameSpeed GameSpeed => gameSpeed;
    public HeroView HeroPrefab => heroPrefab;
    public TutorialView TutorialPrefab => tutorialPrefab;
    public StepSuccesful StepSuccesful => stepSuccesful;
    public SlowDowner SlowDowner => slowDowner;
    public IntVariable Score => score;
    public IntVariable MaxScore => maxScore;
    public CameraController CameraController => cameraController;
    public GameEvent OnTutorialStart => onTutorialStart;
    public GameEvent OnTutorialFinish => onTutorialFinish;
}
