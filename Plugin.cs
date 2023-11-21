﻿using Rocket.Core.Plugins;
using SDG.Unturned;
using Rocket.Unturned.Player;
using UnityEngine;
using Rocket.Unturned;
using Rocket.API.Collections;

namespace DarkerJoinMessage
{
    public class Plugin : RocketPlugin
    {
        public static Plugin Instance;
        public override TranslationList DefaultTranslations => new TranslationList {
            { "JoinMessage", "{0} зашел на сервер!" },
            { "LeaveMessage", "{0} вышел с сервера!" },
        };
        protected override void Load()
        {
            Instance = this;
            U.Events.OnPlayerConnected += Events_OnPlayerConnected;
            U.Events.OnPlayerDisconnected += Events_OnPlayerDisconnected;
        }

        private void Events_OnPlayerDisconnected(UnturnedPlayer player)
        {
            foreach (var p in Provider.clients)
            {
                ChatManager.serverSendMessage(Translate("LeaveMessage", player.DisplayName), Color.green, null, p, EChatMode.LOCAL);
            }
        }

        private void Events_OnPlayerConnected(UnturnedPlayer player)
        {
            foreach(var p in Provider.clients)
            {
                ChatManager.serverSendMessage(Translate("JoinMessage", player.DisplayName),Color.green,null,p, EChatMode.LOCAL);
            }
        }





        protected override void Unload()
        {
            U.Events.OnPlayerConnected -= Events_OnPlayerConnected;
            U.Events.OnPlayerDisconnected -= Events_OnPlayerDisconnected;
        }

    }
}