using HearthDb;
using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Controls;
using Hearthstone_Deck_Tracker.Enums;
using Hearthstone_Deck_Tracker.Enums.Hearthstone;
using Hearthstone_Deck_Tracker.Hearthstone;
using Hearthstone_Deck_Tracker.Hearthstone.RelatedCardsSystem;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
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
        public class CardMatchRule
        {
            public string match { get; set; }
            public string type { get; set; }  // "完全相同", "名称包含"
            public string who { get; set; } //谁打出的
            public string video { get; set; } //视频名称

        }
        private List<CardMatchRule> cardMatchRules;

        public void LoadCardMatchRules(string path)
        {
            var json = File.ReadAllText(path);
            cardMatchRules = JsonConvert.DeserializeObject<List<CardMatchRule>>(json);
        }

        public void CheckRule(Card card, ActivePlayer turn)
        {
            string cardName = GetCardByDbfId(card.DbfId).GetLocName(HearthDb.Enums.Locale.zhCN);

            foreach (var rule in cardMatchRules)
            {
                // 判断适用人群
                if (!((rule.who == "对方" && turn == ActivePlayer.Opponent) || (rule.who == "自己" && turn == ActivePlayer.Player) || rule.who == "双方"))
                    continue;

                // 判断匹配方式
                switch (rule.type)
                {
                    case "完全相同":
                        if (cardName == rule.match)
                        {
                            Pet.Update(rule.video, rule.video);
                            return;
                        }
                        break;

                    case "名称包含":
                        if (cardName.Contains(rule.match))
                        {
                            Pet.Update(rule.video, rule.video);
                            return;
                        }
                        break;

                    case "特殊":
                        if (rule.match == "#highHealthLowAttack" && card.Health >= 15 && card.Attack <= 5)
                        {
                            Pet.Update(rule.video, "超大型屁股");
                            return;
                        }
                        break;
                }
            }

            // 默认提示
            Pet.Update("你使用卡牌：\n" + cardName, null);

        }

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
            string dllDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string path = Path.Combine(dllDir, "rules.json");
            LoadCardMatchRules(path);

            turn = player;
            Pet.Update(player + "回合开始！", null);
            var opp_entity = CoreAPI.Game.Opponent.PlayerEntities.ToList();
            //var player = CoreAPI.Game.Player.Minions.ToList();
            var player_entity = CoreAPI.Game.Player.PlayerEntities.ToList();

            if (turn == ActivePlayer.Player && player_entity.Any(entity =>
                entity.IsInPlay &&
                GetCardByDbfId(entity.Card.DbfId)?.GetLocName(HearthDb.Enums.Locale.zhCN) == "水元素"))
            {
                Pet.Update("水元素冻住还能动吗", "水元素冻住还能动吗");
            }
            else
            {
                Pet.CheckAttack(opp_entity, player_entity, turn);
            }






        }

        internal void GameStart()
        {
            Pet.Update("游戏开始！", "main");
            string dllDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string Path = System.IO.Path.Combine(dllDir, "rules.jpg");
            LoadCardMatchRules(Path);
        }

        public void OnPlayerPlay(Card card)
        {
            CheckRule(card, turn);
            //if (GetCardByDbfId(card.DbfId).GetLocName(HearthDb.Enums.Locale.zhCN) == "亵渎")
            //{
            //    Pet.Update("教科书般的亵渎！", "教科书般的亵渎");
            //}
            //else if (GetCardByDbfId(card.DbfId).GetLocName(HearthDb.Enums.Locale.zhCN) == "骨鸡蛋") {
            //    Pet.Update("亡灵小鸡是亡灵吗？", "亡灵小鸡是亡灵吗");
            //}
            if (card.Health >= 15 && card.Attack <= 5)
            {
                Pet.Update("你打出了一个超大型屁股！", "超大型屁股");
            }
            //else if (GetCardByDbfId(card.DbfId).GetLocName(HearthDb.Enums.Locale.zhCN).Contains("铜须"))
            //{
            //    Pet.Update("铜须是生物吗？", "铜须是生物吗");
            //}
            //else
            //{
            //    Pet.Update("你使用卡牌：\n" + GetCardByDbfId(card.DbfId).GetLocName(HearthDb.Enums.Locale.zhCN), null);
            //}
        }


        public void OnOpponentPlay(Card card)
        {
            CheckRule(card, turn);

            //if (GetCardByDbfId(card.DbfId).GetLocName(HearthDb.Enums.Locale.zhCN) == "骨鸡蛋")
            //{
            //    Pet.Update("亡灵小鸡是亡灵吗？", "亡灵小鸡是亡灵吗");
            //}
            //else if (GetCardByDbfId(card.DbfId).GetLocName(HearthDb.Enums.Locale.zhCN).Contains("铜须"))
            //{
            //    Pet.Update("铜须是生物吗？", "铜须是生物吗");
            //}
            //else
            //{
            //    Pet.Update("对手使用卡牌：\n" + GetCardByDbfId(card.DbfId).GetLocName(HearthDb.Enums.Locale.zhCN), null);
            //}
                
        }

        public void Update()
        {
            var opp = CoreAPI.Game.Opponent.PlayerEntities.ToList();
            //var player = CoreAPI.Game.Player.Minions.ToList();
            var player = CoreAPI.Game.Player.PlayerEntities.ToList();
            Pet.Check(opp,player, turn);
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
                Pet.Update("你在自己的回合爆牌！\n" + GetCardByDbfId(card.DbfId).GetLocName(HearthDb.Enums.Locale.zhCN), "失误");
            }
            if (turn == ActivePlayer.Opponent)
            {
                Pet.Update("你在对手的回合爆牌！\n" + GetCardByDbfId(card.DbfId).GetLocName(HearthDb.Enums.Locale.zhCN), "失误");
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
            Pet.Update("你赢了!", "胜利");
        }

        void IPetAction.OnGameLost()
        {
            if(turn == ActivePlayer.Player)
            {
                Pet.Update("你在自己的回合输了！", "在自己的回合输了");
            }
            else
            {
                Pet.Update("你输了！", "失败");
            }
                
        }

        void IPetAction.OnGameTied()
        {
            Pet.Update("平局！", null);
        }
    }
}