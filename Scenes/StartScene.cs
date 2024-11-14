using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RetroShooter.Scenes;

namespace RetroShooter.Scenes
{
    public class StartScene
    {
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;
        private List<string> _menuItems;
        private int _selectedMenuItem;

        public StartScene(SpriteBatch spriteBatch, SpriteFont font)
        {
            _spriteBatch = spriteBatch;
            _font = font;
            _menuItems = new List<string> { "Play", "Help", "About", "Exit" };
            _selectedMenuItem = 0;
        }

        public void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                _selectedMenuItem--;
                if (_selectedMenuItem < 0)
                {
                    _selectedMenuItem = _menuItems.Count - 1;
                }
            }
            else if (keyboardState.IsKeyDown(Keys.Down))
            {
                _selectedMenuItem++;
                if (_selectedMenuItem >= _menuItems.Count)
                {
                    _selectedMenuItem = 0;
                }
            }
            else if (keyboardState.IsKeyDown(Keys.Enter))
            {
                switch (_selectedMenuItem)
                {
                    case 0:
                        // TODO: Play
                        break;
                    case 1:
                        // TODO: Help
                        break;
                    case 2:
                        // TODO: About
                        break;
                    case 3:
                        // TODO: Exit
                        break;
                }
                
            }
        }

        public void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            for (int i = 0; i < _menuItems.Count; i++)
            {
                var color = (i == _selectedMenuItem) ? Color.Yellow : Color.White;
                _spriteBatch.DrawString(_font, _menuItems[i], new Vector2(100, 100 + i * 50), color);
            }

            _spriteBatch.End();
        }
    }
}
