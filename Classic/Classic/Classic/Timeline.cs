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
    class Timeline
    {
        public struct Spawn
        {
            public Entity entity;
            public float spawnTime;

            public Spawn(Entity entity, float spawnTime)
            {
                this.entity = entity;
                this.spawnTime = spawnTime;
            }
        }

        public List<Spawn> spawns;
        public float duration;

        public Timeline(float duration, List<Spawn> spawns)
        {
            this.duration = duration;
            this.spawns = spawns;
        }
    }
}
