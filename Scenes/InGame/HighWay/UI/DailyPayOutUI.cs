using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DailyPayOutUI : MonoBehaviour
{
    [Header("Variables")]
  //  [SerializeField] FloatVariable dailyPayOut;
  //  [SerializeField] FloatVariable GasPrice;
 //   [SerializeField] FloatVariable Taxes;
  //  [SerializeField] FloatVariable Damages;

    [Header("UI Components")]
    [SerializeField] GameObject dailyPayOutContainer;
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI prePayOut;
    [SerializeField] TextMeshProUGUI Expenses;
    [SerializeField] GameObject FeesContainer;
    [SerializeField] TextMeshProUGUI finalPayOut;
    [Header("UI Slots")]
    [SerializeField] GameObject FeeSlot;


    private void OnEnable() {
        PlayerManager.displayDailyPayOut += onDailyPayOut;
    }

    private void OnDisable() {
        PlayerManager.displayDailyPayOut -= onDailyPayOut;
    }


    
    public void onDailyPayOut(transactionDataPack transaction) {
        prePayOut.text = "cash: $" + transaction.PreAmount.ToString();
        finalPayOut.text = "Payout: $" + transaction.Amount.ToString();
        StartCoroutine(dailyPayOutDisplay(transaction));
    }

     IEnumerator dailyPayOutDisplay(transactionDataPack transaction) {
        Debug.Log("daily payout display started");
        dailyPayOutContainer.SetActive(true);
        yield return new WaitForSeconds(1f);
        prePayOut.gameObject.SetActive(true);
        yield return new WaitForSeconds(.5f);
        foreach (KeyValuePair<string, float> Fee in transaction.Fees) {
          GameObject newFeeSlot = Instantiate(FeeSlot,FeesContainer.transform);
          newFeeSlot.GetComponent<TextMeshProUGUI>().text = Fee.Key + ": $" + Fee.Value;
          yield return new WaitForSeconds(1f);       
        }
        finalPayOut.gameObject.SetActive(true);
    }

}
