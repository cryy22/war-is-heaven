using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using WarIsHeaven.Cards;
using WarIsHeaven.Cards.CardActions;
using WarIsHeaven.Killables;
using WarIsHeaven.Resources;
using WarIsHeaven.UI;
using WarIsHeaven.Units;

namespace WarIsHeaven.Coordination
{
    public class GameCoordinator : MonoBehaviour
    {
        private const string _winText = "-- MISSION --\naccomplished";
        private const string _loseText = "-- MISSION --\nfailed...";

        [SerializeField] private Button EndTurnButton;
        [SerializeField] private Unit PlayerUnit;
        [SerializeField] private EnemyUnit EnemyUnit;
        [SerializeField] private Deck Deck;
        [SerializeField] private Hand PlayerHand;
        [SerializeField] private Deck Discard;
        [SerializeField] private TMP_Text MannaText;
        [SerializeField] private UIFullscreenAnnouncePanel FullscreenAnnouncePanel;

        [SerializeField] private int DrawsPerTurn;
        [SerializeField] private MannaPool PlayerMannaPool;

        private bool _isPlayerTurn = true;
        private bool _isGameWon;

        private int _manna;

        private bool IsGameLost => PlayerUnit.Health <= 0 || EnemyUnit.Health <= 0;
        private bool IsPlayerTurnEnded => !_isPlayerTurn || IsGameEnded;
        private bool IsGameEnded => IsGameLost || _isGameWon;

        private void Start() { StartCoroutine(RunGame()); }

        private void OnEnable()
        {
            PlayerHand.CardSelected += CardSelectedEventHandler;
            if (PlayerHand.SelectedCard != null) PlayerHand.CardDeselected += CardDeselectedEventHandler;
            EnemyUnit.AttackValue.Killed += EnemyAttackKilledEventHandler;

            EndTurnButton.onClick.AddListener(EndTurnButtonClicked);
        }

        private void OnDisable()
        {
            PlayerHand.CardSelected -= CardSelectedEventHandler;
            if (PlayerHand.SelectedCard != null) PlayerHand.CardDeselected -= CardDeselectedEventHandler;
            EnemyUnit.AttackValue.Killed -= EnemyAttackKilledEventHandler;

            EndTurnButton.onClick.RemoveListener(EndTurnButtonClicked);
        }

        private void EndTurnButtonClicked()
        {
            PlayerHand.ResetSelections();
            _isPlayerTurn = false;
        }

        private void CardSelectedEventHandler(object sender, EventArgs _)
        {
            PlayerHand.CardDeselected += CardDeselectedEventHandler;
            KillableRegistry.Instance.Clicked += KillableClickedEventHandler;

            KillableRegistry.Instance.DisplayIndicators(true);
        }

        private void CardDeselectedEventHandler(object sender, EventArgs _)
        {
            PlayerHand.CardDeselected -= CardDeselectedEventHandler;
            KillableRegistry.Instance.Clicked -= KillableClickedEventHandler;

            KillableRegistry.Instance.DisplayIndicators(false);
        }

        private void EnemyAttackKilledEventHandler(object sender, EventArgs _) { _isGameWon = true; }

        private void KillableClickedEventHandler(object sender, EventArgs _)
        {
            if (!_isPlayerTurn) return;

            var killable = (Killable) sender;
            PlaySelectedCard(new Context { Target = killable });
        }

        private IEnumerator RunGame()
        {
            while (true)
            {
                PlayerMannaPool.ResetManna();
                UpdateMannaText();

                DrawCards();
                _isPlayerTurn = true;

                yield return new WaitUntil(() => IsPlayerTurnEnded);
                if (IsGameEnded) break;

                DiscardHand();
                EnemyUnit.Attack(PlayerUnit);

                if (IsGameEnded) break;
            }

            if (_isGameWon)
                yield return FullscreenAnnouncePanel.DisplayMessage(content: _winText, isGood: true);
            else if (IsGameLost)
                yield return FullscreenAnnouncePanel.DisplayMessage(content: _loseText, isGood: false);

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
