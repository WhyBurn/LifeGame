using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SameSpaceHandler : MonoBehaviour
{
    [SerializeField]
    private Dropdown leftDropdown;
    [SerializeField]
    private Dropdown rightDropdown;

    public void SetupLeftDropdown(string[] options, int currentOption, UnityEngine.Events.UnityAction<int> onValueChangeMethod)
    {
        for(int i = 0; i < options.Length; ++i)
        {
            leftDropdown.options.Add(new Dropdown.OptionData(options[i]));
        }
        leftDropdown.value = currentOption;
        leftDropdown.onValueChanged.AddListener(onValueChangeMethod);
    }
    public void SetupRightDropdown(string[] options, int currentOption, UnityEngine.Events.UnityAction<int> onValueChangeMethod)
    {
        for (int i = 0; i < options.Length; ++i)
        {
            rightDropdown.options.Add(new Dropdown.OptionData(options[i]));
        }
        rightDropdown.value = currentOption;
        rightDropdown.onValueChanged.AddListener(onValueChangeMethod);
    }
}
