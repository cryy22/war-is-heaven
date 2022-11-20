using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using WarIsHeaven.Actions;
using WarIsHeaven.Cards;
using WarIsHeaven.Cards.Decks;
using WarIsHeaven.Constants;
using WarIsHeaven.GameResources;
using WarIsHeaven.Killables;
using WarIsHeaven.SceneControls;
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
        [SerializeField] private UIFullscreenAnnouncePanel FullscreenAnnouncePanel;
        [SerializeField] private ActiveSceneConfigDatasource SceneConfigDatasource;
        [SerializeField] private KillableRegistry KillableRegistry;

        [SerializeField] private int DrawsPerTurn;
        [SerializeField] private MannaPool PlayerMannaPool;

        private bool _isPlayerTurn;
        private bool _isCardPlaying;

        private bool _isGameWon;
        private bool _isGameLost;

        private int _manna;

        private bool IsPlayerTurnEnded
        {
            get
            {
                if (_isCardPlaying) return false;
                return !_isPlayerTurn || IsGameEnded;
            }
        }

        private bool IsGameEnded => _isGameLost || _isGameWon;

        private void OnEnable()
        {
            PlayerHand.CardSelected += CardSelectedEventHandler;
            if (PlayerHand.SelectedCard != null) PlayerHand.CardDeselected += CardDeselectedEventHandler;
            PlayerUnit.Health.Killed += UnitHealthKilledEventHandler;
            EnemyUnit.Health.Killed += UnitHealthKilledEventHandler;
            EnemyUnit.Neutralized += NeutralizedEventHandler;

            EndTurnButton.onClick.AddListener(EndTurnButtonClicked);
        }

        private void OnDisable()
        {
            PlayerHand.CardSelected -= CardSelectedEventHandler;
            if (PlayerHand.SelectedCard != null) PlayerHand.CardDeselected -= CardDeselectedEventHandler;
            PlayerUnit.Health.Killed -= UnitHealthKilledEventHandler;
            EnemyUnit.Health.Killed -= UnitHealthKilledEventHandler;
            EnemyUnit.Neutralized -= NeutralizedEventHandler;

            EndTurnButton.onClick.RemoveListener(EndTurnButtonClicked);
        }

        public void BeginGame() { StartCoroutine(RunGame()); }

        private void EndTurnButtonClicked()
        {
            PlayerHand.ResetSelections();
            _isPlayerTurn = false;
        }

        private void CardSelectedEventHandler(object sender, EventArgs _)
        {
            PlayerHand.CardDeselected += CardDeselectedEventHandler;
            KillableRegistry.Clicked += KillableClickedEventHandler;

            KillableRegistry.DisplayIndicators(true);
        }

        private void CardDeselectedEventHandler(object sender, EventArgs _)
        {
            PlayerHand.CardDeselected -= CardDeselectedEventHandler;
            KillableRegistry.Clicked -= KillableClickedEventHandler;

            KillableRegistry.DisplayIndicators(false);
        }

        private void NeutralizedEventHandler(object sender, EventArgs _) { _isGameWon = true; }

        private void UnitHealthKilledEventHandler(object sender, EventArgs _) { _isGameLost = true; }

        private void KillableClickedEventHandler(object sender, EventArgs _)
        {
            if (!_isPlayerTurn) return;

            var target = (Killable) sender;
            StartCoroutine(PlaySelectedCard(BuildContext(target)));
        }

        private IEnumerator RunGame()
        {
            while (true)
            {
                PlayerMannaPool.ResetManna();

                yield return DrawCards();
                EnemyUnit.CreateIntent();

                _isPlayerTurn = true;

                yield return new WaitUntil(() => IsPlayerTurnEnded);
                yield return InvokePoison(PlayerUnit);
                if (IsGameEnded) break;

                yield return DiscardHand();

                yield return EnemyUnit.TakeTurn(BuildContext(target: null));
                if (IsGameEnded) break;
                yield return InvokePoison(EnemyUnit);
            }

            if (_isGameWon)
            {
                SceneConfigDatasource.IncrementCombatConfig();
                yield return FullscreenAnnouncePanel.DisplayMessage(content: _winText, isGood: true);
            }
            else if (_isGameLost)
            {
                yield return FullscreenAnnouncePanel.DisplayMessage(content: _loseText, isGood: false);
            }

            int nextScene = Scenes.CombatIndex;
            if (SceneConfigDatasource.ActiveCombatConfig == null)
            {
                nextScene = Scenes.TitleIndex;
                SceneConfigDatasource.SetCombatConfigIndex(0);
            }

            SceneManager.LoadScene(nextScene);
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
            card.Play(context);
            PlayerMannaPool.SpendManna(card.MannaCost);

            yield return new WaitForSeconds(0.33f);

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

        private Context BuildContext(Killable target)
        {
            return new Context
            {
                Target = target,
                PlayerUnit = PlayerUnit,
                EnemyUnit = EnemyUnit,
            };
        }

        private IEnumerator InvokePoison(Unit unit)
        {
            if (unit.PoisonedStatus != null)
            {
                yield return unit.PoisonedStatus.Animate();
                unit.PoisonedStatus.Invoke();
            }
        }
    }
}
