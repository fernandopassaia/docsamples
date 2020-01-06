using System;

namespace CSOBRF_Validacoes
{
    public class ValidacaoFormatacaoDeCampos
    {        
        #region Validação de E-mail
        /// <summary>
        /// Esse método valida E-mail (verifica a presença de @ na string)
        /// </summary>
        /// <param name="email">E-mail para ser Validado</param>
        /// <returns>True para válido - False para não válido</returns>
        public bool ValidaEmail(string email)
        {
            int tam = email.Length;
            bool teste = false;

            for (int i = 0; i < tam; i++)
            {
                if (email.Substring(i, 1) == "@")
                {
                    teste = true;
                }
            }

            return teste;

        }
        #endregion

        #region Método Valida e Formata Telefone (2018) 
        /// <summary>
        /// Método novo (2018): Recebe o telefone e verifica se ele tem formato de telefone 11999998888 ou 1122223333... Pode enviar com máscará e tudo que ele irá tratar.
        /// Se estiver OK, ele irá formatar para (11)99999-9999 ou (11)2222-3333 e retornar o telefone formatado. Se houver qualquer erro no formato, retornará "".
        /// </summary>
        /// <param name="telefone">Enviar o Telefone no formato 11999998888 ou (11)99999-8888 ou 1144443333 ou (11)4444-3333 ou qualquer outro formato (será tratado)</param>
        /// <returns>Retorna o Telefone formatado bonitinho, SE houver erro, retorna a string vazia "".</returns>
        public string validaFormataTelefone(string telefone)
        {            
            NewContasMatematicas contas = new NewContasMatematicas();
            telefone = retiraPontuacao(telefone);
            if (telefone.Length != 10 && telefone.Length != 11) //verifico pelo tamanho sem a máscara
            {
                return "ERRO";
            }

            for (int i = 0; i < telefone.Length; i++) //varro o telefone verificando se todos caracters são números...
            {
                if (!contas.verificaSeEInteiro(telefone.Substring(i, 1)))
                {
                    return "ERRO";
                }
            }//fim for

            //formato o telefone e coloco as chaves e traços
            if (telefone.Length == 10)
            {
                telefone = "(" + telefone.Substring(0, 2) + ")" + telefone.Substring(2, 4) + "-" + telefone.Substring(6, 4);
            }
            else
            {
                telefone = "(" + telefone.Substring(0, 2) + ")" + telefone.Substring(2, 5) + "-" + telefone.Substring(7, 4);
            }
            return telefone;            
        }//fim método
        #endregion

        #region Validação de Telefone
        /// <summary>
        /// Esse método valida telefones (verifica se está no formato (11)4640-2833 
        /// </summary>
        /// <param name="telefone">Telefone para ser Validado</param>
        /// <returns>True para válido - False para não válido</returns>
        public bool ValidaTelefone(string telefone)
        {
            string textoLimpo = telefone;

            //textoLimpo = textoLimpo.Replace("(", "");
            //textoLimpo = textoLimpo.Replace(")", "");
            //textoLimpo = textoLimpo.Replace("-", "");

            int contador = textoLimpo.Length;

            if (contador == 13 || contador == 14) //se length for 13, esta no formato (11)4640-2481
            {
                return true;
            }
            else
            {
                return false;
            }

        }//fim método que valida telefone
        #endregion

        #region Formata Telefone
        /// <summary>
        /// Esse Método Formata Número de Telefone (ou seja, recebe um número bagunçado e retorna no formato (99)9999-9999 (ou com um 9 a mais - celular)
        /// </summary>
        /// <param name="telefone">Telefone para ser Formatado</param>
        /// <returns>Número Formatado, "" (vazio) se número enviado for inválido</returns>
        public string formataTelefone(string telefone)
        {
            if (ValidaTelefone(telefone))
            {
                telefone = retiraPontuacao(telefone);

                int contador = telefone.Length;

                if (contador == 10 || contador == 11) // 1112345678 ou 11912345678
                {
                    if (contador == 10)
                    {
                        telefone = "(" + telefone.Substring(0, 2) + ")" + telefone.Substring(2, 4) + "-" + telefone.Substring(6, 4);
                    }
                    else
                    {
                        telefone = "(" + telefone.Substring(0, 2) + ")" + telefone.Substring(2, 5) + "-" + telefone.Substring(7, 4);
                    }
                    return telefone;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }//fim método que valida telefone
        #endregion

        #region Validação de CEP
        /// <summary>
        /// Esse método valida CEP (verifica se está no formato 08573-180)
        /// </summary>
        /// <param name="cep">Cep para ser Validado</param>
        /// <returns>True para válido - False para não válido</returns>
        public bool ValidaCep(string cep)
        {
            string textoLimpo = cep;

            textoLimpo = textoLimpo.Replace("(", "");
            textoLimpo = textoLimpo.Replace(")", "");
            textoLimpo = textoLimpo.Replace("-", "");

            int contador = textoLimpo.Length;

            if (contador == 8) //se length for 7, está no formado 08573180 
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        #endregion

        #region Obtem endereço pelo CEP (092017 - WebService Correios)
        /// <summary>
        /// Retorna um Array de Strings sendo as seguintes posições: 0 (End) 1 (Complemento 1 - Número) 2 (Complemento 2) 3 (Cidade) 4 (Bairro) 5 (UF)
        /// </summary>
        /// <param name="cep">Enviar o CEP formato 99999-999</param>
        /// <returns></returns>
        public string[] retornaCepPeloWSCorreios(string cep)
        {
            using (var ws = new CSOBRF_Validacoes.WSCorreios.AtendeClienteService())
            {                
                try
                {
                    var resultado = ws.consultaCEP(cep);
                    string[] cepRetorno = new string[6];
                    cepRetorno[0] = resultado.end;
                    cepRetorno[1] = resultado.complemento;
                    cepRetorno[2] = resultado.complemento2;
                    cepRetorno[3] = resultado.cidade;
                    cepRetorno[4] = resultado.bairro;
                    cepRetorno[5] = resultado.uf;
                    return cepRetorno;
                }
                catch
                {
                    string[] cepRetorno = new string[6];
                    cepRetorno[0] = "";
                    cepRetorno[1] = "";
                    cepRetorno[2] = "";
                    cepRetorno[3] = "";
                    cepRetorno[4] = "";
                    cepRetorno[5] = "";
                    return cepRetorno;
                }
            }
        }
        #endregion

        #region Retira Assentos e Caraters
        public string retiraPontuacao(string variavel)
        {
            string variavel2 = variavel;
            variavel2 = variavel2.Replace("ç", "c");
            variavel2 = variavel2.Replace("Ç", "C");
            variavel2 = variavel2.Replace("Ã", "A");
            variavel2 = variavel2.Replace("ã", "a");
            variavel2 = variavel2.Replace("Â", "A");
            variavel2 = variavel2.Replace("â", "a");
            variavel2 = variavel2.Replace("É", "E");
            variavel2 = variavel2.Replace("é", "e");
            variavel2 = variavel2.Replace("õ", "o");
            variavel2 = variavel2.Replace("Õ", "O");
            variavel2 = variavel2.Replace("Í", "I");
            variavel2 = variavel2.Replace("í", "I");
            variavel2 = variavel2.Replace("ü", "u");
            variavel2 = variavel2.Replace("Ü", "U");
            variavel2 = variavel2.Replace("Á", "A");
            variavel2 = variavel2.Replace("á", "a");
            variavel2 = variavel2.Replace("à", "a");
            variavel2 = variavel2.Replace("ú", "u");
            variavel2 = variavel2.Replace("Ú", "U");
            variavel2 = variavel2.Replace("À", "A");
            variavel2 = variavel2.Replace(",", "");
            variavel2 = variavel2.Replace(".", "");
            variavel2 = variavel2.Replace("/", "");
            variavel2 = variavel2.Replace("|", "");
            variavel2 = variavel2.Replace(@"\", "");
            variavel2 = variavel2.Replace("[", "");
            variavel2 = variavel2.Replace("]", "");
            variavel2 = variavel2.Replace(")", "");
            variavel2 = variavel2.Replace("(", "");
            variavel2 = variavel2.Replace("%", "");
            variavel2 = variavel2.Replace("�", " ");
            variavel2 = variavel2.Replace("-", "");
            variavel2 = variavel2.Replace("_", "");
            variavel2 = variavel2.Replace("\"", "");
            variavel2 = variavel2.Replace("'", "");
            variavel2 = variavel2.Replace("'", "");            
            return variavel2;
        }
        #endregion

        #region Arruma Pontuação de CPF
        /// <summary>
        /// Arruma a Pontuação do CPF ou CNPJ (se vier virgula, ponto, etc, ele limpa, verifica se é válido e etc)
        /// </summary>
        /// <param name="cpf_cnpj">Cpf/Cnpj para ser verificado</param>
        /// <returns>Retorna o CPF/CNPJ validado e limpo</returns>
        public string pontuaCpf_CNPJ(string cpf_cnpj)
        {

            string textoLimpo = cpf_cnpj;

            textoLimpo = textoLimpo.Replace(".", "");
            textoLimpo = textoLimpo.Replace("-", "");
            textoLimpo = textoLimpo.Replace("/", "");
            textoLimpo = textoLimpo.Replace(",", "");

            int tam = textoLimpo.Length;
            string textoRetorno = textoLimpo;
            if (new ContasMatematicas().verificaSeEInteiro(textoLimpo) == false)
            {
                return textoRetorno = "ERRO";
            }





            #region CPF
            if (textoLimpo.Length <= 11)
            {
                if (ValidaCPF(textoLimpo) == false)
                {
                    return textoRetorno = "ERROCPF";
                }

                textoRetorno = "";
                for (int i = 0; i < tam; i++)
                {
                    // string aux = (textoLimpo.Substring(i , 1)).ToString();
                    if (i == 3)
                    {
                        if (textoLimpo.Substring(i, 1).ToString() != ".")
                        {
                            textoRetorno = textoRetorno + ".";
                        }
                    }

                    if (i == 6)
                    {
                        if (textoLimpo.Substring(i, 1).ToString() != ".")
                        {
                            textoRetorno = textoRetorno + ".";
                        }
                    }

                    if (i == 9)
                    {
                        if (textoLimpo.Substring(i, 1).ToString() != "-")
                        {
                            textoRetorno = textoRetorno + "-";
                        }
                    }

                    textoRetorno = textoRetorno + textoLimpo.Substring(i, 1);

                }
            }
            #endregion
            else
            #region CNPJ
            {
                if (textoLimpo.Length >= 14)
                {
                    if (ValidaCNPJ(textoLimpo) == false)
                    {
                        return textoRetorno = "ERROCNPJ";
                    }


                    textoRetorno = "";

                    for (int i = 0; i < tam; i++)
                    {
                        // string aux = (textoLimpo.Substring(i , 1)).ToString();
                        if (i == 2)
                        {
                            if (textoLimpo.Substring(i, 1).ToString() != ".")
                            {
                                textoRetorno = textoRetorno + ".";
                            }
                        }

                        if (i == 5)
                        {
                            if (textoLimpo.Substring(i, 1).ToString() != ".")
                            {
                                textoRetorno = textoRetorno + ".";
                            }
                        }

                        if (i == 8)
                        {
                            if (textoLimpo.Substring(i, 1).ToString() != "/")
                            {
                                textoRetorno = textoRetorno + "/";
                            }
                        }

                        if (i == 12)
                        {
                            if (textoLimpo.Substring(i, 1).ToString() != "-")
                            {
                                textoRetorno = textoRetorno + "-";
                            }
                        }

                        textoRetorno = textoRetorno + textoLimpo.Substring(i, 1);

                    }
                }
            }
            #endregion
            return textoRetorno;

        }//fim método
        #endregion

        #region Validação de CPF
        /// <summary>
        /// Este método Valida o CPF
        /// </summary>
        /// <param name="vrCPF">String com o número do cpf</param>
        /// <returns>True para CPF válido - False para CPF não válido</returns>
        public bool ValidaCPF(string vrCPF)
        {

            string valor = vrCPF.Replace(".", "");
            valor = valor.Replace("-", "");

            if (valor.Length != 11)

                return false;

            bool igual = true;
            for (int i = 1; i < 11 && igual; i++)
                if (valor[i] != valor[0])
                    igual = false;
            if (igual || valor == "12345678909")
                return false;
            int[] numeros = new int[11];
            for (int i = 0; i < 11; i++)

                numeros[i] = int.Parse(

                  valor[i].ToString());

            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];
            int resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {

                if (numeros[9] != 0)

                    return false;

            }

            else if (numeros[9] != 11 - resultado)

                return false;



            soma = 0;

            for (int i = 0; i < 10; i++)

                soma += (11 - i) * numeros[i];



            resultado = soma % 11;



            if (resultado == 1 || resultado == 0)
            {

                if (numeros[10] != 0)

                    return false;

            }

            else

                if (numeros[10] != 11 - resultado)

                return false;



            return true;

        }
        #endregion

        #region Validação de CNPJ
        /// <summary>
        /// Este método Valida o CNPJ 
        /// </summary>
        /// <param name="vrCNPJ">String com o número do CNPJ</param>
        /// <returns>True para CNPJ válido - False para CNPJ não válido</returns>
        public bool ValidaCNPJ(string vrCNPJ)
        {

            string CNPJ = vrCNPJ.Replace(".", "");
            CNPJ = CNPJ.Replace("/", "");
            CNPJ = CNPJ.Replace("-", "");

            int[] digitos, soma, resultado;
            int nrDig;
            string ftmt;
            bool[] CNPJOk;

            ftmt = "6543298765432";
            digitos = new int[14];
            soma = new int[2];
            soma[0] = 0;
            soma[1] = 0;
            resultado = new int[2];
            resultado[0] = 0;
            resultado[1] = 0;
            CNPJOk = new bool[2];
            CNPJOk[0] = false;
            CNPJOk[1] = false;

            try
            {
                for (nrDig = 0; nrDig < 14; nrDig++)
                {
                    digitos[nrDig] = int.Parse(
                        CNPJ.Substring(nrDig, 1));
                    if (nrDig <= 11)
                        soma[0] += (digitos[nrDig] *
                          int.Parse(ftmt.Substring(
                          nrDig + 1, 1)));
                    if (nrDig <= 12)
                        soma[1] += (digitos[nrDig] *
                          int.Parse(ftmt.Substring(
                          nrDig, 1)));
                }

                for (nrDig = 0; nrDig < 2; nrDig++)
                {
                    resultado[nrDig] = (soma[nrDig] % 11);
                    if ((resultado[nrDig] == 0) || (
                         resultado[nrDig] == 1))
                        CNPJOk[nrDig] = (
                        digitos[12 + nrDig] == 0);
                    else
                        CNPJOk[nrDig] = (
                        digitos[12 + nrDig] == (
                        11 - resultado[nrDig]));
                }
                return (CNPJOk[0] && CNPJOk[1]);
            }

            catch
            {
                return false;
            }
        }
        #endregion

        #region Valida SQL
        /// <summary>
        /// Tira Aspas da String - Chamar Rotina para limpar string Antes de montar uma SQL pra jogar no banco
        /// </summary>
        /// <param name="texto">String para serem retiradas as Aspas</param>
        /// <returns>Retorna SQL que tem que ser inserida no banco</returns>
        public string validaSQL(string texto)
        {
            return texto = texto.Replace("'", "");

        }
        #endregion

        #region Limitar Texto
        /// <summary>
        /// Limita o tamanho da STRING - recebe um inteiro e a string, e limita o tamanho dela
        /// </summary>
        /// <param name="texto">Texto a ser limitado</param>
        /// <param name="tam">Quantidades de caracteres que o texto deve ser limitado</param>
        /// <returns>Retorna Texto Limitado</returns>
        public string limitarTexto(string texto, int tam)
        {
            string textoRetorno = texto;
            if (texto.Length > tam)
            {
                textoRetorno = "";
                for (int i = 0; i < tam; i++)
                {
                    textoRetorno = textoRetorno + texto.Substring(i, 1);
                }
            }

            return textoRetorno;
        }
        #endregion

        #region Alongar Texto
        /// <summary>
        /// Alonga o tamanho do texto preenchendo o restante com espaço em branco
        /// </summary>
        /// <param name="texto">Texto a ser limitado</param>
        /// <param name="tam">Quantidades de caracteres que o texto deve sair após caracters</param>
        /// <returns>Retorna Texto Alongado</returns>
        public string alongarTexto(string texto, int tam)
        {
            string textoRetorno = texto;
            if (texto.Length < tam)
            {                
                for (int i = 0; i < tam; i++)
                {
                    textoRetorno = textoRetorno + " ";
                }
            }

            return textoRetorno;
        }
        #endregion

        #region Verifica Virgula (Ponto)
        /// <summary>
        /// Função que Verifica se Existe virgula ou ponto em uma STRING
        /// </summary>
        /// <param name="texto">Recebe a String e verifica se a mesma tem ponto ou virgula</param>
        /// <returns>Retorna True se tiver, false se não tiver</returns>
        public bool verificaVirgula(string texto)
        {
            int tam = texto.Length;
            for (int i = 0; i < tam; i++)
            {
                if (texto.Substring(i, 1) == "," || texto.Substring(i, 1) == ".")
                {
                    return true;
                }
            }
            return false;

        }//fim verificarVirgula
        #endregion

        #region Verifica se valor é uma Data Válida (ex: 11/11/2008 ou 11/11/2008 11:11:11 - se for 1/1/08 ele retorna erro - apenas dd/MM/aaaa hh:mm:ss)
        /// <summary>
        /// Verifica se a String enviada é uma Data Válida (no formado dd/mm/aa ou dd/mm/aa hh:mm:ss - quaisquer outros
        /// formatos retornam erro) (ex: d/m/aa ou d/m/aaaa)
        /// </summary>
        /// <param name="data">data a ser verificada</param>
        /// <returns>retorna TRUE se for uma data Válida, false se não</returns>
        public bool verificaSeEData(string data)
        {
            
            

            try
            {
                DateTime teste = Convert.ToDateTime(data);
            }
            catch
            {
                return false; //retorna false logo no inicio se der pau na conversão
            }//senão, ele irá continuar testando no código a seguir

            //verifica o .Length do campo em busca do tamanho exato!
            int dataLength = data.Length;

            if (dataLength == 10 || dataLength == 16 || dataLength == 17 || dataLength == 18 || dataLength == 19 || dataLength == 20)
            {
                return true;
            }

            else
            {
                return false;
            }
        }
        #endregion

        #region Verifica se a Data Enviada é menor que a Data Atual
        /// <summary>
        /// Verifica se a String enviada é uma Data Menor que a Data Atual
        /// </summary>
        /// <param name="data">data a ser verificada</param>
        /// <returns>retorna TRUE se for menor que a atual, false se não</returns>
        public bool verificaSeEDataEMenorQueAtual(string data)
        {
            if (Convert.ToDateTime(data) < DateTime.Now.Subtract(new TimeSpan(1, 0, 0, 0)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Retorna Data Atual sem nenhuma pontuação
        /// <summary>
        /// Retorna a Data sem Barras nem : - Limpa, ou seja, se hoje for 02/02/2008 11:11
        /// ela retornará 02022008-111100 (dia, mês, ano, hora, minuto,segundo)
        /// </summary>
        /// <returns>retorna data sem pontuação</returns>
        public string retornaDataSemPontosEBarras()
        {
            string dia = DateTime.Now.Day.ToString();
            string mês = DateTime.Now.Month.ToString();
            string ano = DateTime.Now.Year.ToString();
            string hora = DateTime.Now.Hour.ToString();
            string minuto = DateTime.Now.Minute.ToString();
            string segundo = DateTime.Now.Second.ToString();

            string data = dia + mês + ano + hora + minuto + segundo;
            return data;
        }
        #endregion
    }//fim classe limpa string
}//fim namespace

