using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe;
using System.Runtime.CompilerServices;

namespace TicTacToe {
  internal static class AiLogic {
    public static int GetTargetField(Grid grid1, string figure) {
      int target_field, curr_max_weight, curr_weight;
      target_field = curr_max_weight = curr_weight = -1;

      for (int i = 0; i < grid1.grid.Length; i++) {         // обходим массив всех ячеек и для каждой вычисляем вес
        curr_weight = EvaluateField(grid1, i, figure);
        if (curr_weight <= curr_max_weight) continue;       // сравниваем вес текущей ячейки с текущим максимумом

        target_field = i;                                   // если нашлась ячейка с большим весом, запоминаем ее номер и вес
        curr_max_weight = curr_weight;
      }
      return target_field;
    }

    // на самом деле вполне очевидно, что может быть несколько ячеек с одинаковыми (максимальными) весами.
    // бот выбирает наименьший номер ячейки
    // для большего разнообразия можно запоминать (сделать отдельный массив/список или дописать в Field свойство "вес")
    // ячейки с одинаковыми весами и выбирать по рандому. Но не хотелось менять Field по принципу "не трогай чужой код, если не особо надо"
    // а делать еще что-то было лень, хотелось поскорее запустить

    private static int EvaluateField(Grid grid, int field_num, string figure) {
      int field_weight = 0;

      if (grid.grid[field_num].busy) {
        field_weight = -1;
        return field_weight;
      }

      int eval_diagonal = EvalDiagonal(grid, field_num, figure);
      int eval_rev_diagonal = EvalRevDiag(grid, field_num, figure);
      int eval_horizontal = EvalHorizontal(grid, field_num, figure);
      int eval_vertical = EvalVertical(grid, field_num, figure);

      field_weight = field_weight + eval_diagonal + eval_horizontal + eval_vertical + eval_rev_diagonal;

      return field_weight;
    }

    private static int EvalHorizontal(Grid grid, int field_num, string figure) {
      const int grid_size = 3; // для масштабируемости поля утащить в какую-то более глобальную область
      int ycoord = field_num / grid_size;

      int own_count, opp_count, free_count, weight;
      own_count = opp_count = free_count = weight = 0;

      for (int xcoord = 0; xcoord < grid_size; xcoord++) {
        Field field = grid.grid[ycoord * grid_size + xcoord];

        if (!field.busy) free_count++;
          else if (field.value == figure) own_count++;
      }

      opp_count = grid_size - own_count - free_count;

      if (own_count == grid_size - 1) weight += 1000;
      if (opp_count == grid_size - 1) weight += 500;

      if (opp_count == 0) {
        if (own_count > 0) weight += 2;
          else weight += 1;
      }

      return weight;
    }

    private static int EvalVertical(Grid grid, int field_num,  string figure) {
      const int grid_size = 3; // для масштабируемости поля утащить в какую-то более глобальную область

      int xcoord = field_num % grid_size;

      int own_count, opp_count, free_count, weight;
      own_count = opp_count = free_count = weight = 0;

      for (int ycoord = 0; ycoord < grid_size; ycoord++) {
        Field field = grid.grid[ycoord * grid_size + xcoord];

        if (!field.busy) free_count++;
          else if (field.value == figure) own_count++;
      }

      opp_count = grid_size - own_count - free_count;

      if (own_count == grid_size - 1) weight += 1000;
      if (opp_count == grid_size - 1) weight += 500;

      if (opp_count == 0) {
        if (own_count > 0) weight += 2; else weight += 1;
      }

      return weight;
    }

    private static int EvalDiagonal(Grid grid, int field_num, string figure) {
      const int grid_size = 3; // для масштабируемости поля утащить в какую-то более глобальную область

      int xcoord = field_num % grid_size;
      int ycoord = field_num / grid_size;

      int own_count, opp_count, free_count, weight;
      own_count = opp_count = free_count = weight = 0;

      if (xcoord != ycoord) return weight;

      for (int i = 0; i < grid_size; i++) {
        Field field = grid.grid[i * grid_size + i];

        if (!field.busy) free_count++;
          else if (field.value == figure) own_count++;
      }

      opp_count = grid_size - own_count - free_count;

      if (own_count == grid_size - 1) weight += 1000;
      if (opp_count == grid_size - 1) weight += 500;

      if (opp_count == 0) {
        if (own_count > 0) weight += 2; else weight += 1;
      }

      return weight;
    }

    private static int EvalRevDiag(Grid grid, int field_num, string figure) {
      const int grid_size = 3; // для масштабируемости поля утащить в какую-то более глобальную область

      int xcoord = field_num % grid_size;
      int ycoord = field_num / grid_size;

      int own_count, opp_count, free_count, weight;
      own_count = opp_count = free_count = weight = 0;

      if (xcoord + ycoord + 1 != grid_size) return weight;  // ячейка принадлежит побочной диагонали, если сумма координат + 1
                                                            // равна стороне таблицы
      for (int i = 0; i < grid_size; i++) {
        Field field = grid.grid[i * grid_size - i];

        if (!field.busy) free_count++;
        else if (field.value == figure) own_count++;
      }

      opp_count = grid_size - own_count - free_count;

      if (own_count == grid_size - 1) weight += 1000;
      if (opp_count == grid_size - 1) weight += 500;

      if (opp_count == 0) {
        if (own_count > 0) weight += 2; else weight += 1;
      }

      return weight;
    }
  }
}
