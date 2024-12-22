using System.Collections;
using System.Collections.Generic;
using Extensions;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Enemy : MonoBehaviour
{
    private const float SpeedCoefficient = 10;
    
    [SerializeField] private List<Vector2> _patrolWaypoints;
    [SerializeField] private float _speed = 0.2f;
    [SerializeField] private float _epsilon = 0.07f;

    private int _nextWaypoint = 1;
    private bool _isLeft;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void Start()
    {
        StartMoving();
    }

    private void StartMoving()
    {
        StopAllCoroutines();
        StartCoroutine(Patrol());
    }
    
    private IEnumerator Patrol()
    {
        while (true)
        {
            if(_isLeft && transform.position.x < _patrolWaypoints[_nextWaypoint].x || _isLeft == false && transform.position.x > _patrolWaypoints[_nextWaypoint].x)
                Flip();
            
            transform.position = Vector2.MoveTowards(transform.position, _patrolWaypoints[_nextWaypoint], _speed / SpeedCoefficient * Time.deltaTime);
            
            if (IsOnWaypoint())
            {
                if (_nextWaypoint != _patrolWaypoints.Count - 1)
                    _nextWaypoint++;
                else
                    _nextWaypoint = 0;
            }
            
            yield return false;
        }
    }

    private bool IsOnWaypoint()
    {
        return transform.position.ToVector2().IsCloseEnough(_patrolWaypoints[_nextWaypoint], _epsilon);
    }
    
    private void Flip()
    {
        _isLeft = !_isLeft;
        _spriteRenderer.flipX = _isLeft;
    }
}