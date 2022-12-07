using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AndOrHandler : MonoBehaviour
{
    [SerializeField]
    private Dropdown andOrDropdown;

    public void SetupTypeDropdown(string[] options, int currentOption, UnityEngine.Events.UnityAction<int> onValueChangeMethod)
    {
        for (int i = 0; i < options.Length; ++i)
        {
            andOrDropdown.options.Add(new Dropdown.OptionData(options[i]));
        }
        andOrDropdown.value = currentOption;
        andOrDropdown.onValueChanged.AddListener(onValueChangeMethod);
    }

    public void SetupTransforms(GameObject left, GameObject right)
    {
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(left.GetComponent<RectTransform>().rect.width + 40 + right.GetComponent<RectTransform>().rect.width, 30);
        left.transform.SetParent(transform);
        left.transform.localPosition = new Vector3();
        andOrDropdown.transform.localPosition = new Vector3(left.GetComponent<RectTransform>().rect.width, 0, 0);
        right.transform.SetParent(transform);
        right.transform.localPosition = new Vector3(left.GetComponent<RectTransform>().rect.width + 40, 0, 0);
    }
}
