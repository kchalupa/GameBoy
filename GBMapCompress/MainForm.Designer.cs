namespace kchalupa.Wilderness.GBMapCompress
{
  partial class MainForm
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
      this.m_inputDirectoryLabel = new System.Windows.Forms.Label();
      this.m_inputFileNavigator = new System.Windows.Forms.Button();
      this.m_outputFileLabel = new System.Windows.Forms.Label();
      this.m_outputFileNavigator = new System.Windows.Forms.Button();
      this.button1 = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // m_inputDirectoryLabel
      // 
      this.m_inputDirectoryLabel.AutoSize = true;
      this.m_inputDirectoryLabel.Location = new System.Drawing.Point(9, 9);
      this.m_inputDirectoryLabel.Name = "m_inputDirectoryLabel";
      this.m_inputDirectoryLabel.Size = new System.Drawing.Size(87, 13);
      this.m_inputDirectoryLabel.TabIndex = 0;
      this.m_inputDirectoryLabel.Text = "Input Directory:";
      // 
      // m_inputFileNavigator
      // 
      this.m_inputFileNavigator.Location = new System.Drawing.Point(132, 7);
      this.m_inputFileNavigator.Name = "m_inputFileNavigator";
      this.m_inputFileNavigator.Size = new System.Drawing.Size(26, 23);
      this.m_inputFileNavigator.TabIndex = 1;
      this.m_inputFileNavigator.Text = "...";
      this.m_inputFileNavigator.UseVisualStyleBackColor = true;
      this.m_inputFileNavigator.Click += new System.EventHandler(this.OnInputFileClickHandler);
      // 
      // m_outputFileLabel
      // 
      this.m_outputFileLabel.AutoSize = true;
      this.m_outputFileLabel.Location = new System.Drawing.Point(186, 12);
      this.m_outputFileLabel.Name = "m_outputFileLabel";
      this.m_outputFileLabel.Size = new System.Drawing.Size(69, 13);
      this.m_outputFileLabel.TabIndex = 2;
      this.m_outputFileLabel.Text = "Output File:";
      // 
      // m_outputFileNavigator
      // 
      this.m_outputFileNavigator.Location = new System.Drawing.Point(288, 4);
      this.m_outputFileNavigator.Name = "m_outputFileNavigator";
      this.m_outputFileNavigator.Size = new System.Drawing.Size(31, 23);
      this.m_outputFileNavigator.TabIndex = 3;
      this.m_outputFileNavigator.Text = "...";
      this.m_outputFileNavigator.UseVisualStyleBackColor = true;
      this.m_outputFileNavigator.Click += new System.EventHandler(this.OnOutputFileClickHandler);
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(253, 44);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(66, 23);
      this.button1.TabIndex = 4;
      this.button1.Text = "Start";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.OnStartClickHandler);
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.LightSteelBlue;
      this.ClientSize = new System.Drawing.Size(325, 74);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.m_outputFileNavigator);
      this.Controls.Add(this.m_outputFileLabel);
      this.Controls.Add(this.m_inputFileNavigator);
      this.Controls.Add(this.m_inputDirectoryLabel);
      this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ForeColor = System.Drawing.Color.DarkBlue;
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "MainForm";
      this.Text = "GB Map Compress";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label m_inputDirectoryLabel;
    private System.Windows.Forms.Button m_inputFileNavigator;
    private System.Windows.Forms.Label m_outputFileLabel;
    private System.Windows.Forms.Button m_outputFileNavigator;
    private System.Windows.Forms.Button button1;
  }
}

