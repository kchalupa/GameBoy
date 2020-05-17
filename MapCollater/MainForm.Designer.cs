
namespace kchalupa.WIlderness.MapCollater
{
  partial class MainForm
  {

    #region fields

    /// <summary>
    /// Directory label.
    /// </summary>
    private System.Windows.Forms.Label m_directoryLabel;

    /// <summary>
    /// Directory navigator button.
    /// </summary>
    private System.Windows.Forms.Button m_directoryNavigator;

    /// <summary>
    /// Label for the search pattern.
    /// </summary>
    private System.Windows.Forms.Label m_searchPatternLabel;

    /// <summary>
    /// Text box to enter the search pattern.
    /// </summary>
    private System.Windows.Forms.TextBox m_searchPatternTextBox;

    /// <summary>
    /// The start button.
    /// </summary>
    private System.Windows.Forms.Button m_startButton;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    #endregion

    #region methods

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
    } // Dispose(bool disposing)

    #endregion

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.m_directoryLabel = new System.Windows.Forms.Label();
      this.m_directoryNavigator = new System.Windows.Forms.Button();
      this.m_searchPatternLabel = new System.Windows.Forms.Label();
      this.m_searchPatternTextBox = new System.Windows.Forms.TextBox();
      this.m_startButton = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // m_directoryLabel
      // 
      this.m_directoryLabel.AutoSize = true;
      this.m_directoryLabel.ForeColor = System.Drawing.Color.DarkBlue;
      this.m_directoryLabel.Location = new System.Drawing.Point(13, 11);
      this.m_directoryLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.m_directoryLabel.Name = "m_directoryLabel";
      this.m_directoryLabel.Size = new System.Drawing.Size(79, 20);
      this.m_directoryLabel.TabIndex = 0;
      this.m_directoryLabel.Text = "Directory:";
      // 
      // m_directoryNavigator
      // 
      this.m_directoryNavigator.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.m_directoryNavigator.Location = new System.Drawing.Point(282, 8);
      this.m_directoryNavigator.Name = "m_directoryNavigator";
      this.m_directoryNavigator.Size = new System.Drawing.Size(32, 27);
      this.m_directoryNavigator.TabIndex = 1;
      this.m_directoryNavigator.Text = "...";
      this.m_directoryNavigator.UseVisualStyleBackColor = true;
      this.m_directoryNavigator.Click += new System.EventHandler(this.OnDirectoryNavigatorClickHandler);
      // 
      // m_searchPatternLabel
      // 
      this.m_searchPatternLabel.AutoSize = true;
      this.m_searchPatternLabel.Location = new System.Drawing.Point(13, 47);
      this.m_searchPatternLabel.Name = "m_searchPatternLabel";
      this.m_searchPatternLabel.Size = new System.Drawing.Size(115, 20);
      this.m_searchPatternLabel.TabIndex = 2;
      this.m_searchPatternLabel.Text = "Search Pattern:";
      // 
      // m_searchPatternTextBox
      // 
      this.m_searchPatternTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
      this.m_searchPatternTextBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.m_searchPatternTextBox.Location = new System.Drawing.Point(144, 44);
      this.m_searchPatternTextBox.Name = "m_searchPatternTextBox";
      this.m_searchPatternTextBox.Size = new System.Drawing.Size(170, 27);
      this.m_searchPatternTextBox.TabIndex = 3;
      // 
      // m_startButton
      // 
      this.m_startButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.m_startButton.Location = new System.Drawing.Point(239, 92);
      this.m_startButton.Name = "m_startButton";
      this.m_startButton.Size = new System.Drawing.Size(75, 29);
      this.m_startButton.TabIndex = 4;
      this.m_startButton.Text = "Start";
      this.m_startButton.UseVisualStyleBackColor = true;
      this.m_startButton.Click += new System.EventHandler(this.OnStartClickHandler);
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.LightSteelBlue;
      this.ClientSize = new System.Drawing.Size(326, 133);
      this.Controls.Add(this.m_startButton);
      this.Controls.Add(this.m_searchPatternTextBox);
      this.Controls.Add(this.m_searchPatternLabel);
      this.Controls.Add(this.m_directoryNavigator);
      this.Controls.Add(this.m_directoryLabel);
      this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ForeColor = System.Drawing.Color.DarkBlue;
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
      this.MaximizeBox = false;
      this.MaximumSize = new System.Drawing.Size(344, 180);
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(344, 180);
      this.Name = "MainForm";
      this.Text = "Map Collater";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

  } // class MainForm
} // namespace kchalupa.WIlderness.MapCollater
