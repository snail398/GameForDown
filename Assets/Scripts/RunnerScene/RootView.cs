using Assets.Scripts;
using Assets.Scripts.Camera;
using Assets.Scripts.HeroFolder;
using Assets.Scripts.Tutorial;
using UnityEngine;
using System;
using UnityEngine.UI;

public class RootView : MonoBehaviour
{
    public struct Ctx
    {
        public Action returnToStory;
    }
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
    [SerializeField] private HUDController hud;

    //Pause
    [SerializeField] private Image pausePanel;

    private Ctx _ctx;
    private bool _paused;

    public bool Paused => _paused;

    public void SetCtx(Ctx ctx)
    {
        _ctx = ctx;
    }

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
    public HUDController HUD => hud;

    public event Action OnHeroDeath;

    public void ReturnToStory()
    {
        _ctx.returnToStory?.Invoke();
    }

    public void GamePause()
    {
        if (!_paused)
        {
            Time.timeScale = 0;
            _paused = true;
            pausePanel.gameObject.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            _paused = false;
            pausePanel.gameObject.SetActive(false);
        }
    }
    private void OnDestroy()
    {
        Time.timeScale = 1;
    }

    public void HeroDeath()
    {
        OnHeroDeath?.Invoke();
    }
}
