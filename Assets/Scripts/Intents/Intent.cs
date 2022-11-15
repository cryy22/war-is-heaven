using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;
using WarIsHeaven.Cards.CardActions;
using WarIsHeaven.Common;
using WarIsHeaven.Killables;

namespace WarIsHeaven.Intents
{
    [RequireComponent(typeof(Killable))]
    [RequireComponent(typeof(SpriteLibrary))]
    public class Intent : InitializedBehaviour<IntentConfig>
    {
        private SpriteLibrary _spriteLibrary;

        public IReadOnlyList<ActionMagnitude> ActionMagnitudes => Config.ActionMagnitudes;
        public Killable Killable { get; private set; }

        private void Awake()
        {
            Killable = GetComponent<Killable>();
            _spriteLibrary = GetComponent<SpriteLibrary>();
        }

        private void OnEnable() { Killable.Killed += KilledEventHandler; }
        private void OnDisable() { Killable.Killed -= KilledEventHandler; }

        public override void Initialize(IntentConfig config)
        {
            base.Initialize(config);
            _spriteLibrary.spriteLibraryAsset = Config.SpriteLibraryAsset;
            Killable.Initialize(Config.InitialValue);
        }

        public void Play(Context context)
        {
            foreach (ActionMagnitude am in Config.ActionMagnitudes) am.Invoke(context);
        }

        public IEnumerator Animate(Context context)
        {
            List<Coroutine> coroutines = new();
            foreach (ActionMagnitude am in Config.ActionMagnitudes)
                coroutines.Add(StartCoroutine(am.Action.Animate(context)));
            foreach (Coroutine coroutine in coroutines) yield return coroutine;
        }

        private void KilledEventHandler(object sender, EventArgs _) { Destroy(gameObject); }
    }
}
