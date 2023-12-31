using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IAnimationable
{
    bool canAttack { get; }
    Animator Animator { get; }
}

namespace Temp
{
    public class AnimationComponent : MonoBehaviour, IAnimationable
    {
        bool isCheck;

        Weapon weapon;
        public Animator Animator => GetComponent<Animator>();
        public Item CurItem
        {
            get => curItem;
            set
            {
                if (value == null)
                    curItem.weapon = null;
                curItem = value;
                if (curItem == null)
                {
                    attackCol.enabled = true;
                    attackCol = weapon.GetComponent<Collider>();                    
                }
                else
                {                    
                    attackCol = curItem.GetComponent<Collider>();
                    attackCol.enabled = false;
                }
            }
        }

        public bool canAttack { get => isCheck; }

        private Item curItem;        
        private Collider attackCol;        

        private void Start()
        {
            weapon = GetComponent<TestPlayer>().curWeapon;
            attackCol = weapon.GetComponent<Collider>();
            isCheck = true;
        }

        public void CountCool()
        {
            isCheck = false;
        }


        public void AttackStart()
        {            
            attackCol.enabled = true;
        }
        public void AttackEnd()
        {            
            attackCol.enabled = false;
        }

        public void ResetCool()
        {
            isCheck = true;
        }

    }
}



