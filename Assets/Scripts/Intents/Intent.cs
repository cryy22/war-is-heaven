using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Crysc.Initialization;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D.Animation;
using WarIsHeaven.Actions;
using WarIsHeaven.Helpers;
using WarIsHeaven.Killables;

namespace WarIsHeaven.Intents
{
    [RequireComponent(typeof(Killable))]
    [RequireComponent(typeof(SpriteLibrary))]
    public class Intent : InitializationBehaviour<IntentConfig>, IPointerEnterHandler, IPointerExitHandler
    {
        private SpriteLibrary _spriteLibrary;

        public event EventHandler Hovered;
        public event EventHandler Unhovered;
        public string Title => InitParams.Title;
        public string Description => InitParams.Description;

        public Killable Killable { get; private set; }

        private void Awake()
        {
            Killable = GetComponent<Killable>();
            _spriteLibrary = GetComponent<SpriteLibrary>();
        }

        private void Start()
        {
            if (IntentRegistry.I != null) IntentRegistry.I.Register(this);
        }

        private void OnEnable() { Killable.Killed += KilledEventHandler; }

        private void OnDisable()
        {
            Killable.Killed -= KilledEventHandler;
            Unhovered?.Invoke(sender: this, e: EventArgs.Empty);
        }

        public override void Initialize(IntentConfig initParams)
        {
            base.Initialize(initParams);
            _spriteLibrary.spriteLibraryAsset = initParams.SpriteLibraryAsset;
            Killable.Initialize(initParams.InitialValue);
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
            return InitParams.ActionMagnitudes.Where(am => am.CanBeInvoked(context));
        }

        public void OnPointerEnter(PointerEventData eventData) { Hovered?.Invoke(sender: this, e: EventArgs.Empty); }
        public void OnPointerExit(PointerEventData eventData) { Unhovered?.Invoke(sender: this, e: EventArgs.Empty); }
    }
}
