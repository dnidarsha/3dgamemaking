using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyControllerScript : MonoBehaviour
{
    private NavMeshAgent enemyAI;
    private Animator enemyAnimator; // Reference to the Animator

    [SerializeField] private Transform PlayerTrans;
    [SerializeField] private float followDistance = 20f;
    [SerializeField] private float attackDistance = 3f;
    [SerializeField] private int numberOfBulletToDie = 3;
    int bulletCount = 0;

    private void Start()
    {
        enemyAI = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<Animator>(); 
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(PlayerTrans.position, transform.position);

        if (distanceToPlayer <= attackDistance)
        {
            Invoke("playerDie",2f);
            enemyAI.isStopped = true; 
            enemyAnimator.SetInteger("EnemyAni", 2); 
            Debug.Log("Attacking the player!");
        }
        else if (distanceToPlayer <= followDistance)
        {
            enemyAI.isStopped = false; 
            enemyAI.SetDestination(PlayerTrans.position);
            enemyAnimator.SetInteger("EnemyAni", 1); 
        }
        else
        {
            enemyAI.isStopped = true;
            enemyAnimator.SetInteger("EnemyAni", 0); 
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet")) {
            if (bulletCount < (numberOfBulletToDie - 1))
            {
                bulletCount++;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (PlayerTrans == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, followDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }

    void playerDie()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}