using UnityEngine;

public class PickUp : MonoBehaviour
{
    bool isHolding = false;

    [SerializeField] float throwForce = 600f;
    [SerializeField] float maxDistance = 3f;
    [SerializeField] Camera mainCamera; // Ana kamera referansı
    [SerializeField] RenderTexture renderTexture; // Render Texture referansı

    float distance;
    TempParent tempParent;
    Rigidbody rb;
    Vector3 objectPos;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tempParent = TempParent.Instance;

        if (mainCamera == null || renderTexture == null)
        {
            Debug.LogError("Ana kamera veya Render Texture atanmadı!");
        }
    }

    void Update()
    {
        if (isHolding)
            Hold();

        if (Input.GetMouseButtonDown(0))
        {
            TryPickUp();
        }

        if (Input.GetMouseButtonUp(0))
        {
            Drop();
        }
    }

    private void TryPickUp()
    {
        if (mainCamera == null || renderTexture == null)
        {
            Debug.LogError("Kamera veya Render Texture eksik!");
            return;
        }

        // Render Texture'un ekran ortasını hesapla
        Vector3 renderTextureCenter = new Vector3(renderTexture.width / 2, renderTexture.height / 2, 0);

        // Ray oluştur
        Ray ray = mainCamera.ScreenPointToRay(renderTextureCenter);
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
        {
            if (hit.collider.gameObject == gameObject)
            {
                distance = Vector3.Distance(transform.position, tempParent.transform.position);
                if (distance <= maxDistance)
                {
                    isHolding = true;
                    rb.useGravity = false;
                    rb.detectCollisions = true;

                    transform.SetParent(tempParent.transform);
                }
            }
        }
    }

    private void Hold()
    {
        distance = Vector3.Distance(transform.position, tempParent.transform.position);

        if (distance >= maxDistance)
        {
            Drop();
        }

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Sağ tıklama ile fırlatma
        if (Input.GetMouseButtonDown(1))
        {
            rb.AddForce(tempParent.transform.forward * throwForce);
            Drop();
        }
    }

    private void Drop()
    {
        if (isHolding)
        {
            isHolding = false;
            objectPos = transform.position;
            transform.SetParent(null);
            transform.position = objectPos;
            rb.useGravity = true;
        }
    }
}
