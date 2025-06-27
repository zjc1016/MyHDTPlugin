using Hearthstone_Deck_Tracker.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHDTPlugin
{
    interface IPetAction : IUpdateable
    {
        void OnPlayerDraw(Hearthstone_Deck_Tracker.Hearthstone.Card card);
        void OnPlayerGet(Hearthstone_Deck_Tracker.Hearthstone.Card card);
        void OnPlayerPlay(Hearthstone_Deck_Tracker.Hearthstone.Card card);
        void OnPlayerHandDiscard(Hearthstone_Deck_Tracker.Hearthstone.Card card);
        void OnPlayerMulligan(Hearthstone_Deck_Tracker.Hearthstone.Card card);
        void OnPlayerDeckDiscard(Hearthstone_Deck_Tracker.Hearthstone.Card card);
        void OnPlayerPlayToDeck(Hearthstone_Deck_Tracker.Hearthstone.Card card);
        void OnPlayerPlayToHand(Hearthstone_Deck_Tracker.Hearthstone.Card card);
        void OnPlayerPlayToGraveyard(Hearthstone_Deck_Tracker.Hearthstone.Card card);
        void OnPlayerCreateInDeck(Hearthstone_Deck_Tracker.Hearthstone.Card card);
        void OnPlayerCreateInPlay(Hearthstone_Deck_Tracker.Hearthstone.Card card);
        void OnPlayerJoustReveal(Hearthstone_Deck_Tracker.Hearthstone.Card card);
        void OnPlayerDeckToPlay(Hearthstone_Deck_Tracker.Hearthstone.Card card);
        void OnPlayerHeroPower();
        void OnPlayerFatigue();
        void OnPlayerMinionMouseOver(Hearthstone_Deck_Tracker.Hearthstone.Card card);
        void OnPlayerHandMouseOver(Hearthstone_Deck_Tracker.Hearthstone.Card card);
        void OnPlayerMinionAttack(Hearthstone_Deck_Tracker.Hearthstone.Card attacker, Hearthstone_Deck_Tracker.Hearthstone.Card defender);

        void OnOpponentDraw();
        void OnOpponentGet();
        void OnOpponentPlay(Hearthstone_Deck_Tracker.Hearthstone.Card card);
        void OnOpponentHandDiscard(Hearthstone_Deck_Tracker.Hearthstone.Card card);
        void OnOpponentMulligan();
        void OnOpponentDeckDiscard(Hearthstone_Deck_Tracker.Hearthstone.Card card);
        void OnOpponentPlayToDeck(Hearthstone_Deck_Tracker.Hearthstone.Card card);
        void OnOpponentPlayToHand(Hearthstone_Deck_Tracker.Hearthstone.Card card);
        void OnOpponentPlayToGraveyard(Hearthstone_Deck_Tracker.Hearthstone.Card card);
        void OnOpponentCreateInDeck(Hearthstone_Deck_Tracker.Hearthstone.Card card);
        void OnOpponentCreateInPlay(Hearthstone_Deck_Tracker.Hearthstone.Card card);
        void OnOpponentJoustReveal(Hearthstone_Deck_Tracker.Hearthstone.Card card);
        void OnOpponentDeckToPlay(Hearthstone_Deck_Tracker.Hearthstone.Card card);
        void OnOpponentHeroPower();
        void OnOpponentFatigue();
        void OnOpponentMinionMouseOver(Hearthstone_Deck_Tracker.Hearthstone.Card card);
        void OnOpponentMinionAttack(Hearthstone_Deck_Tracker.Hearthstone.Card attacker, Hearthstone_Deck_Tracker.Hearthstone.Card defender);

        void OnGameWon();
        void OnGameLost();
        void OnGameTied();


    }
}   

