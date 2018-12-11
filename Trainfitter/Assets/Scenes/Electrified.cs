using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

//Method for setting selected value from Electricity Dropdown.
[RequireComponent(typeof(Dropdown))]
public class Electrified : MonoBehaviour {

    const string ElecOption = "ElecValue";

    private Dropdown dropdown;

    private void Awake()
    {
        dropdown = GetComponent<Dropdown>();

        //selects the new dropdown value and saves it as a player preference.
        dropdown.onValueChanged.AddListener(new UnityAction<int>(index =>
        {
            PlayerPrefs.SetInt(ElecOption, dropdown.value);
            PlayerPrefs.Save();
        }));
    }
    //Loads the currently selected electricity option, or sets it as a default value if not previously set.
    void Start()
    {
        dropdown.value = PlayerPrefs.GetInt(ElecOption, 0);
    }
}
