using Entitas;
using UnityEngine;

/// <summary>
/// Система управления для игрока
/// </summary>
public class PlayerInputSystem : IExecuteSystem
{
    private const float MoveSpeed = 10.0f;
    private const float RotateSpeed = 120.0f;
    private const float ShotSpeed = 20.0f;

    private Contexts _contexts;
    private IGroup<GameEntity> entities;

    public PlayerInputSystem(Contexts contexts)
    {
        this._contexts = contexts;
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
            
            // shooting

            if (Input.GetMouseButtonDown(0))
            {
                var angle = e.rotation.angle * Mathf.Deg2Rad;
                var dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

                var entity = _contexts.game.CreateEntity();
                entity.isShot = true;
                entity.AddPosition(e.position.value + dir);
                entity.AddRotation(e.rotation.angle);
                entity.AddPrefab(_contexts.game.globals.shotPrefab);
                entity.AddForwardMovement(ShotSpeed);
            }
        }
    }
}
