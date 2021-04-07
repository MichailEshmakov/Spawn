using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Mover))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _dyingTime;
    [SerializeField] private float _movingAccuracy;

    private Mover _mover;

    public UnityEvent OnDie;

    public bool IsDead { get; private set; }
    public Transform Target { get; set; }

    private void Awake()
    {
        IsDead = false;
        _mover = GetComponent<Mover>();
    }

    private void Start()
    {
        StartCoroutine(MoveToTarget());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Dangerous dangerous))
        {
            Die();
        }
    }

    private IEnumerator MoveToTarget()
    {
        float sqrMovingAccuracy = _movingAccuracy * _movingAccuracy;
        
        while (Target != null && sqrMovingAccuracy <= (Target.position - transform.position).sqrMagnitude && IsDead == false)
        {
            _mover.Move(Target.position.x > transform.position.x);
            yield return new WaitForFixedUpdate();
        }

        _mover.Stop();
    }

    private void Die()
    {
        IsDead = true;
        OnDie?.Invoke();
        Destroy(gameObject, _dyingTime);
    }
}
