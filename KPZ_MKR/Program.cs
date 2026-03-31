using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;

namespace LightHTML_System
{
    class Program
    {
        static void Main()
        {
            // =========================================================================
            // БЛОК 1: СТВОРЕННЯ СКЛАДНОЇ HTML-СТРУКТУРИ ТА ВІДКРИТТЯ В БРАУЗЕРІ
            // =========================================================================
            Console.WriteLine("--- КРОК 1: ГЕНЕРАЦІЯ ТА ВІДКРИТТЯ HTML-ФАЙЛУ ---");

            var cardContainer = new LightElementNode("div", DisplayType.Block, ClosingType.Paired);
            cardContainer.AddCssClass("esports-card\" style=\"font-family: Arial; padding: 20px; border: 2px solid #333; width: 400px; border-radius: 10px; background-color: #f9f9f9; text-align: center;");

            var header = new LightElementNode("h2", DisplayType.Block, ClosingType.Paired);
            header.AddChild(new LightTextNode("BLAST.tv Austin Major 2025"));
            cardContainer.AddChild(header);

            var table = new LightElementNode("table", DisplayType.Block, ClosingType.Paired);
            table.AddCssClass("scoreboard\" style=\"width: 100%; margin-top: 15px; border-collapse: collapse;");

            var trHead = new LightElementNode("tr", DisplayType.Block, ClosingType.Paired);
            var th1 = new LightElementNode("th", DisplayType.Inline, ClosingType.Paired); th1.AddChild(new LightTextNode("Team 1"));
            var th2 = new LightElementNode("th", DisplayType.Inline, ClosingType.Paired); th2.AddChild(new LightTextNode("Score"));
            var th3 = new LightElementNode("th", DisplayType.Inline, ClosingType.Paired); th3.AddChild(new LightTextNode("Team 2"));
            trHead.AddChild(th1); trHead.AddChild(th2); trHead.AddChild(th3);
            table.AddChild(trHead);

            var trBody = new LightElementNode("tr", DisplayType.Block, ClosingType.Paired);
            var td1 = new LightElementNode("td", DisplayType.Inline, ClosingType.Paired); td1.AddChild(new LightTextNode("<b>FaZe Clan</b>"));
            var td2 = new LightElementNode("td", DisplayType.Inline, ClosingType.Paired); td2.AddChild(new LightTextNode("<b style='color: red; font-size: 20px;'>2 : 2</b>"));
            var td3 = new LightElementNode("td", DisplayType.Inline, ClosingType.Paired); td3.AddChild(new LightTextNode("<b>Natus Vincere</b>"));
            trBody.AddChild(td1); trBody.AddChild(td2); trBody.AddChild(td3);
            table.AddChild(trBody);

            cardContainer.AddChild(table);

            // Збереження та запуск
            string finalHtml = cardContainer.OuterHTML;
            string filePath = "result.html";
            File.WriteAllText(filePath, finalHtml);

            Console.WriteLine($"HTML-файл '{filePath}' створено.");
            try
            {
                Process.Start(new ProcessStartInfo { FileName = filePath, UseShellExecute = true });
                Console.WriteLine("Браузер відкрито автоматично.");
            }
            catch { Console.WriteLine("Не вдалося відкрити браузер автоматично."); }

            Console.WriteLine("\n" + new string('=', 60) + "\n");


            // =========================================================================
            // БЛОК 2: ТЕСТУВАННЯ ІТЕРАТОРІВ (ОБХІД ДЕРЕВА)
            // =========================================================================
            Console.WriteLine("--- КРОК 2: ОБХІД ДЕРЕВА (ITERATORS) ---");

            Console.WriteLine("\n[Depth-First Search - В глибину]:");
            foreach (var node in cardContainer.GetDepthFirstIterator())
            {
                PrintNodeInfo(node);
            }

            Console.WriteLine("\n[Breadth-First Search - В ширину]:");
            foreach (var node in cardContainer.GetBreadthFirstIterator())
            {
                PrintNodeInfo(node);
            }

            Console.WriteLine("\n" + new string('=', 60) + "\n");


            // =========================================================================
            // БЛОК 3: ПАТЕРН КОМАНДА (UNDO/REDO)
            // =========================================================================
            Console.WriteLine("--- КРОК 3: ПАТЕРН КОМАНДА (UNDO/REDO) ---");

            var button = new LightElementNode("button", DisplayType.Inline, ClosingType.Paired);
            button.AddChild(new LightTextNode("Click me!"));

            Console.WriteLine("\nПочаткова кнопка:");
            Console.WriteLine(button.OuterHTML);

            var commandHistory = new Stack<ICommand>();

            // Додаємо перший клас
            ICommand cmd1 = new AddCssClassCommand(button, "btn-primary");
            cmd1.Execute();
            commandHistory.Push(cmd1);

            // Додаємо другий клас
            ICommand cmd2 = new AddCssClassCommand(button, "active");
            cmd2.Execute();
            commandHistory.Push(cmd2);

            Console.WriteLine("\nПісля додавання класів 'btn-primary' та 'active':");
            Console.WriteLine(button.OuterHTML);

            // Скасування останньої дії
            if (commandHistory.Count > 0)
            {
                Console.WriteLine("\nВиконуємо UNDO...");
                var lastCommand = commandHistory.Pop();
                lastCommand.Undo();

                Console.WriteLine("Стан після скасування:");
                Console.WriteLine(button.OuterHTML);
            }

            Console.WriteLine("\nРоботу завершено. Натисніть Enter для виходу.");
            Console.ReadLine();
        }

        static void PrintNodeInfo(LightNode node)
        {
            if (node is LightElementNode el)
                Console.WriteLine($"Тег: <{el.TagName}>");
            else if (node is LightTextNode txt)
            {
                string content = txt.InnerHTML.Trim();
                if (!string.IsNullOrEmpty(content))
                    Console.WriteLine($"   Текст: {content}");
            }
        }
    }
}