using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForEachHandler : MonoBehaviour
{
    [SerializeField]
    private Dropdown tagDropdown;
    [SerializeField]
    private Text text;

    public void SetupTagDropdown(string[] options, int currentOption, UnityEngine.Events.UnityAction<int> onValueChangeMethod)
    {
        for (int i = 0; i < options.Length; ++i)
        {
            tagDropdown.options.Add(new Dropdown.OptionData(options[i]));
        }
        tagDropdown.value = currentOption;
        tagDropdown.onValueChanged.AddListener(onValueChangeMethod);
    }

    public void SetupText(string variableName)
    {
        text.text = variableName;
    }
}
