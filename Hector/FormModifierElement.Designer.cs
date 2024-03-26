
namespace Hector
{
    partial class FormModifierElement
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
            this.Modifier_Element_Bouton = new System.Windows.Forms.Button();
            this.Nom_Element_TextBox = new System.Windows.Forms.TextBox();
            this.Nom_Element_Label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Modifier_Element_Bouton
            // 
            this.Modifier_Element_Bouton.Location = new System.Drawing.Point(78, 57);
            this.Modifier_Element_Bouton.Name = "Modifier_Element_Bouton";
            this.Modifier_Element_Bouton.Size = new System.Drawing.Size(191, 30);
            this.Modifier_Element_Bouton.TabIndex = 0;
            this.Modifier_Element_Bouton.Text = "Modifier ...";
            this.Modifier_Element_Bouton.UseVisualStyleBackColor = true;
            this.Modifier_Element_Bouton.Click += new System.EventHandler(this.Modifier_Element_Bouton_Click);
            // 
            // Nom_Element_TextBox
            // 
            this.Nom_Element_TextBox.Location = new System.Drawing.Point(29, 29);
            this.Nom_Element_TextBox.Name = "Nom_Element_TextBox";
            this.Nom_Element_TextBox.Size = new System.Drawing.Size(304, 22);
            this.Nom_Element_TextBox.TabIndex = 1;
            // 
            // Nom_Element_Label
            // 
            this.Nom_Element_Label.AutoSize = true;
            this.Nom_Element_Label.Location = new System.Drawing.Point(141, 9);
            this.Nom_Element_Label.Name = "Nom_Element_Label";
            this.Nom_Element_Label.Size = new System.Drawing.Size(73, 17);
            this.Nom_Element_Label.TabIndex = 2;
            this.Nom_Element_Label.Text = "Nom de ...";
            // 
            // FormModifierElement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 103);
            this.Controls.Add(this.Nom_Element_Label);
            this.Controls.Add(this.Nom_Element_TextBox);
            this.Controls.Add(this.Modifier_Element_Bouton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormModifierElement";
            this.Text = "FormModifierElement";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Modifier_Element_Bouton;
        private System.Windows.Forms.TextBox Nom_Element_TextBox;
        private System.Windows.Forms.Label Nom_Element_Label;
    }
}