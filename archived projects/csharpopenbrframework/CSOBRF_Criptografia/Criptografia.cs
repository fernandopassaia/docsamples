namespace CSOBRF_Criptografia
{
    public class Criptografia
    {
        #region Variaveis Internas da Classe
        string encryptedText = string.Empty;
        string decryptedText = string.Empty;
        string hashedText = string.Empty;
        #endregion

        #region Criptografar
        /// <summary>
        /// Recebe uma String e Retorna ela Criptografada pelo algoritmo simétrico de Rijndael
        /// </summary>
        /// <param name="Valor">String para ser Criptografada</param>
        /// <returns>Retorna String já criptografada</returns>
        public string Criptografar(string Valor)
        {
            string key = Valor;
            Crypt _crypt = new Crypt((CryptProvider)4);
            _crypt.Key = "SuaChavePersonalizadaAqui"; 
            encryptedText = _crypt.Encrypt(key);
                        
            string duasPrimeiras = "AB"; //essas duas letras serão inseridas no inicio (você pode trocar ou remover)
            string duasUltimas = "YZ"; //essas duas letras serão inseridas no fim (você pode trocar ou remover)

            //NOTA FERNANDO: Você pode trocar as 3 chaves acima, tanto a "key" como os dois caracters a + do ínicio e os dois do fim.
            //Assim você terá outra criptografia ainda totalmente diferente. Só não se esqueça de Trocar nos dois métodos (crip e decrip).

            return duasPrimeiras + encryptedText + duasUltimas;

        }//fim método Criptografar
        #endregion

        #region Descriptografar
        /// <summary>
        /// Recebe uma String Criptografada e retorna ela Descriptografada
        /// </summary>
        /// <param name="Valor">String para ser Criptografada</param>
        /// <returns>Retorna String já criptografada</returns>
        public string Descriptografar(string Valor)
        {
            string key = Valor.Substring(2, Valor.Length-4); //começo no 2, ignoro os últimos 4 (CR do ínicio e FD do fim)
            Crypt _crypt = new Crypt((CryptProvider)4);
            _crypt.Key = "SuaChavePersonalizadaAqui"; //nota - você pode trocar essa chave para uma pessoal, só não esqueça de trocar no "criptografar"
            decryptedText = _crypt.Decrypt(key);
            return decryptedText;
        }//fim método Descriptografar
        #endregion
    }//fim namespace
}//fim classe
