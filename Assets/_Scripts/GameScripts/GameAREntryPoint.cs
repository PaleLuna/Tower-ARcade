using Cysharp.Threading.Tasks;
using PaleLuna.Architecture.EntryPoint;
using Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAREntryPoint : SceneEntryPoint
{
    protected override async UniTask Setup()
    {
        await base.Setup();
    }

    protected override void FillSceneLocator()
    {
        _sceneServiceLocator.Registarion(new InputSystem());
    }
}
