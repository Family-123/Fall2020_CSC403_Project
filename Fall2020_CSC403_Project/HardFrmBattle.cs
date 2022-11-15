using Fall2020_CSC403_Project.code;
using Fall2020_CSC403_Project.Properties;
using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace Fall2020_CSC403_Project {
  public partial class HardFrmBattle : Form {
    public static HardFrmBattle instance = null;
    private Enemy enemy;
    private HardPlayer hardplayer;
    private int start_time;
    private System.Windows.Forms.Timer tmrbattle_timer;


    private HardFrmBattle() {
      start_time = 30;
      InitializeComponent();
      hardplayer = HardGame.hardplayer;
      this.tmrbattle_timer = new System.Windows.Forms.Timer(this.components);
    }

    public void Setup() {
      // update for this enemy
      picEnemy.BackgroundImage = enemy.Img;
      picEnemy.Refresh();
      BackColor = enemy.Color;
      picBossBattle.Visible = false;

      // Observer pattern
      enemy.AttackEvent += HardPlayerDamage;
      hardplayer.AttackEvent += EnemyDamage;
      hardplayer.HealEvent += HardPlayerMagic;
      hardplayer.Strength_UpEvent += HardPlayerMagic;
      
      // show health
      UpdateHealthBars();

      // show magic
      UpdateMagicBars();
    }

    public void SetupForBossBattle() {
      picBossBattle.Location = Point.Empty;
      picBossBattle.Size = ClientSize;
      picBossBattle.Visible = true;

      SoundPlayer simpleSound = new SoundPlayer(Resources.final_battle);
      simpleSound.Play();

      tmrFinalBattle.Enabled = true;
    }

    public void SetupForVictoryScreen()
        {
            frmVictory victory = new frmVictory();
            victory.Show();
            this.Hide();
        }

    public static HardFrmBattle GetInstance(Enemy enemy) {
      if (instance == null) {
        instance = new HardFrmBattle();
        instance.enemy = enemy;
        instance.Setup();
      }
      return instance;
    }

    private void battle_time_Tick(object sender,EventArgs e)
        {
            lblbattletime.Text = start_time--.ToString();
            if (start_time < 0)
            {
                tmrbattle_time.Stop();
                instance = null;
                if (hardplayer.Health > enemy.Health)
                {
                    SetupForVictoryScreen();
                }
                else if (hardplayer.Health == enemy.Health)
                {
                    Close();
                }
            }

                  
        }

    private void UpdateHealthBars() {
      float playerHealthPer = hardplayer.Health / (float)hardplayer.MaxHealth;
      float enemyHealthPer = enemy.Health / (float)enemy.MaxHealth;

      const int MAX_HEALTHBAR_WIDTH = 226;
      lblPlayerHealthFull.Width = (int)(MAX_HEALTHBAR_WIDTH * playerHealthPer);
      lblEnemyHealthFull.Width = (int)(MAX_HEALTHBAR_WIDTH * enemyHealthPer);

      lblPlayerHealthFull.Text = hardplayer.Health.ToString();
      lblEnemyHealthFull.Text = enemy.Health.ToString();
    }

    private void UpdateMagicBars() {
      float playerMagicPer = hardplayer.Magic / (float)hardplayer.MaxMagic;

      const int MAX_MAGICBAR_WIDTH = 226;
      lblPlayerMagicFull.Width = (int)(MAX_MAGICBAR_WIDTH * playerMagicPer);

      lblPlayerMagicFull.Text = hardplayer.Magic.ToString();
    }

    private void btnAttack_Click(object sender, EventArgs e) {
      if (hardplayer.Strength == 2) {
        hardplayer.OnAttack(-4);
      }

      if (hardplayer.Strength == 5) {
        hardplayer.OnAttack(-3); 
      }

      if (enemy.Health > 0) {
        enemy.OnEnemyAttack(-2);
      }

      UpdateHealthBars();
      if (hardplayer.Health <= 0 || enemy.Health <= 0) {
        hardplayer.ChangeStrengthBack();
        instance = null;
        Close();
      }
      if (enemy.Health <= 0 && hardplayer.Health > enemy.Health)
            {
                enemy.Collider.DeleteCollider();    
                SetupForVictoryScreen();
            }
        if (enemy.Health <= 0)
        {
            instance = null;
            Close();
            enemy.Collider.DeleteCollider();
        }
    }

    private void btnHeal_Click(object sender, EventArgs e) {
        if (hardplayer.Health < 15 && hardplayer.Magic >= 5 && enemy.Health > 0) {
          hardplayer.OnHeal(-5);
        } 
    }

    private void btnStrength_Up_Click(object sender, EventArgs e) {
        if (hardplayer.Strength == 2 && hardplayer.Magic >= 10 && enemy.Health > 0) {
          hardplayer.OnStrength_Up(-10);
          hardplayer.AlterStrength(3);
        }
            
        UpdateMagicBars();
    }

      private void EnemyDamage(int amount) {
      enemy.AlterHealth(amount);
    }

    private void HardPlayerDamage(int amount) {
      hardplayer.AlterHealth(amount);
    }

    private void HardPlayerMagic(int amount) {
      hardplayer.AlterMagic(amount);
      for (int i=1; i<11; i++) {
          if (hardplayer.Health < 15) {
              hardplayer.AlterHealth(1);
              UpdateHealthBars();
          }
          i++;
      }
      UpdateMagicBars();
    }

    private void HardPlayerStrength(int amount) {
      hardplayer.AlterStrength(3);
    }

    private void tmrFinalBattle_Tick(object sender, EventArgs e) {
      picBossBattle.Visible = false;
      tmrFinalBattle.Enabled = true;
    }
        private void lblbattletime_Click(object sender, EventArgs e)
        {

        }
    }
}
