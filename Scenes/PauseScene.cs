using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using RetroShooter.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace RetroShooter.Scenes
{
    public class PauseScene : Scene
    {
        private SpriteBatch _spriteBatch;
        private SpriteFont _menuTitlefont;
        private SpriteFont _menuItemsfont;
        private Game1 _game;
        private InputManager _inputManager;
        private string[] _menuItems = { "Continue", "Save", "Main Menu" };
        private int _selectedIndex;
        private BackgroundManager _backgroundManager;
        private SoundManager _soundManager;

        public PauseScene(SpriteBatch spriteBatch, SpriteFont menuTitlefont, SpriteFont menuItemsfont, Game1 game, SoundManager soundManager)
        {
            _spriteBatch = spriteBatch;
            _menuTitlefont = menuTitlefont;
            _menuItemsfont = menuItemsfont;
            _game = game;
            _inputManager = new InputManager();
            _selectedIndex = 0;
            _backgroundManager = new BackgroundManager(_game.backgroundTexture, _game.floatingMeteorsTextures, 1000, 1500, _spriteBatch);
            _soundManager = new SoundManager();
        }

        public override void Update(GameTime gameTime)
        {
            _inputManager.Update();

            if (_inputManager.IsKeyPressed(Keys.Up))
            {
                _selectedIndex--;
                if (_selectedIndex < 0)
                {
                    _selectedIndex = _menuItems.Length - 1;
                }
                _soundManager.PlaySoundEffect("select_002"); // Sound effect for menu selection
            }
            else if (_inputManager.IsKeyPressed(Keys.Down))
            {
                _selectedIndex++;
                if (_selectedIndex >= _menuItems.Length)
                {
                    _selectedIndex = 0;
                }
                _soundManager.PlaySoundEffect("select_002"); // Sound effect for menu selection
            }
            else if (_inputManager.IsKeyPressed(Keys.Enter))
            {
                _soundManager.PlaySoundEffect("confirmation_001"); // Sound effect for menu selection confirmation
                switch (_selectedIndex)
                {
                    case 0: // Continue
                        SceneManager.PopScene();
                        break;
                    case 1: // Save
                        // Implement save functionality later
                        break;
                    case 2: // Main Menu
                        _game.ChangeScene(GameScene.Start);
                        break;
                }
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            // Draw background and floating meteors
            _backgroundManager.Draw(_spriteBatch);

            string pauseText = "Game Paused";
            Vector2 pauseTextSize = _menuTitlefont.MeasureString(pauseText);
            Vector2 pauseTextPosition = new Vector2(
                (_game.GraphicsDevice.Viewport.Width - pauseTextSize.X) / 2,
                175
            );
            var shadowOffset = new Vector2(4, 4);

            spriteBatch.DrawString(_menuTitlefont, pauseText, pauseTextPosition + shadowOffset, Color.Black);
            spriteBatch.DrawString(_menuTitlefont, pauseText, pauseTextPosition, Color.LightSeaGreen);

            for (int i = 0; i < _menuItems.Length; i++)
            {
                Color color = (i == _selectedIndex) ? Color.IndianRed : Color.LightGray;
                Vector2 textSize = _menuItemsfont.MeasureString(_menuItems[i]);
                Vector2 position = new Vector2(
                    (_game.GraphicsDevice.Viewport.Width - textSize.X) / 2,
                    300 + i * 125
                );

                spriteBatch.DrawString(_menuItemsfont, _menuItems[i], position + shadowOffset, Color.Black);
                spriteBatch.DrawString(_menuItemsfont, _menuItems[i], position, color);
            }

            spriteBatch.End();
        }
    }
}