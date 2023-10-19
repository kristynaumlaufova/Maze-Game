namespace hra
{
    partial class okno
    {
        /// <summary>
        /// Vyžaduje se proměnná návrháře.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Uvolněte všechny používané prostředky.
        /// </summary>
        /// <param name="disposing">hodnota true, když by se měl spravovaný prostředek odstranit; jinak false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kód generovaný Návrhářem Windows Form

        /// <summary>
        /// Metoda vyžadovaná pro podporu Návrháře - neupravovat
        /// obsah této metody v editoru kódu.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.casovac = new System.Windows.Forms.Timer(this.components);
            this.tlacitkoNovaHra = new System.Windows.Forms.Button();
            this.tlacitkoObtiznostL = new System.Windows.Forms.Button();
            this.tlacitkoObtiznostS = new System.Windows.Forms.Button();
            this.tlacitkoObtiznostT = new System.Windows.Forms.Button();
            this.tlacitkoPokracovat = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // casovac
            // 
            this.casovac.Interval = 250;
            this.casovac.Tick += new System.EventHandler(this.casovac_Tick_1);
            // 
            // tlacitkoNovaHra
            // 
            this.tlacitkoNovaHra.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlacitkoNovaHra.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tlacitkoNovaHra.Location = new System.Drawing.Point(179, 251);
            this.tlacitkoNovaHra.Name = "tlacitkoNovaHra";
            this.tlacitkoNovaHra.Size = new System.Drawing.Size(434, 140);
            this.tlacitkoNovaHra.TabIndex = 0;
            this.tlacitkoNovaHra.Text = "Nová hra";
            this.tlacitkoNovaHra.UseVisualStyleBackColor = true;
            this.tlacitkoNovaHra.Click += new System.EventHandler(this.tlacitkoNovaHra_Click);
            // 
            // tlacitkoObtiznostL
            // 
            this.tlacitkoObtiznostL.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlacitkoObtiznostL.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tlacitkoObtiznostL.Location = new System.Drawing.Point(179, 196);
            this.tlacitkoObtiznostL.Name = "tlacitkoObtiznostL";
            this.tlacitkoObtiznostL.Size = new System.Drawing.Size(434, 82);
            this.tlacitkoObtiznostL.TabIndex = 1;
            this.tlacitkoObtiznostL.Text = "Lehká";
            this.tlacitkoObtiznostL.UseVisualStyleBackColor = true;
            this.tlacitkoObtiznostL.Visible = false;
            this.tlacitkoObtiznostL.Click += new System.EventHandler(this.tlacitkaObtiznost_Click);
            // 
            // tlacitkoObtiznostS
            // 
            this.tlacitkoObtiznostS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlacitkoObtiznostS.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tlacitkoObtiznostS.Location = new System.Drawing.Point(179, 295);
            this.tlacitkoObtiznostS.Name = "tlacitkoObtiznostS";
            this.tlacitkoObtiznostS.Size = new System.Drawing.Size(434, 82);
            this.tlacitkoObtiznostS.TabIndex = 2;
            this.tlacitkoObtiznostS.Text = "Střední";
            this.tlacitkoObtiznostS.UseVisualStyleBackColor = true;
            this.tlacitkoObtiznostS.Visible = false;
            this.tlacitkoObtiznostS.Click += new System.EventHandler(this.tlacitkaObtiznost_Click);
            // 
            // tlacitkoObtiznostT
            // 
            this.tlacitkoObtiznostT.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlacitkoObtiznostT.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tlacitkoObtiznostT.Location = new System.Drawing.Point(179, 397);
            this.tlacitkoObtiznostT.Name = "tlacitkoObtiznostT";
            this.tlacitkoObtiznostT.Size = new System.Drawing.Size(434, 82);
            this.tlacitkoObtiznostT.TabIndex = 3;
            this.tlacitkoObtiznostT.Text = "Těžká";
            this.tlacitkoObtiznostT.UseVisualStyleBackColor = true;
            this.tlacitkoObtiznostT.Visible = false;
            this.tlacitkoObtiznostT.Click += new System.EventHandler(this.tlacitkaObtiznost_Click);
            // 
            // tlacitkoPokracovat
            // 
            this.tlacitkoPokracovat.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlacitkoPokracovat.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tlacitkoPokracovat.Location = new System.Drawing.Point(399, 520);
            this.tlacitkoPokracovat.Name = "tlacitkoPokracovat";
            this.tlacitkoPokracovat.Size = new System.Drawing.Size(340, 82);
            this.tlacitkoPokracovat.TabIndex = 4;
            this.tlacitkoPokracovat.Text = "Pokračovat";
            this.tlacitkoPokracovat.UseVisualStyleBackColor = true;
            this.tlacitkoPokracovat.Visible = false;
            this.tlacitkoPokracovat.Click += new System.EventHandler(this.tlacitkoPokracovat_Click);
            // 
            // okno
            // 
            this.ClientSize = new System.Drawing.Size(780, 660);
            this.Controls.Add(this.tlacitkoPokracovat);
            this.Controls.Add(this.tlacitkoObtiznostT);
            this.Controls.Add(this.tlacitkoObtiznostS);
            this.Controls.Add(this.tlacitkoObtiznostL);
            this.Controls.Add(this.tlacitkoNovaHra);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "okno";
            this.Text = "hra";
            this.Load += new System.EventHandler(this.okno_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.okno_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.okno_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button tlacitkoNovaHra;
        private System.Windows.Forms.Timer casovac;
        private System.Windows.Forms.Button tlacitkoObtiznostL;
        private System.Windows.Forms.Button tlacitkoObtiznostS;
        private System.Windows.Forms.Button tlacitkoObtiznostT;
        private System.Windows.Forms.Button tlacitkoPokracovat;
    }
}

