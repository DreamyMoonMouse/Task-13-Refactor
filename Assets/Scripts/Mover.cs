using UnityEngine;

public class Mover : MonoBehaviour
{
    private const float MaxDistance = 0.0001f;
    
    [SerializeField] private float _speed;
    [SerializeField] private Transform _waypointsParent;
    
    private Transform[] _waypoints;
    private int _currentIndex;
    
    private void Start() 
    {
        RefreshChildArray();
    }
    
    private void Update()
    {
        MoveToWaypoint();
    }
    
    [ContextMenu("Refresh Child Array")]
    private void RefreshChildArray()
    {
        int count = _waypointsParent.childCount;
        _waypoints = new Transform[count];

        for (int i = 0; i < count; i++)
        {
            _waypoints[i] = _waypointsParent.GetChild(i);
        } 
    }

    private void MoveToWaypoint()
    {
        Transform target = _waypoints[_currentIndex];
        transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
        Vector3 direction = target.position - transform.position;
        
        if (direction.sqrMagnitude < MaxDistance)
        {
            _currentIndex = ++_currentIndex % _waypoints.Length;
            transform.forward = (_waypoints[_currentIndex].position - transform.position).normalized;
        }
    }
}