using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DrawBox : MonoBehaviour
{

    public Color color = Color.green;
    public bool filled = false;

    BoxCollider box;
    void OnDrawGizmos()
    {
#if UNITY_EDITOR
   
        box = GetComponent<BoxCollider>();
        if (box == null)
        {
            box = GetComponent<BoxCollider>();
            return;
        }
        Gizmos.color = color;
        if(filled)
        {
            Gizmos.DrawCube(box.bounds.center, box.bounds.size);

            Gizmos.color = Color.white;
            Gizmos.DrawWireCube(box.bounds.center, box.bounds.size);
        } else
        {
            Gizmos.DrawWireCube(box.bounds.center, box.bounds.size);
        }

#endif
    }
}
