using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        // Start is called before the first frame update
        Transform childTransform;
        Transform nextChildTransform;
        [SerializeField]
        float radius = 0.3f;
        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++) {
                childTransform =  transform.GetChild(i);
                nextChildTransform = transform.GetChild((i + 1) % transform.childCount);
                Gizmos.color = Color.gray;
                Gizmos.DrawSphere(childTransform.position, radius);
                Gizmos.DrawLine(childTransform.position, nextChildTransform.position);
            }
        }
    }
}