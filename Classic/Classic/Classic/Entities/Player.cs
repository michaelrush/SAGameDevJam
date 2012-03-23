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
    public class Player : Actor
    {
        private KeyboardState keyboardState;
        private KeyboardState oldKeyboardState;

        public Player(Vector2 position)
        {
            ContentManager content = new ContentManager(Game1.serviceProvider, "Content");
            this.position = position;
            this.abilities = new List<Ability> { new LightSpell(), new FireballSpell() };
            velocity = new Vector2();
            height = 50;
            width = 50;
            speed = .5f;
            maxHealthPoints = 6;
            healthPoints = maxHealthPoints;
            invTimer = 0; 
            texture = content.Load<Texture2D>("Textures/wizard");
            collisionType = CollisionType.Player;
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
            oldKeyboardState = keyboardState;
            float timeMod = gameTime.ElapsedGameTime.Milliseconds;
            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                velocity.X -= speed * timeMod;
            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                velocity.X += speed * timeMod;
            }
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                velocity.Y -= speed * timeMod;
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                velocity.Y += speed * timeMod;
            }
            if (CheckKey(Keys.Z))
            {
                abilities[1].tryAction(new Vector2(position.X + width, position.Y), accuracy);
            }
            if (CheckKey(Keys.X))
            {
                abilities[0].tryAction(new Vector2(position.X + width, position.Y), accuracy);
            }

            position += velocity;

            position.X = MathHelper.Clamp(position.X, GameConstants.PlayableArea.Left, GameConstants.PlayableWidth - width);
            position.Y = MathHelper.Clamp(position.Y, GameConstants.PlayableArea.Top, GameConstants.PlayableArea.Bottom - height);
        }

        /// <summary>
        /// Checks if a key is being released
        /// </summary>
        /// <param name="theKey">The key value to be evaluated</param>
        private bool CheckKey(Keys theKey)
        {
            return keyboardState.IsKeyUp(theKey) && oldKeyboardState.IsKeyDown(theKey);
        }

        public override bool collidesWith(Entity e)
        {
            return !(e is Fireball || e is Light);
        }

        public override void doCollision(Entity e)
        {
            if (e is Enemy || e is Arrow ||  e is Spear)
            {
                Console.WriteLine(healthPoints);
                if (invTimer == 0)
                {
                    healthPoints--;
                    invTimer = 1000;
                }
            }
        }


        public override void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            spriteBatch.Draw(texture, boundingRectangle, Color.White);
            base.Draw(spriteBatch, graphicsDevice);
        }
    }
}
