using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;

    [SerializeField] float chaseRange = 5.0f;

    [SerializeField] float turnSpeed = 5.0f;

    float distanceToTarget = Mathf.Infinity;

    private bool isProvoked = false;

    NavMeshAgent nma;

    EnemyHealth health;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nma = GetComponent<NavMeshAgent>();

        target = FindAnyObjectByType<PlayerHealth>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(health.isDead)
        {
            enabled = false;

            nma.enabled = false;

            return;
        }

        distanceToTarget = Vector3.Distance(target.position, transform.position);

        if(isProvoked)
        {
            EngageTarget(); 
        }
        else if(distanceToTarget <= chaseRange)
        {
            isProvoked = true;        
        }     
    }

    private void EngageTarget()
    {
        FaceTarget();
        if(distanceToTarget >= nma.stoppingDistance)
        {
            ChaseTarget(); 
        }

        if(distanceToTarget <= nma.stoppingDistance)
        {
            AttackTarget(); 
        }
    }

    private void ChaseTarget()
    {
        GetComponentInChildren<Animator>().SetBool("attack", false);

        GetComponentInChildren<Animator>().SetTrigger("run");

        nma.SetDestination(target.position);
    }

    private void AttackTarget()
    {
        GetComponentInChildren<Animator>().SetBool("attack", true);

        //temp for now to show that the enemy is attacking
        //print(name + " is attacking " + target.name);
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;

        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    private void OnDamageTaken()
    {
        isProvoked = true;
    }

    void OnDrawGizmosSelected()
    {
        //display the chase radius when selected
        Gizmos.color = Color.red; //just choose a color, could be anything
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }


}
