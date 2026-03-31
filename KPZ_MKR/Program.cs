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

            string finalHtml = cardContainer.Render();
            string filePath = "result.html";
            File.WriteAllText(filePath, finalHtml);

            Console.WriteLine($"HTML-файл '{filePath}' успішно створено.");
            try
            {
                Process.Start(new ProcessStartInfo { FileName = filePath, UseShellExecute = true });
                Console.WriteLine("Результат відкрито у браузері.");
            }
            catch { Console.WriteLine("Не вдалося відкрити браузер автоматично."); }

            Divider();

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

            Divider();

            Console.WriteLine("--- КРОК 3: ПАТЕРН КОМАНДА (UNDO/REDO) ---");

            var btnCmd = new LightElementNode("button", DisplayType.Inline, ClosingType.Paired);
            btnCmd.AddChild(new LightTextNode("Click me!"));

            Console.WriteLine("\nПочаткова кнопка:");
            Console.WriteLine(btnCmd.OuterHTML);

            var commandHistory = new Stack<ICommand>();

            ICommand cmd1 = new AddCssClassCommand(btnCmd, "btn-primary");
            cmd1.Execute();
            commandHistory.Push(cmd1);

            ICommand cmd2 = new AddCssClassCommand(btnCmd, "active");
            cmd2.Execute();
            commandHistory.Push(cmd2);

            Console.WriteLine("\nПісля додавання класів 'btn-primary' та 'active':");
            Console.WriteLine(btnCmd.OuterHTML);

            if (commandHistory.Count > 0)
            {
                Console.WriteLine("\nВиконуємо UNDO...");
                var lastCommand = commandHistory.Pop();
                lastCommand.Undo();

                Console.WriteLine("Стан після скасування останньої команди:");
                Console.WriteLine(btnCmd.OuterHTML);
            }

            Divider();

            Console.WriteLine("--- КРОК 4: ПАТЕРН СТЕЙТ (STATE) ---");

            var alertBox = new LightElementNode("div", DisplayType.Block, ClosingType.Paired);
            alertBox.AddCssClass("alert alert-danger");
            alertBox.AddChild(new LightTextNode("Втрачено з'єднання!"));

            Console.WriteLine("\n1. Нормальний стан:");
            Console.WriteLine(alertBox.OuterHTML);

            Console.WriteLine("\n2. Змінюємо стан на HiddenState...");
            alertBox.SetState(new HiddenState());
            Console.WriteLine("Результат рендеру: " + alertBox.OuterHTML);

            Console.WriteLine("\n3. Повертаємо NormalState...");
            alertBox.SetState(new NormalState());
            Console.WriteLine(alertBox.OuterHTML);

            Divider();

            Console.WriteLine("--- КРОК 5: ШАБЛОННИЙ МЕТОД (LIFECYCLE HOOKS) ---");

            var saveBtn = new LightElementNode("button", DisplayType.Inline, ClosingType.Paired);
            saveBtn.AddCssClass("btn btn-success");
            var text = new LightTextNode("Зберегти");

            Console.WriteLine("--- Виклик Render() для тексту ---");
            text.Render();

            saveBtn.AddChild(text);

            Console.WriteLine("\n--- Виклик Render() для кнопки ---");
            string btnHtml = saveBtn.Render();

            Console.WriteLine("\n=== Підсумковий HTML ===");
            Console.WriteLine(btnHtml);

            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("Демонстрацію завершено!");
            Console.ReadLine();
        }

        static void Divider()
        {
            Console.WriteLine("\n" + new string('=', 60) + "\n");
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