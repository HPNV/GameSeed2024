using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using Particles;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    protected float Health;
    protected float MaxHealth;
    private Coroutine _speedCoroutine;
    private Particle _particle;

    protected void Init(float health, float maxHealth)
    {
        Health = health;
        MaxHealth = maxHealth;
    }
    
    public virtual void Damage(float damage)
    {
        if (!ValidateDamage()) return;
        Health -= damage;
        OnDamage();
        
        if (Health <= 0) 
            OnDie();
    }

    public void Heal(float heal)
    {
        Health += heal;
        Health = Mathf.Min(Health, MaxHealth);
        OnHeal();
    }

    public void SpeedUp(float duration)
    {
        if (_particle == null)
            _particle = SingletonGame.Instance.ParticleManager.Spawn(ParticleName.SpeedUp, transform.position, duration);
        _speedCoroutine = StartCoroutine(SpeedUpCoroutine(duration));
    }
    
    
    private IEnumerator SpeedUpCoroutine(float duration)
    {
        OnSpeedUp();
        yield return new WaitForSeconds(duration);
        OnSpeedUpClear();
    }

    protected void OnDestroy()
    {
        if (_speedCoroutine != null)
            StopCoroutine(_speedCoroutine);

        if (_particle != null)
        {
            _particle.Despawn();
            _particle = null;
        }
    }

    protected abstract bool ValidateDamage();
    protected abstract void OnDamage();
    protected abstract void OnHeal();
    protected abstract void OnDie();
    protected abstract void OnSpeedUp();
    protected abstract void OnSpeedUpClear();
}
