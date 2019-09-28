namespace VSBS.Model
{
    public class Montadora
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Montadora()
        {
            this.Id = 0;
            this.Name = string.Empty;
        }

        public Montadora(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}
