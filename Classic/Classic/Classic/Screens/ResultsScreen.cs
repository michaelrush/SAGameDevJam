using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Classic
{
    public class ResultsScreen : GameScreen
    {
        private KeyboardState keyboardState;
        private KeyboardState oldKeyboardState;

        public Results results;
        Rectangle imageRectangle;
        private SpriteFont spriteFont;

        public ResultsScreen(Game game, SpriteBatch spriteBatch, SpriteFont spriteFont)
            : base(game, spriteBatch)
        {
            this.spriteFont = spriteFont;
            imageRectangle = new Rectangle(0, 0, Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Height);
            results = new Results(0, 0, 0, 0, 0, false);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            oldKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();
        }

        /// <summary>
        /// Checks if a key is being released
        /// </summary>
        /// <param name="theKey">The key value to be evaluated</param>
        private bool CheckKey(Keys theKey)
        {
            return keyboardState.IsKeyUp(theKey) && oldKeyboardState.IsKeyDown(theKey);
        }

        public override void Draw(GameTime gameTime)
        {
            Texture2D pixel = new Texture2D(GraphicsDevice, 1, 1);
            Color[] colorData = { Color.Gray };
            pixel.SetData<Color>(colorData);

            spriteBatch.Begin();
            spriteBatch.Draw(pixel, imageRectangle, Color.White);

            if (results.win)
            {
                spriteBatch.DrawString(spriteFont, "Congratulations!", new Vector2(251, 75), Color.Black);
                spriteBatch.DrawString(spriteFont, "Congratulations!", new Vector2(250, 75), Color.White);
            }
            else
            {
                spriteBatch.DrawString(spriteFont, "Wave " + (results.wavesComplete + 1) + " Failed!", new Vector2(251, 75), Color.Black);
                spriteBatch.DrawString(spriteFont, "Wave " + (results.wavesComplete + 1) + " Failed!", new Vector2(250, 75), Color.White);
            }

            spriteBatch.DrawString(spriteFont, "Waves Complete: " + results.wavesComplete + "/" + results.possibleWaves, new Vector2(251, 195), Color.Black, 0, Vector2.Zero, .7f, SpriteEffects.None, 0);
            spriteBatch.DrawString(spriteFont, "Waves Complete: " + results.wavesComplete + "/" + results.possibleWaves, new Vector2(250, 195), Color.White, 0, Vector2.Zero, .7f, SpriteEffects.None, 0);

            spriteBatch.DrawString(spriteFont, "Kills: " + results.kills + "/" + results.possibleKills, new Vector2(251, 245), Color.Black, 0, Vector2.Zero, .7f, SpriteEffects.None, 0);
            spriteBatch.DrawString(spriteFont, "Kills: " + results.kills + "/" + results.possibleKills, new Vector2(250, 245), Color.White, 0, Vector2.Zero, .7f, SpriteEffects.None, 0);

            int ms = (int)results.elapsedTime % 1000;
            int s = (int)results.elapsedTime / 1000 % 60;
            int m = (int)results.elapsedTime / 1000 / 60 % 60;
            string time = m + ":" + String.Format("{0:00}", s) +"." + ms;

            spriteBatch.DrawString(spriteFont, "Time: " + time, new Vector2(251, 300), Color.Black, 0, Vector2.Zero, .7f, SpriteEffects.None, 0);
            spriteBatch.DrawString(spriteFont, "Time: " + time, new Vector2(250, 300), Color.White, 0, Vector2.Zero, .7f, SpriteEffects.None, 0);

            spriteBatch.DrawString(spriteFont, "Press Esc to Exit", new Vector2(251, 420), Color.Black, 0, Vector2.Zero, .5f, SpriteEffects.None, 0);
            spriteBatch.DrawString(spriteFont, "Press Esc to Exit", new Vector2(251, 420), Color.White, 0, Vector2.Zero, .5f, SpriteEffects.None, 0);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

