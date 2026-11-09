//using System.Windows.Forms;
//using static System.Net.Mime.MediaTypeNames;

namespace WorkflowExecutionDemo;

partial class MainForm
{
    private Button btnStart;
    private Button btnClear;
    private TextBox txtLog;

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
            components.Dispose();

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        btnStart = new Button();
        btnClear = new Button();
        txtLog = new TextBox();

        SuspendLayout();

        // btnStart
        btnStart.Location = new Point(12, 12);
        btnStart.Size = new Size(140, 35);
        btnStart.Text = "Start Workflow";
        btnStart.Click += btnStart_Click;

        // btnClear
        btnClear.Location = new Point(160, 12);
        btnClear.Size = new Size(120, 35);
        btnClear.Text = "Clear Log";
        btnClear.Click += btnClear_Click;

        // txtLog
        txtLog.Location = new Point(12, 60);
        txtLog.Multiline = true;
        txtLog.ScrollBars = ScrollBars.Vertical;
        txtLog.ReadOnly = true;
        txtLog.Font = new Font("Consolas", 9F);
        txtLog.Size = new Size(760, 380);
        txtLog.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

        // MainForm
        ClientSize = new Size(784, 461);
        Controls.Add(btnStart);
        Controls.Add(btnClear);
        Controls.Add(txtLog);
        Text = "Workflow Execution Demo v1";
        ResumeLayout(false);
        PerformLayout();
    }
}

/*
 * namespace WorkflowExecutionDemo
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "Form1";
        }

        #endregion
    }
}
*/
