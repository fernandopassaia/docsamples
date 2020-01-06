using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceProcess;

namespace CSOBRF_Validacoes
{
    #region Classe estática clsFilaProcessosWindows
    public static class FilaProcessosWindows
    {
        #region "Método estático que lista os processos aberto do Windows"
        /// <summary>
        /// Lista todos os processos
        /// </summary>
        /// <returns>retorna a lista com as propriedades do processo</returns>
        public static List<PropriedadesProcessos> ListaProcessos()
        {
            List<PropriedadesProcessos> lstProcessos = new List<PropriedadesProcessos>();

            Process[] Processos = Process.GetProcesses();

            foreach (var item in Processos)
            {
                lstProcessos.Add(new PropriedadesProcessos { Nome = item.ProcessName, ID = item.Id.ToString(), Titulo_Pagina = item.MainWindowTitle });
            }

            //var query = (from o in lstProcessos
              //           select o).OrderBy(p => p.ID);
            return lstProcessos;
        }
        #endregion

        #region "Método estático que inicia um processo no Windows"
        /// <summary>
        /// Inicia um novo processo
        /// </summary>
        /// <param name="Nome">Nome do novo processo</param>
        public static void IniciarProcesso(string Nome)
        {
            Process.Start(Nome);
        }
        #endregion

        #region Método estático que finaliza um processo no Windows por ID
        /// <summary>
        /// Finaliza um processo pelo ID
        /// </summary>
        /// <param name="IDPROC">Id do processo que será finalizado</param>
        public static void FinalizaProcessoID(string IDPROC)
        {
            foreach (var proc in Process.GetProcesses())
            {
                if (proc.Id.ToString() == IDPROC)
                {
                    proc.Kill();
                }

            }
        }
        #endregion

        #region Método estático para finalizar processo (por nome processo)
        public static void finalizarProcessoPorNome(string nome) {                                    
            string id = "";      
            Process[] Processos = Process.GetProcesses();

            foreach (var item in Processos)
            {
                if( nome == item.ProcessName){
                    id = item.Id.ToString();
                    FinalizaProcessoID(id);
                }                
            }          

        }
        #endregion

        #region Método estático para finalizar Programa
        public static void finalizarPrograma()
        {  
            string nome = System.Environment.CommandLine.ToString();// ExitCode.ToString();

            while (nome.IndexOf(@"\") != -1)
            {
                nome= nome.Substring(nome.IndexOf(@"\") + 1);
                //MessageBox.Show(nome);
            }
            nome = nome.Substring(0, nome.IndexOf(".exe"));
            //MessageBox.Show(nome);

            finalizarProcessoPorNome(nome);
        }
        #endregion

        #region Métodos Referentes a Serviços do Windows (Inicia, Para e Reinicia Serviço)
        public static void IniciaServico(string serviceName, int timeoutMilliseconds = 10000)
        {
            ServiceController service = new ServiceController(serviceName);
            try
            {
                TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);
                service.Refresh();
                if (service.Status == ServiceControllerStatus.Stopped)
                {
                    service.Start();
                    service.WaitForStatus(ServiceControllerStatus.Running, timeout);
                }
                else
                {
                    throw new Exception(string.Format("{0} --> já esta iniciado.", service.DisplayName));
                }
            }
            catch
            {
                throw;
            }
        }

        public static void ParaServico(string serviceName, int timeoutMilliseconds = 10000)
        {
            ServiceController service = new ServiceController(serviceName);
            try
            {
                TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);
                service.Refresh();

                if (service.Status == ServiceControllerStatus.Running)
                {
                    service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
                }
                else
                {
                    throw new Exception(string.Format("{0} --> já esta parado.", service.DisplayName));
                }
            }
            catch
            {
                throw;
            }
        }

        public static void ReiniciaServico(string serviceName, int timeoutMilliseconds = 10000)
        {
            ServiceController service = new ServiceController(serviceName);
            try
            {
                int millisec1 = Environment.TickCount;
                TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);
                service.Refresh();

                if (service.Status != ServiceControllerStatus.Stopped)
                {
                    service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);

                    // conta o resto do timeout
                    int millisec2 = Environment.TickCount;
                    timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds - (millisec2 - millisec1));

                    service.Start();
                    service.WaitForStatus(ServiceControllerStatus.Running, timeout);
                }
                else
                {
                    service.Start();
                    throw new Exception(string.Format("{0} --> foi parado e a seguir iniciado", service.DisplayName));
                }
            }
            catch
            {
                throw;
            }
        }

        public static int RetornaStatusServico(string serviceName, int timeoutMilliseconds = 10000)
        {
            ServiceController serviceController = new ServiceController(serviceName);
            try
            {
                int tickCount = Environment.TickCount;
                TimeSpan.FromMilliseconds((double)timeoutMilliseconds);
                serviceController.Refresh();
                if (serviceController.Status == ServiceControllerStatus.Stopped)
                    return 0;
                if (serviceController.Status == ServiceControllerStatus.Running)
                    return 1;
                if (serviceController.Status == ServiceControllerStatus.StartPending)
                    return 2;
                return serviceController.Status == ServiceControllerStatus.StopPending ? 3 : 0;
            }
            catch
            {
                return 0;
            }
        }
        #endregion
    }
    #endregion

    #region Classe Utilizada para armazenar dados dos Processos do windows
    public class PropriedadesProcessos
    {
        public string Nome
        { get; set; }

        public string ID
        { get; set; }

        public string Titulo_Pagina
        { get; set; }
    }
    #endregion
}