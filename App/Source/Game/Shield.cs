using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace TcGame
{
    class Shield : AnimatedActor
    {
        Ship player;
        Timer clock;
        int stage;

        public Shield()
        {
            AnimatedSprite = new AnimatedSprite(Resources.Texture("Textures/Shield"), 3, 2);
            player = Engine.Get.Scene.GetFirst<Ship>();
            Center();
            Origin += new Vector2f(0.0f, -15.0f);
            Scale = new Vector2f(0.0f, 0.0f);
            player.protection=true;
            stage = 0;
            clock = new Timer();
            clock.SetTimer(2, NextStage);
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            clock.Update(dt);
            Position = player.Position;

            switch (stage)
            {
                case 0:
                    Scale += new Vector2f(0.5f * dt, 0.5f * dt);
                    break;
                case 2:
                    Scale -= new Vector2f(0.5f * dt, 0.5f * dt);
                    break;
                default:
                    break;
            }
        }

        void NextStage()
        {
            stage++;
            switch (stage)
            {
                case 1:
                    clock.SetTimer(5, NextStage);
                    break;
                case 2:
                    clock.SetTimer(2, NextStage);
                    break;
                case 3:
                    player.protection = false;
                    Destroy();
                    break;
            }
        }
    }
}
