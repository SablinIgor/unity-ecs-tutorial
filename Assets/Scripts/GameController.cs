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
    
    /// <summary>
    /// Ссылка на prefab для игрока
    /// </summary>
    public GameObject playerPrefab;
    
    private void Awake()
    {
        var contexts = Contexts.sharedInstance;

        _systems = new Systems();
        _systems.Add(new DeathSystem(contexts));
        _systems.Add(new PrefabInstantiateSystem(contexts));
        _systems.Add(new ViewDestroySystem(contexts));
        _systems.Add(new TransformApplySystem(contexts));
        _systems.Initialize();

        var entity = contexts.game.CreateEntity();
        entity.isPlayer = true;
        entity.AddHealth(100.0f);
        entity.AddPrefab(playerPrefab);
        entity.AddPosition(new Vector2(0.0f, 0.0f));
        entity.AddRotation(30.0f);
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
