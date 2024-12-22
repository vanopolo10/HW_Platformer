using UnityEngine;

public class InputService : MonoBehaviour
{
    public bool IsJump => Input.GetButtonDown("Jump");
    public float WalkAxis => Input.GetAxisRaw("Horizontal");
}
