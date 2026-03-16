using UnityEngine;

public class EnemyRegular : Enemy
{
    private IPool<EnemyRegular> _pool;

    public void Init(IPool<EnemyRegular> pool)
    {
        Debug.Assert(pool != null, "Pool cannot be null");

        _pool = pool;
    }

    protected override void Die()
    {
        base.Die();

        Debug.Assert(_pool != null);

        _pool.Despawn(this);
    }
}