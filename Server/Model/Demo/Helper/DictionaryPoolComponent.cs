using System;
using System.Collections.Generic;

namespace ET
{
    public class DictionaryPoolComponent<T,K>:Dictionary<T,K>,IDisposable
    {
        public static DictionaryPoolComponent<T, K> Create()
        {
            return MonoPool.Instance.Fetch(typeof (DictionaryPoolComponent<T, K>)) as DictionaryPoolComponent<T, K>;
        }
        
        public void Dispose()
        {
            this.Clear();
            MonoPool.Instance.Recycle(this);
        }
        
    }
}