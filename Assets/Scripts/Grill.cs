using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Grill : MonoBehaviour
{
    private Collider _coll;

    private void Start()
    {
        _coll = GetComponent<Collider>();
    }

    private void Update()
    {
        
    }
    public void CollisionCheck(Vector3 _initPos)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.localPosition, 1f);

        foreach (Collider collider in colliders)
        {

            if (collider.gameObject != gameObject)
            {
                print(collider.name);
                transform.DOLocalMove(_initPos, 0.1f);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (_coll != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.localPosition, 1f);
        }
    }
}
