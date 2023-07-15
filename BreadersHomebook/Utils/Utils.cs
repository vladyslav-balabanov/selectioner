namespace BreadersHomebook.Utils
{
    public class Utils
    {
        public string[] SeparateStringArr(string arr)
        {
            if (arr.Contains(","))
            {
                var array = arr.Split(',');
                for (var i = 0; i < array.Length; i++) array[i] = array[i].Trim();

                return array;
            }

            return new[] { arr };
        }
    }
}