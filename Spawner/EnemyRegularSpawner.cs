using UnityEngine;

public class EnemyRegularSpawner : SpawnerGeneric<EnemyRegular>
{
    protected override void InitInstance(EnemyRegular instance)
    {
        instance.Init(this);
    }

    protected override void SetPosition(EnemyRegular enemy, Vector3 position, Quaternion rotation)
    {
        enemy.WarpTo(position, rotation);
    }
}