namespace SportLotoService
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Drawing_Generator = new System.ServiceProcess.ServiceProcessInstaller();
            this.serviceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // Drawing_Generator
            // 
            this.Drawing_Generator.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.Drawing_Generator.Password = null;
            this.Drawing_Generator.Username = null;
            // 
            // serviceInstaller
            // 
            this.serviceInstaller.ServiceName = "DrawUpdate Service 2";
            this.serviceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.Drawing_Generator,
            this.serviceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller Drawing_Generator;
        private System.ServiceProcess.ServiceInstaller serviceInstaller;
    }
}