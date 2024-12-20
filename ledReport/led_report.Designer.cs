
namespace ledReport
{
    partial class led_report
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.system_events = new System.Diagnostics.EventLog();
            ((System.ComponentModel.ISupportInitialize)(this.system_events)).BeginInit();
            // 
            // led_report
            // 
            this.ServiceName = "Led Report";
            ((System.ComponentModel.ISupportInitialize)(this.system_events)).EndInit();

        }

        #endregion

        private System.Diagnostics.EventLog system_events;
    }
}
