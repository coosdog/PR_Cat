using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTHinge : MonoBehaviour
{
    public HingeJoint joint;
    int breakCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        joint = GetComponent<HingeJoint>();
        StartCoroutine(Cool());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator Cool()
    {
        while (true)
        {
            if (breakCount > 5)
                break;
            yield return new WaitForSeconds(3);
            joint.useMotor = true;
            yield return new WaitForSeconds(7);
            joint.useMotor = false;
            breakCount++;
        }
    }

}
