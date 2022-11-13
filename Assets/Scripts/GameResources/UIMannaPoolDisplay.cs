using System;
using System.Collections.Generic;
using UnityEngine;

namespace WarIsHeaven.GameResources
{
    public class UIMannaPoolDisplay : MonoBehaviour
    {
        [SerializeField] private MannaPool MannaPool;
        [SerializeField] private List<GameObject> MannaIcons;

        private void Awake() { UpdateMannaIcons(); }

        private void OnEnable() { MannaPool.MannaChanged += MannaChangedEventHandler; }
        private void OnDisable() { MannaPool.MannaChanged -= MannaChangedEventHandler; }

        private void MannaChangedEventHandler(object sender, EventArgs _) { UpdateMannaIcons(); }

        private void UpdateMannaIcons()
        {
            for (var i = 0; i < MannaIcons.Count; i++) MannaIcons[i].SetActive(i < MannaPool.Manna);
        }
    }
}
