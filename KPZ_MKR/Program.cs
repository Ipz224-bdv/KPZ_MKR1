using System;
using System.IO;
using System.Diagnostics;

namespace LightHTML_System
{
    class Program
    {
        static void Main()
        {
            // --- 1. СТВОРЕННЯ HTML СТРУКТУРИ (Кіберспортивна картка) ---

            var cardContainer = new LightElementNode("div", DisplayType.Block, ClosingType.Paired);
            // Додаємо стилі для гарного вигляду
            cardContainer.AddCssClass("esports-card\" style=\"font-family: Arial; padding: 20px; border: 2px solid #333; width: 400px; border-radius: 10px; background-color: #f9f9f9; text-align: center;");

            var header = new LightElementNode("h2", DisplayType.Block, ClosingType.Paired);
            header.AddChild(new LightTextNode("BLAST.tv Austin Major 2025"));
            cardContainer.AddChild(header);

            var table = new LightElementNode("table", DisplayType.Block, ClosingType.Paired);
            table.AddCssClass("scoreboard\" style=\"width: 100%; margin-top: 15px; border-collapse: collapse;");

            // Заголовок таблиці
            var trHead = new LightElementNode("tr", DisplayType.Block, ClosingType.Paired);

            var th1 = new LightElementNode("th", DisplayType.Inline, ClosingType.Paired);
            th1.AddChild(new LightTextNode("Team 1"));
            var th2 = new LightElementNode("th", DisplayType.Inline, ClosingType.Paired);
            th2.AddChild(new LightTextNode("Score"));
            var th3 = new LightElementNode("th", DisplayType.Inline, ClosingType.Paired);
            th3.AddChild(new LightTextNode("Team 2"));

            trHead.AddChild(th1);
            trHead.AddChild(th2);
            trHead.AddChild(th3);
            table.AddChild(trHead);

            // Тіло таблиці з результатом
            var trBody = new LightElementNode("tr", DisplayType.Block, ClosingType.Paired);

            var td1 = new LightElementNode("td", DisplayType.Inline, ClosingType.Paired);
            td1.AddChild(new LightTextNode("<b>FaZe Clan</b>"));

            var td2 = new LightElementNode("td", DisplayType.Inline, ClosingType.Paired);
            td2.AddChild(new LightTextNode("<b style='color: red; font-size: 20px;'>2 : 2</b>"));

            var td3 = new LightElementNode("td", DisplayType.Inline, ClosingType.Paired);
            td3.AddChild(new LightTextNode("<b>Natus Vincere</b>"));

            trBody.AddChild(td1);
            trBody.AddChild(td2);
            trBody.AddChild(td3);
            table.AddChild(trBody);

            cardContainer.AddChild(table);

            // --- 2. ГЕНЕРАЦІЯ ТА ВІДКРИТТЯ ФАЙЛУ ---

            string finalHtml = cardContainer.OuterHTML;

            Console.WriteLine("=== Згенерована HTML-розмітка ===");
            Console.WriteLine(finalHtml);

            string filePath = "result.html";
            File.WriteAllText(filePath, finalHtml);

            Console.WriteLine($"\nФайл {filePath} успішно створено!");

            // Спроба відкрити браузер
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = filePath,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Не вдалося відкрити браузер: {ex.Message}");
            }

            Console.WriteLine("\n" + new string('-', 30));

            // --- 3. ТЕСТУВАННЯ ІТЕРАТОРІВ НА СТВОРЕНІЙ КАРТЦІ ---

            Console.WriteLine("=== Обхід побудованого дерева в глибину (Depth-First) ===");
            foreach (var node in cardContainer.GetDepthFirstIterator())
            {
                PrintNodeInfo(node);
            }

            Console.WriteLine("\n=== Обхід побудованого дерева в ширину (Breadth-First) ===");
            foreach (var node in cardContainer.GetBreadthFirstIterator())
            {
                PrintNodeInfo(node);
            }

            Console.WriteLine("\nНатисніть Enter, щоб вийти...");
            Console.ReadLine();
        }

        // Допоміжний метод для виведення інформації про вузол
        static void PrintNodeInfo(LightNode node)
        {
            if (node is LightElementNode el)
                Console.WriteLine($"Тег: <{el.TagName}>");
            else if (node is LightTextNode txt)
                Console.WriteLine($"  Текст: {txt.InnerHTML.Trim()}");
        }
    }
}