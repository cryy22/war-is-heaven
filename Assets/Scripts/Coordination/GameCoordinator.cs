using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using WarIsHeaven.Cards;
using WarIsHeaven.Cards.CardActions;
using WarIsHeaven.GameResources;
using WarIsHeaven.Killables;
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
        [SerializeField] private Deck PlayedCardSlot;
        [SerializeField] private Deck Discard;
        [SerializeField] private TMP_Text MannaText;
        [SerializeField] private UIFullscreenAnnouncePanel FullscreenAnnouncePanel;

        [SerializeField] private int DrawsPerTurn;
        [SerializeField] private MannaPool PlayerMannaPool;

        private bool _isPlayerTurn;
        private bool _isCardPlaying;
        private bool _isGameWon;

        private int _manna;

        private bool IsGameLost => PlayerUnit.Health <= 0 || EnemyUnit.Health <= 0;

        private bool IsPlayerTurnEnded
        {
            get
            {
                if (_isCardPlaying) return false;
                return !_isPlayerTurn || IsGameEnded;
            }
        }

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
            StartCoroutine(PlaySelectedCard(new Context { Target = killable }));
        }

        private IEnumerator RunGame()
        {
            while (true)
            {
                PlayerMannaPool.ResetManna();
                UpdateMannaText();

                yield return DrawCards();
                EnemyUnit.CreateIntent();

                _isPlayerTurn = true;

                yield return new WaitUntil(() => IsPlayerTurnEnded);
                if (IsGameEnded) break;
                yield return DiscardHand();

                yield return EnemyUnit.TakeTurn(PlayerUnit);
                if (IsGameEnded) break;
            }

            if (_isGameWon)
                yield return FullscreenAnnouncePanel.DisplayMessage(content: _winText, isGood: true);
            else if (IsGameLost)
                yield return FullscreenAnnouncePanel.DisplayMessage(content: _loseText, isGood: false);

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private IEnumerator DrawCards()
        {
            for (var i = 0; i < DrawsPerTurn; i++)
            {
                if (Deck.Count == 0)
                {
                    if (Discard.Count == 0) yield break;

                    Discard.Shuffle();
                    while (Discard.Count > 0) yield return Deck.AddCard(Discard.TakeCard());
                }

                Card card = Deck.TakeCard();
                card.Flip(Card.SideType.Front);
                yield return PlayerHand.AddCard(card);
            }
        }

        private IEnumerator PlaySelectedCard(Context context)
        {
            _isCardPlaying = true;

            Card card = PlayerHand.SelectedCard;
            PlayerHand.RemoveSelectedCard();

            yield return PlayedCardSlot.AddCard(card);
            yield return new WaitForSeconds(0.33f);

            card.Play(context);
            PlayerMannaPool.SpendManna(card.MannaCost);
            UpdateMannaText();

            PlayedCardSlot.RemoveCard(card);
            card.SetSelected(false);
            card.Flip(Card.SideType.Back);
            yield return Discard.AddCard(card);

            _isCardPlaying = false;
        }

        private IEnumerator DiscardHand()
        {
            while (PlayerHand.Count > 0)
            {
                Card card = PlayerHand.TakeCard();
                card.Flip(Card.SideType.Back);
                yield return Discard.AddCard(card);
            }
        }

        private void UpdateMannaText() { MannaText.text = $"Manna: {PlayerMannaPool.GetMannaString()}"; }
    }
}
