using UnityEngine;

public class GetDistance : MonoBehaviour
{
    [SerializeField] Transform _firstObject;
    [SerializeField] Transform _secondObject;

    public float GetDistanceFunc()
    {
        return (_firstObject.position - _secondObject.position).magnitude;
    }
}
