using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Simmer.Player
{
    public class PlayerCurrency : MonoBehaviour
    {
        public TextMeshProUGUI moneyText;
        private int currencyAmt = 0;

        public void addMoney(int amt) 
        {
            currencyAmt += amt;
            moneyText.text = "Money: " + currencyAmt;
        }

        public int getAmt() 
        {
            return currencyAmt;
        }
    }
}