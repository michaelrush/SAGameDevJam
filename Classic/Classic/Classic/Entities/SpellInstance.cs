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
    public abstract class SpellInstance : Projectile
    {
        public float duration;

        public SpellInstance(Vector2 position, float speed, float duration, double angle)
            : base(position, speed, angle)
        {
            this.duration = duration;
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            duration -= gameTime.ElapsedGameTime.Milliseconds;
            if (duration <= 0)
                Level.removeEntity(this, true);
            base.Update(gameTime);
        }
    }
}
