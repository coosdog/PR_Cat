using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JongWoo
{
    public abstract class Interaction : MonoBehaviour
    {
        public abstract void Interact(Collision collision);
    }

}
