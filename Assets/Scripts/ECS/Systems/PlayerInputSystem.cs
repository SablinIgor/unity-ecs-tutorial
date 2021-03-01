using System;
using System.Collections;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

/// <summary>
/// Система управления для игрока
/// </summary>
public class PlayerInputSystem : IExecuteSystem
{
    private const float MoveSpeed = 10.0f;
    private const float RotateSpeed = 120.0f;

    private IGroup<GameEntity> entities;

    public PlayerInputSystem(Contexts contexts)
    {
        entities = contexts.game.GetGroup(GameMatcher.Player);
    }

    /// <summary>
    /// Считываем нажатия WASD и изменяем положение "игрока"
    /// </summary>
    public void Execute()
    {
        foreach (var e in entities)
        {
            // position

            float positionDelta = 0.0f;
            if (Input.GetKey(KeyCode.W))
                positionDelta += 1.0f;
            if (Input.GetKey(KeyCode.S))
                positionDelta -= 1.0f;

            if (!Mathf.Approximately(positionDelta, 0.0f))
            {
                var angle = e.rotation.angle * Mathf.Deg2Rad;
                var dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                e.ReplacePosition(e.position.value + dir * MoveSpeed * Time.deltaTime * positionDelta);
            }

            // rotation

            float rotationDelta = 0.0f;
            if (Input.GetKey(KeyCode.A))
                rotationDelta += 1.0f;
            if (Input.GetKey(KeyCode.D))
                rotationDelta -= 1.0f;
            
            if (!Mathf.Approximately(rotationDelta, 0.0f))
                e.ReplaceRotation(e.rotation.angle + rotationDelta * RotateSpeed * Time.deltaTime);
        }
    }
}
