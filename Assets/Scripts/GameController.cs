using System;
using Entitas;
using UnityEngine;

/// <summary>
/// Контроллер игры
/// Подключение систем ECS
/// </summary>
public class GameController : MonoBehaviour
{
    private Systems _systems;
    private void Awake()
    {
        var contexts = Contexts.sharedInstance;

        _systems = new Systems();
        _systems.Add(new DeathSystem(contexts));
        _systems.Initialize();

        var entity = contexts.game.CreateEntity();
        entity.isPlayer = true;
        entity.AddHealth(100.0f);
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
