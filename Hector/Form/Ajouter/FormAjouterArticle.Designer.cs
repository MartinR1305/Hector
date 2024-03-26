
namespace Hector
{
    partial class FormAjouterArticle
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
            this.Reference_TextBox = new System.Windows.Forms.TextBox();
            this.Reference_Label = new System.Windows.Forms.Label();
            this.Description_Label = new System.Windows.Forms.Label();
            this.Description_TextBox = new System.Windows.Forms.TextBox();
            this.Marque_Label = new System.Windows.Forms.Label();
            this.Marque_ComboBox = new System.Windows.Forms.ComboBox();
            this.Famille_Label = new System.Windows.Forms.Label();
            this.Famille_ComboBox = new System.Windows.Forms.ComboBox();
            this.Sous_Famille_ComboBox = new System.Windows.Forms.ComboBox();
            this.Sous_Famille_Label = new System.Windows.Forms.Label();
            this.PrixHT_Label = new System.Windows.Forms.Label();
            this.PrixHT_TextBox = new System.Windows.Forms.TextBox();
            this.Quantite_Label = new System.Windows.Forms.Label();
            this.Quantite_TextBox = new System.Windows.Forms.TextBox();
            this.Ajouter_Bouton = new System.Windows.Forms.Button();
            this.F_Label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Reference_TextBox
            // 
            this.Reference_TextBox.Location = new System.Drawing.Point(267, 23);
            this.Reference_TextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Reference_TextBox.Name = "Reference_TextBox";
            this.Reference_TextBox.Size = new System.Drawing.Size(327, 22);
            this.Reference_TextBox.TabIndex = 0;
            // 
            // Reference_Label
            // 
            this.Reference_Label.AutoSize = true;
            this.Reference_Label.Location = new System.Drawing.Point(157, 23);
            this.Reference_Label.Name = "Reference_Label";
            this.Reference_Label.Size = new System.Drawing.Size(86, 17);
            this.Reference_Label.TabIndex = 1;
            this.Reference_Label.Text = "Référence : ";
            // 
            // Description_Label
            // 
            this.Description_Label.AutoSize = true;
            this.Description_Label.Location = new System.Drawing.Point(157, 62);
            this.Description_Label.Name = "Description_Label";
            this.Description_Label.Size = new System.Drawing.Size(87, 17);
            this.Description_Label.TabIndex = 2;
            this.Description_Label.Text = "Description :";
            // 
            // Description_TextBox
            // 
            this.Description_TextBox.Location = new System.Drawing.Point(267, 62);
            this.Description_TextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Description_TextBox.Multiline = true;
            this.Description_TextBox.Name = "Description_TextBox";
            this.Description_TextBox.Size = new System.Drawing.Size(327, 50);
            this.Description_TextBox.TabIndex = 3;
            // 
            // Marque_Label
            // 
            this.Marque_Label.AutoSize = true;
            this.Marque_Label.Location = new System.Drawing.Point(176, 135);
            this.Marque_Label.Name = "Marque_Label";
            this.Marque_Label.Size = new System.Drawing.Size(68, 17);
            this.Marque_Label.TabIndex = 4;
            this.Marque_Label.Text = "Marque : ";
            // 
            // Marque_ComboBox
            // 
            this.Marque_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Marque_ComboBox.FormattingEnabled = true;
            this.Marque_ComboBox.Location = new System.Drawing.Point(267, 135);
            this.Marque_ComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Marque_ComboBox.Name = "Marque_ComboBox";
            this.Marque_ComboBox.Size = new System.Drawing.Size(327, 24);
            this.Marque_ComboBox.TabIndex = 5;
            // 
            // Famille_Label
            // 
            this.Famille_Label.AutoSize = true;
            this.Famille_Label.Location = new System.Drawing.Point(180, 193);
            this.Famille_Label.Name = "Famille_Label";
            this.Famille_Label.Size = new System.Drawing.Size(64, 17);
            this.Famille_Label.TabIndex = 6;
            this.Famille_Label.Text = "Famille : ";
            // 
            // Famille_ComboBox
            // 
            this.Famille_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Famille_ComboBox.FormattingEnabled = true;
            this.Famille_ComboBox.Location = new System.Drawing.Point(267, 190);
            this.Famille_ComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Famille_ComboBox.Name = "Famille_ComboBox";
            this.Famille_ComboBox.Size = new System.Drawing.Size(327, 24);
            this.Famille_ComboBox.TabIndex = 7;
            this.Famille_ComboBox.SelectedIndexChanged += new System.EventHandler(this.Famille_ComboBox_SelectedIndexChanged);
            // 
            // Sous_Famille_ComboBox
            // 
            this.Sous_Famille_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Sous_Famille_ComboBox.Enabled = false;
            this.Sous_Famille_ComboBox.FormattingEnabled = true;
            this.Sous_Famille_ComboBox.Location = new System.Drawing.Point(267, 242);
            this.Sous_Famille_ComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Sous_Famille_ComboBox.Name = "Sous_Famille_ComboBox";
            this.Sous_Famille_ComboBox.Size = new System.Drawing.Size(327, 24);
            this.Sous_Famille_ComboBox.TabIndex = 8;
            // 
            // Sous_Famille_Label
            // 
            this.Sous_Famille_Label.AutoSize = true;
            this.Sous_Famille_Label.Location = new System.Drawing.Point(147, 245);
            this.Sous_Famille_Label.Name = "Sous_Famille_Label";
            this.Sous_Famille_Label.Size = new System.Drawing.Size(97, 17);
            this.Sous_Famille_Label.TabIndex = 9;
            this.Sous_Famille_Label.Text = "Sous-Famille :";
            // 
            // PrixHT_Label
            // 
            this.PrixHT_Label.AutoSize = true;
            this.PrixHT_Label.Location = new System.Drawing.Point(181, 295);
            this.PrixHT_Label.Name = "PrixHT_Label";
            this.PrixHT_Label.Size = new System.Drawing.Size(62, 17);
            this.PrixHT_Label.TabIndex = 10;
            this.PrixHT_Label.Text = "Prix HT :";
            // 
            // PrixHT_TextBox
            // 
            this.PrixHT_TextBox.Location = new System.Drawing.Point(267, 295);
            this.PrixHT_TextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PrixHT_TextBox.Name = "PrixHT_TextBox";
            this.PrixHT_TextBox.Size = new System.Drawing.Size(327, 22);
            this.PrixHT_TextBox.TabIndex = 11;
            // 
            // Quantite_Label
            // 
            this.Quantite_Label.AutoSize = true;
            this.Quantite_Label.Location = new System.Drawing.Point(173, 346);
            this.Quantite_Label.Name = "Quantite_Label";
            this.Quantite_Label.Size = new System.Drawing.Size(70, 17);
            this.Quantite_Label.TabIndex = 12;
            this.Quantite_Label.Text = "Quantité :";
            // 
            // Quantite_TextBox
            // 
            this.Quantite_TextBox.Location = new System.Drawing.Point(267, 346);
            this.Quantite_TextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Quantite_TextBox.Name = "Quantite_TextBox";
            this.Quantite_TextBox.Size = new System.Drawing.Size(327, 22);
            this.Quantite_TextBox.TabIndex = 13;
            // 
            // Ajouter_Bouton
            // 
            this.Ajouter_Bouton.Location = new System.Drawing.Point(365, 375);
            this.Ajouter_Bouton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Ajouter_Bouton.Name = "Ajouter_Bouton";
            this.Ajouter_Bouton.Size = new System.Drawing.Size(125, 33);
            this.Ajouter_Bouton.TabIndex = 14;
            this.Ajouter_Bouton.Text = "Ajouter Article";
            this.Ajouter_Bouton.UseVisualStyleBackColor = true;
            this.Ajouter_Bouton.Click += new System.EventHandler(this.Ajouter_Bouton_Click);
            // 
            // F_Label
            // 
            this.F_Label.AutoSize = true;
            this.F_Label.Location = new System.Drawing.Point(243, 27);
            this.F_Label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.F_Label.Name = "F_Label";
            this.F_Label.Size = new System.Drawing.Size(16, 17);
            this.F_Label.TabIndex = 15;
            this.F_Label.Text = "F";
            // 
            // FormAjouterArticle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.F_Label);
            this.Controls.Add(this.Ajouter_Bouton);
            this.Controls.Add(this.Quantite_TextBox);
            this.Controls.Add(this.Quantite_Label);
            this.Controls.Add(this.PrixHT_TextBox);
            this.Controls.Add(this.PrixHT_Label);
            this.Controls.Add(this.Sous_Famille_Label);
            this.Controls.Add(this.Sous_Famille_ComboBox);
            this.Controls.Add(this.Famille_ComboBox);
            this.Controls.Add(this.Famille_Label);
            this.Controls.Add(this.Marque_ComboBox);
            this.Controls.Add(this.Marque_Label);
            this.Controls.Add(this.Description_TextBox);
            this.Controls.Add(this.Description_Label);
            this.Controls.Add(this.Reference_Label);
            this.Controls.Add(this.Reference_TextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "FormAjouterArticle";
            this.Text = "Ajouter un article";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Reference_TextBox;
        private System.Windows.Forms.Label Reference_Label;
        private System.Windows.Forms.Label Description_Label;
        private System.Windows.Forms.TextBox Description_TextBox;
        private System.Windows.Forms.Label Marque_Label;
        private System.Windows.Forms.ComboBox Marque_ComboBox;
        private System.Windows.Forms.Label Famille_Label;
        private System.Windows.Forms.ComboBox Famille_ComboBox;
        private System.Windows.Forms.ComboBox Sous_Famille_ComboBox;
        private System.Windows.Forms.Label Sous_Famille_Label;
        private System.Windows.Forms.Label PrixHT_Label;
        private System.Windows.Forms.TextBox PrixHT_TextBox;
        private System.Windows.Forms.Label Quantite_Label;
        private System.Windows.Forms.TextBox Quantite_TextBox;
        private System.Windows.Forms.Button Ajouter_Bouton;
        private System.Windows.Forms.Label F_Label;
    }
}