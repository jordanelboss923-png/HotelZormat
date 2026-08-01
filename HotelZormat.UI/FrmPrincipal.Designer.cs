namespace HotelZormat.UI
{
    partial class FrmPrincipal
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
            this.btnHuesped = new System.Windows.Forms.Button();
            this.btnHabitacion = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnHuesped
            // 
            this.btnHuesped.Location = new System.Drawing.Point(341, 147);
            this.btnHuesped.Name = "btnHuesped";
            this.btnHuesped.Size = new System.Drawing.Size(75, 23);
            this.btnHuesped.TabIndex = 0;
            this.btnHuesped.Text = "Huespedes";
            this.btnHuesped.UseVisualStyleBackColor = true;
            this.btnHuesped.Click += new System.EventHandler(this.btnHuesped_Click);
            // 
            // btnHabitacion
            // 
            this.btnHabitacion.Location = new System.Drawing.Point(341, 195);
            this.btnHabitacion.Name = "btnHabitacion";
            this.btnHabitacion.Size = new System.Drawing.Size(75, 23);
            this.btnHabitacion.TabIndex = 1;
            this.btnHabitacion.Text = "Habitaciones";
            this.btnHabitacion.UseVisualStyleBackColor = true;
            this.btnHabitacion.Click += new System.EventHandler(this.btnHabitacion_Click);
            // 
            // FrmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnHabitacion);
            this.Controls.Add(this.btnHuesped);
            this.Name = "FrmPrincipal";
            this.Text = " \"Hotel Zormat — Sistema de Gestión\"";
            this.Load += new System.EventHandler(this.FrmPrincipal_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnHuesped;
        private System.Windows.Forms.Button btnHabitacion;
    }
}