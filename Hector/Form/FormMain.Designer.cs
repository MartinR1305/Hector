
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
            this.components = new System.ComponentModel.Container();
            this.MenuStrip1 = new System.Windows.Forms.MenuStrip();
            this.FichierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Actualiser_Bouton = new System.Windows.Forms.ToolStripMenuItem();
            this.Importer_Bouton = new System.Windows.Forms.ToolStripMenuItem();
            this.Exporter_Bouton = new System.Windows.Forms.ToolStripMenuItem();
            this.StatusStrip1 = new System.Windows.Forms.StatusStrip();
            this.SplitContainer1 = new System.Windows.Forms.SplitContainer();
            this.TreeView1 = new System.Windows.Forms.TreeView();
            this.ListView1 = new System.Windows.Forms.ListView();
            this.Menu_Contextuel = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Menu_Contextuel_Ajouter = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Contextuel_Modifier = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Contextuel_Supprimer = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer1)).BeginInit();
            this.SplitContainer1.Panel1.SuspendLayout();
            this.SplitContainer1.Panel2.SuspendLayout();
            this.SplitContainer1.SuspendLayout();
            this.Menu_Contextuel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuStrip1
            // 
            this.MenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.MenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FichierToolStripMenuItem});
            this.MenuStrip1.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip1.Name = "MenuStrip1";
            this.MenuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.MenuStrip1.Size = new System.Drawing.Size(800, 24);
            this.MenuStrip1.TabIndex = 0;
            this.MenuStrip1.Text = "menuStrip1";
            // 
            // FichierToolStripMenuItem
            // 
            this.FichierToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Actualiser_Bouton,
            this.Importer_Bouton,
            this.Exporter_Bouton});
            this.FichierToolStripMenuItem.Name = "FichierToolStripMenuItem";
            this.FichierToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.FichierToolStripMenuItem.Text = "Fichier";
            // 
            // Actualiser_Bouton
            // 
            this.Actualiser_Bouton.Name = "Actualiser_Bouton";
            this.Actualiser_Bouton.Size = new System.Drawing.Size(180, 22);
            this.Actualiser_Bouton.Text = "Actualiser";
            this.Actualiser_Bouton.Click += new System.EventHandler(this.Actualiser_Bouton_Click);
            // 
            // Importer_Bouton
            // 
            this.Importer_Bouton.Name = "Importer_Bouton";
            this.Importer_Bouton.Size = new System.Drawing.Size(180, 22);
            this.Importer_Bouton.Text = "Importer";
            this.Importer_Bouton.Click += new System.EventHandler(this.Importer_Bouton_Click);
            // 
            // Exporter_Bouton
            // 
            this.Exporter_Bouton.Name = "Exporter_Bouton";
            this.Exporter_Bouton.Size = new System.Drawing.Size(180, 22);
            this.Exporter_Bouton.Text = "Exporter";
            this.Exporter_Bouton.Click += new System.EventHandler(this.Exporter_Bouton_Click);
            // 
            // StatusStrip1
            // 
            this.StatusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.StatusStrip1.Location = new System.Drawing.Point(0, 428);
            this.StatusStrip1.Name = "StatusStrip1";
            this.StatusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.StatusStrip1.Size = new System.Drawing.Size(800, 22);
            this.StatusStrip1.TabIndex = 1;
            this.StatusStrip1.Text = "statusStrip1";
            // 
            // SplitContainer1
            // 
            this.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainer1.Location = new System.Drawing.Point(0, 24);
            this.SplitContainer1.Margin = new System.Windows.Forms.Padding(2);
            this.SplitContainer1.Name = "SplitContainer1";
            // 
            // SplitContainer1.Panel1
            // 
            this.SplitContainer1.Panel1.Controls.Add(this.TreeView1);
            // 
            // SplitContainer1.Panel2
            // 
            this.SplitContainer1.Panel2.Controls.Add(this.ListView1);
            this.SplitContainer1.Size = new System.Drawing.Size(800, 404);
            this.SplitContainer1.SplitterDistance = 264;
            this.SplitContainer1.SplitterWidth = 3;
            this.SplitContainer1.TabIndex = 2;
            // 
            // TreeView1
            // 
            this.TreeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TreeView1.Location = new System.Drawing.Point(0, 0);
            this.TreeView1.Margin = new System.Windows.Forms.Padding(2);
            this.TreeView1.Name = "TreeView1";
            this.TreeView1.Size = new System.Drawing.Size(264, 404);
            this.TreeView1.TabIndex = 0;
            this.TreeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView1_AfterSelect);
            // 
            // ListView1
            // 
            this.ListView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListView1.HideSelection = false;
            this.ListView1.Location = new System.Drawing.Point(0, 0);
            this.ListView1.Margin = new System.Windows.Forms.Padding(2);
            this.ListView1.Name = "ListView1";
            this.ListView1.Size = new System.Drawing.Size(533, 404);
            this.ListView1.TabIndex = 0;
            this.ListView1.UseCompatibleStateImageBehavior = false;
            this.ListView1.View = System.Windows.Forms.View.Details;
            this.ListView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ListView1_ColumnClick);
            // 
            // Menu_Contextuel
            // 
            this.Menu_Contextuel.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.Menu_Contextuel.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_Contextuel_Ajouter,
            this.Menu_Contextuel_Modifier,
            this.Menu_Contextuel_Supprimer});
            this.Menu_Contextuel.Name = "Menu_Contextuel";
            this.Menu_Contextuel.Size = new System.Drawing.Size(130, 70);
            // 
            // Menu_Contextuel_Ajouter
            // 
            this.Menu_Contextuel_Ajouter.Name = "Menu_Contextuel_Ajouter";
            this.Menu_Contextuel_Ajouter.Size = new System.Drawing.Size(129, 22);
            this.Menu_Contextuel_Ajouter.Text = "Ajouter";
            this.Menu_Contextuel_Ajouter.Click += new System.EventHandler(this.Menu_Contextuel_Ajouter_Click);
            // 
            // Menu_Contextuel_Modifier
            // 
            this.Menu_Contextuel_Modifier.Name = "Menu_Contextuel_Modifier";
            this.Menu_Contextuel_Modifier.Size = new System.Drawing.Size(129, 22);
            this.Menu_Contextuel_Modifier.Text = "Modifier";
            // 
            // Menu_Contextuel_Supprimer
            // 
            this.Menu_Contextuel_Supprimer.Name = "Menu_Contextuel_Supprimer";
            this.Menu_Contextuel_Supprimer.Size = new System.Drawing.Size(129, 22);
            this.Menu_Contextuel_Supprimer.Text = "Supprimer";
            this.Menu_Contextuel_Supprimer.Click += new System.EventHandler(this.Menu_Contextuel_Supprimer_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.SplitContainer1);
            this.Controls.Add(this.StatusStrip1);
            this.Controls.Add(this.MenuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.MenuStrip1;
            this.Name = "FormMain";
            this.Text = "Hector";
            this.MenuStrip1.ResumeLayout(false);
            this.MenuStrip1.PerformLayout();
            this.SplitContainer1.Panel1.ResumeLayout(false);
            this.SplitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer1)).EndInit();
            this.SplitContainer1.ResumeLayout(false);
            this.Menu_Contextuel.ResumeLayout(false);
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
        private System.Windows.Forms.ContextMenuStrip Menu_Contextuel;
        private System.Windows.Forms.ToolStripMenuItem Menu_Contextuel_Ajouter;
        private System.Windows.Forms.ToolStripMenuItem Menu_Contextuel_Modifier;
        private System.Windows.Forms.ToolStripMenuItem Menu_Contextuel_Supprimer;
    }
}

