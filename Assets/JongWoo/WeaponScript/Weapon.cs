using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JongWoo
{
    public interface IGribable
    {

    }

    public abstract class Weapon : MonoBehaviour, IGribable
    {
        [SerializeField] protected float stunGage;
        [SerializeField] protected float atkSpeed;

       
        public virtual void SetStatus()
        {

        }

        public abstract void Use();
    }


}




