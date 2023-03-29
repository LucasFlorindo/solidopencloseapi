using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;

namespace WindowsFormsApp1.Classes
{
    class SWAPI
    {
        SldWorks swApp = default(SldWorks);

        int ProcessID = 0;

        public void AbrirSolidworks(bool Visivel, int Versao)
        {
            try
            {
                swApp = Activator.CreateInstance(Type.GetTypeFromProgID($"SldWorks.Application.{Versao.ToString()}")) as SldWorks;
                swApp.Visible = Visivel;
                ProcessID = swApp.GetProcessID();


                 
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao abrir o Solidworks: {ex.Message}\n{ex.StackTrace}");
            }

        }

        public void FecharSolidworks()
        {
            try
            {
                Process[] processos = Process.GetProcesses();
                var sldworks = processos.FirstOrDefault(x => x.Id == ProcessID);
                sldworks.Kill();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao fechar o Solidworks: {ex.Message}\n{ex.StackTrace}");
            }
        }

        public void AbrirArquivo(string CaminhoArquivo)
        {
            int err = 0, wars = 0;
            try
            {
                // swDoocumentTypes_e. o ponto puxa qual tipo de documento irá abrir. Nesse exemplo vou abrir um documento de uma peça.
                // essas invocações 'int' são requisitos da função swApp.Opendoc.
                // a configuração "", representa a execução no vazio, default
                ModelDoc2 swModelDoc = swApp.OpenDoc6(CaminhoArquivo, (int)swDocumentTypes_e.swDocPART, 0, "", err, wars);

                PartDoc swPart = (PartDoc)swModelDoc;
                AssemblyDoc swAssmb = (AssemblyDoc)swModelDoc;
                DrawingDoc swDrw = (DrawingDoc)swModelDoc;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao abrir um arquivo no Solidworks: {ex.Message}\n{ex.StackTrace}");
            }
        }

        public void FecharArquivo(string CaminhoArquivo)
        {
            try
            {
                swApp.CloseDoc(CaminhoArquivo);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao fechar um arquivo no Solidworks: {ex.Message}\n{ex.StackTrace}");
            }
        }

        public string ObtemArquivoAtivo()
        {
            string arquivo = "";
            try
            {
                ModelDoc2 swModelDoc = (ModelDoc2)swApp.ActiveDoc;
                arquivo = swModelDoc.GetTitle();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao fechar um arquivo no Solidworks: {ex.Message}\n{ex.StackTrace}");
            }

            return arquivo;
        }

    }
}
