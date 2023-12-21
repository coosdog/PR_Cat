using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JongWoo;
using UnityEngine.UIElements;
using Photon.Pun;
using Photon.Pun.Demo.Cockpit;

namespace Temp
{
    public interface IActionable
    {
        public void Action(IAnimationable animationable);
    }

    public interface IAttackable
    {
        public int Atk { get; }
        public GameObject EffectParticle { get; }
        public void SpawnEffect(); 
        public void Attack(TestPlayer player, Vector3 attackerPos);
    }
    
    public abstract class AttackStrategy : IActionable
    {
        public abstract void Action(IAnimationable animationable);

    }

    public abstract class MeleeAttackStrategy : AttackStrategy, IAttackable
    {        
        public int Atk => atk;
        private int atk = 10;
        public LayerMask Layer;

        //public void SetLayer(LayerMask layer)
        //{
        //    Layer = layer;
        //}

        public GameObject EffectParticle => throw new System.NotImplementedException();
        [SerializeField] private GameObject effectParticle;

        public abstract void Attack(TestPlayer player, Vector3 attackerPos);

        public void SpawnEffect()
        {
            throw new System.NotImplementedException();
        }
    }

    public class DefaultStrategy : MeleeAttackStrategy
    {
        public override void Action(IAnimationable animationable)
        {
            animationable.Animator.SetTrigger("IsAttack"); // ���߿� �ָ����� �ٲܰ�            
        }
        public override void Attack(TestPlayer player, Vector3 attackerPos)
        {
            Vector3 dir = (player.transform.position - attackerPos).normalized;
            Debug.Log(dir);
            player.GetComponent<Rigidbody>().AddForce(dir * 100f, ForceMode.Impulse);
            Debug.Log("공격처리됨");
        }


    }

    public class SwordStrategy : MeleeAttackStrategy
    {
        // ������ ���� ���
        // ȿ���� ���� ���        
        public override void Action(IAnimationable animationable)
        {

        }
        

        public override void Attack(TestPlayer player, Vector3 attackerPos)
        {
            Vector3 dir = (player.transform.position - attackerPos).normalized;
            player.GetComponent<Rigidbody>().AddForce(dir * 100f, ForceMode.Impulse);
        }
    }

    public class BatStrategy : MeleeAttackStrategy
    {
        // ������ ���� ���
        // ȿ���� ���� ���        
        public override void Action(IAnimationable animationable)
        {
            animationable.Animator.SetTrigger("IsAttack");
        }
        public override void Attack(TestPlayer player, Vector3 attackerPos)
        {
            player.StunCnt += 1;
            Vector3 dir = (player.transform.position - attackerPos).normalized;
            Debug.Log(dir);
            player.GetComponent<Rigidbody>().AddForce(dir * 100f, ForceMode.Impulse);
        }
    }

    public abstract class LongAttackStrategy : AttackStrategy { }

    public class GunStrategy : LongAttackStrategy
    {
        public GameObject bullet;
        public Transform shotPoint;

        //public void SetLayer(LayerMask layer)
        //{
        //    bullet.GetComponent<IAttackable>().Layer = layer;
        //}

        public override void Action(IAnimationable animationable)
        {
            GameObject.Instantiate(bullet, shotPoint.transform.position, shotPoint.transform.rotation);
        }
    }
    public interface IHitable
    {        
        public GameObject Obj { get; }
        public void Hit(IAttackable attackable, Vector3 attackerPos);
    }
    /*
    public class Weapon
    {
        public IAttackable attackStrategy;
        public AttackStrategy strategy;
    }
    */
    /*
     public class Gun: Weapon
     {
         public GunStrategy gunStrategy;

         public void GunStrategyAction()
         {
             gunStrategy.Action();
         }
     }
    */

    public class TestPlayer : MonoBehaviourPun, IHitable, IPunObservable
    {       
        
        public Weapon curWeapon;
        public Transform weaponSpot;
        private AnimationComponent animComponent;
        private Item curItem;
        [SerializeField] bool isGrab;

        [SerializeField] private int stunCnt;
        public int StunCnt
        {
            get => stunCnt;
            set
            {
                stunCnt = value;

            }
        }
        public bool IsUse
        {
            get => isUse;
            set
            {
                isUse = value;
                if (!isUse) // ���⸦ �������� ���� �� �⺻�������� ����
                {
                    SetDefault();                    
                }                
            }
        }
        private bool isUse;

        private void Start()
        {
            SetDefault();
            animComponent = GetComponent<AnimationComponent>();
            if (photonView.IsMine)
            {
                curItem = new Item();
                
                PointHandler.grabAct += () => { photonView.RPC("UseStrategy", RpcTarget.AllBuffered); };
                PointHandler.dropAct += () => { photonView.RPC("Drop", RpcTarget.AllBuffered); };
            }
        }

        private void Update()
        {

        }
        public void SetDefault()
        {
            curWeapon.Strategy = Item.weaponDic[Item.WEAPON_TYPE.DEFAULT];
        }

        public GameObject Obj
        {
            get => gameObject;
        }

        [PunRPC]
        public void UseStrategy()
        {
            // IsMine 테스트
            //if (!photonView.IsMine)
            //    return;

            if (BtnInteraction())
                return;
            curWeapon.Strategy.Action(animComponent);
        }

        public bool BtnInteraction()
        {
            Debug.Log(photonView.IsMine);
            Collider col = SearchItem();
            if (col == null || IsUse)
                return false;
            Item item = col.GetComponent<Item>();            
            if (item.weapon != null) 
                return false;

            curItem = item;
            Debug.Log(item.name);
            photonView.RPC("SetWeapon", RpcTarget.AllBuffered);
            
            // SetWeapon(item);
            return true;
        }
                
        [PunRPC]
        public void SetWeapon()
        {
            Debug.Log(curItem.name);
            Debug.Log("포톤뷰 ismine : " + photonView.IsMine);
            //GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.red;
            curItem.grabPoint.transform.parent = weaponSpot;
            curItem.grabPoint.localPosition = Vector3.zero;
            curItem.grabPoint.localRotation = Quaternion.identity;
            //curItem.grabPoint.transform.position = weaponSpot.position;
            //curItem.grabPoint.transform.rotation = weaponSpot.rotation;


            curItem.weapon = curWeapon;
            curItem.weapon.owner = this;
            curWeapon.Strategy = curItem.strategy;
            // other.transform.parent = weaponSpot;

            // curItem.grabPoint.transform.SetParent(weaponSpot);

            animComponent.CurItem = curItem;
            IsUse = true;
            if (curItem is LongAttackItem)
            {
                GetProjectile((LongAttackItem)curItem);
            }
        }
      
        public Collider SearchItem()
        {
            Collider[] cols = Physics.OverlapSphere(transform.position, 2, 1<<7);
            if(cols.Length > 0)
            {                
                return cols[0];
            }
            return null;
        }

        [PunRPC]
        public void Drop()
        {
            if (curWeapon.Strategy is DefaultStrategy)
                return;            
            curWeapon.transform.GetChild(0).transform.SetParent(null);
            IsUse = false;            
            animComponent.CurItem = null;
        }

        private void OnTriggerEnter(Collider other)
        {
            stunCnt -= 1;
            
            //if (other.TryGetComponent(out IAttackable attackable))
            //{                
            //    if (attackable is Weapon)
            //    {
            //        if (((Weapon)attackable).owner == this)
            //            return;
            //    }
            //    Hit(attackable, other.transform.position);
            //}            
        }
        
        public void GetProjectile(LongAttackItem item)
        {
            GunStrategy gs = (GunStrategy)curWeapon.Strategy;
            gs.bullet = item.projectileObj;
            gs.shotPoint = item.shotPos;
        }
        
        public void Hit(IAttackable attackable, Vector3 attackerPos)
        {
            // stunCnt -= attackable.Atk;
            attackable.Attack(this, attackerPos);

            // attackable.SpawnEffect();
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(StunCnt);
            }
            else
            {
                StunCnt = (int)stream.ReceiveNext();
            }
        }
    }
}









