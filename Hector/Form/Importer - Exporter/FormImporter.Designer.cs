
namespace Hector
{
    partial class FormImporter
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Selectionner_Fichier_Bouton = new System.Windows.Forms.Button();
            this.Integration_Mode_Ajout_Bouton = new System.Windows.Forms.Button();
            this.Importation_Mode_Ecrasement_Boutton = new System.Windows.Forms.Button();
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.Integration_En_Cours_Label = new System.Windows.Forms.Label();
            this.Background_Worker = new System.ComponentModel.BackgroundWorker();
            this.Nom_Fichier_CSV_TextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Selectionner_Fichier_Bouton
            // 
            this.Selectionner_Fichier_Bouton.Location = new System.Drawing.Point(285, 36);
            this.Selectionner_Fichier_Bouton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Selectionner_Fichier_Bouton.Name = "Selectionner_Fichier_Bouton";
            this.Selectionner_Fichier_Bouton.Size = new System.Drawing.Size(66, 29);
            this.Selectionner_Fichier_Bouton.TabIndex = 0;
            this.Selectionner_Fichier_Bouton.Text = "Parcourir";
            this.Selectionner_Fichier_Bouton.UseVisualStyleBackColor = true;
            this.Selectionner_Fichier_Bouton.Click += new System.EventHandler(this.Selectionner_Fichier_Bouton_Click);
            // 
            // Integration_Mode_Ajout_Bouton
            // 
            this.Integration_Mode_Ajout_Bouton.Location = new System.Drawing.Point(9, 88);
            this.Integration_Mode_Ajout_Bouton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Integration_Mode_Ajout_Bouton.Name = "Integration_Mode_Ajout_Bouton";
            this.Integration_Mode_Ajout_Bouton.Size = new System.Drawing.Size(173, 29);
            this.Integration_Mode_Ajout_Bouton.TabIndex = 1;
            this.Integration_Mode_Ajout_Bouton.Text = "Intégration - Mode Ajout";
            this.Integration_Mode_Ajout_Bouton.UseVisualStyleBackColor = true;
            this.Integration_Mode_Ajout_Bouton.Click += new System.EventHandler(this.Importation_Mode_Ajout_Bouton_Click);
            // 
            // Importation_Mode_Ecrasement_Boutton
            // 
            this.Importation_Mode_Ecrasement_Boutton.Location = new System.Drawing.Point(182, 88);
            this.Importation_Mode_Ecrasement_Boutton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Importation_Mode_Ecrasement_Boutton.Name = "Importation_Mode_Ecrasement_Boutton";
            this.Importation_Mode_Ecrasement_Boutton.Size = new System.Drawing.Size(173, 29);
            this.Importation_Mode_Ecrasement_Boutton.TabIndex = 2;
            this.Importation_Mode_Ecrasement_Boutton.Text = "Intégration - Mode Écrasement";
            this.Importation_Mode_Ecrasement_Boutton.UseVisualStyleBackColor = true;
            this.Importation_Mode_Ecrasement_Boutton.Click += new System.EventHandler(this.Importation_Mode_Ecrasement_Boutton_Click);
            // 
            // ProgressBar
            // 
            this.ProgressBar.Location = new System.Drawing.Point(9, 140);
            this.ProgressBar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(346, 19);
            this.ProgressBar.TabIndex = 3;
            // 
            // Integration_En_Cours_Label
            // 
            this.Integration_En_Cours_Label.Location = new System.Drawing.Point(9, 161);
            this.Integration_En_Cours_Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Integration_En_Cours_Label.Name = "Integration_En_Cours_Label";
            this.Integration_En_Cours_Label.Size = new System.Drawing.Size(346, 27);
            this.Integration_En_Cours_Label.TabIndex = 5;
            this.Integration_En_Cours_Label.Text = "Integration_En_Cours_Label";
            this.Integration_En_Cours_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Background_Worker
            // 
            this.Background_Worker.WorkerReportsProgress = true;
            this.Background_Worker.WorkerSupportsCancellation = true;
            this.Background_Worker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Background_Worker_DoWork);
            this.Background_Worker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.Background_Worker_ProgressChanged);
            this.Background_Worker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.Background_Worker_RunWorkerCompleted);
            // 
            // Nom_Fichier_CSV_TextBox
            // 
            this.Nom_Fichier_CSV_TextBox.Location = new System.Drawing.Point(12, 41);
            this.Nom_Fichier_CSV_TextBox.Name = "Nom_Fichier_CSV_TextBox";
            this.Nom_Fichier_CSV_TextBox.Size = new System.Drawing.Size(268, 20);
            this.Nom_Fichier_CSV_TextBox.TabIndex = 6;
            // 
            // FormImporter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 206);
            this.Controls.Add(this.Nom_Fichier_CSV_TextBox);
            this.Controls.Add(this.Integration_En_Cours_Label);
            this.Controls.Add(this.ProgressBar);
            this.Controls.Add(this.Importation_Mode_Ecrasement_Boutton);
            this.Controls.Add(this.Integration_Mode_Ajout_Bouton);
            this.Controls.Add(this.Selectionner_Fichier_Bouton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.Name = "FormImporter";
            this.Text = "Importer un fichier CSV";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button Selectionner_Fichier_Bouton;
        private System.Windows.Forms.Button Integration_Mode_Ajout_Bouton;
        private System.Windows.Forms.Button Importation_Mode_Ecrasement_Boutton;
        private System.Windows.Forms.ProgressBar ProgressBar;
        private System.Windows.Forms.Label Integration_En_Cours_Label;
        private System.ComponentModel.BackgroundWorker Background_Worker;
        private System.Windows.Forms.TextBox Nom_Fichier_CSV_TextBox;
    }
}