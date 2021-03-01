using System.Collections;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

/// <summary>
/// Система управлением движения вперед
/// </summary>
public class ForwardMovementSystem : IExecuteSystem
{
    private IGroup<GameEntity> entities;

    /// <summary>
    /// Выбираем все entity с тремя компонентами: ForwardMovement, Position, Rotation
    /// </summary>
    /// <param name="contexts"></param>
    public ForwardMovementSystem(Contexts contexts)
    {
        entities = contexts.game.GetGroup(GameMatcher.AllOf(
            GameMatcher.ForwardMovement,
            GameMatcher.Position,
            GameMatcher.Rotation
        ));
    }

    /// <summary>
    /// Вычисляем координаты для движения вперед
    /// </summary>
    public void Execute()
    {
        foreach (var e in entities)
        {
            var angle = e.rotation.angle * Mathf.Deg2Rad;
            var dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            e.ReplacePosition(e.position.value + dir * e.forwardMovement.speed * Time.deltaTime);
        }
    }
}
