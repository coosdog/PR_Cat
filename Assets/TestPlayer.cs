using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JongWoo;
using UnityEngine.UIElements;

// 플레이어와 몬스터 둘다 맞았을 때 동시처리하도록 인터페이스를 사용할 예정

// 행위(인터페이스) 효과(인터페이스) 나누는 것

namespace Temp
{
    public interface IActionable
    {
        public void Action(IAnimationable animationable);
    }

    public interface IAttackable
    {
        public float Atk { get; }
        public GameObject EffectParticle { get; }
        public void SpawnEffect(); 
        public void Attack(TestPlayer player); // 플레이어에만 효과를 줄 인터페이스 기능

        
    }

    public abstract class AttackStrategy : IActionable
    {
        public abstract void Action(IAnimationable animationable);
    }

    public abstract class MeleeAttackStrategy : AttackStrategy, IAttackable
    {        
        public float Atk => atk;
        private float atk = 10;
        public LayerMask Layer;

        //public void SetLayer(LayerMask layer)
        //{
        //    Layer = layer;
        //}

        public GameObject EffectParticle => throw new System.NotImplementedException();
        [SerializeField] private GameObject effectParticle;

        public abstract void Attack(TestPlayer player);

        public void SpawnEffect()
        {
            throw new System.NotImplementedException();
        }        
    }

    public class DefaultStrategy : MeleeAttackStrategy
    {
        public override void Action(IAnimationable animationable)
        {
            animationable.Animator.SetTrigger("IsAttack"); // 나중에 주먹질로 바꿀것
        }
        public override void Attack(TestPlayer player)
        {            
            // 주먹공격
            // 기절스택 올리기
        }
    }

    public class SwordStrategy : MeleeAttackStrategy
    {
        // 행위에 대한 약속
        // 효과에 대한 약속        
        public override void Action(IAnimationable animationable)
        {

        }
        

        public override void Attack(TestPlayer player)
        {
            // 검의 효과 기능
        }
    }

    public class BatStrategy : MeleeAttackStrategy
    {
        // 행위에 대한 약속
        // 효과에 대한 약속        
        public override void Action(IAnimationable animationable)
        {
            animationable.Animator.SetTrigger("IsAttack");
        }


        public override void Attack(TestPlayer player)
        {
            // 배트의 효과 기능
            Debug.Log("배트 효과");
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
        // public Data D { get; set; }
        public GameObject Obj { get; }
        public void Hit(IAttackable attackable);
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

    public class TestPlayer : MonoBehaviour, IHitable
    {       
        public float stunCnt;
        public Weapon curWeapon;        
        public Transform weaponSpot;
        private AnimationComponent animComponent;

        [SerializeField] bool isGrab;
      
        public bool IsUse
        {
            get => isUse;
            set
            {
                isUse = value;
                if (!isUse) // 무기를 장착하지 않을 때 기본공격으로 세팅
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
            PointHandler.grabAct += UseStrategy;
            PointHandler.dropAct += Drop;
        }

        public void SetDefault()
        {
            curWeapon.Strategy = Item.weaponDic[Item.WEAPON_TYPE.DEFAULT];
        }


        public GameObject Obj
        {
            get => gameObject;
        }
        

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                UseStrategy();
            }
        }

        public void UseStrategy()
        {            
            if(BtnInteraction()) // isUse가 false일때만
                return;            
            curWeapon.Strategy.Action(animComponent);
        }

        public bool BtnInteraction()
        {            
            Collider col = SearchItem();
            if (col == null || IsUse)
                return false;
            Item item = col.GetComponent<Item>();
            if (item.weapon != null) // 이미 소유주가 있는 아이템이라면
                return false;
            SetWeapon(col, item);
            return true;
        }

        public void SetWeapon(Collider col, Item item)
        {
            Debug.Log("다시찾을수있음");
            item.weapon = curWeapon;

            item.weapon.owner = this;

            curWeapon.Strategy = item.strategy;
            item.grabPoint.transform.SetParent(weaponSpot);
            item.grabPoint.transform.position = weaponSpot.position;
            item.grabPoint.transform.rotation = weaponSpot.rotation;
            animComponent.CurItem = item;
            if (item is LongAttackItem)
            {
                GetProjectile((LongAttackItem)item);                
            }
            IsUse = true;
        }

        public Collider SearchItem()
        {
            Collider[] cols = Physics.OverlapSphere(transform.position, 2, 1<<7); // Item Layer만 검출
            if(cols.Length > 0) // 가장가까운놈 처리해줄것
            {                
                return cols[0];
            }
            return null;
        }

        public void Drop()
        {
            if (curWeapon.Strategy == Item.weaponDic[Item.WEAPON_TYPE.DEFAULT])
            {
                Debug.Log("무기없음");
                return;
            }
            //obj.GetComponent<Collider>().enabled = true;
            curWeapon.transform.GetChild(0).transform.SetParent(null); // 수정
            IsUse = false;            
            animComponent.CurItem = null;
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IAttackable attackable))
            {
                //if(attackable.Layer == myLayer)
                //{
                //    Hit(attackable);
                //}
                //attackable.Attack(this);
                // 아이템이거나 나의 무기라면 대미지를 안입게
                // 아이템 레이어거나 나의 레이어라면 대미지를 안입음

                // 총알, 근접무기는 IAttackable, IAttackable이 타겟레이어를 가지고 있고
                // 그놈의 레이어를 가져와 내 레이어와 동일하면 대미지를 주는 형식
                // 포톤을 이용할때
                // 자기자신이 Player라는 레이어고 나머지가 Enemy로 설정을 했을 때
                // 그 Enemy입장에서 자신이 Player고 나머지가 Enemy레이어로 처리가 될텐데
                // 어떻게 해야하나?
                // if(attackable is Weapon)
                if (attackable is Weapon)
                {
                    if (((Weapon)attackable).owner == this)
                        return;
                }
                Hit(attackable);
            }            
        }
        

        public void GetProjectile(LongAttackItem item)
        {
            GunStrategy gs = (GunStrategy)curWeapon.Strategy;
            gs.bullet = item.projectileObj;
            gs.shotPoint = item.shotPos;
        }
        
        public void Hit(IAttackable attackable)
        {
            // stunCnt -= attackable.Atk;         
            attackable.Attack(this);            
            // attackable.SpawnEffect();
        }        
    }

}









