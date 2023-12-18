using System.Collections;
using System.Collections.Generic;
using Temp;
using UnityEngine;

namespace JongWoo
{
    public class Bullet_Mechanism : MonoBehaviour
    {
        public float speed = 15f;
        //파티클이펙트 발생시 거리조절용 변수
        public float distance = 0f;

        //총알이 콜리전충돌 했을때!: 물체에 부딪히면서 나오는 '충돌효과'파티클 게임 오브젝트 [유니티 인스펙터 창에서 넣어줘야 함!]
        public GameObject hitParticle;

        //총알이 발사될 때!: 총구에서 나오는 '섬광효과' 파티클 게임오브젝트 [유니티 인스펙터 창에서 넣어줘야 함!]
        public GameObject bulletPartcle_Falsh;

        private Rigidbody rb;

        Interaction inter;

        //해당 총알이 건에서 Instantiate 된 순간!
        void Start()
        {
            inter = GetComponent<Interaction>();
            rb = GetComponent<Rigidbody>();
            //생성위치에서 즉시 섬광 파티클 소환해줍니다!
            if (bulletPartcle_Falsh != null)
            {
                GameObject flashInstance = Instantiate(bulletPartcle_Falsh, transform.position, Quaternion.identity);
                flashInstance.transform.forward = gameObject.transform.forward;

                //BulletPartivle_Flash 오브젝트에 있는파티클 시스템의 컴포넌트를 가져와서, 재생시간이 끝나면 파괴되도록 했슴돠
                ParticleSystem flashTime = flashInstance.GetComponent<ParticleSystem>();
                Destroy(flashInstance, flashTime.main.duration);
            }

            //벽에 안 부딪혀도 3초 뒤 삭제
            Destroy(gameObject, 3);
        }

        void FixedUpdate()
        {
            //발사되는 순간 앞으로 계속 나감
            rb.velocity = transform.forward * speed;
        }


        void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.TryGetComponent(out TestPlayer player))
            {
                //기절게이지를 올리거나 등등등
            }
            #region 내용정리1번
            //Debug.Log(collision.gameObject.name);

            //collision.contact[] & ContactPoint 관련 유니티 돜유먼ㅌ
            //https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Collision-contacts.html
            //https://docs.unity3d.com/kr/2021.3/ScriptReference/ContactPoint.html
            //collision.contact[0]?:   '콜리전' 충돌이 발생하는 상황에서 충돌지점의 정보를 구조체 '배열' 형식으로 저장할 수 있습니다!
            //ContactPoint 변수 = collision.contact[0]에 저장된 변수의 위치(point), 법선벡터(normal)등을 저장하게 할 수 있습니다!

            //해당 방법을 이용해 충돌지점의 위치와 방향을 저장 => 파티클을 Instantiate 해서 피격 이펙트를 만들어냅니다!

            //1. 최초 충돌 지점의 정보 저장
            #endregion
            ContactPoint contact = collision.GetContact(0);   //collision.GetContact() = collision.contacts[0]랑 같은 의미인데 주석대로 하면 메모리 낭비가 발생한다고 함!;
            #region 내용정리2번
            //2. 총알의 Y축을 충돌체의 법선까지 회전시킨다! ==> 이러면 피격 파티클이 벽면에 겹치는 부분이 줄어들어서 이쁘게 나온대요,, 이해는 아직 덜 됨 
            //https://dallcom-forever2620.tistory.com/13
            #endregion
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);

            #region 내용정리3번
            // Debug.Log(contact.point);
            // Debug.Log(contact.normal);

            //벡터의 좌표 = 충돌위치 + 법선의 방향*거리
            //거리가 길어질수록 닿은 위치로부터 이펙트가 터지는 거리가 멀어집니다!
            #endregion
            Vector3 pos = contact.point + contact.normal * distance;

            if (hitParticle != null)
            {
                GameObject hitInstance = Instantiate(hitParticle, pos, rot);
                ParticleSystem hitTime = hitInstance.GetComponent<ParticleSystem>();
                Destroy(hitInstance, hitTime.main.duration);
            }
            inter.Interact(collision);
            Destroy(gameObject);
        }

       

        public void Attack(IHitable hitable)
        {
            if(hitable == null) return;
            
            if(hitable!=null)
            {
              //hitable
            }
        }
    }


}
