using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMapHandler : MonoBehaviour
{
    [SerializeField]
    private Dropdown mapIndexDropdown;

    public void SetupMapIndexDropdown(int[] options, int currentOption, UnityEngine.Events.UnityAction<int> onValueChangeMethod)
    {
        for (int i = 0; i < options.Length; ++i)
        {
            mapIndexDropdown.options.Add(new Dropdown.OptionData("" + options[i]));
        }
        mapIndexDropdown.value = currentOption;
        mapIndexDropdown.onValueChanged.AddListener(onValueChangeMethod);
    }
}
