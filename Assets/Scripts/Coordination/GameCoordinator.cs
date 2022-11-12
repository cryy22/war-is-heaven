using System;
using System.Collections;
using Cards;
using Cards.CardActions;
using Manna;
using TMPro;
using Units;
using UnityEngine;
using UnityEngine.UI;

namespace Coordination
{
    public class GameCoordinator : MonoBehaviour
    {
        [SerializeField] private Button EndTurnButton;
        [SerializeField] private Unit PlayerUnit;
        [SerializeField] private Unit EnemyUnit;
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
            PlayerUnit.MouseClicked += UnitClickedEventHandler;
            EnemyUnit.MouseClicked += UnitClickedEventHandler;
        }

        private void OnDisable()
        {
            EndTurnButton.onClick.RemoveListener(EndTurnButtonClicked);
            PlayerUnit.MouseClicked -= UnitClickedEventHandler;
            EnemyUnit.MouseClicked -= UnitClickedEventHandler;
        }

        private void EndTurnButtonClicked()
        {
            PlayerHand.ResetSelections();
            _isPlayerTurn = false;
        }

        private void UnitClickedEventHandler(object sender, EventArgs _)
        {
            if (!_isPlayerTurn) return;
            if (PlayerHand.SelectedCard == null) return;

            var unit = (Unit) sender;
            PlaySelectedCard(new CardAction.Context { Target = unit });
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

        private void PlaySelectedCard(CardAction.Context context)
        {
            Card card = PlayerHand.SelectedCard;
            card.Play(context);
            PlayerMannaPool.SpendManna(1);
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
