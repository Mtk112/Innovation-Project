using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Dropdown))]
public class Curviture : MonoBehaviour {
    const string CurveOption = "CurveValue";

    private Dropdown _dropdown;

    private void Awake()
    {
        _dropdown = GetComponent<Dropdown>();

        //selects the new dropdown value and saves it as a player prefence.
        _dropdown.onValueChanged.AddListener(new UnityAction<int>(index =>
        {
            PlayerPrefs.SetInt(CurveOption, _dropdown.value);
            PlayerPrefs.Save();
        }));
    }
    //Loads the currently selected curviture option, or sets it as a default value if not previously set.
    void Start()
    {
        _dropdown.value = PlayerPrefs.GetInt(CurveOption, 0);
    }
}
