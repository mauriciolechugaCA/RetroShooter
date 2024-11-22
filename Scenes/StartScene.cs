using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

        public StartScene(SpriteBatch spriteBatch, SpriteFont font, SpriteFont menuItemsfont, SpriteFont menuTitlefont, Game game, Player player) 
        {
            _spriteBatch = spriteBatch;
            _font = font;
            _menuItemsfont = menuItemsfont;
            _menuTitlefont = menuTitlefont;
            _menuItems = new List<string> { "Play", "Help", "About", "Exit" };
            _selectedMenuItem = 0;
            _game = (Game1)game;
            _inputManager = new InputManager();
            _player = player; 
        }

        public override void Update(GameTime gameTime)
        {
            _inputManager.Update();

            if (_inputManager.IsKeyPressed(Keys.Up) || _inputManager.IsKeyPressed(Keys.W))
            {
                _selectedMenuItem--;
                if (_selectedMenuItem < 0)
                {
                    _selectedMenuItem = _menuItems.Count - 1;
                }
            }
            else if (_inputManager.IsKeyPressed(Keys.Down) || _inputManager.IsKeyPressed(Keys.S))
            {
                _selectedMenuItem++;
                if (_selectedMenuItem >= _menuItems.Count)
                {
                    _selectedMenuItem = 0;
                }
            }
            else if (_inputManager.IsKeyPressed(Keys.Enter) || _inputManager.IsKeyPressed(Keys.Space))
            {
                switch (_selectedMenuItem)
                {
                    case 0:
                        SceneManager.ChangeScene(new PlayScene(_spriteBatch, _font, _game.PlayerManager.Player, _game.laserNormalTexture, _game.enemyBulletTexture)); 
                        break;
                    case 1:
                        SceneManager.ChangeScene(new HelpScene(_spriteBatch, _font));
                        break;
                    case 2:
                        // TODO: AboutScene
                        SceneManager.ChangeScene(new AboutScene(_spriteBatch, _font, _menuItemsfont, _menuTitlefont, _game));
                        break;
                    case 3:
                        // Exit
                        _game.Exit();
                        break;
                }
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _spriteBatch.Begin();

            // Draw the title
            string title = "Retro Shooter";
            var titleSize = _menuTitlefont.MeasureString(title);
            var titlePosition = new Vector2((_game.GraphicsDevice.Viewport.Width - titleSize.X) / 2, 175);

            _spriteBatch.DrawString(_menuTitlefont, title, titlePosition, Color.White);

            // Draw the menu items
            for (int i = 0; i < _menuItems.Count; i++)
            {
                var text = _menuItems[i];
                var textSize = _menuItemsfont.MeasureString(text);
                var textPosition = new Vector2((_game.GraphicsDevice.Viewport.Width - textSize.X) / 2, 275 + i * 100);
                var color = (i == _selectedMenuItem) ? Color.Yellow : Color.White;
                _spriteBatch.DrawString(_menuItemsfont, text, textPosition, color);
            }

            _spriteBatch.End();
        }
    }
}
