using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _waypointsParent;
    
    private Transform[] _waypoints;
    private int _currentIndex;
    private const float _tolerance = 0.0001f;
    
    void Start() 
    {
        int count = _waypointsParent.childCount;
        _waypoints = new Transform[count];

        for (int i = 0; i < count; i++)
        {
            _waypoints[i] = _waypointsParent.GetChild(i);
        }
    }
    private void Update()
    {
        MoveToWaypoint();
    }

    private void MoveToWaypoint()
    {
        Transform target = _waypoints[_currentIndex];
        Vector3 direction = target.position - transform.position;
        transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);

        if (direction.sqrMagnitude < _tolerance)
        {
            _currentIndex = ++_currentIndex % _waypoints.Length;
            transform.forward = (_waypoints[_currentIndex].position - transform.position).normalized;
        }
    }
}