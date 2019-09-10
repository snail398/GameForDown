using UnityEngine;
using System;
using Object = UnityEngine.Object;

public class LoadingScreen 
{  
    public struct Ctx
    {
        public LoadingScreenView prefabView;
    }

    private Ctx _ctx;
    private Transform _canvas;
    private LoadingScreenView _view;
    public LoadingScreen(Ctx ctx)
    {
        _ctx = ctx;
     
    }

    public void SetCanvas(Transform canvas)
    {
        _canvas = canvas;
        _view = Object.Instantiate(_ctx.prefabView, _canvas);
    }

    public void ShowLoadingScreen()
    {
        if (_canvas != null)
        {
            _view.ShowLoaingScreen();
        }
    }

    public void HideLoadingScreen()
    {
        _view.HideLoadingScreen();
    }
}
