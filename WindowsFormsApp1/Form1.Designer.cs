namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.lbl_Code = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_Ho = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_Dong = new System.Windows.Forms.TextBox();
            this.btn_Test = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.lbl_DateTime = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btn_VisitAll = new System.Windows.Forms.Button();
            this.btn_VisitCheck = new System.Windows.Forms.Button();
            this.txt_CarNo = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_Code
            // 
            this.lbl_Code.AutoSize = true;
            this.lbl_Code.Location = new System.Drawing.Point(15, 14);
            this.lbl_Code.Name = "lbl_Code";
            this.lbl_Code.Size = new System.Drawing.Size(77, 12);
            this.lbl_Code.TabIndex = 1;
            this.lbl_Code.Text = "아파트코드 : ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txt_Ho);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txt_Dong);
            this.groupBox1.Controls.Add(this.btn_Test);
            this.groupBox1.Location = new System.Drawing.Point(440, 29);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(348, 62);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " 차량입차 통보 ";
            this.groupBox1.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(171, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "호";
            // 
            // txt_Ho
            // 
            this.txt_Ho.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt_Ho.Location = new System.Drawing.Point(108, 24);
            this.txt_Ho.Name = "txt_Ho";
            this.txt_Ho.Size = new System.Drawing.Size(61, 26);
            this.txt_Ho.TabIndex = 7;
            this.txt_Ho.Text = "2002";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(79, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "동";
            // 
            // txt_Dong
            // 
            this.txt_Dong.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt_Dong.Location = new System.Drawing.Point(18, 24);
            this.txt_Dong.Name = "txt_Dong";
            this.txt_Dong.Size = new System.Drawing.Size(61, 26);
            this.txt_Dong.TabIndex = 3;
            this.txt_Dong.Text = "1001";
            // 
            // btn_Test
            // 
            this.btn_Test.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_Test.Location = new System.Drawing.Point(229, 20);
            this.btn_Test.Name = "btn_Test";
            this.btn_Test.Size = new System.Drawing.Size(109, 32);
            this.btn_Test.TabIndex = 1;
            this.btn_Test.Text = "세대통보 테스트";
            this.btn_Test.UseVisualStyleBackColor = true;
            this.btn_Test.Click += new System.EventHandler(this.btn_Test_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(12, 103);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(776, 340);
            this.listBox1.TabIndex = 10;
            // 
            // lbl_DateTime
            // 
            this.lbl_DateTime.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_DateTime.Location = new System.Drawing.Point(548, 9);
            this.lbl_DateTime.Name = "lbl_DateTime";
            this.lbl_DateTime.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbl_DateTime.Size = new System.Drawing.Size(240, 16);
            this.lbl_DateTime.TabIndex = 11;
            this.lbl_DateTime.Text = "lbl_DateTime";
            this.lbl_DateTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btn_VisitAll
            // 
            this.btn_VisitAll.Location = new System.Drawing.Point(14, 39);
            this.btn_VisitAll.Name = "btn_VisitAll";
            this.btn_VisitAll.Size = new System.Drawing.Size(116, 23);
            this.btn_VisitAll.TabIndex = 12;
            this.btn_VisitAll.Text = "방문예약 전체목록";
            this.btn_VisitAll.UseVisualStyleBackColor = true;
            this.btn_VisitAll.Click += new System.EventHandler(this.btn_VisitAll_Click);
            // 
            // btn_VisitCheck
            // 
            this.btn_VisitCheck.Location = new System.Drawing.Point(14, 66);
            this.btn_VisitCheck.Name = "btn_VisitCheck";
            this.btn_VisitCheck.Size = new System.Drawing.Size(116, 23);
            this.btn_VisitCheck.TabIndex = 13;
            this.btn_VisitCheck.Text = "방문차량 확인";
            this.btn_VisitCheck.UseVisualStyleBackColor = true;
            this.btn_VisitCheck.Click += new System.EventHandler(this.btn_VisitCheck_Click);
            // 
            // txt_CarNo
            // 
            this.txt_CarNo.Location = new System.Drawing.Point(136, 67);
            this.txt_CarNo.Name = "txt_CarNo";
            this.txt_CarNo.Size = new System.Drawing.Size(124, 21);
            this.txt_CarNo.TabIndex = 15;
            this.txt_CarNo.Text = "11가1111";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(136, 40);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(164, 21);
            this.dateTimePicker1.TabIndex = 16;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(800, 454);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.txt_CarNo);
            this.Controls.Add(this.btn_VisitCheck);
            this.Controls.Add(this.btn_VisitAll);
            this.Controls.Add(this.lbl_DateTime);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lbl_Code);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "아파트너 에이젼트";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_Ho;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_Dong;
        private System.Windows.Forms.Button btn_Test;
        public System.Windows.Forms.Label lbl_Code;
        private System.Windows.Forms.Label lbl_DateTime;
        private System.Windows.Forms.Timer timer1;
        public System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button btn_VisitAll;
        private System.Windows.Forms.Button btn_VisitCheck;
        private System.Windows.Forms.TextBox txt_CarNo;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
    }
}

