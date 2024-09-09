using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPathController : MonoBehaviour
{
    public Color LineColor;
    public Color SphereColor;
    private Transform[] nodes;

    private void OnDrawGizmos()
    {
        nodes = new Transform[transform.childCount];

        for(int c = 0; c < nodes.Length; c++)
        {
            nodes[c] = transform.GetChild(c);
        }

        int count = nodes.Length;
        for(int i = 0; i < count; i++)
        {
            Vector3 currentNode = nodes[i].position;
            Vector3 previousNode = nodes[(count - 1 + i) % count].position;

            Gizmos.color = LineColor;
            Gizmos.DrawLine(previousNode, currentNode);
            Gizmos.color = SphereColor;
            Gizmos.DrawSphere(currentNode,2);
        }
    }
}
