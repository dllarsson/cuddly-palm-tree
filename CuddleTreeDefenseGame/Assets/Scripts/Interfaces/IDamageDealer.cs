using System.Collections.Generic;
public interface IDamageDealer
{
    float Damage { get; set; }
    List<string> TargetTag { get; set; }
}