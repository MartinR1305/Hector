﻿
namespace Hector
{
    partial class FormModifierArticle
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
            this.Modifier_Bouton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Description_Label
            // 
            this.Description_Label.AutoSize = true;
            this.Description_Label.Location = new System.Drawing.Point(30, 41);
            this.Description_Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Description_Label.Name = "Description_Label";
            this.Description_Label.Size = new System.Drawing.Size(66, 13);
            this.Description_Label.TabIndex = 18;
            this.Description_Label.Text = "Description :";
            // 
            // Description_TextBox
            // 
            this.Description_TextBox.Location = new System.Drawing.Point(103, 38);
            this.Description_TextBox.Margin = new System.Windows.Forms.Padding(5, 10, 5, 10);
            this.Description_TextBox.Multiline = true;
            this.Description_TextBox.Name = "Description_TextBox";
            this.Description_TextBox.Size = new System.Drawing.Size(246, 41);
            this.Description_TextBox.TabIndex = 19;
            // 
            // Marque_Label
            // 
            this.Marque_Label.AutoSize = true;
            this.Marque_Label.Location = new System.Drawing.Point(44, 102);
            this.Marque_Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Marque_Label.Name = "Marque_Label";
            this.Marque_Label.Size = new System.Drawing.Size(52, 13);
            this.Marque_Label.TabIndex = 20;
            this.Marque_Label.Text = "Marque : ";
            // 
            // Marque_ComboBox
            // 
            this.Marque_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Marque_ComboBox.FormattingEnabled = true;
            this.Marque_ComboBox.Location = new System.Drawing.Point(103, 99);
            this.Marque_ComboBox.Margin = new System.Windows.Forms.Padding(5, 10, 5, 10);
            this.Marque_ComboBox.Name = "Marque_ComboBox";
            this.Marque_ComboBox.Size = new System.Drawing.Size(246, 21);
            this.Marque_ComboBox.TabIndex = 21;
            // 
            // Famille_Label
            // 
            this.Famille_Label.AutoSize = true;
            this.Famille_Label.Location = new System.Drawing.Point(48, 143);
            this.Famille_Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Famille_Label.Name = "Famille_Label";
            this.Famille_Label.Size = new System.Drawing.Size(48, 13);
            this.Famille_Label.TabIndex = 22;
            this.Famille_Label.Text = "Famille : ";
            // 
            // Famille_ComboBox
            // 
            this.Famille_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Famille_ComboBox.FormattingEnabled = true;
            this.Famille_ComboBox.Location = new System.Drawing.Point(103, 140);
            this.Famille_ComboBox.Margin = new System.Windows.Forms.Padding(5, 10, 5, 10);
            this.Famille_ComboBox.Name = "Famille_ComboBox";
            this.Famille_ComboBox.Size = new System.Drawing.Size(246, 21);
            this.Famille_ComboBox.TabIndex = 23;
            this.Famille_ComboBox.SelectedIndexChanged += new System.EventHandler(this.Famille_ComboBox_SelectedIndexChanged);
            // 
            // Sous_Famille_ComboBox
            // 
            this.Sous_Famille_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Sous_Famille_ComboBox.FormattingEnabled = true;
            this.Sous_Famille_ComboBox.Location = new System.Drawing.Point(103, 181);
            this.Sous_Famille_ComboBox.Margin = new System.Windows.Forms.Padding(5, 10, 5, 10);
            this.Sous_Famille_ComboBox.Name = "Sous_Famille_ComboBox";
            this.Sous_Famille_ComboBox.Size = new System.Drawing.Size(246, 21);
            this.Sous_Famille_ComboBox.TabIndex = 24;
            // 
            // Sous_Famille_Label
            // 
            this.Sous_Famille_Label.AutoSize = true;
            this.Sous_Famille_Label.Location = new System.Drawing.Point(24, 184);
            this.Sous_Famille_Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Sous_Famille_Label.Name = "Sous_Famille_Label";
            this.Sous_Famille_Label.Size = new System.Drawing.Size(72, 13);
            this.Sous_Famille_Label.TabIndex = 25;
            this.Sous_Famille_Label.Text = "Sous-Famille :";
            // 
            // PrixHT_Label
            // 
            this.PrixHT_Label.AutoSize = true;
            this.PrixHT_Label.Location = new System.Drawing.Point(48, 225);
            this.PrixHT_Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.PrixHT_Label.Name = "PrixHT_Label";
            this.PrixHT_Label.Size = new System.Drawing.Size(48, 13);
            this.PrixHT_Label.TabIndex = 26;
            this.PrixHT_Label.Text = "Prix HT :";
            // 
            // PrixHT_TextBox
            // 
            this.PrixHT_TextBox.Location = new System.Drawing.Point(103, 222);
            this.PrixHT_TextBox.Margin = new System.Windows.Forms.Padding(5, 10, 5, 10);
            this.PrixHT_TextBox.Name = "PrixHT_TextBox";
            this.PrixHT_TextBox.Size = new System.Drawing.Size(246, 20);
            this.PrixHT_TextBox.TabIndex = 27;
            // 
            // Quantite_Label
            // 
            this.Quantite_Label.AutoSize = true;
            this.Quantite_Label.Location = new System.Drawing.Point(43, 265);
            this.Quantite_Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Quantite_Label.Name = "Quantite_Label";
            this.Quantite_Label.Size = new System.Drawing.Size(53, 13);
            this.Quantite_Label.TabIndex = 28;
            this.Quantite_Label.Text = "Quantité :";
            // 
            // Quantite_TextBox
            // 
            this.Quantite_TextBox.Location = new System.Drawing.Point(103, 262);
            this.Quantite_TextBox.Margin = new System.Windows.Forms.Padding(5, 10, 5, 10);
            this.Quantite_TextBox.Name = "Quantite_TextBox";
            this.Quantite_TextBox.Size = new System.Drawing.Size(246, 20);
            this.Quantite_TextBox.TabIndex = 29;
            // 
            // Modifier_Bouton
            // 
            this.Modifier_Bouton.Location = new System.Drawing.Point(150, 310);
            this.Modifier_Bouton.Margin = new System.Windows.Forms.Padding(2);
            this.Modifier_Bouton.Name = "Modifier_Bouton";
            this.Modifier_Bouton.Size = new System.Drawing.Size(100, 30);
            this.Modifier_Bouton.TabIndex = 30;
            this.Modifier_Bouton.Text = "Modifier Article";
            this.Modifier_Bouton.UseVisualStyleBackColor = true;
            this.Modifier_Bouton.Click += new System.EventHandler(this.Modifier_Bouton_Click);
            // 
            // FormModifierArticle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 361);
            this.Controls.Add(this.Modifier_Bouton);
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "FormModifierArticle";
            this.Text = "Modifier un article";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

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
        private System.Windows.Forms.Button Modifier_Bouton;
    }
}