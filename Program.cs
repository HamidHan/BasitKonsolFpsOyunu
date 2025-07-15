using System;

namespace oyuns
{
    class Program
    {
        static int mapSize = 5;

        class Player
        {
            public int X = 2;
            public int Y = 2;
            public int Health = 10;
            public int Ammo = 5;
        }

        class Enemy
        {
            public int X = 1;
            public int Y = 2;
            public int Health = 3;
            public bool Alive = true;
        }

        static void PrintStatus(Player player)
        {
            Console.WriteLine($"[Can: {player.Health}] [Mermi: {player.Ammo}] [Konum: ({player.X},{player.Y})]");
        }

        static bool IsAdjacent(Player player, Enemy enemy)
        {
            if (!enemy.Alive) return false;
            return Math.Abs(player.X - enemy.X) + Math.Abs(player.Y - enemy.Y) == 1;
        }

        static void Main(string[] args)
        {
            var player = new Player();
            var enemy = new Enemy();

            Console.WriteLine("=== METİN TABANLI FPS (C#) ===");
            Console.WriteLine("Komutlar: ileri, geri, sol, sağ, ateş et, durum, çık");

            while (player.Health > 0)
            {
                Console.Write("> ");
                string command = Console.ReadLine().ToLower();

                switch (command)
                {
                    case "ileri":
                        if (player.Y > 0) player.Y--;
                        else Console.WriteLine("İleri gidemezsin.");
                        break;
                    case "geri":
                        if (player.Y < mapSize - 1) player.Y++;
                        else Console.WriteLine("Geri gidemezsin.");
                        break;
                    case "sol":
                        if (player.X > 0) player.X--;
                        else Console.WriteLine("Sola gidemezsin.");
                        break;
                    case "sağ":
                        if (player.X < mapSize - 1) player.X++;
                        else Console.WriteLine("Sağa gidemezsin.");
                        break;
                    case "ateş et":
                        if (player.Ammo <= 0)
                        {
                            Console.WriteLine("Mermin kalmadı!");
                        }
                        else if (IsAdjacent(player, enemy))
                        {
                            player.Ammo--;
                            enemy.Health--;
                            Console.WriteLine($"Düşmana ateş ettin! Düşman canı: {enemy.Health}");

                            if (enemy.Health <= 0)
                            {
                                enemy.Alive = false;
                                Console.WriteLine("Düşmanı öldürdün!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Yakında düşman yok.");
                        }
                        break;
                    case "durum":
                        PrintStatus(player);
                        break;
                    case "çık":
                        Console.WriteLine("Oyundan çıkılıyor...");
                        return;
                    default:
                        Console.WriteLine("Bilinmeyen komut.");
                        break;
                }

                // Düşman saldırırsa
                if (enemy.Alive && IsAdjacent(player, enemy))
                {
                    player.Health--;
                    Console.WriteLine($"Düşman sana vurdu! Canın: {player.Health}");
                    if (player.Health <= 0)
                    {
                        Console.WriteLine("Öldün! Oyun bitti.");
                    }
                }
            }
        }
    }
}
