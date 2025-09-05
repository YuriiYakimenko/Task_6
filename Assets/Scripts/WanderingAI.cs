using UnityEngine;
using System.Collections;

public class WanderingAI : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    public float speed = 3.0f;
    public float minDistance = 5.0f;
    public float shootRadius = 10.0f;
    public float shootDelay = 1.0f;
    public float bulletSpeed = 20f;

    private bool _alive;
    private float _lastShootTime;

    void Start()
    {
        _alive = true;
        _lastShootTime = -shootDelay;
    }

    [System.Obsolete]
    void Update()
    {
        if (!_alive || player == null) return;
        Vector3 direction = player.position - transform.position;
        direction.y = 0;
        float distance = direction.magnitude;
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
        if (distance > minDistance)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        if (distance <= shootRadius && Time.time - _lastShootTime >= shootDelay)
        {
            ShootBullet((player.position - transform.position).normalized);
            _lastShootTime = Time.time;
        }
    }

    [System.Obsolete]
    void ShootBullet(Vector3 shootDir)
    {
        if (bulletPrefab == null) return;

        Vector3 spawnPos = bulletSpawnPoint != null ? bulletSpawnPoint.position : transform.position + Vector3.up * 1f;
        GameObject bullet = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.Init(shootDir * bulletSpeed, player);
        }
    }

    public void SetAlive(bool alive)
    {
        _alive = alive;
    }
}