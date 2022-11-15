using Fall2020_CSC403_Project.code;
using Fall2020_CSC403_Project.Properties;
using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace Fall2020_CSC403_Project {
  public partial class HarderFrmBattle : Form {
    public static HarderFrmBattle instance = null;
    private Enemy enemy;
    private HarderPlayer harderplayer;
    private int start_time;
    private System.Windows.Forms.Timer tmrbattle_timer;


    private HarderFrmBattle() {
      start_time = 30;
      InitializeComponent();
      harderplayer = HarderGame.harderplayer;
      this.tmrbattle_timer = new System.Windows.Forms.Timer(this.components);
    }

    public void Setup() {
      // update for this enemy
      picEnemy.BackgroundImage = enemy.Img;
      picEnemy.Refresh();
      BackColor = enemy.Color;
      picBossBattle.Visible = false;

      // Observer pattern
      enemy.AttackEvent += HarderPlayerDamage;
      harderplayer.AttackEvent += EnemyDamage;
      harderplayer.HealEvent += HarderPlayerMagic;
      harderplayer.Strength_UpEvent += HarderPlayerMagic;
      
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

    public static HarderFrmBattle GetInstance(Enemy enemy) {
      if (instance == null) {
        instance = new HarderFrmBattle();
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
                if (harderplayer.Health > enemy.Health)
                {
                    SetupForVictoryScreen();
                }
                else if (harderplayer.Health == enemy.Health)
                {
                    Close();
                }
            }

                  
        }

    private void UpdateHealthBars() {
      float playerHealthPer = harderplayer.Health / (float)harderplayer.MaxHealth;
      float enemyHealthPer = enemy.Health / (float)enemy.MaxHealth;

      const int MAX_HEALTHBAR_WIDTH = 226;
      lblPlayerHealthFull.Width = (int)(MAX_HEALTHBAR_WIDTH * playerHealthPer);
      lblEnemyHealthFull.Width = (int)(MAX_HEALTHBAR_WIDTH * enemyHealthPer);

      lblPlayerHealthFull.Text = harderplayer.Health.ToString();
      lblEnemyHealthFull.Text = enemy.Health.ToString();
    }

    private void UpdateMagicBars() {
      float playerMagicPer = harderplayer.Magic / (float)harderplayer.MaxMagic;

      const int MAX_MAGICBAR_WIDTH = 226;
      lblPlayerMagicFull.Width = (int)(MAX_MAGICBAR_WIDTH * playerMagicPer);

      lblPlayerMagicFull.Text = harderplayer.Magic.ToString();
    }

    private void btnAttack_Click(object sender, EventArgs e) {
      if (harderplayer.Strength == 2) {
        harderplayer.OnAttack(-4);
      }

      if (harderplayer.Strength == 5) {
        harderplayer.OnAttack(-3); 
      }

      if (enemy.Health > 0) {
        enemy.OnEnemyAttack(-2);
      }

      UpdateHealthBars();
      if (harderplayer.Health <= 0 || enemy.Health <= 0) {
        harderplayer.ChangeStrengthBack();
        instance = null;
        Close();
      }
      if (enemy.Health <= 0 && harderplayer.Health > enemy.Health)
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
        if (harderplayer.Health < 10 && harderplayer.Magic >= 5 && enemy.Health > 0) {
          harderplayer.OnHeal(-5);
        } 
    }

    private void btnStrength_Up_Click(object sender, EventArgs e) {
        if (harderplayer.Strength == 2 && harderplayer.Magic >= 10 && enemy.Health > 0) {
          harderplayer.OnStrength_Up(-10);
          harderplayer.AlterStrength(3);
        }
            
        UpdateMagicBars();
    }

      private void EnemyDamage(int amount) {
      enemy.AlterHealth(amount);
    }

    private void HarderPlayerDamage(int amount) {
      harderplayer.AlterHealth(amount);
    }

    private void HarderPlayerMagic(int amount) {
      harderplayer.AlterMagic(amount);
      for (int i=1; i<11; i++) {
          if (harderplayer.Health < 10) {
              harderplayer.AlterHealth(1);
              UpdateHealthBars();
          }
          i++;
      }
      UpdateMagicBars();
    }

    private void HarderPlayerStrength(int amount) {
      harderplayer.AlterStrength(3);
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
