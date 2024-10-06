using DG.Tweening;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public Transform[] waypoints;
    public float duration;
    public Transform playerContainer;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.transform.parent = playerContainer;
            if (waypoints.Length > 0)
            {
                Vector3[] path = new Vector3[waypoints.Length];
                for (int i = 0; i < waypoints.Length; i++)
                {
                    path[i] = waypoints[i].position;
                }
                transform.DOPath(path, duration, PathType.CatmullRom).SetEase(Ease.Linear);
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.transform.parent = null;

        }
    }

}
