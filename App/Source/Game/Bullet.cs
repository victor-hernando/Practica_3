using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System;
using System.Diagnostics;

namespace TcGame
{
    public class Bullet : StaticActor
    {
        public static Vector2f Up = new Vector2f(0.0f, -1.0f);

        public Vector2f Forward = new Vector2f(0.0f, -1.0f);
        public float Speed = 500.0f;
        Vector2f mouseDirection;

        public Bullet()
        {
            Sprite = new Sprite(Resources.Texture("Textures/Bullet"));
            Center();
        }

        public virtual void Init()
        {
            mouseDirection = new Vector2f(Engine.Get.MousePos.X - Position.X, Engine.Get.MousePos.Y - Position.Y);
            Forward = mouseDirection.Normal();
        }

        public override void Update(float dt)
        {
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
                    a.currentTexture++;
                    if (a.currentTexture < 3)
                    {
                        a.Sprite = new Sprite(Resources.Texture("Textures/Asteroid0" + a.currentTexture));
                        a.Center();
                        a.DoExplosion();
                    }
                    else a.Destroy();
                    Destroy();
                }
            }
        }
    }
}

