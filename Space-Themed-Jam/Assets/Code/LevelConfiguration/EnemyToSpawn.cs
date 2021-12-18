using UnityEngine;

[CreateAssetMenu(menuName = "Create/EnemyToSpawn")]
public class EnemyToSpawn : ScriptableObject
{
    public EnemyId EnemyId { get; }
    public Vector3 SpawnPosition { get; }
    public int Health { get; }
    public float Speed { get; }
    public float FireRate { get; }
    public int PointsToAdd { get; }
}
