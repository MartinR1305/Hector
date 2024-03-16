
namespace Hector
{
    partial class FormMain
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.MenuStrip1 = new System.Windows.Forms.MenuStrip();
            this.FichierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Actualiser_Bouton = new System.Windows.Forms.ToolStripMenuItem();
            this.Importer_Bouton = new System.Windows.Forms.ToolStripMenuItem();
            this.Exporter_Bouton = new System.Windows.Forms.ToolStripMenuItem();
            this.StatusStrip1 = new System.Windows.Forms.StatusStrip();
            this.SplitContainer1 = new System.Windows.Forms.SplitContainer();
            this.TreeView1 = new System.Windows.Forms.TreeView();
            this.ListView1 = new System.Windows.Forms.ListView();
            this.MenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer1)).BeginInit();
            this.SplitContainer1.Panel1.SuspendLayout();
            this.SplitContainer1.Panel2.SuspendLayout();
            this.SplitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.MenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.MenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FichierToolStripMenuItem});
            this.MenuStrip1.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip1.Name = "menuStrip1";
            this.MenuStrip1.Size = new System.Drawing.Size(1067, 30);
            this.MenuStrip1.TabIndex = 0;
            this.MenuStrip1.Text = "menuStrip1";
            // 
            // fichierToolStripMenuItem
            // 
            this.FichierToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Actualiser_Bouton,
            this.Importer_Bouton,
            this.Exporter_Bouton});
            this.FichierToolStripMenuItem.Name = "fichierToolStripMenuItem";
            this.FichierToolStripMenuItem.Size = new System.Drawing.Size(66, 26);
            this.FichierToolStripMenuItem.Text = "Fichier";
            // 
            // Actualiser_Bouton
            // 
            this.Actualiser_Bouton.Name = "Actualiser_Bouton";
            this.Actualiser_Bouton.Size = new System.Drawing.Size(157, 26);
            this.Actualiser_Bouton.Text = "Actualiser";
            // 
            // Importer_Bouton
            // 
            this.Importer_Bouton.Name = "Importer_Bouton";
            this.Importer_Bouton.Size = new System.Drawing.Size(157, 26);
            this.Importer_Bouton.Text = "Importer";
            this.Importer_Bouton.Click += new System.EventHandler(this.Importer_Bouton_Click);
            // 
            // Exporter_Bouton
            // 
            this.Exporter_Bouton.Name = "Exporter_Bouton";
            this.Exporter_Bouton.Size = new System.Drawing.Size(157, 26);
            this.Exporter_Bouton.Text = "Exporter";
            // 
            // statusStrip1
            // 
            this.StatusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.StatusStrip1.Location = new System.Drawing.Point(0, 532);
            this.StatusStrip1.Name = "statusStrip1";
            this.StatusStrip1.Size = new System.Drawing.Size(1067, 22);
            this.StatusStrip1.TabIndex = 1;
            this.StatusStrip1.Text = "statusStrip1";
            // 
            // splitContainer1
            // 
            this.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainer1.Location = new System.Drawing.Point(0, 30);
            this.SplitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.SplitContainer1.Panel1.Controls.Add(this.TreeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.SplitContainer1.Panel2.Controls.Add(this.ListView1);
            this.SplitContainer1.Size = new System.Drawing.Size(1067, 502);
            this.SplitContainer1.SplitterDistance = 355;
            this.SplitContainer1.TabIndex = 2;
            // 
            // treeView1
            // 
            this.TreeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TreeView1.Location = new System.Drawing.Point(0, 0);
            this.TreeView1.Name = "treeView1";
            this.TreeView1.Size = new System.Drawing.Size(355, 502);
            this.TreeView1.TabIndex = 0;
            // 
            // listView1
            // 
            this.ListView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListView1.HideSelection = false;
            this.ListView1.Location = new System.Drawing.Point(0, 0);
            this.ListView1.Name = "listView1";
            this.ListView1.Size = new System.Drawing.Size(708, 502);
            this.ListView1.TabIndex = 0;
            this.ListView1.UseCompatibleStateImageBehavior = false;
            this.ListView1.View = System.Windows.Forms.View.Details;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.SplitContainer1);
            this.Controls.Add(this.StatusStrip1);
            this.Controls.Add(this.MenuStrip1);
            this.MainMenuStrip = this.MenuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormMain";
            this.Text = "Hector";
            this.MenuStrip1.ResumeLayout(false);
            this.MenuStrip1.PerformLayout();
            this.SplitContainer1.Panel1.ResumeLayout(false);
            this.SplitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer1)).EndInit();
            this.SplitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem FichierToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Actualiser_Bouton;
        private System.Windows.Forms.ToolStripMenuItem Importer_Bouton;
        private System.Windows.Forms.ToolStripMenuItem Exporter_Bouton;
        private System.Windows.Forms.StatusStrip StatusStrip1;
        private System.Windows.Forms.SplitContainer SplitContainer1;
        private System.Windows.Forms.TreeView TreeView1;
        private System.Windows.Forms.ListView ListView1;
    }
}

