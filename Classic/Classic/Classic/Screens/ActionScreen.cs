using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace Classic
{
    public class ActionScreen : GameScreen
    {
        private KeyboardState keyboardState;
        private KeyboardState oldKeyboardState;
        private Level level;

        public ActionScreen(Game game, SpriteBatch spriteBatch) : base(game, spriteBatch)
        {
        }

        public void RequestLoadLevel()
        {
            screenManager.LoadScreenContent(this, new Action(LoadLevel));
        }

        private void LoadLevel()
        {
            level = new Level();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            level.Update(gameTime);
            if (level.complete)
                showResults(level.results);
            handleInput();
        }

        private void showResults(Results r)
        {
            screenManager.resultsScreen.results = r;
            screenManager.transitionScreens(this, screenManager.resultsScreen);
        }

        /// <summary>
        /// Escapes to start screen if espace key is pressed
        /// </summary>
        private void handleInput()
        {
            if (CheckKey(Keys.Escape))
            {
                this.Hide();
                screenManager.activeScreen = screenManager.startScreen;
                screenManager.activeScreen.Show();
            }
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
            base.Draw(gameTime);
            level.Draw(spriteBatch, GraphicsDevice);
        }
    }
}

