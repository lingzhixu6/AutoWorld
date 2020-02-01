using System;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldBean : MonoBehaviour
{
    public InputField steelQuantityInputField;
    public InputField glassQuantityInputField;
    public InputField aluminumQuantityInputField;
    public InputField rubberQuantityInputField;
    public static int steelQuantity;
    public static int glassQuantity;
    public static int aluminumQuantity;
    public static int rubberQuantity;

    private void Update()
    {
        steelQuantity = Int16.Parse(steelQuantityInputField.text);
        glassQuantity = Int16.Parse(glassQuantityInputField.text);
        aluminumQuantity = Int16.Parse(aluminumQuantityInputField.text);
        rubberQuantity = Int16.Parse(rubberQuantityInputField.text);
    }

    public void ClearInputField()
    {
        steelQuantityInputField.text = "";
        glassQuantityInputField.text = "";
        aluminumQuantityInputField.text = "";
        rubberQuantityInputField.text = "";
    }

}
