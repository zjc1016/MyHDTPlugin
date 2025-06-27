using HearthDb;
using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Controls;
using Hearthstone_Deck_Tracker.Enums;
using Hearthstone_Deck_Tracker.Hearthstone;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Card = Hearthstone_Deck_Tracker.Hearthstone.Card;
using Config = Hearthstone_Deck_Tracker.Config;
using CoreAPI = Hearthstone_Deck_Tracker.API.Core;


namespace MyHDTPlugin
{   
    class PetAction: IPetAction
    {
        private static MyPet Pet = null;
        private static ActivePlayer turn;
        public PetAction(MyPet pet)
        {
            Pet = pet;
            // Hide in menu, if necessary
            if (Config.Instance.HideInMenu && CoreAPI.Game.IsInMenu)
                Pet.Hide();
        }
        internal void InMenu()
        {
            if (Config.Instance.HideInMenu)
            {
                Pet.Hide();
            }
        }
        internal void TurnStart(ActivePlayer player)
        {
            turn = player;
            Pet.Update(player + "回合开始！", null);
        }

        internal void GameStart()
        {
            Pet.Update("游戏开始！", null);
        }

        public void OnPlayerPlay(Card card)
        {
            Pet.Update("你使用卡牌：" + GetCardByDbfId(card.DbfId).GetLocName(HearthDb.Enums.Locale.zhCN) + "!", null);
        }


        public void OnOpponentPlay(Card card)
        {
            Pet.Update("对手使用卡牌！", null);
        }

        public void Update()
        {
            Pet.Check();
        }
        private HearthDb.Card GetCardByDbfId(int DbfId)
        {
            var matched = HearthDb.Cards.All.Values.FirstOrDefault(c => c.DbfId == DbfId);
            return matched;
        }

        void IPetAction.OnPlayerDraw(Card card)
        {
            Pet.Update("你抽牌！", null);
        }

        void IPetAction.OnPlayerGet(Card card)
        {
            Pet.Update("你获得牌！"  , null);
        }

        void IPetAction.OnPlayerHandDiscard(Card card)
        {
            if (turn == ActivePlayer.Player)
            {
                Pet.Update("你在自己的回合弃牌！", null);
            }
            if (turn == ActivePlayer.Opponent)
            {
                Pet.Update("你在对手的回合弃牌！", null);
            }


        }

        void IPetAction.OnPlayerMulligan(Card card)
        {
            Pet.Update("你起手调度！", null);
        }

        void IPetAction.OnPlayerDeckDiscard(Card card)
        {
            if(turn == ActivePlayer.Player)
            {
                Pet.Update("你在自己的回合爆牌！", "爆牌.mp4");
            }
            if (turn == ActivePlayer.Opponent)
            {
                Pet.Update("你在对手的回合爆牌！", "爆牌.mp4");
            }

        }

        void IPetAction.OnPlayerPlayToDeck(Card card)
        {
            Pet.Update("你往牌库洗牌！", null);
        }

        void IPetAction.OnPlayerPlayToHand(Card card)
        {
            Pet.Update("你PlayToHand！"   , null);
        }

        void IPetAction.OnPlayerPlayToGraveyard(Card card)
        {
            Pet.Update("你PlayToGraveyard！", null);
        }

        void IPetAction.OnPlayerCreateInDeck(Card card)
        {
            Pet.Update("你CreateInDeck！", null);
        }

        void IPetAction.OnPlayerCreateInPlay(Card card)
        {
            Pet.Update("你CreateInPlay！", null);
        }

        void IPetAction.OnPlayerJoustReveal(Card card)
        {
            Pet.Update("你拼点！"   , null);
        }

        void IPetAction.OnPlayerDeckToPlay(Card card)
        {
            Pet.Update("DeckToPlay！", null);
        }

        void IPetAction.OnPlayerHeroPower()
        {
            Pet.Update("你使用英雄技能！", null);
        }

        void IPetAction.OnPlayerFatigue()
        {
            Pet.Update("你疲劳！", null);
        }

        void IPetAction.OnPlayerMinionMouseOver(Card card)
        {
            Pet.Update("你MinionMouseOver！", null);
        }

        void IPetAction.OnPlayerHandMouseOver(Card card)
        {
            Pet.Update("你HandMouseOver！", null);
        }

        void IPetAction.OnPlayerMinionAttack(Card attacker, Card defender)
        {
            Pet.Update("你使用随从攻击！", null);
        }

        void IPetAction.OnOpponentDraw()
        {
            Pet.Update("对手抽牌！", null);
        }

        void IPetAction.OnOpponentGet()
        {
            Pet.Update("对手获得牌！", null);
        }

        void IPetAction.OnOpponentHandDiscard(Card card)
        {
            
            if (turn == ActivePlayer.Opponent)
            {
                Pet.Update("对手在自己的回合弃牌！", null);
            }
            if (turn == ActivePlayer.Player)
            {
                Pet.Update("对手在你的回合弃牌！", null);
            }
        }

        void IPetAction.OnOpponentMulligan()
        {
            Pet.Update("对手起手调度！", null);
        }

        void IPetAction.OnOpponentDeckDiscard(Card card)
        {

            if (turn == ActivePlayer.Opponent)
            {
                Pet.Update("对手在自己的回合爆牌！", null);
            }
            if (turn == ActivePlayer.Player)
            {
                Pet.Update("对手在你的回合爆牌！", null);
            }
        }

        void IPetAction.OnOpponentPlayToDeck(Card card)
        {
            Pet.Update("对手PlayToDeck！", null);
        }

        void IPetAction.OnOpponentPlayToHand(Card card)
        {
            Pet.Update("对手PlayToHand！", null);
        }

        void IPetAction.OnOpponentPlayToGraveyard(Card card)
        {
            Pet.Update("对手PlayToGraveyard！" , null);
        }

        void IPetAction.OnOpponentCreateInDeck(Card card)
        {
            Pet.Update("对手CreateInDeck！"    , null);
        }

        void IPetAction.OnOpponentCreateInPlay(Card card)
        {
            Pet.Update("对手CreateInPlay！", null);
        }

        void IPetAction.OnOpponentJoustReveal(Card card)
        {
            Pet.Update("对手拼点！", null);
        }

        void IPetAction.OnOpponentDeckToPlay(Card card)
        {
            Pet.Update("对手DeckToPlay！", null);
        }

        void IPetAction.OnOpponentHeroPower()
        {
            Pet.Update("对手使用英雄技能！", null);
        }

        void IPetAction.OnOpponentFatigue()
        {
            Pet.Update("对手疲劳！", null);
        }

        void IPetAction.OnOpponentMinionMouseOver(Card card)
        {
            Pet.Update("对手MinionMouseOver！", null);
        }

        void IPetAction.OnOpponentMinionAttack(Card attacker, Card defender)
        {
            Pet.Update("对手使用随从攻击！", null);
        }

        void IPetAction.OnGameWon()
        {
            Pet.Update("你赢了!", "胜利.mp4");
        }

        void IPetAction.OnGameLost()
        {
            Pet.Update("你输了！", null);
        }

        void IPetAction.OnGameTied()
        {
            Pet.Update("平局！", null);
        }
    }
}