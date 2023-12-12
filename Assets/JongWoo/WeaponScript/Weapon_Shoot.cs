using JongWoo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JongWoo
{
    public class Weapon_Shoot : Weapon
    {
        //총구: 총알이 발사되는 위치를 인스펙터에서 설정해줘요!
        [SerializeField] protected GameObject shotPoint;
        //부렛또노 사테쿄리 데스
        public float range;

        public GameObject bullet;



        public override void SetStatus() { }

        public override void Use()
        {
            Debug.Log("뿅뿅 쏘는 무기 시퀀스");
        }

    }

}
