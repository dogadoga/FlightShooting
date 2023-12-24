using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace DesignPatterns
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        /// <summary>
        /// gameObject，コンポーネントのどちらを1つに制限するか．
        /// 派生クラスでオーバーライドして使う．
        /// </summary>
        protected virtual bool DestroyTargetGameObject => false;
        
        public static T I {get; private set;} = null;

        /// <summary>
        /// Singleton が有効か
        /// </summary>
        /// <returns></returns>
        public static bool IsValid() => I != null;

        private void Awake()
        {
            if (I==null)
            {
                I = this as T;
                I.Init();
                return;
            }
            if (DestroyTargetGameObject)
            {
                Destroy(gameObject);
            }
            else
            {
                Destroy(this);
            }
        }

        /// <summary>
        /// 派生クラス用のAwake
        /// </summary>
        protected virtual void Init()
        {
        }

        private void OnDestroy()
        {
            if (I == this)
            {
                I = null;
            }
            OnRelease();
        }

        /// <summary>
        /// 派生クラス用のOnDestroy
        /// </summary>
        protected virtual void OnRelease()
        {
        }
    }
}
