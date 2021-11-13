using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]

public class Target : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        gameObject.GetComponent<CircleCollider2D>().enabled = false; // отключаем коллайдер объекта при старте игры
    }

    public void Hit()
    {
        _animator.Play("Hit");
        gameObject.GetComponent<CircleCollider2D>().enabled = false; // отключаем коллайдер объекта при попадании
    }

    public void Show()
    {
        _animator.Play("Show");
        gameObject.GetComponent<CircleCollider2D>().enabled = true; // включаем коллайдер объекта при появлении
    }
}
