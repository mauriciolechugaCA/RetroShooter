using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RetroShooter.Scenes;
using RetroShooter.Entities;
using RetroShooter.Managers;

namespace RetroShooter
{
    public enum GameScene
    {
        Start,
        Play,
        Help,
        About,
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // Fonts
        private SpriteFont _font;
        private SpriteFont _hudFont;
        private SpriteFont _creditsTitleFont;
        private SpriteFont _menuTitle;
        private SpriteFont _menuItems;

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

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();

            PlayerManager = new PlayerManager(new Vector2(400, 800), 4, playerTexture, 1.0f);

            // Initialize first scene
            SceneManager.ChangeScene(new StartScene(
                _spriteBatch,
                _font,
                _menuItems,
                _menuTitle,
                this,
                PlayerManager.Player
            ));

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
            enemyTexture = Content.Load<Texture2D>("images/enemies/enemyShip1");
            enemyShooterTexture = Content.Load<Texture2D>("images/enemies/enemyShip3");


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

            // Initialize player
            PlayerManager = new PlayerManager(new Vector2(400, 600), 4, playerTexture, 1.0f);

            // Initialize scenes
            _startScene = new StartScene(_spriteBatch, _font, _menuItems, _menuTitle, this, PlayerManager.Player);
            _playScene = new PlayScene(_spriteBatch, _hudFont, PlayerManager.Player, laserNormalTexture, enemyBulletTexture, enemyTexture, powerupHealthTexture, powerupLaserTexture, this);
            _helpScene = new HelpScene(_spriteBatch, _font, this);
            _aboutScene = new AboutScene(_spriteBatch, _creditsTitleFont, _menuItems, _menuTitle, this);

            // Set initial scene
            SceneManager.ChangeScene(_startScene);
        }

        protected override void Update(GameTime gameTime)
        {
            // Exit game if escape is pressed
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            // Update current scene
            SceneManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // Draw current scene
            SceneManager.Draw(gameTime, _spriteBatch);

            base.Draw(gameTime);
        }

        // Helper method to change scenes
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
            }
        }

        // Method to get current scene
        public GameScene GetCurrentScene()
        {
            return _currentScene;
        }
    }
}

