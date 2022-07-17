using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dumpster : MonoBehaviour
{
    public enum Type
    {
        Paper,
        Glass,
        Plastic,
        Bio,
    }

    public Type dumpsterType;
    public Transform discardPoint;

    private void Awake()
    {
        if(discardPoint == null)
        {
            Debug.LogError("Dumpster has no discard point defined! Don't know where I'm supposed to drop the garbage.");
        }

    }
    public void Discard(TrashObject t)
    {
        t.transform.SetParent(null);
        t.transform.position = discardPoint.position;
        t.transform.rotation = Random.rotation;
        t.rb.isKinematic = false;
    }

    /// <summary>
    /// Triggers when Trash enters the validation zone
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        var t = other.GetComponent<TrashObject>();
        if(t != null)
        {
            if(t.trashType == dumpsterType)
            {
                Destroy(t.gameObject);
                Debug.Log("Success");
            } else
            {
                Dropzone.Drop(t);
                Debug.LogWarning("Dropped trash in the wrong bucket!");
            }
        }
    }
}
