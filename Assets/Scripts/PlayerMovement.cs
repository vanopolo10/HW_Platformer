using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    private const float SpeedCoefficient = 10;

    [SerializeField] private InputService _inputService;
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private float _jumpForce = 12f;
    [SerializeField] private float _groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _coyoteTime = 0.2f;
    [SerializeField] private float _jumpBufferTime = 0.2f;
    
    private Rigidbody2D _rigidbody;

    private bool _isGrounded;
    private float _lastGroundedTime;
    private float _lastJumpInputTime;
    private float _currentSpeed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_inputService.IsJump && _isGrounded)
        {
            _lastJumpInputTime = Time.time;
            Jump();
        }
    }

    private void FixedUpdate()
    {
        _isGrounded = Physics2D.OverlapCircle(transform.position, _groundCheckRadius, _groundLayer);

        if (_isGrounded)
        {
            _lastGroundedTime = Time.time;
        }

        float moveInput = _inputService.WalkAxis;
        float targetSpeed = moveInput * _moveSpeed / SpeedCoefficient;
        
        _rigidbody.linearVelocity = new Vector2(targetSpeed, _rigidbody.linearVelocity.y);
    }

    private void Jump()
    {
        if ((Time.time - _lastGroundedTime <= _coyoteTime) && (Time.time - _lastJumpInputTime <= _jumpBufferTime))
        {
            _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, _jumpForce);
            _lastJumpInputTime = float.MinValue;
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _groundCheckRadius);
    }
}
