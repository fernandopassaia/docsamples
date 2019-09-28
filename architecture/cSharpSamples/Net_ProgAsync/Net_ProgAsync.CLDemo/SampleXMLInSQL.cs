using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Net_ProgAsync.CLDemo
{
    public class SampleXMLInSQL
    {
        string dataBase = "Data Source=PCDEVWIN\\FUTURADATA2014;Initial Catalog=AsyncAwait;Integrated Security=False;User ID=SA;Password=@1234Fd@";
        SqlConnection conexao;

        //Essa classe irá ter dois métodos: Um irá ler 10x o mesmo arquivo XML (um arquivo de 2.5MB) gerando dessa maneira 22MB
        //de informação (mesmo que repetida). Essa informação toda será persistida no SQL Server, o que tornará a operação
        //ainda mais penosa (posso apontar pra um SQL na nuvem, pra deixar ainda mais lento). A intenção é que o aplicativo faça
        //isso em PARALELO, sem travar, e vá dando Feedbacks no OUTPUT do Visual Studio (olhar OutPut enquanto gira).

        //método com retorno void (Task)
        //async Task Task_Void_Async()
        //{
        //    //nota: ele vai entrar nesse método em paralelo e vai ficar 10 segundos aqui esperando...
        //    await Task.Delay(9999);
        //    Console.WriteLine("10 segundos de atraso");
        //}

        //TABELA NO BANCO DE DADOS:
        //CREATE TABLE[dbo].[COURSES]
        //(
        //   [PK][int] IDENTITY(1,1) NOT NULL,
        //   [Course] [varchar] (10) NULL,
        //   [Title] [varchar] (200) NULL,
        //   CONSTRAINT[PK_COURSES] PRIMARY KEY CLUSTERED
        //(
        //[PK] ASC
        //)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]
        //)ON[PRIMARY]

        //GO

        async Task<int> InsereDadosSQL(string curso, string descricao)
        {
            //return await
            //Task.FromResult<bool>(DateTime.IsLeapYear(DateTime.Now.Year));            
            
            SqlCommand command = new SqlCommand("INSERT INTO COURSES(Course,Title)VALUES(@Course,@Title)", conexao);
            SqlParameter parametro1 = new SqlParameter("@Course", curso);
            SqlParameter parametro2 = new SqlParameter("@Title", descricao);
            command.Parameters.Add(parametro1);
            command.Parameters.Add(parametro2);
            await Task.Delay(50); //faço ele parar por 0,05s dentro da Task propositalmente (pra simular uma operação mais lenta) - como se fosse um Thread.Sleep
            return await
            Task.FromResult<int>(command.ExecuteNonQuery()); //retorna um INT do ExecuteNonQuery, tenho exemplo de bool na outra classe
            //return await command.ExecuteNonQuery();
        }        

        public async Task Task_LongaDuracao()
        {
            conexao = new SqlConnection(dataBase);
            conexao.Open(); //abro a conexao

            Console.WriteLine("Iniciando Leitura do XML " + DateTime.Now.ToString("hh:mm:ss"));
            DataSet dsDados = new DataSet();
            string caminhoXml = Directory.GetCurrentDirectory().Replace("\\bin\\Debug", "");
            dsDados.ReadXml(caminhoXml + "\\XML2MB.xml");

            //nota: irá ler o XML de 2.2MB e irá inserir 2112 registros no banco de dados            
            DataTable dtDados = dsDados.Tables["course_listing"];
            
            Console.WriteLine("Iniciando inserções do dtDados no SQL Server " + DateTime.Now.ToString("hh:mm:ss"));
            for (int i = 0; i < dtDados.Rows.Count; i++)
            {
                Task taskInserirBanco = InsereDadosSQL(dtDados.Rows[i]["course"].ToString(), dtDados.Rows[i]["title"].ToString());
                await taskInserirBanco; //poderia dar o await InsereDados direto (nota: essa task dentro tem um sleep de 0,05s pra simular lentidão)
                Console.WriteLine("Inserindo Registro " + i.ToString() + " - " + DateTime.Now.ToString("hh:mm:ss"));
            }
            Console.WriteLine("Fechando inserções do dtDados no SQL Server " + DateTime.Now.ToString("hh:mm:ss"));
            conexao.Close(); //fecho ela
        }
    }//fim classe
}//fim namespace
