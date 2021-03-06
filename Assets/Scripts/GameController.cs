﻿using System;
using Entitas;
using UnityEngine;

/// <summary>
/// Контроллер игры
/// Подключение систем ECS
/// </summary>
public class GameController : MonoBehaviour
{
    private Systems _systems;
    public GameObject shotPrefab;
    
    private void Awake()
    {
        var contexts = Contexts.sharedInstance;

        contexts.game.SetGlobals(shotPrefab);

        _systems = new Systems();
        _systems.Add(new DeathSystem(contexts));
        _systems.Add(new PrefabInstantiateSystem(contexts));
        _systems.Add(new ViewDestroySystem(contexts));
        _systems.Add(new PlayerInputSystem(contexts));
        _systems.Add(new ForwardMovementSystem(contexts));
        _systems.Add(new TransformApplySystem(contexts));
        _systems.Initialize();
    }

    private void OnDestroy()
    {
        _systems.TearDown();
    }

    private void Update()
    {
        _systems.Execute();
        _systems.Cleanup();
    }
}
