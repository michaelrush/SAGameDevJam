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
    public abstract class Ability
    {
        public float coolDownDuration;
        public float coolDownRemaining;
        public bool ready;

        public Ability()
        {
            coolDownRemaining = 0;
            ready = true;
        }

        protected abstract void doAction(Vector2 position, double accuracy);

        public bool tryAction(Vector2 position, double accuracy)
        {
            if (ready)
            {
                coolDownRemaining = coolDownDuration;
                ready = false;
                doAction(position, accuracy);
                return true;
            }
            return false;
        }

        public void Update(GameTime gameTime)
        {
            coolDownRemaining = Math.Max(coolDownRemaining - gameTime.ElapsedGameTime.Milliseconds, 0);
            if (coolDownRemaining <= 0)
                ready = true;
        }
    }
}
