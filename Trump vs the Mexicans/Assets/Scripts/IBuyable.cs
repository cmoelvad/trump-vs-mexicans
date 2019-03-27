using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    interface IBuyable
    {
        bool CanAffordNew(int moneyInWallet);
        int BuyNew(int moneyInWallet); //Returns the leftover money after subtraction

        bool CanAffordUpgrade(int moneyInWallet);
        int BuyUpgrade(int moneyInWallet); //Returns the leftover money after subtraction

    }
}
