
public interface IProjectile
{
    public void SetObjectPoolReference(ProjectileObjectPool objectPool);
    public void ReturnMeToPool();
    public void SetMyStats(float dmg, float speed);
}
