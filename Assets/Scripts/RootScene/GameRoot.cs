using UnityEngine;
using Story;
using UniRx;

public class GameRoot : MonoBehaviour
{
    private AnaliticsCore _analiticsCore;
    private PlayersData _playersData;
    private SceneLoader _sceneLoader;

    private void Awake()
    {
        //Create SceneLoader object
        SceneLoader.Ctx loaderCtx = new SceneLoader.Ctx
        {
            onStorySceneLoaded = OnSceneLoaded,
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
        //Create StoryRoot object
        StoryRoot.Ctx storyCtx = new StoryRoot.Ctx()
        {
            playersData = _playersData,
            analiticsCore = _analiticsCore,
            reloadGame = _sceneLoader.ReloadGame,
            loadRunScene = _sceneLoader.LoadRunScene,
        };
        new StoryRoot(storyCtx);
    }
}
