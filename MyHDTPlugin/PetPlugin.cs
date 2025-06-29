using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Enums;
using Hearthstone_Deck_Tracker.Hearthstone;
using Hearthstone_Deck_Tracker.Hearthstone.RelatedCardsSystem.Cards.Hunter;
using Hearthstone_Deck_Tracker.Plugins;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MyHDTPlugin
{
    public class Class1Plugin : IPlugin 
    {
        private MyPet Pet;
        private readonly IGameMonitor _gameMonitor;

        public Class1Plugin()
        {
            Pet = new MyPet();
            var petaction = new PetAction(Pet);
            _gameMonitor = new GameMonitor(petaction);
            Core.OverlayCanvas.Children.Add(Pet);
            GameEvents.OnGameStart.Add(petaction.GameStart);
            GameEvents.OnTurnStart.Add(petaction.TurnStart);
            GameEvents.OnInMenu.Add(petaction.InMenu);
        }
        public void OnLoad()
        {
            _gameMonitor.Initialize();


            // Triggered upon startup and when the user ticks the plugin on
        }

        public void OnUnload()
        {
            Core.OverlayCanvas.Children.Remove(Pet);
            // Triggered when the user unticks the plugin, however, HDT does not completely unload the plugin.
            // see https://git.io/vxEcH
        }

        public void OnButtonPress()
        {
            // Triggered when the user clicks your button in the plugin list
        }

        public void OnUpdate()
        {
            _gameMonitor.Update();
            // called every ~100ms
        }

        public string Name => "宠物";

        public string Description => "宠物插件";

        public string ButtonText => "";

        public string Author => "失智香风乃";

        public Version Version => new Version(0, 0, 1);

        public MenuItem MenuItem => null;
    }
    public class Class1
    {
        internal static void TurnStart(ActivePlayer player)
        {


        }

        internal static void GameStart()
        {
        }
    }
}
