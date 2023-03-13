using System;
using System.IO;

namespace Logic
{
    // fighter class, both player and AI
    internal class Player {
        public int health;
        public int energy;

        public Player() {
            health = 100;
            energy = 100;
        }
    }

    // Service class to provide player & ai actions
    internal class GameControl {
      public Player player;
      public Player xenos;

      private Random rnd;

      const string LOW_ENERGY = "Недостаточно энергии, ты пропусскаешь ход!";

      public GameControl(Player p, Player v){
        player = p;
        xenos = v;
        rnd = new Random();
      }

      public void ShowMenu(Player pc, Player ai) {
          Console.Clear();

          Console.WriteLine(@"    Жизни:   {0}         Жизни ксеноса: {1}", pc.health, ai.health);
          Console.WriteLine(@"    Энергия: {0}         Энергия ксеноса: {1}", pc.energy, ai.energy);
          Console.WriteLine();

          Console.WriteLine("1. Атака цепным мечом. (5-20 урона; -10 энергии)");
          Console.WriteLine("2. Атака болтером (20-50 урона; -40 энергии)");
          Console.WriteLine("3. Молитва Богу-Императору (+20 энергии)");
          Console.WriteLine("4. Применить стимпак (+30 жизни, -20 энергии)");

          Console.WriteLine();
      }

      // Player methods
      public void chainsword_attack() {
        if (player.energy >= 10) {
          int damage = rnd.Next(5, 20);
          Console.WriteLine(@"Воин Империума наносит {0} урона грязному ксеносу!", damage);

          player.energy -= 10;
          xenos.health -= damage;
        } else {
          Console.WriteLine(LOW_ENERGY);
        }
      }

      public void bolter_attack() {
        int damage = rnd.Next(20, 50);
        Console.WriteLine(@"Воин Империума наносит {0} урона грязному ксеносу!", damage);

        if (player.energy >= 40) {
          xenos.health -= damage;
          player.energy -= 40;
        } else {
          Console.WriteLine(LOW_ENERGY);
          Console.ReadLine();
        }
      }

      public void pray() {
        player.energy += 20;
      }

      public void steampack() {
        if (player.energy >= 20) {
            player.health += 30;
            player.energy -= 20;
        } else {
            Console.WriteLine(LOW_ENERGY);
            Console.ReadLine();
        }
      }

      // AI methods
      public void light_attack() {
          int damage = rnd.Next(15, 30);
          Console.WriteLine("Ксенос кусает вашу могучую броню! {0} урона", damage);

          if (xenos.energy < 12) return;

          player.health -= damage;
          xenos.energy -= 12;
      }

      public void heavy_attack() {
          int damage = rnd.Next(20, 40);
          Console.WriteLine("Ксенос стреляет в вас! {0} урона", damage);
          if (xenos.energy < 40) return;

          player.health -= damage;
          xenos.energy -= 40;
      }

      public void deep_breath() {
          Console.WriteLine("Ксенос пытается перевести дух и собрать энергию!");
          xenos.energy += 20;
      }
      public void flee() {
          Console.WriteLine("Ксенос в ужасе съеживается, пытаясь пережить ваши атаки и зализать раны!");
          if (xenos.energy < 20) return;

          xenos.health += 25;
          xenos.energy -= 20;
      }
    }

    internal class Game
    {
        const int ACTION_LIGHT_ATTACK = 1;
        const int ACTION_HEAVY_ATTACK = 2;
        const int ACTION_RESTORE_ENERGY = 3;
        const int ACTION_RESTORE_HEALTH = 4;

        static bool CheckGameOver(Player pc, Player ai) {
            bool result = false;
            // определение победы или поражения
            if (pc.health <= 0)
            {
                Console.WriteLine("Грязный ксенос победил. Ты разочаровал Бога-Императора!");
                Console.ReadKey();

                result = true;
            }

            if (ai.health <= 0)
            {
                Console.WriteLine("Ты победил! Слава Империуму!");
                Console.ReadKey();

                result = true;
            }

            return result;
        }


        static void Main(string[] args)
        {
            Random rnd = new Random();

            Player player = new Player();
            Player xenos = new Player();

            GameControl gc = new GameControl(player, xenos);

            int action = -1;

            while (true)
            {
                gc.ShowMenu(player, xenos);
                if (CheckGameOver(player, xenos)) break;

                action = int.Parse(Console.ReadLine());

                // Player input processing
                switch (action)
                {
                    case ACTION_LIGHT_ATTACK:
                      gc.chainsword_attack();

                      break;
                    case ACTION_HEAVY_ATTACK:
                        gc.bolter_attack();

                        break;
                    case ACTION_RESTORE_ENERGY:
                        gc.pray();

                        break;
                    case ACTION_RESTORE_HEALTH:
                        gc.steampack();

                        break;
                }

                // AI Logic
                action = rnd.Next(1, 4);

                if(xenos.health >= 20 && xenos.energy <= 20) action = ACTION_RESTORE_ENERGY;
                // AI heals if his HP is low
                if (xenos.health <= 20) action = ACTION_RESTORE_HEALTH;

                switch (action)
                {
                    case ACTION_LIGHT_ATTACK:
                        gc.light_attack();

                        break;
                    case ACTION_HEAVY_ATTACK:
                        gc.heavy_attack();

                        break;
                    case ACTION_RESTORE_ENERGY:
                       gc.deep_breath();

                        break;
                    case ACTION_RESTORE_HEALTH:
                        gc.flee();

                        break;
                }

                Console.ReadKey();
            }
        }
    }
}
