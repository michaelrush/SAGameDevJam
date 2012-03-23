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
    public class Spearman : Enemy
    {
        public Spearman(Vector2 position)
        {
            ContentManager content = new ContentManager(Game1.serviceProvider, "Content");
            this.abilities = new List<Ability> { new ThrowSpear() };
            this.position = position;
            this.velocity = new Vector2();
            this.speed = .225f;
            this.width = 25;
            this.height = 60;
            this.maxHealthPoints = 2;
            this.healthPoints = maxHealthPoints;
            this.accuracy = .9;
            this.texture = content.Load<Texture2D>("Textures/orc");
            this.collisionType = CollisionType.Enemy;
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);

            velocity = Vector2.Zero;
            prevPosition = position;
            float timeMod = gameTime.ElapsedGameTime.Milliseconds;

            velocity.X -= speed * timeMod;

            abilities[0].tryAction(position, accuracy);

            position += velocity;

            if (position.X - width > GameConstants.PlayableWidth ||
               position.X + width < GameConstants.PlayableArea.Left ||
               position.Y > GameConstants.PlayableArea.Bottom ||
               position.Y < GameConstants.PlayableArea.Top)
            {
                Level.removeEntity(this, false);
            }
        }

        public override bool collidesWith(Entity e)
        {
            return (e is Player || e is Fireball || e is Light);
        }

        public override void doCollision(Entity e)
        {
            if (e is Fireball || e is Light)
                healthPoints--;
        }

        public override void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            spriteBatch.Draw(texture, boundingRectangle, Color.White);
            base.Draw(spriteBatch, graphicsDevice);
        }
    }
}
