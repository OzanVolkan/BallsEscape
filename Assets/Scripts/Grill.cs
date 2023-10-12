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
    public void CollisionCheck(Vector3 _initPos)
    {
        Vector3 halfExtents = _coll.bounds.extents;

        Collider[] colliders = Physics.OverlapBox(transform.localPosition, halfExtents, Quaternion.identity);

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject != gameObject)
            {
                transform.DOLocalMove(_initPos, 0.1f);
                GameManager.Instance.CurrentGrill = null;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (_coll != null)
        {
            Vector3 halfExtents = _coll.bounds.extents;
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.localPosition, 2 * halfExtents);
        }
    }
}
