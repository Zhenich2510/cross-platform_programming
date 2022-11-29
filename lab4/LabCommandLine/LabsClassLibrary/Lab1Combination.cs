namespace LabsClassLibrary
{
    public static class Lab1Combination
    {
        public static List<string> CombineString(string inputString)
        {
            //Якщо залишився 1 символ - просто вертаємо список з 1 елементом
            if (inputString.Length == 1) return new List<string> { inputString };
            //ліст для зберегання комбінацій
            List<string> result = new();
            //Обходимо в циклі кожну літеру вхідної строки
            for (int i = 0; i < inputString.Length; i++)
            {
                //Беремо символ згідно інтексу у вхідної строки
                //який далі буде першим символом у підкобінаціях
                char firstChar = inputString[i];
                //Виділяємо залишок строки символів без "першого" символа, який ми взяли окремо
                string substring = inputString.Remove(i, 1);
                //Рекурсивно викликаємо метод для перебору залишку символів
                List<string> combinations = CombineString(substring);
                //Конкатенуємо "перший" символ до кожного повернутого елемента списку
                IEnumerable<string> combinationsWithFirst = combinations.Select(s => $"{firstChar}{s}");
                //foreach (var item in combinations)
                //{
                //    result.Add($"{firstChar}{item}"); // new String( firstChar) + item
                //}
                //Додаємо отриманий результат в ліст
                result.AddRange(combinationsWithFirst);
            }
            return result;
        }
    }
}