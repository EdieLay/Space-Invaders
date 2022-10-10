using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Space_Invaders
{

    internal class Player
    {
        public float x;
        public float y;
        public int size;
        public Image playerImg;

        public Player(float x, float y)
        {
            this.x = x;
            this.y = y;
            playerImg = new Bitmap(Resource1.PlayerShip);
            size = 60;
        }
    }
}
