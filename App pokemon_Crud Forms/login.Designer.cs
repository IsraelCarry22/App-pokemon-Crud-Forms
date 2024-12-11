namespace App_pokemon_Crud_Forms
{
    partial class login
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            Continuar_Btm = new Button();
            Regsitro_Btm = new Button();
            txtUsuario = new TextBox();
            txtPassword = new TextBox();
            label2 = new Label();
            label3 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16F);
            label1.Location = new Point(115, 29);
            label1.Name = "label1";
            label1.Size = new Size(175, 30);
            label1.TabIndex = 0;
            label1.Text = "Inicio de Seccion";
            // 
            // Continuar_Btm
            // 
            Continuar_Btm.Location = new Point(219, 275);
            Continuar_Btm.Name = "Continuar_Btm";
            Continuar_Btm.Size = new Size(120, 36);
            Continuar_Btm.TabIndex = 1;
            Continuar_Btm.Text = "Continuar";
            Continuar_Btm.UseVisualStyleBackColor = true;
            Continuar_Btm.Click += Continuar_Btm_Click;
            // 
            // Regsitro_Btm
            // 
            Regsitro_Btm.Location = new Point(67, 275);
            Regsitro_Btm.Name = "Regsitro_Btm";
            Regsitro_Btm.Size = new Size(120, 36);
            Regsitro_Btm.TabIndex = 2;
            Regsitro_Btm.Text = "Registro";
            Regsitro_Btm.UseVisualStyleBackColor = true;
            Regsitro_Btm.Click += Regsitro_Btm_Click;
            // 
            // txtUsuario
            // 
            txtUsuario.Font = new Font("Segoe UI", 12F);
            txtUsuario.Location = new Point(181, 134);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(158, 29);
            txtUsuario.TabIndex = 3;
            // 
            // txtPassword
            // 
            txtPassword.Font = new Font("Segoe UI", 12F);
            txtPassword.Location = new Point(181, 181);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(158, 29);
            txtPassword.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 16F);
            label2.Location = new Point(67, 130);
            label2.Name = "label2";
            label2.Size = new Size(91, 30);
            label2.TabIndex = 5;
            label2.Text = "Usuario:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 16F);
            label3.Location = new Point(67, 177);
            label3.Name = "label3";
            label3.Size = new Size(108, 30);
            label3.TabIndex = 6;
            label3.Text = "Password:";
            // 
            // login
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(407, 412);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(txtPassword);
            Controls.Add(txtUsuario);
            Controls.Add(Regsitro_Btm);
            Controls.Add(Continuar_Btm);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "login";
            Text = "login";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button Continuar_Btm;
        private Button Regsitro_Btm;
        private TextBox txtUsuario;
        private TextBox txtPassword;
        private Label label2;
        private Label label3;
    }
}
