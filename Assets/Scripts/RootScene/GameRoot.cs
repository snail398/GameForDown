using UnityEngine;
using Story;

public class GameRoot : MonoBehaviour
{
    AnaliticsCore _analiticsCore;
    PlayersData _playersData;
    private void Awake()
    {
        //Create SceneLoader object
        SceneLoader.Ctx loaderCtx = new SceneLoader.Ctx
        {
            onStorySceneLoaded = OnSceneLoaded,
        };
        SceneLoader sceneLoader = new SceneLoader(loaderCtx);
        //Load Story Scene
        sceneLoader.LoadStoryScene();

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
        };
        new StoryRoot(storyCtx);
    }
}
