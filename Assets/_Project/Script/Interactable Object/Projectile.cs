using DG.Tweening;
using System;
using UCExtension;
using UnityEngine;

public class Projectile : RecylableObject
{
    [SerializeField] float timeMove;

    [SerializeField] RecylableObject effect;

    [SerializeField] AudioClip hitSound;

    public void Init(Transform startPoint, Transform endPoint, Action finish = null)
    {
        transform.position = startPoint.position;
        transform.DOMove(endPoint.position, timeMove)
            .SetEase(Ease.Linear).OnComplete(() =>
            {
                AudiosManager.Instance.PlaySFX("hitSound");
                finish?.Invoke();
                Recyle();
                PoolObjects.Ins.Spawn(effect, transform.position);
            });
    }

    public void Init(Transform startPoint)
    {
        transform.position = startPoint.position;
        transform.GetComponent<Rigidbody>().AddForce(10 * transform.right, ForceMode.Impulse);

    }

}
