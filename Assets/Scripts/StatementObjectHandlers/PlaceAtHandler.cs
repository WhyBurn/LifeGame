using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceAtHandler : MonoBehaviour
{
    [SerializeField]
    private Dropdown tagDropdown;
    [SerializeField]
    private Dropdown xDropdown;
    [SerializeField]
    private Dropdown yDropdown;

    public void SetupTagDropdown(string[] options, int currentOption, UnityEngine.Events.UnityAction<int> onValueChangeMethod)
    {
        for (int i = 0; i < options.Length; ++i)
        {
            tagDropdown.options.Add(new Dropdown.OptionData(options[i]));
        }
        tagDropdown.value = currentOption;
        tagDropdown.onValueChanged.AddListener(onValueChangeMethod);
    }

    public void SetupXDropdown(int[] options, int currentOption, UnityEngine.Events.UnityAction<int> onValueChangeMethod)
    {
        for (int i = 0; i < options.Length; ++i)
        {
            xDropdown.options.Add(new Dropdown.OptionData("" + options[i]));
        }
        xDropdown.value = currentOption;
        xDropdown.onValueChanged.AddListener(onValueChangeMethod);
    }

    public void SetupYDropdown(int[] options, int currentOption, UnityEngine.Events.UnityAction<int> onValueChangeMethod)
    {
        for (int i = 0; i < options.Length; ++i)
        {
            yDropdown.options.Add(new Dropdown.OptionData("" + options[i]));
        }
        yDropdown.value = currentOption;
        yDropdown.onValueChanged.AddListener(onValueChangeMethod);
    }
}
