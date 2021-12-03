using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using Object = UnityEngine.Object;

namespace Tools
{
    public class ResourceManager
    {
        private static ResourceManager instace;

        public static ResourceManager Instance => instace ?? (instace = new ResourceManager());

        //加载resources下图片
        public Sprite GteSprByPath(string path)
        {
            return Resources.Load<Sprite>(path);
        }

        //加载资源
        public T LoadResourceOfType<T>(string path) where T : Object
        {
            return Resources.Load<T>(path);
        }
    }

    /// <summary>
    /// Tween工具
    /// </summary>
    public static class CommonTools
    {
        public static void DoScaleWithMove(Transform targetTrans, Transform targetPos, float timer, Action callBack)
        {
            targetTrans.DOScale(Vector3.one, 1f).OnComplete(() =>
            {
                targetTrans.DOMove(targetPos.position, timer).OnComplete(() =>
                {
                    if (callBack != null)
                    {
                        callBack();
                    }
                });
            });
        }

        /// <summary>
        /// 延时函数
        /// </summary>
        /// <param name="count">执行次数</param>
        /// <param name="timer">时间</param>
        /// <param name="callBack">回掉</param>
        /// <returns></returns>
        public static IEnumerator DelayFunc(int count, float timer, Action<int> callBack)
        {
            while (count > 0)
            {
                yield return new WaitForSeconds(timer);
                if (callBack != null)
                {
                    callBack(count - 1);
                }

                count--;
            }
        }
    }
}