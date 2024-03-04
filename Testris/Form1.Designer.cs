namespace Testris
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Main_panel = new System.Windows.Forms.Panel();
            this.Score_label = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.start_btn = new System.Windows.Forms.Button();
            this.reset_btn = new System.Windows.Forms.Button();
            this.setting_btn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.soft_drop_timer = new System.Windows.Forms.Timer(this.components);
            this.dropped_jud_timer = new System.Windows.Forms.Timer(this.components);
            this.st_label = new System.Windows.Forms.Label();
            this.st_wait_timer = new System.Windows.Forms.Timer(this.components);
            this.geme_ov = new System.Windows.Forms.Timer(this.components);
            this.Main_panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // Main_panel
            // 
            this.Main_panel.Controls.Add(this.st_label);
            this.Main_panel.Controls.Add(this.Score_label);
            this.Main_panel.Controls.Add(this.label5);
            this.Main_panel.Controls.Add(this.start_btn);
            this.Main_panel.Controls.Add(this.reset_btn);
            this.Main_panel.Controls.Add(this.setting_btn);
            this.Main_panel.Controls.Add(this.label4);
            this.Main_panel.Controls.Add(this.label3);
            this.Main_panel.Controls.Add(this.label2);
            this.Main_panel.Controls.Add(this.label1);
            this.Main_panel.Controls.Add(this.panel3);
            this.Main_panel.Controls.Add(this.panel4);
            this.Main_panel.Controls.Add(this.panel5);
            this.Main_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Main_panel.Location = new System.Drawing.Point(0, 0);
            this.Main_panel.Name = "Main_panel";
            this.Main_panel.Size = new System.Drawing.Size(684, 761);
            this.Main_panel.TabIndex = 0;
            // 
            // Score_label
            // 
            this.Score_label.AutoSize = true;
            this.Score_label.Font = new System.Drawing.Font("MV Boli", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Score_label.ForeColor = System.Drawing.Color.MediumTurquoise;
            this.Score_label.Location = new System.Drawing.Point(428, 47);
            this.Score_label.Name = "Score_label";
            this.Score_label.Size = new System.Drawing.Size(27, 28);
            this.Score_label.TabIndex = 32;
            this.Score_label.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("MV Boli", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.LimeGreen;
            this.label5.Location = new System.Drawing.Point(428, 396);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 28);
            this.label5.TabIndex = 31;
            this.label5.Text = "HOLD";
            // 
            // start_btn
            // 
            this.start_btn.BackColor = System.Drawing.Color.Black;
            this.start_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.start_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.start_btn.Font = new System.Drawing.Font("MV Boli", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.start_btn.ForeColor = System.Drawing.Color.LimeGreen;
            this.start_btn.Location = new System.Drawing.Point(96, 275);
            this.start_btn.Name = "start_btn";
            this.start_btn.Size = new System.Drawing.Size(288, 110);
            this.start_btn.TabIndex = 29;
            this.start_btn.Text = "Start";
            this.start_btn.UseVisualStyleBackColor = false;
            this.start_btn.MouseClick += new System.Windows.Forms.MouseEventHandler(this.start_btn_MouseClick);
            this.start_btn.MouseEnter += new System.EventHandler(this.start_btn_MouseEnter);
            this.start_btn.MouseLeave += new System.EventHandler(this.start_btn_MouseLeave);
            // 
            // reset_btn
            // 
            this.reset_btn.BackColor = System.Drawing.Color.Silver;
            this.reset_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.reset_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.reset_btn.Image = global::Testris.Properties.Resources._495;
            this.reset_btn.Location = new System.Drawing.Point(433, 551);
            this.reset_btn.Name = "reset_btn";
            this.reset_btn.Size = new System.Drawing.Size(70, 60);
            this.reset_btn.TabIndex = 28;
            this.reset_btn.UseVisualStyleBackColor = false;
            this.reset_btn.MouseClick += new System.Windows.Forms.MouseEventHandler(this.reset_btn_MouseClick);
            // 
            // setting_btn
            // 
            this.setting_btn.BackColor = System.Drawing.Color.Silver;
            this.setting_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.setting_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.setting_btn.Image = global::Testris.Properties.Resources._3737;
            this.setting_btn.Location = new System.Drawing.Point(433, 630);
            this.setting_btn.Name = "setting_btn";
            this.setting_btn.Size = new System.Drawing.Size(70, 60);
            this.setting_btn.TabIndex = 27;
            this.setting_btn.UseVisualStyleBackColor = false;
            this.setting_btn.MouseClick += new System.Windows.Forms.MouseEventHandler(this.setting_btn_MouseClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("MV Boli", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.LimeGreen;
            this.label4.Location = new System.Drawing.Point(426, 275);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 28);
            this.label4.TabIndex = 26;
            this.label4.Text = "third";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MV Boli", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.LimeGreen;
            this.label3.Location = new System.Drawing.Point(428, 177);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 28);
            this.label3.TabIndex = 25;
            this.label3.Text = "second";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MV Boli", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.LimeGreen;
            this.label2.Location = new System.Drawing.Point(428, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 28);
            this.label2.TabIndex = 24;
            this.label2.Text = "next";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MV Boli", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.MediumSpringGreen;
            this.label1.Location = new System.Drawing.Point(421, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 28);
            this.label1.TabIndex = 23;
            this.label1.Text = "SCORE";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Silver;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Location = new System.Drawing.Point(60, 660);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(360, 30);
            this.panel3.TabIndex = 21;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Silver;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Location = new System.Drawing.Point(390, 60);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(30, 630);
            this.panel4.TabIndex = 22;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Silver;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel5.Location = new System.Drawing.Point(60, 60);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(30, 630);
            this.panel5.TabIndex = 20;
            // 
            // soft_drop_timer
            // 
            this.soft_drop_timer.Interval = 500;
            this.soft_drop_timer.Tick += new System.EventHandler(this.soft_drop_timer_Tick);
            // 
            // dropped_jud_timer
            // 
            this.dropped_jud_timer.Interval = 150;
            this.dropped_jud_timer.Tick += new System.EventHandler(this.dropped_jud_timer_Tick);
            // 
            // st_label
            // 
            this.st_label.AutoSize = true;
            this.st_label.Font = new System.Drawing.Font("MV Boli", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.st_label.ForeColor = System.Drawing.Color.LimeGreen;
            this.st_label.Location = new System.Drawing.Point(219, 293);
            this.st_label.Name = "st_label";
            this.st_label.Size = new System.Drawing.Size(47, 49);
            this.st_label.TabIndex = 33;
            this.st_label.Text = "3";
            this.st_label.Visible = false;
            // 
            // st_wait_timer
            // 
            this.st_wait_timer.Interval = 1000;
            this.st_wait_timer.Tick += new System.EventHandler(this.st_wait_timer_Tick);
            // 
            // geme_ov
            // 
            this.geme_ov.Interval = 1200;
            this.geme_ov.Tick += new System.EventHandler(this.geme_ov_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(684, 761);
            this.Controls.Add(this.Main_panel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.Main_panel.ResumeLayout(false);
            this.Main_panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Main_panel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button reset_btn;
        private System.Windows.Forms.Button setting_btn;
        private System.Windows.Forms.Button start_btn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Timer soft_drop_timer;
        private System.Windows.Forms.Timer dropped_jud_timer;
        private System.Windows.Forms.Label Score_label;
        private System.Windows.Forms.Label st_label;
        private System.Windows.Forms.Timer st_wait_timer;
        private System.Windows.Forms.Timer geme_ov;
    }
}

