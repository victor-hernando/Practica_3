using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Diagnostics;

namespace TcGame
{
    public class Ship : StaticActor
    {
        private static Vector2f Up = new Vector2f(0.0f, -1.0f);
        private Vector2f Forward = Up;
        private float Speed = 200.0f;
        private float RotationSpeed = 100.0f;
        private float RotationModifier;
        public bool protection;
        private DateTime time;
        private TimeSpan frecuency = TimeSpan.FromSeconds(0.2f);

        public Ship()
        {
            Sprite = new Sprite(Resources.Texture("Textures/Ship"));
            Center();
            OnDestroy += OnShipDestroy;

            Engine.Get.Window.KeyPressed += HandleKeyPressed;
            //Engine.Get.Window.MouseButtonPressed += HandleMousePressed;

            var flame = Engine.Get.Scene.Create<Flame>(this);
            flame.Position = Origin + new Vector2f(20.0f, 62.0f);

            var flame2 = Engine.Get.Scene.Create<Flame>(this);
            flame2.Position = Origin + new Vector2f(-20.0f, 62.0f);
        }

        private void HandleKeyPressed(object sender, KeyEventArgs e)
        {
            if(e.Code == Keyboard.Key.C)
            {
                Shoot<Rocket>();
            }
            if (e.Code == Keyboard.Key.G)
            {
                Engine.Get.Scene.Create<Shield>();
            }
        }

        /*private void HandleMousePressed(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                Shoot<Bullet>();
            }
        }*/

        public override void Update(float dt)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.A))
            {
                Rotation -= RotationSpeed * dt*RotationModifier;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
            {
                Rotation += RotationSpeed * dt*RotationModifier;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.LShift))
            {
                RotationModifier=0.0f;
                Speed = 400.0f;
            }
            else
            {
                RotationModifier = 1.0f;
                Speed = 200.0f;
            }

            if (Mouse.IsButtonPressed(Mouse.Button.Left) && time + frecuency <= DateTime.Now)
            {
                Shoot<Bullet>();
                time = DateTime.Now;
            }

            Forward = Up.Rotate(Rotation);
            Position += Forward * Speed * dt;

            MyGame.ResolveLimits(this);
            CheckCollision();
        }

        private void CheckCollision()
        {
            if (!protection)
            {
                var asteroids = Engine.Get.Scene.GetAll<Asteroid>();
                foreach (var a in asteroids)
                {
                    Vector2f toAsteroid = a.WorldPosition - WorldPosition;
                    if (toAsteroid.Size() < 50.0f)
                    {
                        Destroy();
                        a.Destroy();
                    }
                }
            }
        }

        void OnShipDestroy(Actor obj)
        {
            Engine.Get.Window.KeyPressed -= HandleKeyPressed;
        }

        private void Shoot<T>() where T : Bullet
        {
            var rocket = Engine.Get.Scene.Create<T>();
            rocket.WorldPosition = WorldPosition;
            rocket.Init();
        }
    }
}

