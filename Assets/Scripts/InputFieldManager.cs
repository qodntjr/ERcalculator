using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldManager : MonoBehaviour
{
    public DamageCalculator damageCalculator;
    public InputField inputFiled;
    public int number;

    private void Start()
    {
        inputFiled.onValueChanged.AddListener(ValueChanged);
    }

    void ValueChanged(string text)
    {
        damageCalculator.Input_Value(number, float.Parse(text));
    }
}
