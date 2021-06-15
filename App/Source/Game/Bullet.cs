using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace TcGame
{
    public class Bullet : StaticActor
    {
        public static Vector2f Up = new Vector2f(0.0f, -1.0f);

        public Vector2f Forward = new Vector2f(0.0f, -1.0f);
        public float Speed = 500.0f;

        public Bullet()
        {
            Sprite = new Sprite(Resources.Texture("Textures/Bullet"));
            Center();
        }

        public override void Update(float dt)
        {
            //Vector2f mousePosition = new Vector2f(Engine.Get.MousePos.X, Engine.Get.MousePos.Y);
            //Forward = Up.Rotate(mousePosition);

            Rotation = MathUtil.AngleWithSign(Forward, Up);
            Position += Forward * Speed * dt;

            CheckScreenLimits();
            CheckAsteroidCollision();
        }

        protected void CheckScreenLimits()
        {
            var Bounds = GetGlobalBounds();
            var ScreenSize = Engine.Get.Window.Size;

            if ((Bounds.Left > ScreenSize.X) ||
                (Bounds.Left + Bounds.Width < 0.0f) ||
                (Bounds.Top + Bounds.Width < 0.0f) ||
                (Bounds.Top > ScreenSize.Y))
            {
                Destroy();
            }
        }

        protected void CheckAsteroidCollision()
        {
            var asteroids = Engine.Get.Scene.GetAll<Asteroid>();
            foreach (Asteroid a in asteroids)
            {
                var toAsteroid = a.WorldPosition - WorldPosition;
                if (toAsteroid.Size() < 50.0f)
                {
                    // ==> EJERCICIO 1
                    // Here is where the asteroid is destroyed!
                    // We need to modify this code, so it is damaged instead of destroyed.
                    // Then, if the total damage of the asteroid is enough, it can be destroyed.
                    // REMEMBER that we still want to show the explosion everytime a Bullet hits the asteroid!!
                    a.Destroy();
                    // 

                    Destroy();
                }
            }
        }
    }
}

