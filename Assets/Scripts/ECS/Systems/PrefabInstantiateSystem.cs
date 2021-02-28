using System;
using System.Collections;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

/// <summary>
/// Реактивная система привязки prefab к entity
/// </summary>
public class PrefabInstantiateSystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="contexts">Контекст игры</param>
    public PrefabInstantiateSystem(Contexts contexts) : base(contexts.game)
    {
        this._contexts = contexts;
    }

    /// <summary>
    /// Реагируем на изменение entity с компонентом Prefab
    /// </summary>
    /// <param name="context">Контекст игры</param>
    /// <returns></returns>
    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Prefab);
    }

    /// <summary>
    /// Дополнительные условия отбора entity
    /// </summary>
    /// <param name="entity">Entity на проверку</param>
    /// <returns>Без фильтра</returns>
    protected override bool Filter(GameEntity entity)
    {
        return entity.hasPrefab && !entity.hasView;
    }

    /// <summary>
    /// Обработка списка изменившихся entities 
    /// </summary>
    /// <param name="entities">Список entities</param>
    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            e.AddView(GameObject.Instantiate(e.prefab.prefab));
        }
    }
}
