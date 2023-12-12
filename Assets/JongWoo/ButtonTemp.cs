using JongWoo;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JongWoo
{
    public class ButtonTemp : MonoBehaviour
    {
        public enum SwitchToggle
        {
            GRAB = 0,
            DROP = -1,
        }

        public UnityEngine.UI.Button button;
        public TextMeshProUGUI textInput;

        public SwitchToggle toggle;

        string grab;
        string drop;

        private void Start()
        {
            grab = "Grab";
            drop = "Drop";

            button = GetComponent<UnityEngine.UI.Button>();
            textInput = GetComponentInChildren<TextMeshProUGUI>();

            toggle = SwitchToggle.GRAB;
            ToggleCheck();
        }

        public void ToggleCheck()
        {
            if (toggle == SwitchToggle.GRAB)
            {
                textInput.text = grab;
                Debug.Log("상태체크:그랩");
            }
            else if (toggle == SwitchToggle.DROP)
            {
                textInput.text = drop;
                Debug.Log("상태체크:드랍");
            }
        }

        public void TestColor()
        {
            ColorBlock btnColor = button.colors;
            btnColor.normalColor = Color.yellow;

            button.colors = btnColor;
        }

        public void TestButton(ref bool changeValue)
        {
            changeValue = true;
            toggle = SwitchToggle.DROP;
            ToggleCheck();
            //Debug.Log(toggle);
        }

        public void Drop(Weapon obj, ref bool changeValue)
        {
            changeValue = false;
            if (obj == null)
            {
                toggle = SwitchToggle.GRAB;
                return;
            }
            obj.transform.SetParent(null);
            toggle = SwitchToggle.GRAB;
            ToggleCheck();
        }

        public void Update()
        {
            ToggleCheck();
        }

    }

}
