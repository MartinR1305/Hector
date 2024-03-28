
namespace Hector
{
    partial class FormAjouterElement
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
            this.Ajouter_Element = new System.Windows.Forms.Button();
            this.Element_TextBox = new System.Windows.Forms.TextBox();
            this.Element_Label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Ajouter_Element
            // 
            this.Ajouter_Element.Location = new System.Drawing.Point(150, 70);
            this.Ajouter_Element.Margin = new System.Windows.Forms.Padding(2);
            this.Ajouter_Element.Name = "Ajouter_Element";
            this.Ajouter_Element.Size = new System.Drawing.Size(100, 30);
            this.Ajouter_Element.TabIndex = 15;
            this.Ajouter_Element.Text = "Ajouter";
            this.Ajouter_Element.UseVisualStyleBackColor = true;
            this.Ajouter_Element.Click += new System.EventHandler(this.Ajouter_Element_Click);
            // 
            // Element_TextBox
            // 
            this.Element_TextBox.Location = new System.Drawing.Point(66, 35);
            this.Element_TextBox.Margin = new System.Windows.Forms.Padding(2);
            this.Element_TextBox.Name = "Element_TextBox";
            this.Element_TextBox.Size = new System.Drawing.Size(246, 20);
            this.Element_TextBox.TabIndex = 17;
            // 
            // Element_Label
            // 
            this.Element_Label.AutoSize = true;
            this.Element_Label.Location = new System.Drawing.Point(63, 20);
            this.Element_Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Element_Label.Name = "Element_Label";
            this.Element_Label.Size = new System.Drawing.Size(35, 13);
            this.Element_Label.TabIndex = 16;
            this.Element_Label.Text = "Nom :";
            // 
            // FormAjouterElement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 111);
            this.Controls.Add(this.Element_TextBox);
            this.Controls.Add(this.Element_Label);
            this.Controls.Add(this.Ajouter_Element);
            this.Name = "FormAjouterElement";
            this.Text = "FormAjouterElement";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Ajouter_Element;
        private System.Windows.Forms.TextBox Element_TextBox;
        private System.Windows.Forms.Label Element_Label;
    }
}