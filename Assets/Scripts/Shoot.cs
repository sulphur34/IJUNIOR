using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private bool _isTurnRight;
    [SerializeField] private Vector2 _muzzleEndPosition;

    private SpriteRenderer _spriteRenderer;

    private void ShootEvent()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        float direction;

        if (_spriteRenderer.flipX == _isTurnRight)
            direction = -1;
        else
            direction = 1;
        
        Vector3 currentPosition = transform.position + new Vector3(_muzzleEndPosition.x * direction, _muzzleEndPosition.y, 0f);
        Instantiate(_bullet, currentPosition, Quaternion.identity);
    }
}
