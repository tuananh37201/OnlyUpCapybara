using UCExtension;
using UnityEngine;


public class Shooter : MonoBehaviour
{
    [SerializeField] Projectile bulletPrefab;
    [SerializeField] Transform bulletStartPoint;
    [SerializeField] Transform bulletEndPoint;

    public float cooldown;

    void Start()
    {
        InvokeRepeating("SpawnBullet", 0f, cooldown);
    }

    private void SpawnBullet()
    {
        //AudiosManager.Instance.PlaySFX("shootSound");
        var bullet = PoolObjects.Ins.Spawn(bulletPrefab);
        bullet.Init(bulletStartPoint);
    }
}
