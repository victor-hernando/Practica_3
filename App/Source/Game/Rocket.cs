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

        public Rocket()
        {
            Sprite = new Sprite(Resources.Texture("Textures/Rocket"));
            Center();
            Speed = 300f;
            rotationSpeed = 90.0f;
        }

        public override void Update(float dt)
        {
            asteroids = Engine.Get.Scene.GetAll<Asteroid>();

            foreach(Asteroid a in asteroids)
            {
                Vector2f desiredForward = (a.Position - Position).Normal();
                float angle = MathUtil.AngleWithSign(desiredForward, Forward);

                float newAngle = angle > 0.0f ? rotationSpeed * dt : -rotationSpeed * dt;

                Transform rotationTransform = Transform.Identity;
                rotationTransform.Rotate(newAngle);

                Forward = rotationTransform * Forward;
                Position += Forward * Speed * dt;

                Rotation = MathUtil.AngleWithSign(Forward, Up);
            }

            CheckScreenLimits();
            CheckAsteroidCollision();
        }
    }
}
