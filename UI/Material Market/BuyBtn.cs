using UnityEditor;
using UnityEngine;

public class BuyBtn : MonoBehaviour
{
    private Database.DataBridge _dataBridge = Database.DataBridge.GetInstance();

    private int calculateTotal()
    {
        int total = InputFieldBean.steelQuantity * 20 
            + InputFieldBean.glassQuantity * 20
            + InputFieldBean.aluminumQuantity * 20
            + InputFieldBean.rubberQuantity * 20;
        return total;
    }
    
    private bool IsBalanceSufficientToBuyDesiredAmt()
    {
        int playerBalance = _dataBridge.GetPlayerBalance();
        int total = calculateTotal();
        return total <= playerBalance;
    }

    private void TryUpdatePlayerMaterialQty()
    {
        if (IsBalanceSufficientToBuyDesiredAmt())
        {
            _dataBridge.UpdatePlayerMaterialQty();
            PromptPurchaseSucceeded();   //Need to check if update is actually done. BUT HOW?
        }
        else
        {
            PromptInsufficientBalance();
        }
    }

    private void PromptPurchaseSucceeded()
    {
        EditorUtility.DisplayDialog("Success", "Purchase succeeded", "Close");
    }
    
    private void PromptInsufficientBalance()
    {
        EditorUtility.DisplayDialog("Failure", "Purchase Failed Due To Insufficient Balance", "Close");
    }
}
