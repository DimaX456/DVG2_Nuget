namespace Nigget
{
    public class dvg23<T> where T : class
    {
        private List<T> value = new List<T>();

        public dvg23() { }
        public List<T> abiturientst()
        {
            return value;
        }
        public void Addbit(T abiturient)
        {
            value.Add(abiturient);
        }
        public void Deletebit (T abiturient)
        {
            value.Remove(abiturient);
        }
        public void Changebit (T abiturient, T Id)
        {
            var index = value.IndexOf (Id);
            value[index] = abiturient;
        }
    }
}