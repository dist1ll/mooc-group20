using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton object for dropzone. A place where trashobjects are born!
/// </summary>
[RequireComponent(typeof(BoxCollider))]
public class Dropzone : MonoBehaviour
{

    public static Dropzone dz;
    private static BoxCollider col;

    private void Awake()
    {
        if(dz != null)
        {
            Destroy(gameObject);
            Debug.LogError("There was more than 1 dropzone active in this scene!");
            return;
        }
        dz = this;
        col = GetComponent<BoxCollider>();
    }
    public static void Drop(TrashObject t)
    {
        t.rb.isKinematic = false;
        t.transform.position = RandomColliderPoint(col);
    }

    static Vector3 RandomColliderPoint(Collider c)
    {
        Vector3 p = c.bounds.center;
        p.x += rngpm() * c.bounds.extents.x;
        p.y += rngpm() * c.bounds.extents.y;
        p.z += rngpm() * c.bounds.extents.z;
        return p;
    }

    /// <summary>
    /// Returns a rng number from [-1, 1]
    /// </summary>
    static float rngpm()
    {
        return (Random.value * 2 - 1);
    }
}
