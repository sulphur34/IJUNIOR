using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class Controls : MonoBehaviour
{
    private const string Shoot = "Shoot";
    private const string IsArmed = "isArmed";
    private const string MoveX = "moveX";
    private const string Damage = "Damage";

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Vector2 _moveVector;
    private int _shootIndex;
    private int _isArmedIndex;
    private int _moveXIndex;
    private int _damageIndex;

    private void Start()
    {        
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _shootIndex = Animator.StringToHash(Shoot);
        _isArmedIndex = Animator.StringToHash(IsArmed);
        _moveXIndex = Animator.StringToHash(MoveX);
        _damageIndex = Animator.StringToHash(Damage);
    }

    private void Update()
    {
        float speed;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _animator.SetTrigger(_shootIndex);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            bool isArmed = _animator.GetBool(_isArmedIndex);
            _animator.SetBool(_isArmedIndex, !isArmed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift))
                speed = 1f;
            else
                speed = 0.4f;

            Flip();
            Move(speed);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            _animator.SetFloat(_moveXIndex, 0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift))
                speed = -1f;
            else
                speed = -0.4f;

            Flip();
            Move(speed);
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            _animator.SetFloat(_moveXIndex, 0);
        }
    }

    private void Move(float speed)
    {
        _moveVector.x = speed * Time.deltaTime;
        Debug.Log(_moveVector.x);
        _animator.SetFloat(_moveXIndex, Mathf.Abs(_moveVector.x));
        transform.Translate(_moveVector.x, 0, 0);
    }

    private void Flip()
    {
        _spriteRenderer.flipX = _moveVector.x < 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float bulletforce = 10f;
        collision.gameObject.GetComponent<Rigidbody>();
        Vector3 impactForce = collision.relativeVelocity;

        if (impactForce.magnitude > bulletforce)
            _animator.SetTrigger(_damageIndex);
    }
}
