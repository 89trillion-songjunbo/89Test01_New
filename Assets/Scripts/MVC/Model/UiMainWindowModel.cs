using System.Collections.Generic;
using Common;
using SimpleJSON;
using Tools;
using UnityEngine;

namespace MVC.Model
{
    public class UiMainWindowModel
    {
        public ProductData ProductData { get; private set; }

        public void Init()
        {
            var json = ResourceManager.Instance.LoadResourceOfType<TextAsset>(Const.DailyJsonPath);
            ProductData = ReadData(json.ToString());
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <returns></returns>
        private static ProductData ReadData(string data)
        {
            var root = JSON.Parse(data);

            var productData = new ProductData
            {
                DailyProduct = new List<ProductDataInfo>(root["dailyProduct"].Count)
            };

            for (var i = 0; i < root["dailyProduct"].Count; i++)
            {
                var productLis = root["dailyProduct"][i];

                var info = new ProductDataInfo(
                    productLis["productId"].AsInt,
                    productLis["type"].AsInt,
                    productLis["subType"].AsInt,
                    productLis["num"].AsInt,
                    productLis["costGold"].AsInt,
                    productLis["isPurchased"].AsInt
                );

                productData.DailyProduct.Add(info);
            }

            productData.DailyProductCountDown = root["dailyProductCountDown"].AsInt;

            return productData;
        }

        //释放函数
        public void Release()
        {
            ProductData = null;
        }
    }

    public class ProductData
    {
        public List<ProductDataInfo> DailyProduct;
        public int DailyProductCountDown { get; set; }
    }

    public class ProductDataInfo
    {
        private int ProductId { get; }
        public int Type { get; }
        public int SubType { get; }
        private int Num { get; }
        public int CostGold { get; }
        public int IsPurchased { get; private set; }
        public bool IsLock { get; }

        public ProductDataInfo(int productId, int type, int subType, int num, int costGold, int isPurchased)
        {
            ProductId = productId;
            Type = type;
            SubType = subType;
            Num = num;
            CostGold = costGold;
            IsPurchased = isPurchased;

            IsLock = false;
        }

        public ProductDataInfo(bool isLock)
        {
            IsLock = isLock;
        }

        public void RefreshPurchaseInfo(int value)
        {
            IsPurchased = value;
        }
    }
}