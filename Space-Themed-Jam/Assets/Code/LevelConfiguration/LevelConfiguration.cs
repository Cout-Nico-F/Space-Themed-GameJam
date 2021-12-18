using UnityEngine;

[CreateAssetMenu(menuName = "Create/LevelConfiguration")]
public class LevelConfiguration : ScriptableObject
{
    public WaveConfiguration[] WaveConfigurations { get; }
}
