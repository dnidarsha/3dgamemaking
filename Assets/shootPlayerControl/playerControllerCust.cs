using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;

public class playerControllerCust : MonoBehaviour
{
    [SerializeField] private LayerMask hitLayer;
    [SerializeField] private Transform GunPoint;

    [SerializeField] private GameObject bulletObj;

    [SerializeField] private Transform targetIK;

    [SerializeField] private MultiAimConstraint gunIK, hipIK;
    private float targetWeight = 0f;

    private bool fireFlag = false;

    void OnFire(InputValue inputValue) {
        if (inputValue.isPressed)
        {
            fireFlag = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width/2,Screen.height/2));
        Debug.DrawRay(ray.origin, ray.direction * 999f,Color.green);

        if (Physics.Raycast(ray, out RaycastHit rayHit, 999f, hitLayer)) {

            Debug.DrawRay(GunPoint.transform.position, (rayHit.point - ray.origin));

            //Debug.Log(Vector3.Dot(ray.direction, transform.forward));

            targetIK.transform.position = rayHit.point;

            if (Vector3.Dot(ray.direction, transform.forward) > 0)
            {
                targetWeight = 1f;
                if (fireFlag) {
                    Debug.Log("Fire");
                    fireFlag = false;
                    Instantiate(bulletObj, GunPoint.transform.position, Quaternion.LookRotation((rayHit.point - GunPoint.position).normalized, Vector3.forward));
                }
                
            }
            else
            {
                targetWeight = 0f;
            }

            gunIK.weight = Mathf.Lerp(gunIK.weight, targetWeight, Time.deltaTime * 5f);
            hipIK.weight = Mathf.Lerp(gunIK.weight, targetWeight * 0.8f, Time.deltaTime * 5f);
        }
    }
}
