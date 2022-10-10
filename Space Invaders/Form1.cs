namespace Space_Invaders
{
    public partial class Form1 : Form
    {
        const int noobsAmount = 12;
        const int raresAmount = 10;
        const int bossesAmount = 8;
        GameObjects player;
        Enemy[] enemies;
        Bullet bulletPlayer, bulletEnemy;
        int bulletTimerLimit = 150;
        int bulletTimer;
        int enemiesLeft;
        int enemySpeed = 4;
        int score = 0;
        bool canShoot = true;
        bool gameOver = false;

        public Form1()
        {
            InitializeComponent();
            gameOverText.Hide();
            Init();
            Invalidate(); // גûחûגאול, קעמבû מענטסמגאכמסü
        }

        public void Init()
        {
            bulletTimer = bulletTimerLimit;
            enemiesLeft = noobsAmount + raresAmount + bossesAmount;
            player = new GameObjects((this.Width - 60) / 2, this.Height - 100, 60, 60);
            Enemy.MakeEnemies(out enemies, noobsAmount, raresAmount, bossesAmount);
            bulletPlayer = new Bullet(-20, -20, 6, 24, 30);
            bulletEnemy = new Bullet(player.x, -30, 6, 20, 20, false);

            timer1.Interval = 30;
            timer1.Tick += new EventHandler(update);
            timer1.Start();
        }

        private void update(object? sender, EventArgs e)
        {
            if (enemiesLeft < 1)
                GameOver("You won!!!");
            var localPosition = this.PointToClient(Cursor.Position);
            player.x = localPosition.X - player.sizeX / 2;

            if (enemiesLeft <= bossesAmount)
                enemySpeed = 10;
            else if (enemiesLeft <= raresAmount + bossesAmount)
                enemySpeed = 7;

            bulletPlayer.y -= bulletPlayer.speed;
            if (bulletPlayer.y < 0)
                canShoot = true;

            bulletEnemy.y += bulletEnemy.speed;
            bulletTimer -= 3;
            if (bulletTimer < 0)
            {
                bulletTimer = bulletTimerLimit;
                bulletEnemy = new Bullet(player.x, -30, 6, 20, 20, false);
            }

            Enemy.MoveEnemies(enemies, enemySpeed, this.Width);

            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i] != null && bulletPlayer != null)
                {
                    Rectangle enemyBounds = new Rectangle(enemies[i].x, enemies[i].y, enemies[i].sizeX, enemies[i].sizeY - 13);
                    Rectangle bulletPlayerBounds = new Rectangle(bulletPlayer.x, bulletPlayer.y, bulletPlayer.sizeX, bulletPlayer.sizeY);
                    if (enemyBounds.IntersectsWith(bulletPlayerBounds))
                    {
                        bulletPlayer.y = -20;
                        canShoot = true;
                        enemies[i].hitPoints--;
                        if (enemies[i].hitPoints < 1)
                        {
                            score += enemies[i].reward;
                            enemiesLeft--;
                            enemies[i] = null;
                        }
                        break;
                    }
                }
                Rectangle playerBounds = new Rectangle(player.x, player.y, player.sizeX, player.sizeY);
                if (enemies[i] != null)
                {
                    Rectangle enemyBounds = new Rectangle(enemies[i].x, enemies[i].y, enemies[i].sizeX, enemies[i].sizeY - 13);
                    if (enemyBounds.IntersectsWith(playerBounds))
                    {
                        GameOver("You lost :(");
                        break;
                    }
                }
                Rectangle bulletEnemyBounds = new Rectangle(bulletEnemy.x, bulletEnemy.y, bulletEnemy.sizeX, bulletEnemy.sizeY);
                if (bulletEnemyBounds.IntersectsWith(playerBounds))
                {
                    GameOver("You lost :(");
                    break;
                }
            }

            Invalidate();
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;

            graphics.DrawImage(player.playerImg, player.x, player.y, player.sizeX, player.sizeY);
            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i] != null)
                    graphics.DrawImage(enemies[i].enemyImg, enemies[i].x, enemies[i].y, enemies[i].sizeX, enemies[i].sizeY);
            }
            /*foreach (Enemy enemy in enemies)
                graphics.DrawImage(enemy.enemyImg, enemy.x, enemy.y, enemy.sizeX, enemy.sizeY);*/
            graphics.DrawImage(bulletPlayer.bulletImg, bulletPlayer.x, bulletPlayer.y, bulletPlayer.sizeX, bulletPlayer.sizeY);
            graphics.DrawImage(bulletEnemy.bulletImg, bulletEnemy.x, bulletEnemy.y, bulletEnemy.sizeX, bulletEnemy.sizeY);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Click(object sender, EventArgs e)
        {
            if (canShoot)
            {
                bulletPlayer = new Bullet(player.x + player.sizeX / 2 - 3, player.y - 27, 6, 24, 40);
                canShoot = false;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && gameOver == true)
            {
                Application.Restart();
            }
        }

        private void gameOverText_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && gameOver == true)
            {
                Application.Restart();
            }
        }

        private void GameOver(string msg)
        {
            gameOver = true;
            timer1.Stop();
            gameOverText.Text =
                msg + Environment.NewLine +
                "Total score: " + score + Environment.NewLine +
                "Enemies Left: " + enemiesLeft + Environment.NewLine +
                "Press Enter to play again.";
            gameOverText.Show();
        }
    }
}