using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RetroShooter.Managers
{
    public static class SceneManager
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

        public static void PopScene()
        {
            if (_scenes.Count > 0)
            {
                _scenes.Pop();
            }
        }

        public static Scene GetCurrentScene()
        {
            return _scenes.Count > 0 ? _scenes.Peek() : null;
        }
    }

    public abstract class Scene
    {
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
