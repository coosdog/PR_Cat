using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IAnimationable
{
    Animator Animator { get; }
}

namespace Temp
{
    public class AnimationComponent : MonoBehaviour, IAnimationable
    {
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
        private Item curItem;

        
        private Collider attackCol;

        private void Start()
        {
            weapon = GetComponent<TestPlayer>().curWeapon;
            attackCol = weapon.GetComponent<Collider>();
        }

        public void AttackStart()
        {            
            attackCol.enabled = true;
        }
        public void AttackEnd()
        {            
            attackCol.enabled = false;
        }
    }
}



