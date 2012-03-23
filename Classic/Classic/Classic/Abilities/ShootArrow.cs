using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Classic
{
    public class ShootArrow : Attack
    {
        public ShootArrow()
        {
            coolDownDuration = 500;
            speed = -.75f;
        }

        protected override void doAction(Vector2 position, double accuracy)
        {
            double angle = (GameConstants.random.NextDouble() * 2 - 1) * (1 - accuracy) * Math.PI / 2;
            Level.addEntity(new Arrow(position, speed, angle));
        }
    }
}
