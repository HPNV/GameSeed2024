using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    protected float Health;
    protected float MaxHealth;

    protected void InitHealth(float health, float maxHealth)
    {
        Health = health;
        MaxHealth = maxHealth;
    }
    
    public void Damage(float damage)
    {
        if (!ValidateDamage()) return;
        Health -= damage;
        OnDamage();
        if (Health <= 0) OnDie();
    }

    public void Heal(float heal)
    {
        Health += heal;
        Health = Mathf.Min(Health, MaxHealth);
        OnHeal();
    }
    
    protected abstract bool ValidateDamage();
    protected abstract void OnDamage();
    protected abstract void OnHeal();
    protected abstract void OnDie();
}
