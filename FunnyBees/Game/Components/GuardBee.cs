﻿using FunnyBees.Engine;

namespace FunnyBees.Game.Components
{
    public class GuardBee : Component<Bee>
    {
        private readonly Beehive beehive;

        public GuardBee(Beehive beehive)
        {
            this.beehive = beehive;
        }
    }
}