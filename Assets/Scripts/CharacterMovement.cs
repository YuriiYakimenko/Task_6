using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
    [Header("Character movement stats")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateSpeed;

    [Header("Animation")]
    [SerializeField] private Animator _animator;

    [Header("Gravity handling")]
    private float _currentAttractionCharacter = 0;
    [SerializeField] private float _gravityForce = 20;

    [Header("Character component")]
    private CharacterController _charController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _charController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GravityHandling();
    }

    public void MoveCharacter(Vector3 moveDirection)
    {
        moveDirection = moveDirection * _moveSpeed;
        moveDirection.y = _currentAttractionCharacter;
        _charController.Move(moveDirection * Time.deltaTime);
        if (_animator != null)
        {
            Vector3 localMove = transform.InverseTransformDirection(moveDirection);
            if (localMove.magnitude < 0.01f)
            {
                _animator.SetTrigger("idle");
            }
            else
            {
                _animator.ResetTrigger("idle");
                _animator.ResetTrigger("run_forward");
                if (localMove.z > 0.1f)
                    _animator.SetTrigger("run_forward");
            }
        }
    }

    public void RotateCharacter(Vector3 moveDitertion)
    {
        if (_charController.isGrounded)
        {
            if (Vector3.Angle(transform.forward, moveDitertion) > 0)
            {
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, moveDitertion, _rotateSpeed, 0);
                transform.rotation = Quaternion.LookRotation(newDirection);
            }
        }
    }
    private void GravityHandling()
    {
        if (!_charController.isGrounded)
        {
            _currentAttractionCharacter -= _gravityForce * Time.deltaTime;
        }
        else
        {
            _currentAttractionCharacter = 0;
        }
    }
}
