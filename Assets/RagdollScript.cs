using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Temp;
using Photon.Pun;

public class RagdollScript : MonoBehaviourPun
{
    public enum CatState
    {
        Walking,
        Ragdoll
    }
    [SerializeField]
    private Transform _pelvisTransform;

    [SerializeField]
    private Rigidbody[] _ragdollRigidbodies;

    public Collider[] _colliders;
    public CatState _CurrentState
    {
        get => _currentState;
        set
        {
            _currentState = value;
            if (_currentState == CatState.Ragdoll)
            {
                photonView.RPC("RagdollBehaviour", RpcTarget.AllBuffered);
            }
        }
    }

    private CatState _currentState = CatState.Walking;



    private Animator _animator;
    //private CharacterController _characterController;
    private float _timeToWakeUp;
    private Transform _hipsBone;
    bool isHit;
    float tempSignal;

    [SerializeField]
    private PlayerController _playerController;
    [SerializeField]
    private TestPlayer _player;
    const float wakeOffset = 0.1f;


    Vector3 origin;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _hipsBone = _animator.GetBoneTransform(HumanBodyBones.Hips);

        // photonView.RPC("WalkingBehaviour",RpcTarget.AllBuffered);
    }


    void Update()
    {
        //if (_currentState == CatState.Ragdoll)
        //    // RagdollBehaviour();
        //    photonView.RPC("RagdollBehaviour", RpcTarget.AllBuffered);

        //RagdollBehaviour();

        
    }

    private void DisableRagdoll()
    {
        _player.GetComponent<Collider>().enabled = true;
        _player.GetComponent<Rigidbody>().useGravity = true;
        _player.GetComponent<Rigidbody>().isKinematic = false;
        _playerController.freeLook.transform.position = origin;


        photonView.RPC("EnabledAnimator", RpcTarget.AllBuffered);
        
        _player.enabled = true;
        isHit = false;
        // 임시방편
        _player.transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y + wakeOffset, _player.transform.position.z);
        // 삐용비용 애니 틀고 애니시간만큼진행하고 나선 아이들로 돌아감
        // 프렐이어 컨트롤러
        // 프레이어 컨트로럴

        StartCoroutine(StunCo());

     
    }
    IEnumerator StunCo()
    {
        _animator.SetTrigger("IsStun");
        yield return new WaitForSeconds(3f);
        _playerController.enabled = true;
    }

    [PunRPC]
    public void EnabledAnimator()
    {
        _animator.enabled = true;
    }
    
    public void RpcWalking()
    {
        // 내가 이미 래그돌된 상태면 또 안먹게
        if (isHit)
            return;
        StartCoroutine(WaitCo());        
    }

    IEnumerator WaitCo()
    {
        isHit = true;
        Rigidbody rigid = GetComponent<Rigidbody>();
        Debug.Log(rigid.velocity);
        yield return new WaitForSeconds(0.5f);
        while (rigid.velocity != Vector3.zero)
        {
            yield return null;
        }
        photonView.RPC("WalkingBehaviour", RpcTarget.AllBuffered);
    }


    [PunRPC]
    public void WalkingBehaviour()
    {        
        Debug.Log("랙돌시작");
        EnableRagdoll();
        // 3초뒤
        _CurrentState = CatState.Ragdoll;
        _timeToWakeUp = 3.0f;
    }

    private void EnableRagdoll()
    {
        origin = _playerController.freeLook.transform.position;
        foreach (Rigidbody rigidbody in _ragdollRigidbodies) 
        {
            rigidbody.isKinematic = false;
            rigidbody.useGravity = true;
            rigidbody.GetComponent<Collider>().isTrigger = false;
        }
        //_player.GetComponent<Rigidbody>().useGravity = false;
        _player.GetComponent<Rigidbody>().isKinematic = true;
        _player.GetComponent<Collider>().enabled = false;
        _playerController.freeLook.transform.position = gameObject.transform.position;

        // testCol.isTrigger = false;
        _playerController.enabled = false;
        _player.enabled = false;
    }



    Quaternion startQuaternion;    
    
    [PunRPC]
    private void RagdollBehaviour()
    {
        StartCoroutine(WakeUpCo());
    }

    IEnumerator WakeUpCo()
    {
        yield return new WaitForSeconds(3f);                
        _animator.enabled = true;
        tempSignal = 0;
        startQuaternion = _pelvisTransform.localRotation;
        //while (tempSignal < 0.5f)
        //{            
        //    tempSignal += Time.deltaTime;
        //    _pelvisTransform.localRotation = Quaternion.Lerp(startQuaternion, Quaternion.Euler(0f, -90f, -90f), tempSignal);
        //    //foreach (var rigidbody in _ragdollRigidbodies)
        //    //{
        //    //    rigidbody.velocity = Vector3.zero;
        //    //    rigidbody.angularVelocity = Vector3.zero;               
        //    //}
        //    yield return null;
        //}        
        _hipsBone.position = transform.position;
        AlignPositionToHips();
        _currentState = CatState.Walking;
        DisableRagdoll();        
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
        foreach (Rigidbody rigidbody in _ragdollRigidbodies)
        {
            rigidbody.isKinematic = true;
            rigidbody.useGravity = false;
            rigidbody.GetComponent<Collider>().isTrigger = true;
        }
    }
}