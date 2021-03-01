using System.Collections;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

/// <summary>
/// Система изменения координат и угла направления
/// </summary>
public class TransformApplySystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;

    public TransformApplySystem(Contexts contexts) : base(contexts.game)
    {
        this._contexts = contexts;
    }

    /// <summary>
    /// Реакция на ситуацию, когда используется либо компонент Position или Rotation или есть компонент View
    /// </summary>
    /// <param name="context">Контекст игры</param>
    /// <returns>Список измененных компоннетов</returns>
    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return new Collector<GameEntity>(
            new[]
            {
                context.GetGroup(GameMatcher.AnyOf(GameMatcher.Position, GameMatcher.Rotation)),
                context.GetGroup(GameMatcher.View),
            }, new[]
            {
                GroupEvent.Added,
                GroupEvent.Added
            }
        );
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasView;
    }
    
    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            var t = e.view.gameObject.transform;
            if (e.hasPosition)
                t.position = e.position.value;
            if (e.hasRotation)
                t.rotation = Quaternion.Euler(0.0f, 0.0f, e.rotation.angle);
        }
    }
}
