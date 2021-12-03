using Common;
using MVC.Model;
using UnityEngine;
using UnityEngine.UI;

namespace MVC.View
{
    public class DailyBoxPanelView : MonoBehaviour
    {
        [SerializeField] private Button mUIBoxbtn;
        [SerializeField] private GameObject mUIFreeObj;
        [SerializeField] private Image mUIBg;
        [SerializeField] private GameObject mUINoemalObj;
        [SerializeField] private Text mUINeedCoinNumText;

        [field: SerializeField] public Image MUINeedCoinNumSpr { get; }

        [SerializeField] private Image mUISubImg;
        [SerializeField] private Image mUIMoneyImg;
        [SerializeField] private GameObject mUILockObj;

        [SerializeField] private GameObject mUIUnLockObj;

        //购买与否预制体
        [SerializeField] private GameObject mUIIsPurchased;

        private ProductDataInfo dataInfo;

        public void Init(ProductDataInfo dataInfo)
        {
            this.dataInfo = dataInfo;
            mUIBoxbtn.onClick.AddListener(OnClickBox);
            SetInfo();
        }

        //设置信息
        private void SetInfo()
        {
            mUILockObj.SetActive(dataInfo.IsLock);
            mUIUnLockObj.SetActive(!dataInfo.IsLock);
            mUIFreeObj.SetActive(dataInfo.CostGold <= 0);
            mUINoemalObj.SetActive(dataInfo.CostGold > 0);
            mUIIsPurchased.SetActive(dataInfo.IsPurchased == 1);

            if (dataInfo.CostGold > 0)
            {
                mUINeedCoinNumText.text = dataInfo.CostGold.ToString();
            }

            Sprite spr = null;
            Sprite sprBg = null;
            Sprite sprKuangBg = null;

            switch (dataInfo.Type)
            {
                case 2:
                    spr = Tools.ResourceManager.Instance.GteSprByPath(Const.CoinSprPath);
                    break;
                case 3:
                    spr = Tools.ResourceManager.Instance.GteSprByPath(GetSubSprPath());
                    sprBg = Tools.ResourceManager.Instance.GteSprByPath(Const.CardPersonBgPath);
                    sprKuangBg = Tools.ResourceManager.Instance.GteSprByPath(Const.CardPersonKuangPath);
                    break;
            }

            if (spr != null)
            {
                mUISubImg.sprite = spr;
            }

            if (sprBg != null)
            {
                mUIBg.sprite = sprBg;
            }

            if (sprKuangBg != null)
            {
                mUIMoneyImg.sprite = sprKuangBg;
            }
        }

        //点击宝箱事件
        private void OnClickBox()
        {
            dataInfo.RefreshPurchaseInfo(1);
            mUIIsPurchased.SetActive(dataInfo.IsPurchased == 1);
            mUIBoxbtn.gameObject.SetActive(false);
        }

        //获取子物体图片
        private string GetSubSprPath()
        {
            var path = "";
            switch (dataInfo.SubType)
            {
                case 7:
                    path = Const.SubCardType7Path;
                    break;
                case 13:
                    path = Const.SubCardType13Path;
                    break;
                case 18:
                    path = Const.SubCardType18Path;
                    break;
                case 20:
                    path = Const.SubCardType20Path;
                    break;
            }

            return path;
        }
    }
}