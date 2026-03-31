using System;
using System.IO;
using System.Diagnostics;

namespace LightHTML_System
{
    class Program
    {
        static void Main()
        {
            // --- 1. СТВОРЕННЯ HTML СТРУКТУРИ ---

            var cardContainer = new LightElementNode("div", DisplayType.Block, ClosingType.Paired);
            // Додамо трохи вбудованих стилів (CSS), щоб воно виглядало гарно в браузері
            cardContainer.AddCssClass("esports-card\" style=\"font-family: Arial; padding: 20px; border: 2px solid #333; width: 400px; border-radius: 10px; background-color: #f9f9f9; text-align: center;");

            var header = new LightElementNode("h2", DisplayType.Block, ClosingType.Paired);
            header.AddChild(new LightTextNode("BLAST.tv Austin Major 2025"));
            cardContainer.AddChild(header);

            var table = new LightElementNode("table", DisplayType.Block, ClosingType.Paired);
            table.AddCssClass("scoreboard\" style=\"width: 100%; margin-top: 15px; border-collapse: collapse;");

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

            // Отримуємо фінальний HTML рядок
            string finalHtml = cardContainer.OuterHTML;

            // --- 2. ВИВЕДЕННЯ ТА "ВИКОНАННЯ" РЕЗУЛЬТАТУ ---

            Console.WriteLine("=== Згенерована HTML-розмітка ===");
            Console.WriteLine(finalHtml);

            // Створюємо HTML файл
            string filePath = "result.html";
            File.WriteAllText(filePath, finalHtml);

            Console.WriteLine($"\nФайл {filePath} успішно створено!");
            Console.WriteLine("Відкриваємо результат у браузері...");

            // Автоматично відкриваємо створений файл у браузері за замовчуванням
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
                Console.WriteLine($"Не вдалося автоматично відкрити браузер. Помилка: {ex.Message}");
                Console.WriteLine($"Знайдіть файл {filePath} у папці з проєктом та відкрийте його вручну.");
            }

            Console.ReadLine(); // Щоб консоль не закрилася миттєво
        }
    }
}