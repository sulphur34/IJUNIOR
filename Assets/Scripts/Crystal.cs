using System.Collections;
using System.Linq;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    [SerializeField] private float _speed;

    public event System.Action<float> RedColorChanged;
    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private Vector3 _currentDestinationPosition;
    private SpriteRenderer _spriteRenderer;
    private AudioSource _audioSource;
    private bool _isContinue;

    private void Start()
    {
        float floatingDistance = -0.3f;
        _startPosition = new Vector3(0f,0f,0f) + transform.position;
        _endPosition = new Vector3(0f,floatingDistance, 0f) + transform.position;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
        _isContinue = true;
        StartCoroutine(DetectEnemy());
        _audioSource.Play();
    }

    private void Update()
    {
        if (transform.position == _startPosition)
            _currentDestinationPosition = _endPosition;
        else if (transform.position == _endPosition)
            _currentDestinationPosition = _startPosition;

        MoveTowardsTarget(_currentDestinationPosition);
    }

    private IEnumerator DetectEnemy()
    {
        while (_isContinue)
        {
            Alert();
            yield return null;
        }
    }

    private void MoveTowardsTarget(Vector3 destinationPosition)
    {
        var moveVector = Vector2.MoveTowards(transform.position, destinationPosition, _speed * Time.deltaTime);
        transform.position = moveVector;
    }

    private void Alert()
    {
        GameObject[] ennemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (ennemies.Length == 0)
        {
            _spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            _audioSource.volume = 0f;
        }
        else
        {
            float minDistance = ennemies.Select(x => Mathf.Abs((x.transform.position - transform.position).magnitude)).Min();
            _spriteRenderer.color = ConvertDistanceToColor(minDistance);

            if (minDistance < 1)
                _audioSource.volume = 1f;
            else
                _audioSource.volume = 1/minDistance;     
        }
    }

    private Color ConvertDistanceToColor(float distance)
    {
        float koefficient = 2f;
        float channelValue = distance / koefficient;
        return new Color(1f, channelValue, channelValue, 1f);
    }

}
