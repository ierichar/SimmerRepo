using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using Simmer.SceneManagement;
using Simmer.UI;

namespace Simmer.Player
{
    public class PlayerCurrency : MonoBehaviour
    {
        private MoneyUI _moneyUI;
        private int currencyAmt = 0;

        public void Construct(MoneyUI moneyUI)
        {
            _moneyUI = moneyUI;
            currencyAmt = GlobalPlayerData.playerMoney;
            UpdateDisplay();
        }

        public void addMoney(int amt) 
        {
            currencyAmt += amt;
            UpdateDisplay();
            GlobalPlayerData.SetMoney(currencyAmt);
        }

        private void UpdateDisplay()
        {
            _moneyUI.textManager.SetText(currencyAmt.ToString());
        }

        public int getAmt() 
        {
            return currencyAmt;
        }
    }
}