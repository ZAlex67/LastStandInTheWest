public class EnemyBoss : Enemy
{
    protected override void Die()
    {
        base.Die();
        Destroy(gameObject);
    }
}