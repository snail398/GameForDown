using UnityEngine;
using System;
using Object = UnityEngine.Object;

public class LoadingScreen 
{  
    public struct Ctx
    {
        public LoadingScreenView view;
    }

    private Ctx _ctx;
    private Transform _canvas;
    public LoadingScreen(Ctx ctx)
    {
        _ctx = ctx;
     
    }

    public void SetCanvas(Transform canvas)
    {
        _canvas = canvas;
    }

    public void ShowLoadingScreen()
    {
        if (_canvas != null)
        {
            _ctx.view = Object.Instantiate(_ctx.view, _canvas);
            _ctx.view.ShowLoaingScreen();
        }
    }

    public void HideLoadingScreen()
    {
        _ctx.view.HideLoadingScreen();
    }
}
