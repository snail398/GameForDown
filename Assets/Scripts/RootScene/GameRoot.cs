using UnityEngine;
using Story;
using UniRx;
using Assets.Scripts;

public class GameRoot : MonoBehaviour
{
    [SerializeField] LoadingScreenView loadingScreenView;
    private AnaliticsCore _analiticsCore;
    private PlayersData _playersData;
    private SceneLoader _sceneLoader;
    private StoryRoot _storyRoot;
    private Root _runnerRoot;
    private LoadingScreen _loadingScreen;

    private void Awake()
    {
        LoadingScreen.Ctx loadCtx = new LoadingScreen.Ctx
        {
            view = loadingScreenView,
        };
        _loadingScreen = new LoadingScreen(loadCtx);
        //Create SceneLoader object
        SceneLoader.Ctx loaderCtx = new SceneLoader.Ctx
        {
            onStorySceneLoaded = OnSceneLoaded,
            onRunSceneLoaded = OnRunSceneLoaded,
            hideLoadingScreen = _loadingScreen.HideLoadingScreen,
            showLoadingScreen = _loadingScreen.ShowLoadingScreen,
        };
        _sceneLoader = new SceneLoader(loaderCtx);
        //Load Story Scene
        _sceneLoader.LoadStoryScene();

        //Creating Analitics System 
        _analiticsCore = new AnaliticsCore();

        //Create PlayerData object
        _playersData = new PlayersData();
    }

    private void OnSceneLoaded()
    {
        _runnerRoot = null;
        if (_storyRoot == null)
        {
            //Create StoryRoot object
            StoryRoot.Ctx storyCtx = new StoryRoot.Ctx()
            {
                playersData = _playersData,
                analiticsCore = _analiticsCore,
                reloadGame = _sceneLoader.ReloadGame,
                loadRunScene = _sceneLoader.LoadRunScene,
                setCanvas = _loadingScreen.SetCanvas,
            };
            _storyRoot = new StoryRoot(storyCtx);
        }
        _storyRoot.Initialize();
    }

    private void OnRunSceneLoaded()
    {
        _storyRoot = null;
        if (_runnerRoot == null)
        { 
            Root.Ctx runnerRootCtx = new Root.Ctx
            {
                restartScene = _sceneLoader.LoadRunScene,
                returnToStoryScene = _sceneLoader.LoadStoryScene,
                playerData = _playersData,
            };
            _runnerRoot = new Root(runnerRootCtx);
        }
        _runnerRoot.InitializeRunnerRoot();
    }

}
