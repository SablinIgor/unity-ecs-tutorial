using Entitas;
using UnityEngine;

/// <summary>
/// Система удаление GameObject со сцены
/// </summary>
public class ViewDestroySystem : IInitializeSystem, ITearDownSystem
{
    private IGroup<GameEntity> _group;
    
    /// <summary>
    /// Выбираем entities с компонентом View
    /// </summary>
    /// <param name="contexts">Контекст игры</param>
    public ViewDestroySystem(Contexts contexts)
    {
        _group = contexts.game.GetGroup(GameMatcher.View);
    }

    /// <summary>
    /// Подписка на событие удаления entity
    /// </summary>
    public void Initialize()
    {
        _group.OnEntityRemoved += OnViewRemoved;
    }

    /// <summary>
    /// Отписываемся от события после обработки
    /// </summary>
    public void TearDown()
    {
        _group.OnEntityRemoved -= OnViewRemoved;
    }

    void OnViewRemoved(IGroup<GameEntity> group, GameEntity entity, int index, IComponent component)
    {
        var view = (ViewComponent) component;
        GameObject.Destroy(view.gameObject);
    }
}
