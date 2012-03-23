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
    public abstract class Projectile : MovableEntity
    {
        public double angle = 0; //radians
        public double accuracy = 1; // 0-1, 1 being perfect accuracy

        public Projectile(Vector2 position, float speed, double angle)
        {
            this.position = position;
            this.speed = speed;
            this.angle = angle;
            this.collisionType = CollisionType.Projectile;
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            prevPosition = position;
            float offset = speed * gameTime.ElapsedGameTime.Milliseconds;
            velocity.X = (float) Math.Cos(angle) * offset;
            velocity.Y = (float) Math.Sin(angle) * offset;

            position += velocity;

            if (position.X - width > GameConstants.PlayableWidth ||
               position.X + width < GameConstants.PlayableArea.Left ||
               position.Y > GameConstants.PlayableArea.Bottom ||
               position.Y < GameConstants.PlayableArea.Top)
            {
                if(!(this is Light))
                    Level.removeEntity(this, false);
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            spriteBatch.Draw(texture, boundingRectangle, Color.White);
        }
    }
}
