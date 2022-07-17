using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    [Header("Player Components")]
    public Transform hand;
    public GameObject crosshair;

    private TrashObject currentlyHeldObject { get; set; }
    
    [Header("Physics")]
    public LayerMask collisionLayer;
    [Range(0.1f, 5f)]
    public float sensitivity = 1;

    Transform cam;
    Vector3 camRotation, ownRotation;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
 
        cam = Camera.main.transform;
        camRotation = cam.localEulerAngles;
        ownRotation = transform.localEulerAngles;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            ClickEvent(cam.position, cam.forward);
        }
        // Rotate camera up and down
        camRotation.x -= Input.GetAxisRaw("Mouse Y") * sensitivity * 100 * Time.deltaTime;
        camRotation.x = Mathf.Clamp(camRotation.x, -80, +90);
        // Rotate myself
        ownRotation.y += Input.GetAxisRaw("Mouse X") * sensitivity * 100 * Time.deltaTime;
        ownRotation.y = ownRotation.y % 360;

        cam.localEulerAngles = camRotation;
        transform.localEulerAngles = ownRotation;
    }

    private void OnEnable()
    {
        crosshair.SetActive(true);
    }

    private void OnDisable()
    {
        crosshair.SetActive(false);
    }

    /// Executes click behavior, from a given position and direction.
    void ClickEvent(Vector3 pos, Vector3 dir)
    {
        var hit = Physics.RaycastAll(pos, dir, 20f, collisionLayer, QueryTriggerInteraction.Ignore).ToList();

        RaycastHit trash = hit.Find(h => h.collider.GetComponent<TrashObject>() != null);
        RaycastHit table = hit.Find(h => h.collider.GetComponent<Table>() != null);
        RaycastHit dumpster = hit.Find(h => h.rigidbody?.GetComponent<Dumpster>() != null);

        // no trash clicked
        if(trash.IsDefault())
        {
            // table was clicked
            if (!table.IsDefault())
            {
                PutAt(table.point);
            }
            // dumpster was clicked
            else if (!dumpster.IsDefault())
            {
                ThrowIntoDumpster(dumpster.rigidbody.GetComponent<Dumpster>());
            }
        }

        // trash was clicked
        if(!trash.IsDefault())
        {
            Pickup(trash.collider.GetComponent<TrashObject>());
        }
    }

    /// <summary>
    /// Throws object into the dumpster
    /// </summary>
    public void ThrowIntoDumpster(Dumpster d)
    {
        if(currentlyHeldObject == null) { return; }

        d.Discard(currentlyHeldObject);
        currentlyHeldObject = null;
    }

    /// <summary>
    /// Moves trash object to anchor, with a smoothing animation
    /// </summary>
    public void Pickup(TrashObject t)
    {
        // if we are holding an object right now, we switch positions
        if(currentlyHeldObject != null) 
        {
            PutAt(t.bottomAnchor.position);
        }
        currentlyHeldObject = t;
        t.rb.isKinematic = true;
        t.transform.SetParent(hand);
        t.transform.localRotation = Quaternion.identity;
        t.transform.localPosition = Vector3.zero;
    }

    /// <summary>
    /// Puts down currently held object 
    /// </summary>
    public void PutAt(Vector3 pos)
    {
        if(currentlyHeldObject == null) { return; }
        currentlyHeldObject.PlaceDown(pos);
        currentlyHeldObject = null;
    }

    /// <summary>
    /// Throw the currently held object back to its previous location
    /// </summary>
    public void ThrowBack()
    {
        if(currentlyHeldObject == null) { return; }

        currentlyHeldObject.transform.SetParent(null);
        Dropzone.Drop(currentlyHeldObject);
        currentlyHeldObject = null;
    }

}
