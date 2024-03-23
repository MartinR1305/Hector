
namespace Hector
{
    partial class FormExporter
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
            this.Exportation_En_Cours_Label = new System.Windows.Forms.Label();
            this.ProgressBar_Exporter = new System.Windows.Forms.ProgressBar();
            this.Selectionner_Dossier_Bouton_Exporter = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Chemin_Dossier_TextBox = new System.Windows.Forms.TextBox();
            this.Background_Worker_Exporter = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // Exportation_En_Cours_Label
            // 
            this.Exportation_En_Cours_Label.Location = new System.Drawing.Point(9, 114);
            this.Exportation_En_Cours_Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Exportation_En_Cours_Label.Name = "Exportation_En_Cours_Label";
            this.Exportation_En_Cours_Label.Size = new System.Drawing.Size(346, 27);
            this.Exportation_En_Cours_Label.TabIndex = 11;
            this.Exportation_En_Cours_Label.Text = "Exportation_En_Cours_Label";
            this.Exportation_En_Cours_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ProgressBar_Exporter
            // 
            this.ProgressBar_Exporter.Location = new System.Drawing.Point(9, 143);
            this.ProgressBar_Exporter.Margin = new System.Windows.Forms.Padding(2);
            this.ProgressBar_Exporter.Name = "ProgressBar_Exporter";
            this.ProgressBar_Exporter.Size = new System.Drawing.Size(346, 19);
            this.ProgressBar_Exporter.TabIndex = 9;
            // 
            // Selectionner_Dossier_Bouton_Exporter
            // 
            this.Selectionner_Dossier_Bouton_Exporter.Location = new System.Drawing.Point(274, 33);
            this.Selectionner_Dossier_Bouton_Exporter.Margin = new System.Windows.Forms.Padding(2);
            this.Selectionner_Dossier_Bouton_Exporter.Name = "Selectionner_Dossier_Bouton_Exporter";
            this.Selectionner_Dossier_Bouton_Exporter.Size = new System.Drawing.Size(77, 28);
            this.Selectionner_Dossier_Bouton_Exporter.TabIndex = 6;
            this.Selectionner_Dossier_Bouton_Exporter.Text = "Parcourir";
            this.Selectionner_Dossier_Bouton_Exporter.UseVisualStyleBackColor = true;
            this.Selectionner_Dossier_Bouton_Exporter.Click += new System.EventHandler(this.Selectionner_Dossier_Bouton_Exporter_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(11, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(169, 27);
            this.label1.TabIndex = 12;
            this.label1.Text = "Nom du fichier CSV :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Chemin_Dossier_TextBox
            // 
            this.Chemin_Dossier_TextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Chemin_Dossier_TextBox.Location = new System.Drawing.Point(12, 34);
            this.Chemin_Dossier_TextBox.Name = "Chemin_Dossier_TextBox";
            this.Chemin_Dossier_TextBox.Size = new System.Drawing.Size(257, 26);
            this.Chemin_Dossier_TextBox.TabIndex = 13;
            // 
            // Background_Worker_Exporter
            // 
            this.Background_Worker_Exporter.WorkerReportsProgress = true;
            this.Background_Worker_Exporter.WorkerSupportsCancellation = true;
            this.Background_Worker_Exporter.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Background_Worker_Exporter_DoWork);
            this.Background_Worker_Exporter.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.Background_Worker_Exporter_ProgressChanged);
            this.Background_Worker_Exporter.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.Background_Worker_Exporter_RunWorkerCompleted);
            // 
            // FormExporter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 206);
            this.Controls.Add(this.Chemin_Dossier_TextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Exportation_En_Cours_Label);
            this.Controls.Add(this.ProgressBar_Exporter);
            this.Controls.Add(this.Selectionner_Dossier_Bouton_Exporter);
            this.Name = "FormExporter";
            this.Text = "FormExporter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Exportation_En_Cours_Label;
        private System.Windows.Forms.ProgressBar ProgressBar_Exporter;
        private System.Windows.Forms.Button Selectionner_Dossier_Bouton_Exporter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Chemin_Dossier_TextBox;
        private System.ComponentModel.BackgroundWorker Background_Worker_Exporter;
    }
}