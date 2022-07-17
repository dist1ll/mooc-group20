using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TrashObject : MonoBehaviour, IVoiceTarget
{
    public string voiceName;
    public string VoiceName { get { return name; } set { name = value; } }

    public Dumpster.Type trashType;

    /// <summary>
    /// A child object that defines the bottom sitting point of the object. 
    /// </summary>
    public Transform bottomAnchor;

    // Cached rigidbody
    public Rigidbody rb { get {
            if (_rb == null) { _rb = GetComponent<Rigidbody>(); }
            return _rb;
        } set { _rb = value; }}
    private Rigidbody _rb;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Moves the trash object, so that its bottom anchor lies on the given position.
    /// </summary>
    public void PlaceDown(Vector3 position)
    {
        this.transform.SetParent(null);
        transform.rotation = Quaternion.identity;

        var diff = bottomAnchor.position - transform.position;
        transform.position = position - diff;

        rb.isKinematic = false;
    }
}
