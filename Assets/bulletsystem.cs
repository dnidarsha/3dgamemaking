using UnityEngine;

using UnityEngine;
using System.Collections;

public class bulletManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float speed = 10f;
    private Rigidbody rb;
    
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(coroutinedestoryBullet());
    }
    private void Update()
    {
        rb.linearVelocity = transform.forward * speed ;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy Hiy");
            destoryBullet();
        }
    }

    void destoryBullet()
    {
        Destroy(gameObject);
    }

    private IEnumerator coroutinedestoryBullet()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}