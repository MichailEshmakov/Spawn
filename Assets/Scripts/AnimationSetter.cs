using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class AnimationSetter : MonoBehaviour
{
    [SerializeField] private float _speedCoefficient;
    [SerializeField] private float _deathSpeed;

    private Mover _mover;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        StartCoroutine(ÑonformSpeed());
        StartCoroutine(ConformFlip());
    }

    private IEnumerator ConformFlip()
    {
        while (_animator.GetBool("isDead") == false)
        {
            _spriteRenderer.flipX = _mover.IsRightDirection == false;
            yield return null;
        }
    }

    private IEnumerator ÑonformSpeed()
    {
        while (_animator.GetBool("isDead") == false)
        {
            _animator.speed = _speedCoefficient * _mover.Speed;
            yield return null;
        }
    }

    public void Die()
    {
        _animator.speed = _deathSpeed;
        _animator.SetBool("isDead", true);
    }
}
