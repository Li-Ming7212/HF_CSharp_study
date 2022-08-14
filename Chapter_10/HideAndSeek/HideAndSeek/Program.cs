using System;

namespace HideAndSeek {
    class Program {
        static void Main( string[] args ) {
            while (true) {
                var gameController = new GameController();
                while (!gameController.GameOver) {
                    Console.WriteLine(gameController.Status);
                    Console.Write(gameController.Prompt);
                    Console.WriteLine(gameController.ParseInput(Console.ReadLine()));
                }
                Console.WriteLine($"你获胜了，总共走了 {gameController.MoveNumber} 步！");
                Console.WriteLine("按p键重新开始游戏，其他键退出");
                if (Console.ReadKey(true).KeyChar.ToString().ToUpper() != "P")
                    return;
            }
        }
    }
}