using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;
using WarIsHeaven.Cards.CardActions;
using WarIsHeaven.Killables;

namespace WarIsHeaven.Intents
{
    [RequireComponent(typeof(Killable))]
    [RequireComponent(typeof(SpriteLibrary))]
    public class Intent : MonoBehaviour
    {
        private Killable _killable;
        private IntentConfig _config;
        private SpriteLibrary _spriteLibrary;
        private bool _isInitialized;

        public IReadOnlyList<ActionMagnitude> ActionMagnitudes => _config.ActionMagnitudes;

        private void Awake()
        {
            _killable = GetComponent<Killable>();
            _spriteLibrary = GetComponent<SpriteLibrary>();
        }

        private void OnEnable() { _killable.Killed += KilledEventHandler; }
        private void OnDisable() { _killable.Killed -= KilledEventHandler; }

        public void Initialize(IntentConfig config)
        {
            if (_isInitialized) throw new InvalidOperationException("Intent already initialized");
            _config = config;
            _spriteLibrary.spriteLibraryAsset = _config.SpriteLibraryAsset;

            _isInitialized = true;
        }

        public void Play(Context context)
        {
            foreach (ActionMagnitude am in _config.ActionMagnitudes) am.Invoke(context);
        }

        public IEnumerator Animate(Context context)
        {
            List<Coroutine> coroutines = new();
            foreach (ActionMagnitude am in _config.ActionMagnitudes)
                coroutines.Add(StartCoroutine(am.Action.Animate(context)));
            foreach (Coroutine coroutine in coroutines) yield return coroutine;
        }

        private void KilledEventHandler(object sender, EventArgs _) { Destroy(gameObject); }
    }
}
