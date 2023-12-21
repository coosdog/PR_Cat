using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Temp;

public class RagdollScript : MonoBehaviour
{
    private enum CatState
    {
        Walking,
        Ragdoll
    }
    [SerializeField]
    private Transform _pelvisTransform;

    [SerializeField]
    private Rigidbody[] _ragdollRigidbodies;
    private CatState _currentState = CatState.Walking;
    private Animator _animator;
    //private CharacterController _characterController;
    private float _timeToWakeUp;
    private Transform _hipsBone;

    float tempSignal;

    [SerializeField]
    private PlayerController _playerController;
    [SerializeField]
    private TestPlayer _player;

    void Awake()
    {
        _ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        _animator = GetComponent<Animator>();
        _hipsBone = _animator.GetBoneTransform(HumanBodyBones.Hips);


        WalkingBehaviour();
    }


    void Update()
    {
        if (_currentState == CatState.Ragdoll)
            RagdollBehaviour();
    }

    private void DisableRagdoll()
    {
        _player.GetComponent<Collider>().enabled = true;
        _player.GetComponent<Rigidbody>().useGravity = true;
        _player.GetComponent<Rigidbody>().isKinematic = true;


        _animator.enabled = true;
        _playerController.enabled = true;
        _player.enabled = true;
    }
    public void WalkingBehaviour()
    {
        EnableRagdoll();
        _currentState = CatState.Ragdoll;
        _timeToWakeUp = 3.0f;
    }

    private void EnableRagdoll()
    {
        foreach (var rigidbody in _ragdollRigidbodies)
        {
            rigidbody.isKinematic = false;
        }
        _player.GetComponent<Rigidbody>().useGravity = false;
        _player.GetComponent<Collider>().enabled = false;

        _animator.enabled = false;
        _playerController.enabled = false;
        _player.enabled = false;
    }



    Quaternion startQuaternion;
    bool isCheck;
    private void RagdollBehaviour()
    {
        _timeToWakeUp -= Time.deltaTime;

        if (_timeToWakeUp <= 0)
        {
            if (isCheck == false)
                StartCoroutine(WakeUpCo());
        }
    }

    IEnumerator WakeUpCo()
    {
        isCheck = true;
        tempSignal = 0;
        startQuaternion = _pelvisTransform.localRotation;
        while (tempSignal < 1)
        {
            tempSignal += Time.deltaTime;
            _pelvisTransform.localRotation = Quaternion.Lerp(startQuaternion, Quaternion.Euler(0f, -90f, -90f), tempSignal);
            foreach (var rigidbody in _ragdollRigidbodies)
            {
                rigidbody.velocity = Vector3.zero;
                rigidbody.angularVelocity = Vector3.zero;
            }
            yield return null;
        }
        AlignPositionToHips();
        _currentState = CatState.Walking;
        DisableRagdoll();
        isCheck = false;
    }

    private void AlignPositionToHips()
    {
        Vector3 originalHipsPosition = _hipsBone.position;
        transform.position = _hipsBone.position;

        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo))
        {
            transform.position = new Vector3(transform.position.x, hitInfo.point.y, transform.position.z);
        }

        _hipsBone.position = originalHipsPosition;
    }
}