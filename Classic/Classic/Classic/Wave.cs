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
    class Wave
    {
        public float elapsedTime;
        public bool complete;
        public Timeline timeline;
        public int killCount;
        public List<Timeline.Spawn> toDelete;

        public Wave(int waveNumber)
        {
            complete = false;
            // find a way to clone this to prevent butchering the list
            timeline = timelines[waveNumber];
            timeline.spawns.Sort((s1, s2) => s1.spawnTime.CompareTo(s2.spawnTime));
            killCount = timeline.spawns.Count;
            toDelete = new List<Timeline.Spawn>();
        }

        public void Update(GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime.Milliseconds;

            foreach(Timeline.Spawn s in timeline.spawns) {
                if (s.spawnTime < elapsedTime)
                {
                    Level.addEntity(s.entity);
                    toDelete.Add(s);
                }
                else
                    break; //assumes sorted by spawnTime
            }

            foreach (Timeline.Spawn s in toDelete)
            {
                timeline.spawns.Remove(s);
            }
            toDelete.Clear();


            if (elapsedTime > timeline.duration && timeline.spawns.Count == 0 && Level.noEnemies)
                endWave();
        }

        public void endWave()
        {
            complete = true;
        }

        public static List<Wave> getWaves()
        {
            List<Wave> waves = new List<Wave>();
            for (int i = 0; i < timelines.Count; i++)
            {
                waves.Add(new Wave(i));
            }
            return waves;
        }

        public static List<Timeline> timelines = new List<Timeline>() 
        {
            new Timeline(18000, new List<Timeline.Spawn>() {
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 220)), 3000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 380)), 5000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 300)), 8000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 140)), 9000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 220)), 10000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 380)), 12000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 460)), 13000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 220)), 16000),
                new Timeline.Spawn(new Spearman(new Vector2(GameConstants.PlayableWidth, 300)), 16000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 380)), 16000)
            }),
            new Timeline(22000, new List<Timeline.Spawn>() {
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 220)), 2000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 220)), 4000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 300)), 4000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 60)), 7000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 220)), 7000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 460)), 7000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 220)), 10000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 300)), 11000),
                new Timeline.Spawn(new Spearman(new Vector2(GameConstants.PlayableWidth, 380)), 12000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 220)), 13000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 460)), 14000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 60)), 15000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 220)), 15000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 380)), 16000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 460)), 16000),
                new Timeline.Spawn(new Spearman(new Vector2(GameConstants.PlayableWidth, 60)), 18000),
                new Timeline.Spawn(new Spearman(new Vector2(GameConstants.PlayableWidth, 220)), 18000),
                new Timeline.Spawn(new Spearman(new Vector2(GameConstants.PlayableWidth, 220)), 18000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 300)), 19000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 380)), 19000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 460)), 19000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 60)), 21000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 220)), 21000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 380)), 21000),
                new Timeline.Spawn(new Spearman(new Vector2(GameConstants.PlayableWidth, 300)), 22000),
                new Timeline.Spawn(new Spearman(new Vector2(GameConstants.PlayableWidth, 380)), 22000),
                new Timeline.Spawn(new Spearman(new Vector2(GameConstants.PlayableWidth, 460)), 22000)
            }),
            new Timeline(22000, new List<Timeline.Spawn>() {
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 60)), 2000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 140)), 2000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 220)), 2000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 300)), 4000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 380)), 4000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 460)), 4000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth,60)), 6000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 140)), 6000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 300)), 6000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 460)), 6000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 60)), 9000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 140)), 9000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 220)), 9000),
                new Timeline.Spawn(new Spearman(new Vector2(GameConstants.PlayableWidth,60)), 11000),
                new Timeline.Spawn(new Spearman(new Vector2(GameConstants.PlayableWidth, 220)), 11000),
                new Timeline.Spawn(new Spearman(new Vector2(GameConstants.PlayableWidth, 300)), 11000),
                new Timeline.Spawn(new Spearman(new Vector2(GameConstants.PlayableWidth, 460)), 11000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 300)), 13000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 380)), 13000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 460)), 13000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth,60)), 18000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 140)), 18000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 220)), 18000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 300)), 18000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 380)), 18000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 460)), 18000),
                new Timeline.Spawn(new Spearman(new Vector2(GameConstants.PlayableWidth, 60)), 20000),
                new Timeline.Spawn(new Spearman(new Vector2(GameConstants.PlayableWidth, 140)), 20000),
                new Timeline.Spawn(new Spearman(new Vector2(GameConstants.PlayableWidth, 220)), 20000),
                new Timeline.Spawn(new Spearman(new Vector2(GameConstants.PlayableWidth, 300)), 20000),
                new Timeline.Spawn(new Spearman(new Vector2(GameConstants.PlayableWidth, 380)), 20000),
                new Timeline.Spawn(new Spearman(new Vector2(GameConstants.PlayableWidth, 460)), 20000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 60)), 22000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 140)), 22000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 220)), 22000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 300)), 22000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 380)), 22000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 460)), 22000),
            }),
            new Timeline(26000, new List<Timeline.Spawn>() {
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 60)), 2000),
                new Timeline.Spawn(new Spearman(new Vector2(GameConstants.PlayableWidth, 140)), 3000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 220)), 2000),
                new Timeline.Spawn(new Spearman(new Vector2(GameConstants.PlayableWidth, 300)), 3000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 380)), 2000),
                new Timeline.Spawn(new Spearman(new Vector2(GameConstants.PlayableWidth, 460)), 3000),
                new Timeline.Spawn(new Spearman(new Vector2(GameConstants.PlayableWidth,60)), 5000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 140)), 4000),
                new Timeline.Spawn(new Spearman(new Vector2(GameConstants.PlayableWidth, 220)), 5000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 300)), 4000),
                new Timeline.Spawn(new Spearman(new Vector2(GameConstants.PlayableWidth, 460)), 5000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 60)), 9000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 140)), 9000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 60)), 10000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 140)), 10000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 60)), 11000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 140)), 11000),
                new Timeline.Spawn(new Spearman(new Vector2(GameConstants.PlayableWidth, 380)), 9000),
                new Timeline.Spawn(new Spearman(new Vector2(GameConstants.PlayableWidth, 460)), 9000),
                new Timeline.Spawn(new Spearman(new Vector2(GameConstants.PlayableWidth, 380)), 10000),
                new Timeline.Spawn(new Spearman(new Vector2(GameConstants.PlayableWidth, 460)), 10000),
                new Timeline.Spawn(new Spearman(new Vector2(GameConstants.PlayableWidth, 380)), 11000),
                new Timeline.Spawn(new Spearman(new Vector2(GameConstants.PlayableWidth, 460)), 11000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 220)), 11000),
                new Timeline.Spawn(new Spearman(new Vector2(GameConstants.PlayableWidth, 300)), 11000),
                new Timeline.Spawn(new Spearman(new Vector2(GameConstants.PlayableWidth, 220)), 12000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 300)), 12000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 220)), 13000),
                new Timeline.Spawn(new Spearman(new Vector2(GameConstants.PlayableWidth, 300)), 13000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 300)), 16000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 220)), 16500),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 380)), 16500),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 140)), 17000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 460)), 17000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 80)), 17500),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 460)), 17500),
                new Timeline.Spawn(new Spearman(new Vector2(GameConstants.PlayableWidth, 80)), 18000),
                new Timeline.Spawn(new Spearman(new Vector2(GameConstants.PlayableWidth, 140)), 18000),
                new Timeline.Spawn(new Spearman(new Vector2(GameConstants.PlayableWidth, 220)), 18000),
                new Timeline.Spawn(new Spearman(new Vector2(GameConstants.PlayableWidth, 300)), 18000),
                new Timeline.Spawn(new Spearman(new Vector2(GameConstants.PlayableWidth, 380)), 18000),
                new Timeline.Spawn(new Spearman(new Vector2(GameConstants.PlayableWidth, 460)), 18000),
                new Timeline.Spawn(new Spearman(new Vector2(GameConstants.PlayableWidth, 80)), 19000),
                new Timeline.Spawn(new Spearman(new Vector2(GameConstants.PlayableWidth, 460)), 19000),
                new Timeline.Spawn(new Spearman(new Vector2(GameConstants.PlayableWidth, 140)), 20000),
                new Timeline.Spawn(new Spearman(new Vector2(GameConstants.PlayableWidth, 380)), 20000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 60)), 22000),
                new Timeline.Spawn(new Spearman(new Vector2(GameConstants.PlayableWidth, 140)), 22000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 220)), 22000),
                new Timeline.Spawn(new Spearman(new Vector2(GameConstants.PlayableWidth, 300)), 22000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 60)), 24000),
                new Timeline.Spawn(new Spearman(new Vector2(GameConstants.PlayableWidth, 140)), 24000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 220)), 24000),
                new Timeline.Spawn(new Spearman(new Vector2(GameConstants.PlayableWidth, 300)), 24000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 380)), 22000),
                new Timeline.Spawn(new Spearman(new Vector2(GameConstants.PlayableWidth, 460)), 22000),
                new Timeline.Spawn(new Spearman(new Vector2(GameConstants.PlayableWidth, 380)), 22000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 460)), 22000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 380)), 24000),
                new Timeline.Spawn(new Spearman(new Vector2(GameConstants.PlayableWidth, 460)), 24000),
                new Timeline.Spawn(new Spearman(new Vector2(GameConstants.PlayableWidth, 380)), 24000),
                new Timeline.Spawn(new Archer(new Vector2(GameConstants.PlayableWidth, 460)), 24000),
            })
        };

    }
}
