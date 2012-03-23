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
    public class Results
    {
        public int wavesComplete;
        public int possibleWaves;
        public int kills;
        public int possibleKills;
        public float elapsedTime;
        public bool win;

        public Results(int wavesComplete, int possibleWaves, int kills, int possibleKills, float elapsedTime, bool win)
        {
            this.wavesComplete = wavesComplete;
            this.possibleWaves = possibleWaves;
            this.kills = kills;
            this.possibleKills = possibleKills;
            this.elapsedTime = elapsedTime;
            this.win = win;
        }
    }
}