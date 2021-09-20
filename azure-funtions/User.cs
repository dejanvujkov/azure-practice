namespace azure_funtions
{
    public class User
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public override string ToString()
        {
            return $"{Name} {Surname}";
        }
    }
}