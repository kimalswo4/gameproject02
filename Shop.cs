using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Shop : MonoBehaviour {

    private const int POSTION_COST = 200;

    public Text PostionCost;
    public Text PostionCount;

    public void BuyPosition()
    {
        if (POSTION_COST <= StateManager.Instance.Gold)
        {
            StateManager.Instance.Gold -= POSTION_COST;
            StateManager.Instance.Postion++;
            SetShop();
        }
    }

    public void SetShop()
    {
        PostionCost.text = POSTION_COST.ToString();
        PostionCount.text = StateManager.Instance.Postion.ToString();
    }
}
