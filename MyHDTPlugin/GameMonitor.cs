using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHDTPlugin
{
    class GameMonitor : IGameMonitor
    {
        private readonly IPetAction _petaction;

        public GameMonitor(IPetAction playHandler)
        {
            _petaction = playHandler;
        }

        public void Initialize()
        {
            Hearthstone_Deck_Tracker.API.GameEvents.OnPlayerDraw.Add(_petaction.OnPlayerDraw);
            Hearthstone_Deck_Tracker.API.GameEvents.OnPlayerGet.Add(_petaction.OnPlayerGet);
            Hearthstone_Deck_Tracker.API.GameEvents.OnPlayerPlay.Add(_petaction.OnPlayerPlay);
            Hearthstone_Deck_Tracker.API.GameEvents.OnPlayerHandDiscard.Add(_petaction.OnPlayerHandDiscard);
            Hearthstone_Deck_Tracker.API.GameEvents.OnPlayerMulligan.Add(_petaction.OnPlayerMulligan); //调度，起手换牌
            Hearthstone_Deck_Tracker.API.GameEvents.OnPlayerDeckDiscard.Add(_petaction.OnPlayerDeckDiscard);
            Hearthstone_Deck_Tracker.API.GameEvents.OnPlayerPlayToDeck.Add(_petaction.OnPlayerPlayToDeck);
            Hearthstone_Deck_Tracker.API.GameEvents.OnPlayerPlayToHand.Add(_petaction.OnPlayerPlayToHand);
            Hearthstone_Deck_Tracker.API.GameEvents.OnPlayerPlayToGraveyard.Add(_petaction.OnPlayerPlayToGraveyard);
            Hearthstone_Deck_Tracker.API.GameEvents.OnPlayerCreateInDeck.Add(_petaction.OnPlayerCreateInDeck);
            Hearthstone_Deck_Tracker.API.GameEvents.OnPlayerCreateInPlay.Add(_petaction.OnPlayerCreateInPlay);
            Hearthstone_Deck_Tracker.API.GameEvents.OnPlayerJoustReveal.Add(_petaction.OnPlayerJoustReveal);
            Hearthstone_Deck_Tracker.API.GameEvents.OnPlayerDeckToPlay.Add(_petaction.OnPlayerDeckToPlay);
            Hearthstone_Deck_Tracker.API.GameEvents.OnPlayerHeroPower.Add(_petaction.OnPlayerHeroPower);
            // Fix for CS1503: Wrap the method group in a lambda to match the expected Action<int> signature
            Hearthstone_Deck_Tracker.API.GameEvents.OnPlayerFatigue.Add(fatigue => _petaction.OnPlayerFatigue());
            Hearthstone_Deck_Tracker.API.GameEvents.OnPlayerMinionMouseOver.Add(_petaction.OnPlayerMinionMouseOver);
            Hearthstone_Deck_Tracker.API.GameEvents.OnPlayerHandMouseOver.Add(_petaction.OnPlayerHandMouseOver);
            // Fix for CS1503: Wrap the method group in a lambda to match the expected Action<AttackInfo> signature
            Hearthstone_Deck_Tracker.API.GameEvents.OnPlayerMinionAttack.Add(attackInfo =>
                _petaction.OnPlayerMinionAttack(attackInfo.Attacker, attackInfo.Defender));
            Hearthstone_Deck_Tracker.API.GameEvents.OnOpponentDraw.Add(_petaction.OnOpponentDraw);
            Hearthstone_Deck_Tracker.API.GameEvents.OnOpponentGet.Add(_petaction.OnOpponentGet);
            Hearthstone_Deck_Tracker.API.GameEvents.OnOpponentPlay.Add(_petaction.OnOpponentPlay);
            Hearthstone_Deck_Tracker.API.GameEvents.OnOpponentHandDiscard.Add(_petaction.OnOpponentHandDiscard);
            Hearthstone_Deck_Tracker.API.GameEvents.OnOpponentMulligan.Add(_petaction.OnOpponentMulligan); //调度，起手换牌
            Hearthstone_Deck_Tracker.API.GameEvents.OnOpponentDeckDiscard.Add(_petaction.OnOpponentDeckDiscard);
            Hearthstone_Deck_Tracker.API.GameEvents.OnOpponentPlayToDeck.Add(_petaction.OnOpponentPlayToDeck);
            Hearthstone_Deck_Tracker.API.GameEvents.OnOpponentPlayToHand.Add(_petaction.OnOpponentPlayToHand);
            Hearthstone_Deck_Tracker.API.GameEvents.OnOpponentPlayToGraveyard.Add(_petaction.OnOpponentPlayToGraveyard);
            Hearthstone_Deck_Tracker.API.GameEvents.OnOpponentCreateInDeck.Add(_petaction.OnOpponentCreateInDeck);
            Hearthstone_Deck_Tracker.API.GameEvents.OnOpponentCreateInPlay.Add(_petaction.OnOpponentCreateInPlay);
            Hearthstone_Deck_Tracker.API.GameEvents.OnOpponentJoustReveal.Add(_petaction.OnOpponentJoustReveal);
            Hearthstone_Deck_Tracker.API.GameEvents.OnOpponentDeckToPlay.Add(_petaction.OnOpponentDeckToPlay);
            Hearthstone_Deck_Tracker.API.GameEvents.OnOpponentHeroPower.Add(_petaction.OnOpponentHeroPower);
            // Fix for CS1503: Wrap the method group in a lambda to match the expected Action<int> signature
            Hearthstone_Deck_Tracker.API.GameEvents.OnOpponentFatigue.Add(fatigue => _petaction.OnOpponentFatigue());
            Hearthstone_Deck_Tracker.API.GameEvents.OnOpponentMinionMouseOver.Add(_petaction.OnOpponentMinionMouseOver);
            // Fix for CS1503: Wrap the method group in a lambda to match the expected Action<AttackInfo> signature
            Hearthstone_Deck_Tracker.API.GameEvents.OnOpponentMinionAttack.Add(attackInfo =>
                _petaction.OnOpponentMinionAttack(attackInfo.Attacker, attackInfo.Defender));
            Hearthstone_Deck_Tracker.API.GameEvents.OnGameWon.Add(_petaction.OnGameWon);
            Hearthstone_Deck_Tracker.API.GameEvents.OnGameLost.Add(_petaction.OnGameLost);
            Hearthstone_Deck_Tracker.API.GameEvents.OnGameLost.Add(_petaction.OnGameLost);


        }

        public void Update()
        {
            _petaction.Update();
        }
    }
}