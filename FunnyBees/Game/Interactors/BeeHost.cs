﻿using FunnyBees.Engine;
using FunnyBees.Game.Components;

namespace FunnyBees.Game.Interactors
{
    public class BeeHost : Interactor, IInteractor<Bee, Beehive>, IInteractor<Beehive, Bee>
    {
        /// <summary>
        /// Пчела прилетела в улей.
        /// </summary>
        /// <param name="bee"></param>
        /// <param name="beehive"></param>
        public void Interact(Bee bee, Beehive beehive)
        {
            var connector = beehive.GetComponent<BeesOwner>();

            connector.AddBee(bee);
        }

        /// <summary>
        /// Пчела улетела из улья.
        /// </summary>
        /// <param name="component"></param>
        /// <param name="target"></param>
        public void Interact(Beehive component, Bee target)
        {
            throw new System.NotImplementedException();
        }
    }
}