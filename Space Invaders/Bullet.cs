using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Space_Invaders
{
    internal class Bullet
    {
        public float x;
        public float y;
        public int sizeX;
        public int sizeY;
        public float speed;
        public Image bulletImg;

        public Bullet(float x, float y)
        {
            this.x = x;
            this.y = y;
            sizeX = 6;
            sizeY = 24;
            speed = 30;
            bulletImg = new Bitmap(Resource1.Bullet);
        }
    }
}
