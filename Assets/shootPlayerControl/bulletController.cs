using UnityEngine;
using System.Collections;

public class bulletController : MonoBehaviour
{
    [SerializeField] float speed = 15f;
    Rigidbody rb;
    [SerializeField] private ParticleSystem blast;
    [SerializeField] private MeshRenderer meshrender;
    [SerializeField] private SphereCollider collider;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        meshrender = GetComponent<MeshRenderer>();
        collider = GetComponent<SphereCollider>();  
        StartCoroutine(timerDelete());
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(effectAndDestroy());
    }

    IEnumerator timerDelete() {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    IEnumerator effectAndDestroy() {
        blast.Play();
        meshrender.enabled = false;
        collider.enabled = false;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);

    }

}
