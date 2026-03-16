using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Health))]
public abstract class Enemy : MonoBehaviour, IPoolable
{
    private NavMeshAgent _agent;

    protected Health Health { get; private set; }

    public NavMeshAgent Agent => _agent;

    public event Action<Enemy> Died;

    private void Awake()
    {
        Health = GetComponent<Health>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        Health.OnDeath += Die;
    }

    private void OnDisable()
    {
        Health.OnDeath -= Die;
    }

    public virtual void WarpTo(Vector3 position, Quaternion rotation)
    {
        if (_agent == null)
        {
            transform.SetPositionAndRotation(position, rotation);
            return;
        }

        _agent.enabled = false;
        transform.SetPositionAndRotation(position, rotation);
        _agent.Warp(position);
        _agent.enabled = true;
    }

    public void OnSpawn()
    {
        Health.OnSpawn();
    }

    public void OnDespawn()
    {
        Health.OnDespawn();
    }

    protected virtual void Die()
    {
        Died?.Invoke(this);
    }
}