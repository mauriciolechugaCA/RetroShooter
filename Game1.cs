using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroShooter.Scenes;
using RetroShooter.Managers;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace RetroShooter
{
    public enum GameScene
    {
        Start,
        Play,
        Help,
        About,
        GameOver
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;

        // Fonts
        public SpriteFont _font;
        public SpriteFont _hudFont;
        public SpriteFont _creditsTitleFont;
        public SpriteFont _menuTitle;
        public SpriteFont _menuItems;

        // Game objects
        public PlayerManager PlayerManager { get; private set; }
        public Texture2D playerTexture;
        public Texture2D laserNormalTexture;
        public Texture2D enemyBulletTexture; 
        public Texture2D enemyTexture;
        public Texture2D enemyShooterTexture;
        public Texture2D powerupHealthTexture;
        public Texture2D powerupLaserTexture;

        // Background and floating meteors
        public Texture2D backgroundTexture;
        public Texture2D[] floatingMeteorsTextures;

        // Scene management
        private GameScene _currentScene;
        private StartScene _startScene;
        private PlayScene _playScene;
        private HelpScene _helpScene;
        private AboutScene _aboutScene;
        private GameOverScene _gameOverScene;
        private PauseScene _pauseScene;

        // Sound manager
        private SoundManager _soundManager;
        public SoundManager SoundManager => _soundManager;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();

            Texture2D playerDeathTexture = Content.Load<Texture2D>("animations/playerShip_Death/playerDeath");
            int frameCount = 60;
            float frameSpeed = 3f; // Adjust the frame speed to match the animation

            PlayerManager = new PlayerManager(new Vector2(400, 850), 100, playerTexture, 1.0f, this, playerDeathTexture, frameCount, frameSpeed);

            // Initialize first scene
            SceneManager.ChangeScene(new StartScene(
                _spriteBatch,
                _font,
                _menuItems,
                _menuTitle,
                this,
                PlayerManager.Player,
                _soundManager
            ));

            SceneManager.ChangeScene(_startScene);

            // Set the window size
            _graphics.PreferredBackBufferWidth = 768;
            _graphics.PreferredBackBufferHeight = 1024;
            _graphics.ApplyChanges();

            // Initialize the SceneManager
            SceneManager.Initialize(this);
            _currentScene = GameScene.Start;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _soundManager = new SoundManager();

            // Load fonts
            _font = Content.Load<SpriteFont>("fonts/gameFont");
            _hudFont = Content.Load<SpriteFont>("fonts/hudFont");
            _creditsTitleFont = Content.Load<SpriteFont>("fonts/creditsTitle");
            _menuTitle = Content.Load<SpriteFont>("fonts/menuTitle");
            _menuItems = Content.Load<SpriteFont>("fonts/menuItems");

            // Load game textures
            playerTexture = Content.Load<Texture2D>("images/player/playerShip");
            laserNormalTexture = Content.Load<Texture2D>("images/player/laserNormal");
            enemyBulletTexture = Content.Load<Texture2D>("images/enemies/laserEnemy");
            enemyTexture = Content.Load<Texture2D>("images/enemies/enemyShip2");
            enemyShooterTexture = Content.Load<Texture2D>("images/enemies/enemyShip5");

            // Load powerup textures
            powerupHealthTexture = Content.Load<Texture2D>("images/powerups/powerupHealth");
            powerupLaserTexture = Content.Load<Texture2D>("images/powerups/powerupLaser");

            // Load background and floating meteors
            backgroundTexture = Content.Load<Texture2D>("images/world/background");
            floatingMeteorsTextures = new Texture2D[]
            {
                Content.Load<Texture2D>("images/world/meteorBrown_big1"),
                Content.Load<Texture2D>("images/world/meteorBrown_big3"),
                Content.Load<Texture2D>("images/world/meteorBrown_med1"),
                Content.Load<Texture2D>("images/world/meteorBrown_med3"),
                Content.Load<Texture2D>("images/world/meteorBrown_tiny1"),
                Content.Load<Texture2D>("images/world/meteorBrown_tiny2"),
                Content.Load<Texture2D>("images/world/meteorGrey_big2"),
                Content.Load<Texture2D>("images/world/meteorGrey_big4"),
                Content.Load<Texture2D>("images/world/meteorGrey_med2"),
                Content.Load<Texture2D>("images/world/meteorGrey_small1"),
                Content.Load<Texture2D>("images/world/meteorGrey_small2")
            };

            // Load sounds
            _soundManager.LoadSoundEffect("confirmation_001", Content.Load<SoundEffect>("sounds/confirmation_001")); // ##AI Assisted debugging## 
            _soundManager.LoadSoundEffect("select_002", Content.Load<SoundEffect>("sounds/select_002"));
            _soundManager.LoadSoundEffect("player_hit", Content.Load<SoundEffect>("sounds/impactMetal_000"));
            _soundManager.LoadSoundEffect("player_death", Content.Load<SoundEffect>("sounds/explosionCrunch_001"));
            _soundManager.LoadSoundEffect("powerup", Content.Load<SoundEffect>("sounds/powerUp2"));
            _soundManager.LoadSoundEffect("playerLaser", Content.Load<SoundEffect>("sounds/sfx_laser1"));
            _soundManager.LoadSoundEffect("enemyLaser", Content.Load<SoundEffect>("sounds/sfx_laser2"));

            // Load music
            _soundManager.LoadSong("background_music", Content.Load<Song>("sounds/background_music"));
            _soundManager.LoadSong("gameplay_music", Content.Load<Song>("sounds/gameplay_music"));

            // Load animations
            Texture2D playerDeathTexture = Content.Load<Texture2D>("animations/playerShip_Death/playerDeath");
            int frameCount = 60;
            float frameSpeed = 3f;

            // Initialize player
            PlayerManager = new PlayerManager(new Vector2(400, 850), 100, playerTexture, 1.0f, this, playerDeathTexture, frameCount, frameSpeed);

            // Initialize scenes
            _startScene = new StartScene(_spriteBatch, _font, _menuItems, _menuTitle, this, PlayerManager.Player, _soundManager);
            _playScene = new PlayScene(_spriteBatch, _hudFont, PlayerManager.Player, laserNormalTexture, enemyBulletTexture, enemyTexture, enemyShooterTexture, powerupHealthTexture, powerupLaserTexture, this, _soundManager, playerDeathTexture, frameCount, frameSpeed);
            _helpScene = new HelpScene(_spriteBatch, _font, _menuTitle, _menuItems, this, _soundManager);
            _aboutScene = new AboutScene(_spriteBatch, _creditsTitleFont, _menuItems, _menuTitle, this, _soundManager);
            _gameOverScene = new GameOverScene(_spriteBatch, _menuItems, _menuTitle, this, SoundManager);
            _pauseScene = new PauseScene(_spriteBatch, _menuTitle, _menuItems, this, _soundManager);

            // Set initial scene
            SceneManager.ChangeScene(_startScene);
        }

        protected override void Update(GameTime gameTime)
        {
            SceneManager.Update(gameTime);
            PlayerManager.Player.UpdateTimeBasedScore(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            SceneManager.Draw(gameTime, _spriteBatch);
            base.Draw(gameTime);
        }

        // Method to change the current scene
        public void ChangeScene(GameScene newScene)
        {
            _currentScene = newScene;
            switch (newScene)
            {
                case GameScene.Start:
                    SceneManager.ChangeScene(_startScene);
                    break;
                case GameScene.Play:
                    SceneManager.ChangeScene(_playScene);
                    break;
                case GameScene.Help:
                    SceneManager.ChangeScene(_helpScene);
                    break;
                case GameScene.About:
                    SceneManager.ChangeScene(_aboutScene);
                    break;
                case GameScene.GameOver:
                    SceneManager.ChangeScene(_gameOverScene);
                    break;
            }
        }

        // Method to get current scene
        public GameScene GetCurrentScene()
        {
            return _currentScene;
        }

        // Method to save the game
        public void SaveGame()
        {
            var saveData = new SaveData
            {
                PlayerPosition = PlayerManager.Player.Position,
                PlayerHealth = PlayerManager.Player.Health,
                PlayerScore = PlayerManager.Player.Score,
                CurrentScene = _currentScene
                // Add other game state data as needed
            };

            SaveManager.SaveGame(saveData);
        }

        public void LoadGame()
        {
            var saveData = SaveManager.LoadGame();
            if (saveData == null)
                return;

            PlayerManager.Player.Position = saveData.PlayerPosition;
            PlayerManager.Player.Health = saveData.PlayerHealth;
            PlayerManager.Player.Score = saveData.PlayerScore;
            ChangeScene(saveData.CurrentScene);
        }
    }
}