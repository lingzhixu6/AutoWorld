using System;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldBean : MonoBehaviour
{
    public InputField steelQuantityInputField;
    public InputField glassQuantityInputField;
    public InputField aluminumQuantityInputField;
    public InputField rubberQuantityInputField;
    
    
    public int getInputtedSteelQuantity()
    {
        return Int16.Parse(steelQuantityInputField.text);
    }
    public int getInputtedGlassQuantity()
    {
        return Int16.Parse(glassQuantityInputField.text);
    }
    public int getInputtedAluminumQuantity()
    {
        return Int16.Parse(aluminumQuantityInputField.text);
    }
    public int getInputtedRubberQuantity()
    {
        return Int16.Parse(rubberQuantityInputField.text);
    }
    
}
