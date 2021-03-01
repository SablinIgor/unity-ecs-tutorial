using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

/// <summary>
/// Компонент-синглтон (контекста) для хранения глобальных значений
/// </summary>
[Unique]
public class GlobalsComponent : IComponent
{
    public GameObject shotPrefab;
}
