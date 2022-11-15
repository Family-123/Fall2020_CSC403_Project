using Fall2020_CSC403_Project.code;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Fall2020_CSC403_Project
{
    public partial class HardFrmLevel : Form
    {
        private HardPlayer hardplayer;

        private Enemy enemyPoisonPacket;
        private Enemy bossKoolaid;
        private Enemy enemyCheeto;
        private Character[] walls;
        public static bool IsPaused = false;

        private Stopwatch stopwatch;
        private DateTime timeBegin;
        private HardFrmBattle hardfrmBattle;

        public HardFrmLevel()
        {
            InitializeComponent();
        }

        private void FrmLevel_Load(object sender, EventArgs e)
        {
            const int PADDING = 7;
            const int NUM_WALLS = 13;

            hardplayer = new HardPlayer(CreatePosition(picPlayer), CreateCollider(picPlayer, PADDING));
            bossKoolaid = new Enemy(CreatePosition(picBossKoolAid), CreateCollider(picBossKoolAid, PADDING));
            enemyPoisonPacket = new Enemy(CreatePosition(picEnemyPoisonPacket), CreateCollider(picEnemyPoisonPacket, PADDING));
            enemyCheeto = new Enemy(CreatePosition(picEnemyCheeto), CreateCollider(picEnemyCheeto, PADDING));

            bossKoolaid.Img = picBossKoolAid.BackgroundImage;
            enemyPoisonPacket.Img = picEnemyPoisonPacket.BackgroundImage;
            enemyCheeto.Img = picEnemyCheeto.BackgroundImage;

            bossKoolaid.Color = Color.Red;
            enemyPoisonPacket.Color = Color.Green;
            enemyCheeto.Color = Color.FromArgb(255, 245, 161);

            walls = new Character[NUM_WALLS];
            for (int w = 0; w < NUM_WALLS; w++)
            {
                PictureBox pic = Controls.Find("picWall" + w.ToString(), true)[0] as PictureBox;
                walls[w] = new Character(CreatePosition(pic), CreateCollider(pic, PADDING));
            }

            HardGame.hardplayer = hardplayer;
            timeBegin = DateTime.Now;
            stopwatch = new Stopwatch();
        }

        private Vector2 CreatePosition(PictureBox pic)
        {
            return new Vector2(pic.Location.X, pic.Location.Y);
        }

        private Collider CreateCollider(PictureBox pic, int padding)
        {
            Rectangle rect = new Rectangle(pic.Location, new Size(pic.Size.Width - padding, pic.Size.Height - padding));
            return new Collider(rect);
        }

        private void FrmLevel_KeyUp(object sender, KeyEventArgs e)
        {
            hardplayer.ResetMoveSpeed();
        }

        private void tmrUpdateInGameTime_Tick(object sender, EventArgs e)
        {
            if (IsPaused == false)
            {
                stopwatch.Start();
                //TimeSpan span = DateTime.Now - timeBegin;
                TimeSpan span = DateTime.Now - timeBegin;
                string time = span.ToString(@"hh\:mm\:ss");
                lblInGameTime.Text = "Time: " + time.ToString();
            }
            else
            {
                stopwatch.Stop();
            }
        }

        private void tmrPlayerMove_Tick(object sender, EventArgs e)
        {
            // move player
            hardplayer.Move();

            // check collision with walls
            if (HitAWall(hardplayer))
            {
                hardplayer.MoveBack();
            }

            // check collision with enemies
            if (HitAChar(hardplayer, enemyPoisonPacket))
            {
                Fight(enemyPoisonPacket);
            }
            else if (HitAChar(hardplayer, enemyCheeto))
            {
                Fight(enemyCheeto);
            }
            if (HitAChar(hardplayer, bossKoolaid))
            {
                Fight(bossKoolaid);
            }

            // update player's picture box
            picPlayer.Location = new Point((int)hardplayer.Position.x, (int)hardplayer.Position.y);
        }

        // updates Cheeto picture box
        private void tmrEnemyCheetoMove_Tick(object sender, EventArgs e)
        {
            if (IsPaused == false)
            {
                if (enemyCheeto.Health >= 0)
                {
                    switch (enemyCheeto.Counter)
                    {
                        case 0:
                            enemyCheeto.GoLeft();
                            break;
                        case 1:
                            enemyCheeto.GoUp();
                            break;
                        case 2:
                            enemyCheeto.GoDown();
                            break;
                        case 3:
                            enemyCheeto.GoRight();
                            break;
                    }

                    enemyCheeto.Move();

                    if (HitAWall(enemyCheeto))
                    {
                        enemyCheeto.MoveBack();
                        enemyCheeto.Counter += 1;
                    }

                    while (HitAChar(enemyCheeto, hardplayer))
                    {
                        enemyCheeto.MoveBack();
                        enemyCheeto.ResetMoveSpeed();
                    }

                    if (enemyCheeto.Counter >= 4)
                    {
                        enemyCheeto.Counter = 0;
                    }
                    picEnemyCheeto.Location = new Point((int)enemyCheeto.Position.x, (int)enemyCheeto.Position.y);
                }
            }
        }

        private void tmrEnemyPoisonPacketMove_Tick(object sender, EventArgs e)
        {
            if (IsPaused == false)
            {
                if (enemyPoisonPacket.Health >= 0)
                {
                    switch (enemyPoisonPacket.Counter)
                    {
                        case 0:
                            enemyPoisonPacket.GoDown();
                            break;
                        case 1:
                            enemyPoisonPacket.GoRight();
                            break;
                        case 2:
                            enemyPoisonPacket.GoLeft();
                            break;
                        case 3:
                            enemyPoisonPacket.GoUp();
                            break;
                    }
                    enemyPoisonPacket.Move();

                    if (HitAWall(enemyPoisonPacket))
                    {
                        enemyPoisonPacket.MoveBack();
                        enemyPoisonPacket.Counter += 1;
                    }

                    while (HitAChar(enemyPoisonPacket, hardplayer))
                    {
                        enemyPoisonPacket.MoveBack();
                        enemyPoisonPacket.ResetMoveSpeed();
                    }

                    if (enemyPoisonPacket.Counter >= 4)
                    {
                        enemyPoisonPacket.Counter = 0;
                    }

                    picEnemyPoisonPacket.Location = new Point((int)enemyPoisonPacket.Position.x, (int)enemyPoisonPacket.Position.y);
                }
            }

        }
        private bool HitAWall(Character c)
        {
            bool hitAWall = false;
            for (int w = 0; w < walls.Length; w++)
            {
                if (c.Collider.Intersects(walls[w].Collider))
                {
                    hitAWall = true;
                    break;
                }
            }
            return hitAWall;
        }

        private bool HitAChar(Character you, Character other)
        {
            return you.Collider.Intersects(other.Collider);
        }

        private void Fight(Enemy enemy)
        {
            hardplayer.ResetMoveSpeed();
            hardplayer.MoveBack();
            hardfrmBattle = HardFrmBattle.GetInstance(enemy);
            hardfrmBattle.Show();

            if (enemy == bossKoolaid)
            {
                hardfrmBattle.SetupForBossBattle();
            }
        }

        private void FrmLevel_KeyDown(object sender, KeyEventArgs e)
        {
            if (IsPaused == false)
            {
                switch (e.KeyCode)
                {


                    case Keys.Left:
                        hardplayer.GoLeft();
                        break;

                    case Keys.Right:
                        hardplayer.GoRight();
                        break;

                    case Keys.Up:
                        hardplayer.GoUp();
                        break;

                    case Keys.Down:
                        hardplayer.GoDown();
                        break;

                    case Keys.Escape:
                        PauseMenu pauseMenu = new PauseMenu();
                        pauseMenu.Show();
                        IsPaused = true;
                        hardplayer.ResetMoveSpeed();
                        break;

                    default:
                        hardplayer.ResetMoveSpeed();
                        break;
                }
            }
        }

        private void lblInGameTime_Click(object sender, EventArgs e)
        {

        }

        private void tmrUpdateEnemyPic_Tick(object sender, EventArgs e)
        {

        }
    }
}
