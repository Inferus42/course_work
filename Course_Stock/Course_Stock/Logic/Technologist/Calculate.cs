using Course_Stock.DAO;
using Course_Stock.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Course_Stock.Logic.Technologist
{
    public class Calculate
    {

        public int CalcMax(List<Component> recordList)
        {
            Log.Log.For(this).Info("Start CalcMax");
            int valueInBox = Int32.MaxValue;

            foreach (Component r in recordList)
            {
                if (r.quantityInStockNow / r.quantityInProduct < valueInBox)
                {
                    valueInBox = r.quantityInStockNow / r.quantityInProduct;
                }

            }
            return valueInBox;
        }

        public List<Component> CalcResult(List<Component> recordList, int Max)
        {
            Log.Log.For(this).Info("Start CalcResult");
            foreach (Component r in recordList)
            {
                r.quantityInStockAfter = r.quantityInStockNow - (r.quantityInProduct * Max);
            }
            return recordList;
        }

    }
}