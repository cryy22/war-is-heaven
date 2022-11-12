using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WarIsHeaven.Cards;
using WarIsHeaven.Cards.CardActions;
using WarIsHeaven.Killables;
using WarIsHeaven.Resources;
using WarIsHeaven.Units;

namespace WarIsHeaven.Coordination
{
    public class GameCoordinator : MonoBehaviour
    {
        [SerializeField] private Button EndTurnButton;
        [SerializeField] private Unit PlayerUnit;
        [SerializeField] private EnemyUnit EnemyUnit;
        [SerializeField] private Deck Deck;
        [SerializeField] private Hand PlayerHand;
        [SerializeField] private Deck Discard;
        [SerializeField] private TMP_Text MannaText;

        [SerializeField] private int DrawsPerTurn;
        [SerializeField] private MannaPool PlayerMannaPool;

        private bool _isPlayerTurn = true;
        private int _manna;

        private void Start() { StartCoroutine(RunGame()); }

        private void OnEnable()
        {
            EndTurnButton.onClick.AddListener(EndTurnButtonClicked);
            KillableRegistry.Instance.Clicked += KillableClickedEventHandler;
        }

        private void OnDisable()
        {
            EndTurnButton.onClick.RemoveListener(EndTurnButtonClicked);
            KillableRegistry.Instance.Clicked -= KillableClickedEventHandler;
        }

        private void EndTurnButtonClicked()
        {
            PlayerHand.ResetSelections();
            _isPlayerTurn = false;
        }

        private void KillableClickedEventHandler(object sender, EventArgs _)
        {
            if (!_isPlayerTurn) return;
            if (PlayerHand.SelectedCard == null) return;

            var killable = (Killable) sender;
            PlaySelectedCard(new Context { Target = killable });
        }

        private IEnumerator RunGame()
        {
            while (true)
            {
                DrawCards();
                PlayerMannaPool.ResetManna();
                UpdateMannaText();

                _isPlayerTurn = true;
                yield return new WaitUntil(() => _isPlayerTurn == false);

                DiscardHand();
                EnemyUnit.Attack(PlayerUnit);
            }
        }

        private void DrawCards()
        {
            for (var i = 0; i < DrawsPerTurn; i++)
            {
                if (Deck.Count == 0)
                {
                    if (Discard.Count == 0) return;

                    Discard.Shuffle();
                    while (Discard.Count > 0) Deck.AddCard(Discard.TakeCard());
                }

                Card card = Deck.TakeCard();
                card.Flip(Card.SideType.Front);
                PlayerHand.AddCard(card);
            }
        }

        private void PlaySelectedCard(Context context)
        {
            Card card = PlayerHand.SelectedCard;
            card.Play(context);
            PlayerMannaPool.SpendManna(card.MannaCost);
            UpdateMannaText();

            PlayerHand.RemoveCard(card);
            Discard.AddCard(card);
        }

        private void DiscardHand()
        {
            while (PlayerHand.Count > 0)
            {
                Card card = PlayerHand.TakeCard();
                Discard.AddCard(card);
            }
        }

        private void UpdateMannaText() { MannaText.text = $"Manna: {PlayerMannaPool.GetMannaString()}"; }
    }
}
