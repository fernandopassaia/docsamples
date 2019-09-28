using System;
using System.Threading.Tasks;

namespace Net_ProgAsync.CLDemo
{
    public class Exemplo
    {
        //método Task com retorno Task<TResult>
        async Task<bool> Task_TResult_Async()
        {
            return await
            Task.FromResult<bool>(DateTime.IsLeapYear(DateTime.Now.Year));
        }

        //método com retorno void (Task)
        async Task Task_Void_Async()
        {
            //nota: ele vai entrar nesse método em paralelo e vai ficar 10 segundos aqui esperando...
            await Task.Delay(9999);
            Console.WriteLine("10 segundos de atraso");
        }

        public async Task Task_LongaDuracao()
        {
            Console.WriteLine("Teste iniciado as " + DateTime.Now.ToString("hh:mm:ss"));
            bool isAnoBissexto = await Task_TResult_Async();
            for (int i = 0; i <= 10000; i++)
            {
                i++;
            }
            isAnoBissexto = await Task_TResult_Async();

            Console.WriteLine($"{DateTime.Now.Year} {(isAnoBissexto ? " é " : " não é ")} um Ano Bissexto");
            await Task_Void_Async();

            Task taskTResultAsync = Task_TResult_Async();
            for (int i = 0; i <= 10000; i++)
            {
                i++;
            }
            await taskTResultAsync;
            Console.WriteLine("Teste Finalizado as " + DateTime.Now.ToString("hh:mm:ss"));
        }
    }
}
