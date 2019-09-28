namespace VSBS.Core
{
    using Model;
    using System.Collections.Generic;
    using System.Linq;

    public class MontadoraCore
    {
        /// <summary>
        /// Cria Lista privada para armazenar as montadoras
        /// </summary>
        private List<Montadora> Montadoras;

        /// <summary>
        /// Construtor da Classe - Inicializa a lista de montadoras
        /// </summary>
        public MontadoraCore()
        {
            Montadoras = new List<Montadora>();
            Montadoras.Add(new Montadora(1, "Adamo"));
            Montadoras.Add(new Montadora(2, "Agrale"));
            Montadoras.Add(new Montadora(3, "Aldee"));
            Montadoras.Add(new Montadora(4, "Alfa Romeo"));
            Montadoras.Add(new Montadora(5, "Americar"));
            Montadoras.Add(new Montadora(6, "Avallone"));
            Montadoras.Add(new Montadora(7, "Audi"));
            Montadoras.Add(new Montadora(8, "Aurora"));
            Montadoras.Add(new Montadora(9, "Bianco"));
            Montadoras.Add(new Montadora(10, "BMW"));
            Montadoras.Add(new Montadora(11, "Brasinca"));
            Montadoras.Add(new Montadora(12, "Chery"));
            Montadoras.Add(new Montadora(13, "Chevrolet"));
            Montadoras.Add(new Montadora(14, "Citroën"));
            Montadoras.Add(new Montadora(15, "Cross Lander"));
            Montadoras.Add(new Montadora(16, "DKW-Vemag"));
            Montadoras.Add(new Montadora(17, "Dodge"));
            Montadoras.Add(new Montadora(18, "Fiat"));
            Montadoras.Add(new Montadora(19, "Ford"));
            Montadoras.Add(new Montadora(20, "GMC"));
            Montadoras.Add(new Montadora(21, "Gurgel"));
            Montadoras.Add(new Montadora(22, "Honda"));
            Montadoras.Add(new Montadora(23, "Hyundai"));
            Montadoras.Add(new Montadora(24, "Jeep"));
            Montadoras.Add(new Montadora(25, "Lafer"));
            Montadoras.Add(new Montadora(26, "Mercedes-Benz"));
            Montadoras.Add(new Montadora(27, "Mitsubishi"));
            Montadoras.Add(new Montadora(28, "Nissan"));
            Montadoras.Add(new Montadora(29, "Peugeot"));
            Montadoras.Add(new Montadora(30, "Renault"));
            Montadoras.Add(new Montadora(31, "Suzuki"));
            Montadoras.Add(new Montadora(32, "Toyota"));
            Montadoras.Add(new Montadora(33, "Troller"));
            Montadoras.Add(new Montadora(34, "Volkswagen"));
            Montadoras.Add(new Montadora(35, "Willys Overland"));
        }

        /// <summary>
        /// Retorna todas montadoras
        /// </summary>
        /// <returns>Lista de Montadoras</returns>
        public List<Montadora> getAll()
        {
            return Montadoras;
        }

        /// <summary>
        /// Retorna lista de Montadoras a partir de Consulta por Nome
        /// </summary>
        /// <param name="key">Nome ou Parte do Nome da montadora</param>
        /// <returns>Lista de Montadoras</returns>
        public List<Montadora> getByQuery(string key)
        {
            return Montadoras.Where(w => w.Name.ToLower().Contains(key.ToLower().Trim())).ToList();
        }

        /// <summary>
        /// Retorna montadora pelo Id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Montadora</returns>
        public Montadora getById(int id)
        {
            return Montadoras.Where(w => w.Id.Equals(id)).FirstOrDefault();
        }
    }
}
