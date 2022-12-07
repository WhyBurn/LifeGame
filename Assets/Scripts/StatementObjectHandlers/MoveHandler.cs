using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveHandler : MonoBehaviour
{
    [SerializeField]
    private Dropdown tagDropdown;
    [SerializeField]
    private Dropdown directionDropdown;

    public void SetupTagDropdown(string[] options, int currentOption, UnityEngine.Events.UnityAction<int> onValueChangeMethod)
    {
        for (int i = 0; i < options.Length; ++i)
        {
            tagDropdown.options.Add(new Dropdown.OptionData(options[i]));
        }
        tagDropdown.value = currentOption;
        tagDropdown.onValueChanged.AddListener(onValueChangeMethod);
    }

    public void SetupDirectionDropdown(string[] options, int currentOption, UnityEngine.Events.UnityAction<int> onValueChangeMethod)
    {
        for (int i = 0; i < options.Length; ++i)
        {
            directionDropdown.options.Add(new Dropdown.OptionData(options[i]));
        }
        directionDropdown.value = currentOption;
        directionDropdown.onValueChanged.AddListener(onValueChangeMethod);
    }
}
