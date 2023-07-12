namespace BreadersHomebook.Services
{
    public class Utils
    {
        public string[] SeparateStringArr(string arr)
        {
            if (arr.Contains(","))
            {
                string[] array = arr.Split(',');
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = array[i].Trim();
                }

                return array;
            }

            return new[] { arr };
        }
    }
}