using ledReport;
using ledReport.Class;
using ledReport.Models;
//using smtLocations.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ledReport{
    class CMailSender
    {

        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private CSQL m_db;
        excel m_excel;
        public CMailSender()
        {
            m_db = new CSQL();
            m_excel = new excel();
        }
        public void sendMail(System.Diagnostics.EventLog system_events)
        {
            try
            {
                
                String pathReport = "";
                String pathReporterror = "";
                String fileName = "LED_REPORT" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".xlsx";
                CUtils utils = new CUtils();
                String error = "";
                DataTable results = new DataTable();
                DataTable yest = new DataTable();
                //system_events.WriteEntry("Obteniendo registros de base de datos de Oracle.");
                if (m_db.getMonthlyDetail(ref results)) {

                    try {
                        m_db.getYesterdayDetail(ref yest);
                        m_excel.write_fileOLE(results,yest, fileName, "C:\\Reports", ref pathReport, system_events);
                    }
                    catch(Exception ex)
                    {
                        system_events.WriteEntry("Ocurrio un error al Construir Reporte. " + ex.Message);
                    }

                    List<string> lstArchivos = new List<string>();
                    lstArchivos.Add(pathReport);

                    //if(m_oracle.get_error_report(ref pathReporterror, system_events))
                    //    lstArchivos.Add(pathReporterror);
                    //String mails = "asn-sem@siix-sem.com.mx;warehouse.receiving@SIIX-SEM.com.mx;ruben.regis@SIIX-SEM.com.mx;kenny.manzanilla@SIIX-SEM.com.mx;christian.gonzalez@siix-sem.com.mx;luisfernando.torres@SIIX-SEM.com.mx;cristobal.munoz@siix-sem.com.mx;antonio.hernandez@siix-sem.com.mx;javier.gallardo@siix-sem.com.mx;victor.moreno@siix-sem.com.mx;pre-receiving@siix.mx;dulce.loredo@siix-sem.com.mx;raymundo.salas@siix-sem.com.mx;indirectos@siix-sem.com.mx";
                    String mails = "led.report@siix-global.com;antonio.hernandez@siix-global.com";
                    //String mails = "antonio.hernandez@siix-global.com";

                    //creamos nuestro objeto de la clase que hicimos
                    CMail oMail = new CMail("siixsem.reports@siix-global.com", mails,
                                         "Led Report", "Led Report", lstArchivos);

                    oMail.Message = "Se anexa reporte de Leds / Attached you will find Leds report.<br><br> Saludos / Regards.";

                    //y enviamos
                    if (oMail.enviaMail(ref error))
                    {
                        system_events.WriteEntry("Se envio por E-mail Led Report.");

                    }
                    else
                    {
                        system_events.WriteEntry("No se envio el mail: " + oMail.error + "  \n" + error);
                       //logger.Error("No se envio el mail: " + oMail.error);

                    }
                }
            }
            catch(Exception ex)
            {
                system_events.WriteEntry("Ocurrio un error al Construir Reporte. " + ex.Message);
            }
        }
    }
}
