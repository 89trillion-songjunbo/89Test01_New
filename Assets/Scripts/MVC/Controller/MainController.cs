using MVC.Model;
using MVC.View;
using UnityEngine;
using UnityEngine.UI;

namespace MVC.Controller
{
    public class MainController : MonoBehaviour
    {
        [SerializeField] private Button mUIStartBtn;
        [SerializeField] private GameObject mMainObj;
        public UiMainWindowView mUIMainView;

        #region View 对应 Logic info

        private UiMainWindowModel mainWindowModel;
        private UiMainWindowModel MainWindowModel => mainWindowModel ?? (mainWindowModel = new UiMainWindowModel());

        #endregion

        private void Awake()
        {
            InitLogic();
        }

        private void Start()
        {
            SetState();
            mUIStartBtn.onClick.AddListener(() =>
            {
                if (mainWindowModel.ProductData == null)
                {
                    return;
                }

                mUIMainView.Init(mainWindowModel.ProductData);
                SetState(false);
            });
        }

        private void SetState(bool isInit = true)
        {
            mUIStartBtn.gameObject.SetActive(isInit);
            mMainObj.gameObject.SetActive(!isInit);
        }

        private void InitLogic()
        {
            MainWindowModel.Init();
        }

        private void OnDestroy()
        {
            mainWindowModel?.Release();
        }
    }
}