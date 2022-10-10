using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Space_Invaders
{
    internal class Enemy
    {
        public float x;
        public float y;
        public int sizeX;
        public int sizeY;
        public Image enemyImg;

        public Enemy(float x, float y, int sizeX, int sizeY, string tag)
        {
            this.x = x;
            this.y = y;
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            if (tag == "noob")
                enemyImg = new Bitmap(Resource1.NoobShip);
            else
                enemyImg = new Bitmap(Resource1.RareShip);
        }
    }
}
