using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace Classic
{
    class Level
    {
        public Player player;
        public bool complete;
        public float elapsedTime;
        public int waveNumber;
        public int score;
        public int totalScore;
        public int maxScore;
        public Results results;
        private static List<Enemy> orcs;
        public static bool noEnemies
        {
            get { return (orcs.Count == 0); }
        }
        private static List<Projectile> projectiles;
        private static List<Entity> toDestroy;
        private static List<Entity> toEscape;
        private static List<Wave> waves;
        private List<Entity> entities
        {
            get
            {
                List<Entity> entities = new List<Entity>();
                entities.Add(player);
                entities.AddRange(projectiles);
                entities.AddRange(orcs);
                return entities;
            }
        }

        private SpriteFont spriteFont;

        /// <summary>
        /// Constructs a new level.
        /// </summary>
        /// <param name="game">
        /// The game object that will be used to contruct the level components
        /// </param>
        /// <param name="levelIndex">
        /// The name of the level file to be loaded
        /// </param>
        public Level()
        {
            player = new Player(new Vector2(10, 250));
            orcs = new List<Enemy>();
            projectiles = new List<Projectile>();
            toDestroy = new List<Entity>();
            toEscape = new List<Entity>();
            waves = Wave.getWaves();
            waveNumber = 0;
            score = 0;
            totalScore = 0;
            maxScore = waves.Select((w, s) => w.killCount + s).Sum();
            complete = false;
            ContentManager content = new ContentManager(Game1.serviceProvider, "Content");
            spriteFont = content.Load<SpriteFont>("cantarell-16");
        }

        /// <summary>
        /// Updates all objects in the world and performs collision between them
        /// </summary>
        public void Update(GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime.Milliseconds;

            // Update wave to populate
            waves[waveNumber].Update(gameTime);
            if (waves[waveNumber].complete)
                nextWave();

            // Update entities
            player.Update(gameTime);
            foreach(Projectile p in projectiles)
                p.Update(gameTime);
            foreach (Enemy o in orcs)
                o.Update(gameTime);

            // Resolve collisions
            CollisionManager.resolveCollisions(entities);

            // Remove deleted entities
            foreach (Entity e in toDestroy)
            {
                if (e is Projectile)
                    projectiles.Remove(e as Projectile);
                if (e is Enemy)
                {
                    score++;
                    orcs.Remove(e as Enemy);
                }
                if (e is Player)
                    gameOver(false);
            }
            foreach (Entity e in toEscape)
            {
                if (e is Projectile)
                    projectiles.Remove(e as Projectile);
                if (e is Enemy)
                    orcs.Remove(e as Enemy);
            }
            toDestroy.Clear();
            toEscape.Clear();
        }

        public static void addEntity(Entity e) {
            if (e is Projectile)
                projectiles.Add(e as Projectile);
            if (e is Enemy)
                orcs.Add(e as Enemy);
        }

        public static void removeEntity(Entity e, bool destroyed)
        {
            if (destroyed)
                toDestroy.Add(e);
            else
                toEscape.Add(e);
        }

        public void nextWave()
        {
            totalScore += score;
            score = 0;
            player.healthPoints = player.maxHealthPoints;
            waveNumber++;
            if (waveNumber >= waves.Count)
            {
                gameOver(true);
            }
        }

        public void gameOver(bool success)
        {
            results = new Results(waveNumber, waves.Count, totalScore, maxScore, elapsedTime, success);
            complete = true;
        }

        /// <summary>
        /// Draw everything in the level from background to foreground.
        /// </summary>
        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            Texture2D blackPixel = new Texture2D(graphics, 1, 1);
            Texture2D grayPixel = new Texture2D(graphics, 1, 1);
            Texture2D whitePixel = new Texture2D(graphics, 1, 1);
            Color[] blackData = { Color.Black };
            Color[] grayData = { new Color(100, 100, 100, 150) };
            Color[] whiteData = { Color.White };
            blackPixel.SetData<Color>(blackData);
            grayPixel.SetData<Color>(grayData);
            whitePixel.SetData<Color>(whiteData);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
            // Draw Top bar
            Rectangle topBar = new Rectangle(0, 0, GameConstants.BackBufferWidth, GameConstants.BackBufferHeight - GameConstants.PlayableHeight);
            spriteBatch.Draw(blackPixel, topBar, Color.White);

            // Draw Entities
            player.Draw(spriteBatch, graphics);
            foreach (Enemy o in orcs)
                o.Draw(spriteBatch, graphics);
            foreach (Projectile p in projectiles)
                p.Draw(spriteBatch, graphics);

            // Draw stat text
            spriteBatch.DrawString(spriteFont, "Wave: " + (waveNumber + 1) + "/" + waves.Count, new Vector2(70, 5), Color.White);
            spriteBatch.DrawString(spriteFont, "Killed: " + score + "/" + waves[waveNumber].killCount, new Vector2(200, 5), Color.White);

            // Draw cooldown icons
            Rectangle zIcon = new Rectangle(10, 10, 20, 20);
            Rectangle xIcon = new Rectangle(40, 10, 20, 20);
            int xCdHeight = (int)(20 * (player.abilities[0].coolDownRemaining / player.abilities[0].coolDownDuration));
            Rectangle xCd = new Rectangle(40, 30 - xCdHeight, 20, xCdHeight);

            spriteBatch.Draw(whitePixel, zIcon, Color.White);
            spriteBatch.DrawString(spriteFont, "Z", new Vector2(10, 5), Color.Black);

            spriteBatch.Draw(whitePixel, xIcon, Color.White);
            spriteBatch.DrawString(spriteFont, "X", new Vector2(40, 5), Color.Black);
            spriteBatch.Draw(grayPixel, xCd, Color.White);


            spriteBatch.End();
        }
    }
}
