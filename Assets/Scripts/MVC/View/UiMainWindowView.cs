using System.Collections.Generic;
using Common;
using MVC.Model;
using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace MVC.View
{
    public class UiMainWindowView : MonoBehaviour
    {
        [SerializeField] private RectTransform mUIContentTrans;
        [SerializeField] private Transform mUICoinStartPos;
        [SerializeField] private Transform mUICoinRecyclePos;
        [SerializeField] private RewardBoxPanelView mRewardPanelView;
        [SerializeField] private Text mUICurCoinNum;

        public Transform mUICoinEndSPos;

        public List<DailyBoxPanelView> DailyBoxPanelViews;

        public GameObject coinObjPrefab;
        //金币lis
        // private readonly List<CoinPanelView> coinAnimPanels = new List<CoinPanelView>();

        private readonly List<GameObject> coinAnimPanels = new List<GameObject>();

        //缓存位置备用
        private Transform startParentPosCache;

        private void Awake()
        {
            AddCoinPrefab();
            startParentPosCache = mUICoinStartPos.parent.transform;
        }

        private ProductData productData;

        /// <summary>
        /// 批量添加金币预制备用
        /// </summary>
        private void AddCoinPrefab()
        {
            coinAnimPanels.Clear();
            for (var i = 0; i < Const.InitCoinNum; i++)
            {
                // var coinPanelView =
                //     ResourceManager.Instance.LoadGameObjectByPath<CoinPanelView>(Const.CoinPath, mUICoinRecyclePos);
                // if (coinPanelView == null)
                // {
                //     continue;
                // }
                //
                // coinPanelView.gameObject.SetActive(false);
                // coinPanelView.TargetPos = mUICoinEndSPos;
                // coinAnimPanels.Add(coinPanelView);

                GameObject coinObj = Instantiate(coinObjPrefab, mUICoinRecyclePos);
                ResetObjState(coinObj);
                coinAnimPanels.Add(coinObj);
            }
        }

        private void ResetObjState(GameObject obj)
        {
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;
            obj.transform.localScale = Vector3.one;
        }

        public void Init(ProductData data)
        {
            productData = data;
            var count = productData.DailyProduct.Count;
            if (count % 3 != 0)
            {
                AddLockItem(productData);
            }

            // foreach (var t in productData.DailyProduct)
            // {
            //     var curPanelView =
            //         ResourceManager.Instance.LoadGameObjectByPath<DailyBoxPanelView>("Prefabs/DailyItem",
            //             mUIContentTrans);
            //     curPanelView.Init(t);
            // }

            var length = Mathf.Min(productData.DailyProduct.Count, DailyBoxPanelViews.Count);

            for (var i = 0; i < length; i++)
            {
                DailyBoxPanelViews[i].gameObject.SetActive(true);
                DailyBoxPanelViews[i].Init(productData.DailyProduct[i]);
            }

            if (mRewardPanelView == null)
            {
                return;
            }

            mRewardPanelView.EventOnClickExchange += RefreshOnClickExchange;
            mRewardPanelView.Init();
        }

        /// <summary>
        /// 点击按钮刷新金币
        /// </summary>
        private void RefreshOnClickExchange()
        {
            var curNun = PlayerPrefs.GetInt("ButCount") * Const.CoinAddNumPercent;
            if (curNun > Const.InitCoinNum)
            {
                curNun = Const.InitCoinNum;
            }

            StartCoroutine(CommonTools.DelayFunc(curNun, 0.1f, curIndex =>
            {
                SetCoinInfo(curIndex);
                if (curIndex == 0)
                {
                    StartCoroutine(CommonTools.DelayFunc(1, 1f, index =>
                    {
                        mRewardPanelView.SetBoxState(false);
                        mUICurCoinNum.text = (int.Parse(mUICurCoinNum.text) + curNun).ToString();
                    }));
                }
            }));
        }

        /// <summary>
        /// 设置货币信息
        /// </summary>
        /// <param name="index"></param>
        private void SetCoinInfo(int index)
        {
            var curTran = coinAnimPanels[index].transform;
            SetStartPos();
            curTran.SetParent(mUICoinStartPos);
            curTran.gameObject.SetActive(true);
            ResetTransformInfo(coinAnimPanels[index].transform);
            CommonTools.DoScaleWithMove(curTran, mUICoinEndSPos, 1f, () =>
            {
                curTran.SetParent(mUICoinRecyclePos);
                curTran.localScale = Vector3.zero;
                ResetTransformInfo(curTran);
                curTran.gameObject.SetActive(false);
            });
        }

        /// <summary>
        /// 设置特效起始点位置
        /// </summary>
        private void SetStartPos()
        {
            mUICoinStartPos.SetParent(mRewardPanelView.SubSprTrans.transform);
            mUICoinStartPos.transform.localPosition = Vector3.zero;
            mUICoinStartPos.SetParent(startParentPosCache);
            mUICoinStartPos.transform.localScale = Vector3.one;
            mUICoinStartPos.SetAsLastSibling();
        }

        private static void ResetTransformInfo(Transform trans)
        {
            trans.localPosition = Vector3.zero;
            trans.localScale = Vector3.zero;
            trans.localRotation = Quaternion.identity;
        }

        /// <summary>
        /// 数量不足添加锁定Item
        /// </summary>
        /// <param name="productData"></param>
        private static void AddLockItem(ProductData productData)
        {
            var count = productData.DailyProduct.Count;
            if (count % 3 == 0)
            {
                return;
            }

            for (var i = 0; i < 3 - count % 3; i++)
            {
                var info = new ProductDataInfo(true);
                productData.DailyProduct.Add(info);
            }
        }
    }
}