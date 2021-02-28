using System.Collections.Generic;
using Entitas;

/// <summary>
/// Система реализации смерти.
/// При уменьшении показателя здоровья до нуля или ниже entity должно быть уничтожено
/// </summary>
public class DeathSystem : IExecuteSystem
{
    private IGroup<GameEntity> entities;
    private List<Entity> deadEntities = new List<Entity>();
    
    /// <summary>
    /// Поиск entities с компонентом Health
    /// </summary>
    /// <param name="contexts"></param>
    public DeathSystem(Contexts contexts)
    {
        entities = contexts.game.GetGroup(GameMatcher.Health);
    }

    /// <summary>
    /// Уничтожение entity с показателем здоровья меньше или равным нулю
    /// </summary>
    public void Execute()
    {
        deadEntities.Clear();
        foreach (var e in entities)
        {
            if (e.health.value <= 0)
                deadEntities.Add(e);
        }

        foreach (var e in deadEntities)
        {
            e.Destroy();
        }
    }
}
