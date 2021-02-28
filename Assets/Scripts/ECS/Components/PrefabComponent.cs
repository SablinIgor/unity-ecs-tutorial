using Entitas;
using UnityEngine;

/// <summary>
/// Компонент для привязки prefab-а к entity
/// </summary>
public class PrefabComponent : IComponent
{
    /// <summary>
    /// Ссылка на prefab
    /// </summary>
    public GameObject prefab;
}
