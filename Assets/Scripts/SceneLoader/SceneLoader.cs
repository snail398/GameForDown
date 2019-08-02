using UnityEngine.SceneManagement;
using System;
using UnityEngine;

public class SceneLoader
{
    public struct Ctx
    {
        public Action onStorySceneLoaded;
    }

    private Ctx _ctx;

    public SceneLoader(Ctx ctx)
    {
        _ctx = ctx;
    }
    public void LoadStoryScene()
    {
       AsyncOperation asyncSceneLoad =  SceneManager.LoadSceneAsync(1);
       asyncSceneLoad.completed += OnSceneLoaded;
    }

    private void OnSceneLoaded(AsyncOperation op)
    {
        _ctx.onStorySceneLoaded?.Invoke();
    }
}
