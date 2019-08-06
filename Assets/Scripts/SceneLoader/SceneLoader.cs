using UnityEngine.SceneManagement;
using System;
using UnityEngine;
using System.Collections.Generic;

public class SceneLoader
{
    public struct Ctx
    {
        public Action onStorySceneLoaded;
        public Action onRunSceneLoaded;
    }

    private Ctx _ctx;
    private List<AsyncOperation> _sceneList = new List<AsyncOperation>();
    private AsyncOperation _asyncSceneLoad;

    public SceneLoader(Ctx ctx)
    {
        _ctx = ctx;
    }
    public void LoadStoryScene()
    {
        _asyncSceneLoad =  SceneManager.LoadSceneAsync(1);
        _asyncSceneLoad.completed += OnSceneLoaded;
    }


    private void OnSceneLoaded(AsyncOperation op)
    {
        _ctx.onStorySceneLoaded?.Invoke();
        _asyncSceneLoad.completed -= OnSceneLoaded;
    }

    public void ReloadGame()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void LoadRunScene()
    {
        _asyncSceneLoad = SceneManager.LoadSceneAsync(2);
        _asyncSceneLoad.completed += OnRunSceneLoaded;
    }

    private void OnRunSceneLoaded(AsyncOperation op)
    {
        _ctx.onRunSceneLoaded?.Invoke();
        _asyncSceneLoad.completed -= OnRunSceneLoaded;
    }
}
