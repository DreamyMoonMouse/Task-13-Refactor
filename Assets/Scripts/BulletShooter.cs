using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletShooter : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed = 10f;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _target;
    [SerializeField] private float _shootInterval;
    
    private void Start() 
    {
        StartCoroutine(ShootRoutine());
    }
    
    private IEnumerator ShootRoutine()
    {
        bool isWorking = true;
        WaitForSeconds waitForShootInterval = new WaitForSeconds(_shootInterval);
        
        while (isWorking)
        {
            Shoot();
            yield return waitForShootInterval;
        }
    }
    
    private void Shoot()
    {
        Vector3 direction = (_target.position - transform.position).normalized;
        Bullet bullet = Instantiate(_bulletPrefab, transform.position + direction, Quaternion.LookRotation(direction));
        bullet.Initialize(direction, _bulletSpeed);
    }
}