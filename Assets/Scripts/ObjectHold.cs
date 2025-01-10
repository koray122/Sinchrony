using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHold : MonoBehaviour
{
    public Transform PlayerTransform;
    public float range = 3f;
    public float Go = 100f;
    public Camera Camera;
    public string pickUpTag = "PickUp"; // Taþýnabilir nesneler için kullanýlacak tag
    private bool isHolding = false; // Nesnenin alýnýp alýnmadýðýný takip eden deðiþken
    private GameObject heldObject; // Taþýnan nesneyi takip eden deðiþken

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            if (isHolding)
            {
                Drop();
            }
            else
            {
                StartPickUp();
            }
        }
    }

    void StartPickUp()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.transform.position, Camera.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            // Tag kontrolü
            if (hit.transform.CompareTag(pickUpTag))
            {
                PickUp(hit.transform.gameObject);
            }
        }
    }

    void PickUp(GameObject obj)
    {
        heldObject = obj;
        heldObject.transform.SetParent(PlayerTransform);
        MeshCollider meshCollider = heldObject.GetComponent<MeshCollider>();
        if (meshCollider != null)
        {
            meshCollider.enabled = false; // MeshCollider'ý kapat
        }
        isHolding = true; // Nesne alýndý
    }

    void Drop()
    {
        if (heldObject != null)
        {
            heldObject.transform.SetParent(null);
            MeshCollider meshCollider = heldObject.GetComponent<MeshCollider>();
            if (meshCollider != null)
            {
                meshCollider.enabled = true; // MeshCollider'ý aç
            }
            heldObject = null;
        }
        isHolding = false; // Nesne býrakýldý
    }
}