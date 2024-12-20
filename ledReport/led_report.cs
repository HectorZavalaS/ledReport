using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;

namespace ledReport
{
    public partial class led_report : ServiceBase
    {
        CMailSender senderM;
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        Timer timer = new Timer();
        public led_report()
        {
            InitializeComponent();
            senderM = new CMailSender();
            system_events = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists("Led Report"))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    "Led Report", "Application");
            }
            system_events.Source = "Led Report";
            system_events.Log = "Application";
        }

        protected override void OnStart(string[] args)
        {
            try
            {

                system_events.WriteEntry("Iniciado servicio de reporte de Leds. ");
                timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
                timer.Interval = 1000; //number in milisecinds  
                timer.Enabled = true;

            }
            catch (Exception ex)
            {
                system_events.WriteEntry("Ocurrio un error al iniciar el Timer. " + ex.Message);
                //logger.Error(ex, "Ocurrio un error al iniciar el Timer.");
            }
        }
        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            try
            {
                int day = (int)DateTime.Now.DayOfWeek;
                if (day >= 1 && day <= 6)
                {
                    //if ((DateTime.Now.Hour == 10 && DateTime.Now.Minute == 23 && DateTime.Now.Second == 0))
                    if ((DateTime.Now.Hour == 0 && DateTime.Now.Minute == 25 && DateTime.Now.Second == 0))
                    {
                        system_events.WriteEntry("Se enviara reporte de Leds.");
                        senderM.sendMail(system_events);
                    }
                }
            }
            catch (Exception ex)
            {
                system_events.WriteEntry("Ocurrio un error al ejecutar Timer. " + ex.Message);
            }
        }
        protected override void OnStop()
        {
        }
    }
}
