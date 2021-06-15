using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace TcGame
{
    class Shield : AnimatedActor
    {
        Ship player;
        Timer clock;
        TimerDelegate timerUp;
        int stage;

        public Shield()
        {
            AnimatedSprite = new AnimatedSprite(Resources.Texture("Textures/Shield"), 3, 2);
            player = Engine.Get.Scene.GetFirst<Ship>();
            Center();
            Origin += new Vector2f(0.0f, -15.0f);
            Scale = new Vector2f(0.0f, 0.0f);

            stage = 0;
            timerUp += NextStage;
            clock = new Timer();
            clock.SetTimer(2, timerUp);
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
                    clock.SetTimer(5, timerUp);
                    break;
                case 2:
                    clock.SetTimer(2, timerUp);
                    break;
                case 3:
                    Destroy();
                    break;
            }
        }
    }
}
