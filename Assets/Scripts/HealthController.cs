using System;

public class HealthController
{

    private int health;
    private int healtmax;

    public HealthController(int healtmax)
    {
        this.healtmax = healtmax;
        health = healtmax;
    }

    public int GetHealth()
    {
        return health;
    }

    public bool Damage(int DamageAcount)
    {
        health -= DamageAcount;
        if (health < 0) 
        {
            health = 0;
            return false;
        }
        return true;
    }

    public void Heal(int healAmount)
    {
        health += healAmount;
        if (health > healtmax) health = healtmax;
    }

}
