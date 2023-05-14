using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using TicTacToe;
using System.Dynamic;

namespace TicTacToe {
  class Game {
    static void Main(string[] args) {
      Random rnd = new Random();
      bool gameOngoing = true;

      Grid grid = new Grid();

      Player player;
      Player bot;

      // Toss a coin to your witcher:
      int coin = rnd.Next(1, 100);
      if (coin % 2 == 0) {
        player = new Player(Field.FIGURE_CROSS);
        bot = new Player(Field.FIGURE_CIRCLE);
      } else {
        player = new Player(Field.FIGURE_CIRCLE);
        bot = new Player(Field.FIGURE_CROSS);
      }

      Console.WriteLine(@"You are playing as {0}", player.Draw());
      Console.WriteLine("Press any key to start!");
      Console.ReadKey();

      while (gameOngoing) {
        grid.Draw();

        int[] free_fields = grid.AvailableFields();

        if (free_fields.Length == 0) {
          Console.WriteLine("It's a draw!");
          gameOngoing = false;

          break;
        }

        // Player turn
        if (player.current_turn == true) {
          Console.WriteLine("Enter field number");

          // User Input check
          try {
            int field = int.Parse(Console.ReadLine());
            // Check if field is already marked
            if (grid.CheckField(field - 1)) throw new IndexOutOfRangeException();

            grid.Turn(field - 1, player.Draw());
          } catch(FormatException) {
            Console.WriteLine("Wrong input! Press any key to retry.");
            Console.ReadKey();

            continue;
          } catch(IndexOutOfRangeException) {
            Console.WriteLine("Wrong number! Press any key to retry.");
            Console.ReadKey();

            continue;
          }

          // Check win condition
          if (player.CheckWin(grid)) {
            grid.Draw();
            Console.WriteLine("You win!");
            gameOngoing = false;

            break;
          }

          // Player switch
          player.Turn();
          bot.Ready();

          // Avoid passing turn to AI while current turn
          continue;
        } else {
          // AI turn
          //int bot_field = rnd.Next(0, free_fields.Length);
          //grid.Turn(free_fields[bot_field], bot.Draw());
          int bot_field = AiLogic.GetTargetField(grid, bot.Draw());
          if (bot_field == -1) Console.WriteLine("Something went wrong"); //���� ������� �� ������ ������
                                                                          //�� ����� �� ���� ��� � �� ����������
          else grid.Turn(bot_field, bot.Draw());

          if (bot.CheckWin(grid)) {
            grid.Draw();
            Console.WriteLine("Bot wins!");
            gameOngoing = false;

            break;
          }

          // Player switch
          bot.Turn();
          player.Ready();
        }
      }

      Console.ReadKey();
    }
  }
}
