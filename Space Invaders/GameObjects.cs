using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Space_Invaders
{
    enum EnemyType
    {
        noob,
        rare,
        boss
    }

    internal class SpaceShip
    {
        public int x;
        public int y;
        public int sizeX;
        public int sizeY;

        public SpaceShip(int x, int y, int sizeX, int sizeY)
        {
            this.x = x;
            this.y = y;
            this.sizeX = sizeX;
            this.sizeY = sizeY;
        }

    }

    internal class GameObjects: SpaceShip
    {
        public Image playerImg;

        public GameObjects(int x, int y, int sizeX, int sizeY) : base(x, y, sizeX, sizeY)
        {
            playerImg = new Bitmap(Resource1.PlayerShip);
        }
    }

    internal class Enemy: SpaceShip
    {
        public int hitPoints;
        public int reward;
        public EnemyType tag;
        public Image enemyImg;

        public Enemy(int x, int y, int sizeX, int sizeY, int hitPoints, int reward, EnemyType tag = EnemyType.noob) : base(x, y, sizeX, sizeY)
        {
            this.hitPoints = hitPoints;
            this.reward = reward;
            this.tag = tag;
            if (tag == EnemyType.noob)
                enemyImg = new Bitmap(Resource1.NoobShip);
            else if (tag == EnemyType.rare)
                enemyImg = new Bitmap(Resource1.RareShip);
            else enemyImg = new Bitmap(Resource1.BossShip);
        }

        public static void MakeEnemies(out Enemy[] enemies, int noobsAmount, int raresAmount, int bossesAmount)
        {
            enemies = new Enemy[noobsAmount + raresAmount + bossesAmount];
            int positionX = 0;

            for (int i = 0; i < noobsAmount; i++)
            {
                enemies[i] = new Enemy(positionX, 30, 56, 20, 1, 50);
                positionX = positionX - 70;
            }

            for (int i = noobsAmount; i < noobsAmount + raresAmount; i++)
            {
                enemies[i] = new Enemy(positionX, 30, 56, 56, 2, 100, EnemyType.rare);
                positionX = positionX - 70;
            }

            for (int i = noobsAmount + raresAmount; i < enemies.Length; i++)
            {
                enemies[i] = new Enemy(positionX, 30, 60, 60, 3, 150, EnemyType.boss);
                positionX = positionX - 70;
            }
        }

        public static void MoveEnemies(Enemy[] enemies, int enemySpeed, int width)
        {
            /*foreach (Enemy enemy in enemies)
            {
                enemy.x += enemySpeed;
                if (enemy.x >=  width)
                {
                    enemy.x = -60;
                    enemy.y += 70;
                }
            }*/
            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i] != null)
                {
                    enemies[i].x += enemySpeed;
                    if (enemies[i].x >= width)
                    {
                        enemies[i].x = -60;
                        enemies[i].y += 80;
                    }
                }
            }
        }
    }

    internal class Bullet: SpaceShip
    {
        public int speed;
        public Image bulletImg;

        public Bullet(int x, int y, int sizeX, int sizeY, int speed, bool isPlayer = true) : base(x, y, sizeX, sizeY)
        {
            this.speed = speed;
            
            if (isPlayer)
                bulletImg = new Bitmap(Resource1.Bullet);
            else
                bulletImg = new Bitmap(Resource1.EnemyBullet);
        }
    }
}
