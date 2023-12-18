using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionFreeze : Interaction
{
    public override void Interact(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController controller))
        {
            controller.Speed = 0;
        }
    }
}
