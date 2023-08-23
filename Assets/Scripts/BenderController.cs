using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BenderController : MonoBehaviour
{
    NavMeshAgent _benderAgent;
    Animator _benderAnimator;

    [SerializeField]
    AudioSource _benderStepsAudio;

    [SerializeField]
    AudioSource _benderVoiceAudio;

    [SerializeField]
    AudioClip[] _benderVoiceSFX;

    [SerializeField]
    Transform[] _waypoints;

    [SerializeField]
    Transform _player;

    [SerializeField]
    BenderCheckForPlayer _eyesCollider;

    bool _randomBool = false;
    bool _isChasing = false;

    public Coroutine WalkingRoutine;

    float _lostSightCounter = 0;

    bool _raycastHit = false;
    RaycastHit rayHit;

    [SerializeField]
    LayerMask _layerMask;

    void Start()
    {
        _benderAgent = GetComponent<NavMeshAgent>();
        _benderAnimator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (_eyesCollider.IsWatching() && !_isChasing)
        {
            Ray rayOrigin = new Ray(transform.position, (_player.position - transform.position));

            //Physics.Raycast(rayOrigin, out rayHit);
            Physics.Raycast(rayOrigin, out rayHit, Mathf.Infinity, _layerMask);

            // Debug.DrawRay(
            //     transform.position,
            //     (_player.position - transform.position),
            //     Color.green,
            //     5f,
            //     true
            // );

            //print(rayHit.transform);

            _raycastHit = _player.transform == rayHit.transform;

            if (_raycastHit)
            {
                //StopCoroutine(WalkingRoutine);

                if (Random.Range(0, 100) > 50)
                {
                    _randomBool = true;
                }
                else
                {
                    _randomBool = false;
                }

                _benderAnimator.SetBool("isAlt", _randomBool);
                _benderAnimator.SetBool("chasingPlayer", true);

                StartCoroutine(ChasePlayer());

                _benderVoiceAudio.PlayOneShot(
                    _benderVoiceSFX[Random.Range(0, _benderVoiceSFX.Length)],
                    1f
                );

                _isChasing = true;
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Door")
        {
            StartCoroutine(InteractWithDoor(other));
        }
    }

    IEnumerator InteractWithDoor(Collision collision)
    {
        Animator doorAnim = collision.gameObject.GetComponentInParent<Animator>();
        Collider doorCollider = collision.gameObject.GetComponent<Collider>();

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
        if (!_isChasing)
        {
            int randomNumber = Random.Range(0, _waypoints.Length);

            Vector3 currentWaypoint = _waypoints[randomNumber].position;

            if (Vector3.Distance(transform.position, currentWaypoint) < 4)
            {
                WalkingRoutine = StartCoroutine(WalkRoutine());
                yield break;
            }

            _benderAgent.SetDestination(currentWaypoint);

            _benderStepsAudio.enabled = true;
            _benderAgent.speed = 4f;
            _benderStepsAudio.pitch = 1f;

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
            _benderStepsAudio.enabled = false;

            _benderAgent.ResetPath();
            _benderAnimator.SetBool("isAlt", _randomBool);
            _benderAnimator.SetTrigger("Idle");
        }
    }

    IEnumerator ChasePlayer()
    {
        if (!_eyesCollider.IsWatching())
        {
            _lostSightCounter++;
        }
        else
        {
            _lostSightCounter = 0;
        }

        if (_lostSightCounter >= 40)
        {
            _isChasing = false;

            _benderAgent.velocity = Vector3.zero;
            _benderStepsAudio.enabled = false;

            _benderAgent.ResetPath();
            _benderAnimator.SetBool("chasingPlayer", false);
            yield break;
        }

        _benderAgent.SetDestination(_player.position);
        _benderStepsAudio.enabled = true;
        _benderAgent.speed = 9f;
        _benderStepsAudio.pitch = 3f;

        yield return new WaitForSeconds(0.1f);

        StartCoroutine(ChasePlayer());
    }
}
