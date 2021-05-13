public interface IHealthHandler
{
    float Health { get; set; }
    float MaxHealth { get; }
    void OnDamage(float damage);
    void OnDeath();
}