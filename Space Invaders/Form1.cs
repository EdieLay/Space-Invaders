namespace Space_Invaders
{
    public partial class Form1 : Form
    {
        Player player;
        Enemy[] noobs;
        Enemy[] rares;
        Bullet bulletplayer;
        int enemySpeed = 5;

        public Form1()
        {
            InitializeComponent();
            Init();
            Invalidate(); // вызываем, чтобы отрисовался корабль
        }

        public void Init()
        {
            player = new Player((this.Width - 60) / 2, this.Height - 100);
            MakeEnemies();
            bulletplayer = new Bullet(1000, player.y - 24);

            timer1.Interval = 25;
            timer1.Tick += new EventHandler(update);
            timer1.Start();
        }

        private void update(object? sender, EventArgs e)
        {
            var localPosition = this.PointToClient(Cursor.Position);
            player.x = localPosition.X - player.size / 2;

            bulletplayer.y -= bulletplayer.speed;
            MoveEnemies();

            Invalidate();
        }

        private void MakeEnemies()
        {
            noobs = new Enemy[20];
            rares = new Enemy[15];

            int positionX = 0;

            for (int i = 0; i < noobs.Length; i++)
            {
                noobs[i] = new Enemy(positionX, 10, 56, 20, "noob");
                positionX = positionX - 70;
            }

            for (int i = 0; i < rares.Length; i++)
            {
                rares[i] = new Enemy(positionX, 10, 56, 56, "rare");
                positionX = positionX - 70;
            }
        }

        private void MoveEnemies()
        {
            for (int i = 0; i < noobs.Length; i++)
            {
                if (noobs[i] != null)
                {
                    noobs[i].x += enemySpeed;
                    if (noobs[i].x >= this.Width)
                    {
                        noobs[i].x = -60;
                        noobs[i].y += 60;
                    }
                }
            }

            for (int i = 0; i < rares.Length; i++)
            {
                if (rares[i] != null)
                {
                    rares[i].x += enemySpeed;
                    if (rares[i].x >= this.Width)
                    {
                        rares[i].x = -60;
                        rares[i].y += 60;
                    }
                }
            }
        }



        private void OnPaint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;

            graphics.DrawImage(player.playerImg, player.x, player.y, player.size, player.size);
            for (int i = 0; i < noobs.Length; i++)
                graphics.DrawImage(noobs[i].enemyImg, noobs[i].x, noobs[i].y, noobs[i].sizeX, noobs[i].sizeY);
            for (int i = 0; i < rares.Length; i++)
                graphics.DrawImage(rares[i].enemyImg, rares[i].x, rares[i].y, rares[i].sizeX, rares[i].sizeY);
            graphics.DrawImage(bulletplayer.bulletImg, bulletplayer.x, bulletplayer.y, bulletplayer.sizeX, bulletplayer.sizeY);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Click(object sender, EventArgs e)
        {

            bulletplayer = new Bullet(player.x + (float)player.size / 2 - 3, player.y - 24);
            Invalidate();
        }

        public int GetRandom(int a, int b)
        {
            Random random = new Random();
            int i = 0;
            while (i == 0)
            {
                i = random.Next(a, b);
            }
            return i;
        }
    }
}