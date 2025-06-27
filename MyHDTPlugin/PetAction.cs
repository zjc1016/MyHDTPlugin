using HearthDb;
using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Controls;
using Hearthstone_Deck_Tracker.Enums;
using Hearthstone_Deck_Tracker.Hearthstone;
using Hearthstone_Deck_Tracker.Hearthstone.RelatedCardsSystem;
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
            Pet.Update("你使用卡牌：\n" + GetCardByDbfId(card.DbfId).GetLocName(HearthDb.Enums.Locale.zhCN) , null);
        }


        public void OnOpponentPlay(Card card)
        {
            Pet.Update("对手使用卡牌：\n" + GetCardByDbfId(card.DbfId).GetLocName(HearthDb.Enums.Locale.zhCN) , null);
        }

        public void Update()
        {
            var opp = CoreAPI.Game.Opponent.PlayerEntities.ToList();
            //var player = CoreAPI.Game.Player.Minions.ToList();
            var player = CoreAPI.Game.Player.PlayerEntities.ToList();
            Pet.Check(opp,player);
        }
        private HearthDb.Card GetCardByDbfId(int DbfId)
        {
            var matched = HearthDb.Cards.All.Values.FirstOrDefault(c => c.DbfId == DbfId);
            return matched;
        }

        void IPetAction.OnPlayerDraw(Card card)
        {
            Pet.Update("你抽牌！\n" + GetCardByDbfId(card.DbfId).GetLocName(HearthDb.Enums.Locale.zhCN), null);
        }

        void IPetAction.OnPlayerGet(Card card)
        {
            Pet.Update("你获得牌！\n" + GetCardByDbfId(card.DbfId).GetLocName(HearthDb.Enums.Locale.zhCN), null);
        }

        void IPetAction.OnPlayerHandDiscard(Card card)
        {
            if (turn == ActivePlayer.Player)
            {
                Pet.Update("你在自己的回合弃牌！\n" + GetCardByDbfId(card.DbfId).GetLocName(HearthDb.Enums.Locale.zhCN), null);
            }
            if (turn == ActivePlayer.Opponent)
            {
                Pet.Update("你在对手的回合弃牌！\n" + GetCardByDbfId(card.DbfId).GetLocName(HearthDb.Enums.Locale.zhCN), null);
            }


        }

        void IPetAction.OnPlayerMulligan(Card card)
        {
            Pet.Update("你起手调度！\n" + GetCardByDbfId(card.DbfId).GetLocName(HearthDb.Enums.Locale.zhCN), null);
        }

        void IPetAction.OnPlayerDeckDiscard(Card card)
        {
            if(turn == ActivePlayer.Player)
            {
                Pet.Update("你在自己的回合爆牌！\n" + GetCardByDbfId(card.DbfId).GetLocName(HearthDb.Enums.Locale.zhCN), "爆牌.mp4");
            }
            if (turn == ActivePlayer.Opponent)
            {
                Pet.Update("你在对手的回合爆牌！\n" + GetCardByDbfId(card.DbfId).GetLocName(HearthDb.Enums.Locale.zhCN), "爆牌.mp4");
            }

        }

        void IPetAction.OnPlayerPlayToDeck(Card card)
        {
            Pet.Update("你往牌库洗牌！\n" + GetCardByDbfId(card.DbfId).GetLocName(HearthDb.Enums.Locale.zhCN), null);
        }

        void IPetAction.OnPlayerPlayToHand(Card card)
        {
            Pet.Update("你PlayToHand！\n" + GetCardByDbfId(card.DbfId).GetLocName(HearthDb.Enums.Locale.zhCN)   , null);
        }

        void IPetAction.OnPlayerPlayToGraveyard(Card card)
        {
            Pet.Update("你PlayToGraveyard！\n" + GetCardByDbfId(card.DbfId).GetLocName(HearthDb.Enums.Locale.zhCN), null);
        }

        void IPetAction.OnPlayerCreateInDeck(Card card)
        {
            Pet.Update("你CreateInDeck！\n" + GetCardByDbfId(card.DbfId).GetLocName(HearthDb.Enums.Locale.zhCN), null);
        }

        void IPetAction.OnPlayerCreateInPlay(Card card)
        {
            Pet.Update("你CreateInPlay！\n" + GetCardByDbfId(card.DbfId).GetLocName(HearthDb.Enums.Locale.zhCN), null);
        }

        void IPetAction.OnPlayerJoustReveal(Card card)
        {
            Pet.Update("你拼点！\n" + GetCardByDbfId(card.DbfId).GetLocName(HearthDb.Enums.Locale.zhCN)   , null);
        }

        void IPetAction.OnPlayerDeckToPlay(Card card)
        {
            Pet.Update("DeckToPlay！\n" + GetCardByDbfId(card.DbfId).GetLocName(HearthDb.Enums.Locale.zhCN), null);
        }

        void IPetAction.OnPlayerHeroPower()
        {
            Pet.Update("你使用英雄技能！\n", null);
        }

        void IPetAction.OnPlayerFatigue()
        {
            Pet.Update("你疲劳！\n", null);
        }

        void IPetAction.OnPlayerMinionMouseOver(Card card)
        {
            Pet.Update("你MinionMouseOver！\n" + GetCardByDbfId(card.DbfId).GetLocName(HearthDb.Enums.Locale.zhCN), null);
        }

        void IPetAction.OnPlayerHandMouseOver(Card card)
        {
            Pet.Update("你HandMouseOver！\n" + GetCardByDbfId(card.DbfId).GetLocName(HearthDb.Enums.Locale.zhCN), null);
        }

        void IPetAction.OnPlayerMinionAttack(Card attacker, Card defender)
        {
            Pet.Update("你使用随从攻击！\n" + GetCardByDbfId(attacker.DbfId).GetLocName(HearthDb.Enums.Locale.zhCN) +"->"+ GetCardByDbfId(defender.DbfId).GetLocName(HearthDb.Enums.Locale.zhCN), null);
        }

        void IPetAction.OnOpponentDraw()
        {
            Pet.Update("对手抽牌！\n", null);
        }

        void IPetAction.OnOpponentGet()
        {
            Pet.Update("对手获得牌！\n", null);
        }

        void IPetAction.OnOpponentHandDiscard(Card card)
        {
            
            if (turn == ActivePlayer.Opponent)
            {
                Pet.Update("对手在自己的回合弃牌！\n" + GetCardByDbfId(card.DbfId).GetLocName(HearthDb.Enums.Locale.zhCN), null);
            }
            if (turn == ActivePlayer.Player)
            {
                Pet.Update("对手在你的回合弃牌！\n" + GetCardByDbfId(card.DbfId).GetLocName(HearthDb.Enums.Locale.zhCN), null);
            }
        }

        void IPetAction.OnOpponentMulligan()
        {
            Pet.Update("对手起手调度！\n", null);
        }

        void IPetAction.OnOpponentDeckDiscard(Card card)
        {

            if (turn == ActivePlayer.Opponent)
            {
                Pet.Update("对手在自己的回合爆牌！\n" + GetCardByDbfId(card.DbfId).GetLocName(HearthDb.Enums.Locale.zhCN), null);
            }
            if (turn == ActivePlayer.Player)
            {
                Pet.Update("对手在你的回合爆牌！\n" + GetCardByDbfId(card.DbfId).GetLocName(HearthDb.Enums.Locale.zhCN), null);
            }
        }

        void IPetAction.OnOpponentPlayToDeck(Card card)
        {
            Pet.Update("对手PlayToDeck！\n" + GetCardByDbfId(card.DbfId).GetLocName(HearthDb.Enums.Locale.zhCN), null);
        }

        void IPetAction.OnOpponentPlayToHand(Card card)
        {
            Pet.Update("对手PlayToHand！\n" + GetCardByDbfId(card.DbfId).GetLocName(HearthDb.Enums.Locale.zhCN), null);
        }

        void IPetAction.OnOpponentPlayToGraveyard(Card card)
        {
            Pet.Update("对手PlayToGraveyard！\n" + GetCardByDbfId(card.DbfId).GetLocName(HearthDb.Enums.Locale.zhCN), null);
        }

        void IPetAction.OnOpponentCreateInDeck(Card card)
        {
            Pet.Update("对手CreateInDeck！\n" + GetCardByDbfId(card.DbfId).GetLocName(HearthDb.Enums.Locale.zhCN), null);
        }

        void IPetAction.OnOpponentCreateInPlay(Card card)
        {
            Pet.Update("对手CreateInPlay！\n" + GetCardByDbfId(card.DbfId).GetLocName(HearthDb.Enums.Locale.zhCN), null);
        }

        void IPetAction.OnOpponentJoustReveal(Card card)
        {
            Pet.Update("对手拼点！\n" + GetCardByDbfId(card.DbfId).GetLocName(HearthDb.Enums.Locale.zhCN), null);
        }

        void IPetAction.OnOpponentDeckToPlay(Card card)
        {
            Pet.Update("对手DeckToPlay！\n" + GetCardByDbfId(card.DbfId).GetLocName(HearthDb.Enums.Locale.zhCN), null);
        }

        void IPetAction.OnOpponentHeroPower()
        {
            Pet.Update("对手使用英雄技能！\n", null);
        }

        void IPetAction.OnOpponentFatigue()
        {
            Pet.Update("对手疲劳！\n", null);
        }

        void IPetAction.OnOpponentMinionMouseOver(Card card)
        {
            Pet.Update("你OpponnentMinionMouseOver！\n" + GetCardByDbfId(card.DbfId).GetLocName(HearthDb.Enums.Locale.zhCN), null);
        }

        void IPetAction.OnOpponentMinionAttack(Card attacker, Card defender)
        {
            Pet.Update("对手使用随从攻击！\n" + GetCardByDbfId(attacker.DbfId).GetLocName(HearthDb.Enums.Locale.zhCN) + "->" + GetCardByDbfId(defender.DbfId).GetLocName(HearthDb.Enums.Locale.zhCN), null);
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