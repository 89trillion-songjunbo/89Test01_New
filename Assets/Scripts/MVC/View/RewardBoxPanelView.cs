using System;
using UnityEngine;
using UnityEngine.UI;

namespace MVC.View
{
    public class RewardBoxPanelView : MonoBehaviour
    {
        [SerializeField] private Button mUIBoxBtn;

        //宝箱图片
        [SerializeField] private Image mUISubSpr;
        [SerializeField] private Image mUISubOpenSpr;
        [SerializeField] private Transform mUIEffParentTrans;
        [SerializeField] private GameObject mUIEffectObj;

        public Action EventOnClickExchange = null;

        public void Init()
        {
            PlayerPrefs.SetInt("ButCount", 0);
            mUIBoxBtn.onClick.AddListener(OnClickExchange);
            SetBoxState(false);
        }

        //点击事件
        private void OnClickExchange()
        {
            if (isOpen)
            {
                return;
            }

            var num = PlayerPrefs.GetInt("ButCount");
            PlayerPrefs.SetInt("ButCount", ++num);
            EventOnClickExchange?.Invoke();
            SetBoxState(true);
        }

        private bool isOpen;
        private GameObject tmpObj;

        public void SetBoxState(bool isOpen)
        {
            this.isOpen = isOpen;
            mUISubSpr.gameObject.SetActive(!isOpen);
            mUISubOpenSpr.gameObject.SetActive(isOpen);
            if (isOpen && tmpObj == null)
            {
                tmpObj = AddParticle();
            }
            else
            {
                if (tmpObj == null)
                {
                    return;
                }

                Destroy(tmpObj);
                tmpObj = null;
            }
        }

        private GameObject AddParticle()
        {
            var go = Instantiate(mUIEffectObj, mUIEffParentTrans);
            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = Quaternion.identity;
            return go;
        }

        public Transform SubSprTrans => mUISubSpr.transform;
    }
}