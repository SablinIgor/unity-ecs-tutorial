using UnityEngine;

/// <summary>
/// Родительский класс всех entities
/// </summary>
public abstract class AbstractEntity : MonoBehaviour
{
    /// <summary>
    /// Доступ к контексту
    /// </summary>
    protected  Contexts _contexts { get; private set; }
    protected  GameEntity entity { get; private set; }

    /// <summary>
    /// Создание entity c указанием позиции и поворота
    /// </summary>
    protected virtual void Start()
    {
        _contexts = Contexts.sharedInstance;
        entity = _contexts.game.CreateEntity();
        entity.AddPosition(transform.position);
        entity.AddRotation(transform.rotation.eulerAngles.z);
        Destroy(gameObject);
    }
}
