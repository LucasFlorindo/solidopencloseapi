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

        //Objeto para lidar com arquivos
        ModelDoc2 swModelDoc = null;

        //Objeto para lidar com peças
        PartDoc swPart = null;

        //Objeto para lidar com montagens
        AssemblyDoc swAsmb = null;

        //Objeto para lidar com desenhos
        DrawingDoc swDrw = null;

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

        public void AbrirArquivo(string CaminhoArquivo, string Extensao)
        {
            int err = 0, wars = 0;
            try
            {
                if (Extensao.ToUpper() == ".SLDPRT")
                {
                    swModelDoc = swApp.OpenDoc6(CaminhoArquivo, (int)swDocumentTypes_e.swDocPART, 0, "", err, wars);
                    swPart = (PartDoc)swModelDoc;
                }
                else if (Extensao.ToUpper() == ".SLDASM")
                {
                    swModelDoc = swApp.OpenDoc6(CaminhoArquivo, (int)swDocumentTypes_e.swDocASSEMBLY, 0, "", err, wars);
                    swAsmb = (AssemblyDoc)swModelDoc;
                }
                else if (Extensao.ToUpper() == "SLDDRW")
                {
                    swModelDoc = swApp.OpenDoc6(CaminhoArquivo, (int)swDocumentTypes_e.swDocDRAWING, 0, "", err, wars);
                    swDrw = (DrawingDoc)swModelDoc;

                }


                // swDoocumentTypes_e. o ponto puxa qual tipo de documento irá abrir. Nesse exemplo vou abrir um documento de uma peça.
                // essas invocações 'int' são requisitos da função swApp.Opendoc.
                // a configuração "", representa a execução no vazio, default
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

        //Verify if the archive has metalic plate resource
        private bool VerificaSheetMetal()
        {
            // receives first archive feature
            Feature feat = (Feature)swModelDoc.FirstFeature();

            //starts a loop that goes through all features
            while (feat != null)
            {
                //everytime it finds a sheetmeal feature, returns true
                if (feat.GetTypeName2().ToUpper() == "SHEETMETAL")
                {
                    return true;
                }
                feat = (Feature)feat.GetNextFeature();

            }

            return false;
        }


        //Exports an archive to JPG

        public void ExportarParaJPG(string PathDestino)
        {
            try
            {
                bool resultado = swModelDoc.SaveAs(PathDestino);

                if (!resultado)
                {
                    throw new ArgumentException($"Erro em {nameof(ExportarParaJPG)}: Não foi possível converter o arquivo para JPG.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro em {nameof(ExportarParaJPG)}: Não foi possível converter o arquivo para JPG.\n{ex.Message}\n{ex.StackTrace}");

            }
        }


        //exports an archive to DWG



    }
}
