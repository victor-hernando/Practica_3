using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace TcGame
{
    public class Asteroid : StaticActor
    {
        public float RotationSpeed = 20.0f;
        public float Speed = 200.0f;
        public Vector2f Forward = new Vector2f(1.0f, 0.0f);
        public int currentTexture;

        public Asteroid()
        {
            Sprite = new Sprite(Resources.Texture("Textures/Asteroid0"+currentTexture));
            Center();
            OnDestroy += OnAsteroidDestroyed;
        }

        public override void Update(float dt)
        {
            Position += Forward * Speed * dt;
            Rotation += RotationSpeed * dt;
            MyGame.ResolveLimits(this);
        }

        void OnAsteroidDestroyed(Actor obj)
        {
            var hud = Engine.Get.Scene.GetFirst<HUD>();
            if (hud != null)
            {
                hud.Points += 100;
            }
            DoExplosion();
        }
        public void DoExplosion()
        {

            var explosion = Engine.Get.Scene.Create<Explosion>();
            explosion.WorldPosition = WorldPosition;
        }
    }
}
