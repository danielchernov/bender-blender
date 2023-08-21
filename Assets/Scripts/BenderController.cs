using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BenderController : MonoBehaviour
{
    NavMeshAgent _benderAgent;
    Animator _benderAnimator;

    [SerializeField]
    Transform[] _waypoints;

    bool _randomBool;

    void Start()
    {
        _benderAgent = GetComponent<NavMeshAgent>();
        _benderAnimator = GetComponentInChildren<Animator>();

        StartCoroutine(WalkRoutine());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Door")
        {
            StartCoroutine(FuckWithDoor(other));
        }
    }

    IEnumerator FuckWithDoor(Collider collider)
    {
        Animator doorAnim = collider.GetComponentInParent<Animator>();
        Collider doorCollider = collider.GetComponent<Collider>();

        doorCollider.isTrigger = true;

        if (doorAnim.GetInteger("DoorOpened") == 0)
        {
            doorAnim.SetInteger("DoorOpened", -1);
        }

        yield return new WaitForSeconds(1);

        doorCollider.isTrigger = false;
    }

    public IEnumerator WalkRoutine()
    {
        int randomNumber = Random.Range(0, _waypoints.Length);
        print(randomNumber);

        Vector3 currentWaypoint = _waypoints[randomNumber].position;

        if (Vector3.Distance(transform.position, currentWaypoint) < 4)
        {
            StartCoroutine(WalkRoutine());
            yield break;
        }

        _benderAgent.SetDestination(currentWaypoint);

        while (Vector3.Distance(transform.position, currentWaypoint) > 4)
        {
            yield return null;
        }

        if (Random.Range(0, 100) > 50)
        {
            _randomBool = true;
        }
        else
        {
            _randomBool = false;
        }

        _benderAgent.velocity = Vector3.zero;
        _benderAgent.ResetPath();
        _benderAnimator.SetBool("isAlt", _randomBool);
        _benderAnimator.SetTrigger("Idle");
    }
}
