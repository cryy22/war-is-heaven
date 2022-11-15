using System;
using UnityEngine;

namespace WarIsHeaven.Common
{
    public abstract class InitializedBehaviour<TConfig> : MonoBehaviour
    {
        protected bool IsInitialized { get; private set; }
        protected TConfig Config { get; private set; }

        public virtual void Initialize(TConfig config)
        {
            if (IsInitialized) throw new InvalidOperationException("Already initialized");
            IsInitialized = true;

            Config = config;
        }
    }
}
