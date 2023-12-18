using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JongWoo;
using UnityEngine.UIElements;

// �÷��̾�� ���� �Ѵ� �¾��� �� ����ó���ϵ��� �������̽��� ����� ����

// ����(�������̽�) ȿ��(�������̽�) ������ ��

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
        public void Attack(TestPlayer player); // �÷��̾�� ȿ���� �� �������̽� ���

        
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
            animationable.Animator.SetTrigger("IsAttack"); // ���߿� �ָ����� �ٲܰ�
        }
        public override void Attack(TestPlayer player)
        {            
            // �ָ԰���
            // �������� �ø���
        }
    }

    public class SwordStrategy : MeleeAttackStrategy
    {
        // ������ ���� ���
        // ȿ���� ���� ���        
        public override void Action(IAnimationable animationable)
        {

        }
        

        public override void Attack(TestPlayer player)
        {
            // ���� ȿ�� ���
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


        public override void Attack(TestPlayer player)
        {
            // ��Ʈ�� ȿ�� ���
            Debug.Log("��Ʈ ȿ��");
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
            if(BtnInteraction()) // isUse�� false�϶���
                return;            
            curWeapon.Strategy.Action(animComponent);
        }

        public bool BtnInteraction()
        {            
            Collider col = SearchItem();
            if (col == null || IsUse)
                return false;
            Item item = col.GetComponent<Item>();
            if (item.weapon != null) // �̹� �����ְ� �ִ� �������̶��
                return false;
            SetWeapon(col, item);
            return true;
        }

        public void SetWeapon(Collider col, Item item)
        {
            Debug.Log("�ٽ�ã��������");
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
            Collider[] cols = Physics.OverlapSphere(transform.position, 2, 1<<7); // Item Layer�� ����
            if(cols.Length > 0) // ���尡���� ó�����ٰ�
            {                
                return cols[0];
            }
            return null;
        }

        public void Drop()
        {
            if (curWeapon.Strategy == Item.weaponDic[Item.WEAPON_TYPE.DEFAULT])
            {
                Debug.Log("�������");
                return;
            }
            //obj.GetComponent<Collider>().enabled = true;
            curWeapon.transform.GetChild(0).transform.SetParent(null); // ����
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
                // �������̰ų� ���� ������ ������� ���԰�
                // ������ ���̾�ų� ���� ���̾��� ������� ������

                // �Ѿ�, ��������� IAttackable, IAttackable�� Ÿ�ٷ��̾ ������ �ְ�
                // �׳��� ���̾ ������ �� ���̾�� �����ϸ� ������� �ִ� ����
                // ������ �̿��Ҷ�
                // �ڱ��ڽ��� Player��� ���̾�� �������� Enemy�� ������ ���� ��
                // �� Enemy���忡�� �ڽ��� Player�� �������� Enemy���̾�� ó���� ���ٵ�
                // ��� �ؾ��ϳ�?
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









