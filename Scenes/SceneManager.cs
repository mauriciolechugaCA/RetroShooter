using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RetroShooter.Scenes
{
    internal class SceneManager
    {
        private static Stack<Scene> _scenes = new Stack<Scene>();
        private static Game1 _game;
        private static Scene _currentScene;

        public static void Initialize(Game game)
        {
            _game = (Game1)game;
        }

        public static void ChangeScene(Scene newScene)
        {
            _scenes.Push(newScene);
        }

        public static void Update(GameTime gameTime)
        {
            if (_scenes.Count > 0)
            {
                var currentScene = _scenes.Peek();
                currentScene.Update(gameTime);
            }
        }

        public static void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (_scenes.Count > 0)
            {
                var currentScene = _scenes.Peek();
                currentScene.Draw(gameTime, spriteBatch);
            }
        }
    }

    public abstract class Scene
    {
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
