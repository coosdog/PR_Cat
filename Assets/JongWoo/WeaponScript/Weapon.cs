using System.Collections;
using System.Collections.Generic;
using Temp;
using UnityEngine;

namespace Temp
{
    public class Weapon : MonoBehaviour, IAttackable
    {
        [SerializeField] protected float stunGage;
        [SerializeField] protected float atkSpeed;

        public TestPlayer owner;
        MeleeAttackStrategy mst;

        public AttackStrategy Strategy
        {
            get => strategy;
            set
            {                
                strategy = value;
                if (strategy is MeleeAttackStrategy)
                {                    
                    mst = (MeleeAttackStrategy)strategy;
                }
                else
                    mst = null;
            }
        }
        private AttackStrategy strategy;
        

        public int Atk => mst.Atk;

        public GameObject EffectParticle => mst.EffectParticle;


        // public LayerMask Layer => mst.Layer;

        public void SpawnEffect()
        {
            mst.SpawnEffect();
        }        

        public void Attack(Temp.TestPlayer player, Vector3 attackerPos)
        {
            if (mst != null)
            {
                mst.Attack(player, attackerPos);
            }
        }

        // public abstract void SetStrategy();

        /*
        public virtual void SetStatus()
        {

        }
        */
        /*
        public abstract void Use();
        */
    }
}




