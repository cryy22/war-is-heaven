using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D.Animation;
using WarIsHeaven.Actions;
using WarIsHeaven.Common;
using WarIsHeaven.Helpers;
using WarIsHeaven.Killables;

namespace WarIsHeaven.Intents
{
    [RequireComponent(typeof(Killable))]
    [RequireComponent(typeof(SpriteLibrary))]
    public class Intent : InitializedBehaviour<IntentConfig>
    {
        private SpriteLibrary _spriteLibrary;
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
            foreach (ActionMagnitude am in GetPlayableActionMagnitudes(context)) am.Invoke(context);
        }

        public IEnumerator Animate(Context context)
        {
            yield return Waiter.WaitForAll(
                GetPlayableActionMagnitudes(context)
                    .Select(am => StartCoroutine(am.Action.Animate(context)))
                    .ToArray()
            );
        }

        private void KilledEventHandler(object sender, EventArgs _) { Destroy(gameObject); }

        private IEnumerable<ActionMagnitude> GetPlayableActionMagnitudes(Context context)
        {
            return Config.ActionMagnitudes.Where(am => am.CanBeInvoked(context));
        }
    }
}
