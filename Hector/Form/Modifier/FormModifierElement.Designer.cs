
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
            this.Modifier_Element_Bouton.Location = new System.Drawing.Point(122, 70);
            this.Modifier_Element_Bouton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Modifier_Element_Bouton.Name = "Modifier_Element_Bouton";
            this.Modifier_Element_Bouton.Size = new System.Drawing.Size(150, 30);
            this.Modifier_Element_Bouton.TabIndex = 0;
            this.Modifier_Element_Bouton.Text = "Modifier ...";
            this.Modifier_Element_Bouton.UseVisualStyleBackColor = true;
            this.Modifier_Element_Bouton.Click += new System.EventHandler(this.Modifier_Element_Bouton_Click);
            // 
            // Nom_Element_TextBox
            // 
            this.Nom_Element_TextBox.Location = new System.Drawing.Point(66, 35);
            this.Nom_Element_TextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Nom_Element_TextBox.Name = "Nom_Element_TextBox";
            this.Nom_Element_TextBox.Size = new System.Drawing.Size(246, 20);
            this.Nom_Element_TextBox.TabIndex = 1;
            // 
            // Nom_Element_Label
            // 
            this.Nom_Element_Label.AutoSize = true;
            this.Nom_Element_Label.Location = new System.Drawing.Point(63, 20);
            this.Nom_Element_Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Nom_Element_Label.Name = "Nom_Element_Label";
            this.Nom_Element_Label.Size = new System.Drawing.Size(56, 13);
            this.Nom_Element_Label.TabIndex = 2;
            this.Nom_Element_Label.Text = "Nom de ...";
            // 
            // FormModifierElement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 111);
            this.Controls.Add(this.Nom_Element_Label);
            this.Controls.Add(this.Nom_Element_TextBox);
            this.Controls.Add(this.Modifier_Element_Bouton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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