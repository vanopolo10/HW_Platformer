using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D))]
public class PlayerAnimator : MonoBehaviour
{
    private static readonly int _horizontalSpeedID = Animator.StringToHash("Horizontal Speed");
    
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private bool _isLeft;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _animator.SetFloat(_horizontalSpeedID, Mathf.Abs(_rigidbody.linearVelocity.x));

        if (_isLeft && _rigidbody.linearVelocity.x > 0 || _isLeft == false && _rigidbody.linearVelocity.x < 0)
            Flip();
    }
    
    private void Flip()
    {
        int mirrorRotation = 180;
        
        transform.rotation = Quaternion.Euler(0, transform.rotation.y == 0 ? mirrorRotation : 0, 0);
        _isLeft = transform.rotation.y != 0;
    }
}
