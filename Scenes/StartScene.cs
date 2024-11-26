using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using RetroShooter.Entities;
using RetroShooter.Managers;

namespace RetroShooter.Scenes
{
    public class StartScene : Scene
    {
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;
        private SpriteFont _menuItemsfont;
        private SpriteFont _menuTitlefont;
        private List<string> _menuItems;
        private int _selectedMenuItem;
        private Game1 _game;
        private InputManager _inputManager;
        private Player _player; 
        private BackgroundManager _backgroundManager;
        private SoundManager _soundManager;
        private Texture2D playerDeathTexture;
        private int frameCount;
        private float frameSpeed;

        public StartScene(SpriteBatch spriteBatch, SpriteFont font, SpriteFont menuItemsfont, SpriteFont menuTitlefont, Game1 game, Player player, SoundManager soundManager) 
        {
            _spriteBatch = spriteBatch;
            _font = font;
            _menuItemsfont = menuItemsfont;
            _menuTitlefont = menuTitlefont;
            _menuItems = new List<string> { "Play", "Help", "About", "Exit" };
            _selectedMenuItem = 0;
            _game = game;
            _inputManager = new InputManager();
            _player = player;
            _backgroundManager = new BackgroundManager(_game.backgroundTexture, _game.floatingMeteorsTextures, 1000, 1500, _spriteBatch);
            _soundManager = soundManager;

            _soundManager.PlaySong("background_music");
        }

        public override void Update(GameTime gameTime)
        {
            _inputManager.Update();

            if (MediaPlayer.State != MediaState.Playing)
            {
                _soundManager.PlaySong("background_music");
            }

            // Update background and floating meteors
            _backgroundManager.Update(gameTime);

            if (_inputManager.IsKeyPressed(Keys.Up) || _inputManager.IsKeyPressed(Keys.W))
            {
                _selectedMenuItem--;
                if (_selectedMenuItem < 0)
                {
                    _selectedMenuItem = _menuItems.Count - 1;
                }
                _soundManager.PlaySoundEffect("select_002"); // Sound effect for menu selection
            }
            else if (_inputManager.IsKeyPressed(Keys.Down) || _inputManager.IsKeyPressed(Keys.S))
            {
                _selectedMenuItem++;
                if (_selectedMenuItem >= _menuItems.Count)
                {
                    _selectedMenuItem = 0;
                }
                _soundManager.PlaySoundEffect("select_002"); // Sound effect for menu selection
            }
            else if (_inputManager.IsKeyPressed(Keys.Enter) || _inputManager.IsKeyPressed(Keys.Space))
            {
                _soundManager.PlaySoundEffect("confirmation_001"); // Sound effect for menu selection confirmation
                SelectMenuItem();
            }

            // Checks for mouse input
            var mousePosition = _inputManager.GetMousePosition();
            for (int i = 0; i < _menuItems.Count; i++)
            {
                var text = _menuItems[i];
                var textSize = _menuItemsfont.MeasureString(_menuItems[i]);
                var textPosition = new Vector2((_game.GraphicsDevice.Viewport.Width - textSize.X) / 2, 300 + i * 100);

                var textRectangle = new Rectangle((int)textPosition.X, (int)textPosition.Y, (int)textSize.X, (int)textSize.Y);
                if (textRectangle.Contains(mousePosition))
                {
                    _selectedMenuItem = i;
                    if (_inputManager.IsMouseButtonPressed(ButtonState.Pressed))
                    {
                        _soundManager.PlaySoundEffect("select_002"); // Sound effect for menu selection
                        SelectMenuItem();
                    }
                    break;
                }
            }
        }

        private void SelectMenuItem()
        {
            switch (_selectedMenuItem)
            {
                case 0:
                    SceneManager.ChangeScene(new PlayScene(_spriteBatch, _font, _game.PlayerManager.Player, _game.laserNormalTexture, _game.enemyBulletTexture, _game.enemyTexture, _game.powerupHealthTexture, _game.powerupLaserTexture, _game, _soundManager, playerDeathTexture, frameCount, frameSpeed));
                    break;
                case 1:
                    SceneManager.ChangeScene(new HelpScene(_spriteBatch, _font, _menuTitlefont, _menuItemsfont, _game, _soundManager));
                    break;
                case 2:
                    SceneManager.ChangeScene(new AboutScene(_spriteBatch, _font, _menuItemsfont, _menuTitlefont, _game, _soundManager));
                    break;
                case 3:
                    _game.Exit();
                    break;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _spriteBatch.Begin();

            // Draw background and floating meteors
            _backgroundManager.Draw(_spriteBatch);

            // Draw the title
            string title = "Retro Shooter";
            var titleSize = _menuTitlefont.MeasureString(title);
            var titlePosition = new Vector2((_game.GraphicsDevice.Viewport.Width - titleSize.X) / 2, 175);

            var shadowOffset = new Vector2(4, 4);

            _spriteBatch.DrawString(_menuTitlefont, title, titlePosition + shadowOffset, Color.Black);
            _spriteBatch.DrawString(_menuTitlefont, title, titlePosition, Color.LightSeaGreen);

            // Draw the menu items
            for (int i = 0; i < _menuItems.Count; i++)
            {
                var text = _menuItems[i];
                var textSize = _menuItemsfont.MeasureString(text);
                var textPosition = new Vector2((_game.GraphicsDevice.Viewport.Width - textSize.X) / 2, 300 + i * 100);
                var color = (i == _selectedMenuItem) ? Color.IndianRed : Color.LightGray;
                _spriteBatch.DrawString(_menuItemsfont, text, textPosition + shadowOffset, Color.Black);
                _spriteBatch.DrawString(_menuItemsfont, text, textPosition, color);
            }

            _spriteBatch.End();
        }
    }
}
