public interface IHealthHandler
{
    float Health { get; set; }
    void OnDamage(float damage);
    void OnDeath();
}