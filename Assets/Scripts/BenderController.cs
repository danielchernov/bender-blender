using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BenderController : MonoBehaviour
{
    NavMeshAgent _benderAgent;
    Animator _benderAnimator;

    [SerializeField]
    AudioSource _benderStepsAudio;

    [SerializeField]
    AudioSource _benderVoiceAudio;

    [SerializeField]
    AudioSource _whiteNoiseAudio;

    [SerializeField]
    AudioClip[] _benderVoiceSFX;

    [SerializeField]
    Transform[] _waypoints;

    [SerializeField]
    Transform _player;

    [SerializeField]
    BenderCheckForPlayer _eyesCollider;

    [SerializeField]
    float _walkSpeed = 4;

    [SerializeField]
    float _chaseSpeed = 8;

    [SerializeField]
    float _chaseTime = 50;

    [SerializeField]
    float _distanceToCloseIn = 20;

    [SerializeField]
    Image _benderClosingIn;

    [SerializeField]
    Light[] _benderEyesLights;

    int _randomNumber = 0;
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
        Ray rayOrigin = new Ray(transform.position, (_player.position - transform.position));

        Physics.Raycast(rayOrigin, out rayHit, Mathf.Infinity, _layerMask);

        _raycastHit = _player.transform == rayHit.transform;

        float distanceToPlayer = Vector3.Distance(transform.position, _player.position);

        if (_raycastHit && distanceToPlayer < _distanceToCloseIn)
        {
            if (!_benderClosingIn.transform.parent.gameObject.activeSelf)
            {
                _benderClosingIn.transform.parent.gameObject.SetActive(true);
            }

            if (!_whiteNoiseAudio.isPlaying)
            {
                _whiteNoiseAudio.Play();
            }

            float newAlpha = Mathf.InverseLerp(_distanceToCloseIn, 5, distanceToPlayer);

            _whiteNoiseAudio.volume = (Mathf.Pow(newAlpha, 2)) * 0.2f;

            _benderClosingIn.color = new Color(
                1f - (newAlpha / 2f),
                1f - (newAlpha / 2f),
                1f - (newAlpha / 2f),
                Mathf.Pow(newAlpha, 4)
            );

            if (distanceToPlayer <= 5)
            {
                StartCoroutine(GameManager.Instance.GameOver());
            }
        }
        else if (_benderClosingIn.transform.parent.gameObject.activeSelf)
        {
            _benderClosingIn.transform.parent.gameObject.SetActive(false);
            _whiteNoiseAudio.volume = 0;
        }

        if (_eyesCollider.IsWatching() && !_isChasing)
        {
            if (_raycastHit)
            {
                //StopCoroutine(WalkingRoutine);

                _randomNumber = Random.Range(0, 4);

                _benderAnimator.SetInteger("AltNumber", _randomNumber);
                _benderAnimator.SetBool("chasingPlayer", true);

                StartCoroutine(ChasePlayer());

                _benderVoiceAudio.PlayOneShot(
                    _benderVoiceSFX[Random.Range(0, _benderVoiceSFX.Length)],
                    1.2f
                );

                _benderEyesLights[0].color = Color.red;
                _benderEyesLights[1].color = Color.red;

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
            _benderAgent.speed = _walkSpeed;
            _benderStepsAudio.pitch = 1f;

            _benderEyesLights[0].color = Color.white;
            _benderEyesLights[1].color = Color.white;

            while (Vector3.Distance(transform.position, currentWaypoint) > 4)
            {
                yield return null;
            }

            _benderAgent.velocity = Vector3.zero;
            _benderStepsAudio.enabled = false;

            _randomNumber = Random.Range(0, 4);

            _benderAgent.ResetPath();
            _benderAnimator.SetInteger("AltNumber", _randomNumber);
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

        if (_lostSightCounter >= _chaseTime)
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
        _benderAgent.speed = _chaseSpeed;
        _benderStepsAudio.pitch = 3f;

        yield return new WaitForSeconds(0.1f);

        StartCoroutine(ChasePlayer());
    }
}
