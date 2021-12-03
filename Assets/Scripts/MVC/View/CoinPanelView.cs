using MVC.Controller;
using UnityEngine;
using UnityEngine.UI;

namespace MVC.View
{
    public class CoinPanelView : MonoBehaviour
    {
        [SerializeField] private Image mUiSprSelf;
        [SerializeField] private Sprite[] mUiSprArr;

        private int curIndex;

        private void Start()
        {
            mUiSprSelf.sprite = mUiSprArr[5];
            Invoke(nameof(ChangeIndex), 0.3f);
        }

        //改变图片
        private void ChangeIndex()
        {
            curIndex++;
            if (curIndex == mUiSprArr.Length)
            {
                curIndex = 0;
            }

            mUiSprSelf.sprite = mUiSprArr[curIndex];
            Invoke(nameof(ChangeIndex), 0.06f);
        }
    }
}