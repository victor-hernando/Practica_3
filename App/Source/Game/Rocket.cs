using System;
using System.Collections.Generic;
using System.Text;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace TcGame
{
    class Rocket : Bullet
    {
        float rotationSpeed;
        List<Asteroid> asteroids = new List<Asteroid>();
        float closestDistance;
        Asteroid closestAsteroid;

        public Rocket()
        {
            Sprite = new Sprite(Resources.Texture("Textures/Rocket"));
            Center();
            Speed = 300f;
            rotationSpeed = 90.0f;

            var flame = Engine.Get.Scene.Create<Flame>(this);
            flame.Position = Origin + new Vector2f(1.0f, 35.0f);
        }

        public override void Init()
        {

        }

        public override void Update(float dt)
        {
            asteroids = Engine.Get.Scene.GetAll<Asteroid>();

            if(asteroids.Count != 0)
            {
                closestDistance = (asteroids[0].Position - Position).Size();
                closestAsteroid = asteroids[0];

                foreach (Asteroid a in asteroids)
                {
                    float nextAsteroidDistance = (a.Position - Position).Size();

                    if (nextAsteroidDistance < closestDistance)
                    {
                        closestAsteroid = a;
                    }
                }

                Vector2f newForward = (closestAsteroid.Position - Position).Normal();
                float angle = MathUtil.AngleWithSign(newForward, Forward);

                float newAngle = angle > 0.0f ? rotationSpeed * dt : -rotationSpeed * dt;

                Transform rotationTransform = Transform.Identity;
                rotationTransform.Rotate(newAngle);

                Forward = rotationTransform * Forward;
                Position += Forward * Speed * dt;

                Rotation = MathUtil.AngleWithSign(Forward, Up);
            }
            else
            {
                base.Update(dt);
            }

            CheckScreenLimits();
            CheckAsteroidCollision();
        }
    }
}
