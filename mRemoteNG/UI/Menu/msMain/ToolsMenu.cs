using System;
using System.Runtime.Versioning;
using System.Windows.Forms;
using mRemoteNG.App;
using mRemoteNG.Connection.Protocol.RDP;
using mRemoteNG.Container;
using mRemoteNG.Credential;
using mRemoteNG.Resources.Language;

namespace mRemoteNG.UI.Menu
{
    [SupportedOSPlatform("windows")]
    public class ToolsMenu : ToolStripMenuItem
    {
        private ToolStripMenuItem _mMenToolsSshTransfer;
        private ToolStripMenuItem _mMenToolsExternalApps;
        private ToolStripMenuItem _mMenToolsPortScan;
        private ToolStripMenuItem _mMenToolsUvncsc;
        private ToolStripMenuItem _mMenToolsQConnectRDPFile;

        public Form MainForm { get; set; }
        public ICredentialRepositoryList CredentialProviderCatalog { get; set; }

        public ToolsMenu()
        {
            Initialize();
        }

        private void Initialize()
        {
            _mMenToolsSshTransfer = new ToolStripMenuItem();
            _mMenToolsUvncsc = new ToolStripMenuItem();
            _mMenToolsExternalApps = new ToolStripMenuItem();
            _mMenToolsPortScan = new ToolStripMenuItem();
            _mMenToolsQConnectRDPFile = new ToolStripMenuItem();
            // 
            // mMenTools
            // 
            DropDownItems.AddRange(new ToolStripItem[]
            {
                _mMenToolsSshTransfer,
                _mMenToolsUvncsc,
                _mMenToolsExternalApps,
                _mMenToolsPortScan,
                _mMenToolsQConnectRDPFile
            });
            Name = "mMenTools";
            Size = new System.Drawing.Size(48, 20);
            Text = Language._Tools;
            // 
            // mMenToolsSSHTransfer
            // 
            _mMenToolsSshTransfer.Image = Properties.Resources.SyncArrow_16x;
            _mMenToolsSshTransfer.Name = "mMenToolsSSHTransfer";
            _mMenToolsSshTransfer.Size = new System.Drawing.Size(184, 22);
            _mMenToolsSshTransfer.Text = Language.SshFileTransfer;
            _mMenToolsSshTransfer.Click += mMenToolsSSHTransfer_Click;
            // 
            // mMenToolsUVNCSC
            // 
            _mMenToolsUvncsc.Name = "mMenToolsUVNCSC";
            _mMenToolsUvncsc.Size = new System.Drawing.Size(184, 22);
            _mMenToolsUvncsc.Text = Language.UltraVNCSingleClick;
            _mMenToolsUvncsc.Visible = false;
            _mMenToolsUvncsc.Click += mMenToolsUVNCSC_Click;
            // 
            // mMenToolsExternalApps
            // 
            _mMenToolsExternalApps.Image = Properties.Resources.Console_16x;
            _mMenToolsExternalApps.Name = "mMenToolsExternalApps";
            _mMenToolsExternalApps.Size = new System.Drawing.Size(184, 22);
            _mMenToolsExternalApps.Text = Language.ExternalTool;
            _mMenToolsExternalApps.Click += mMenToolsExternalApps_Click;
            // 
            // mMenToolsPortScan
            // 
            _mMenToolsPortScan.Image = Properties.Resources.SearchAndApps_16x;
            _mMenToolsPortScan.Name = "mMenToolsPortScan";
            _mMenToolsPortScan.Size = new System.Drawing.Size(184, 22);
            _mMenToolsPortScan.Text = Language.PortScan;
            _mMenToolsPortScan.Click += mMenToolsPortScan_Click;
            // 
            // mMenToolsPortScan
            // 
            _mMenToolsQConnectRDPFile.Image = Properties.Resources.OpenFile_16x;
            _mMenToolsQConnectRDPFile.Name = "mMenToolsQConnectRDPFile";
            _mMenToolsQConnectRDPFile.Size = new System.Drawing.Size(184, 22);
            _mMenToolsQConnectRDPFile.Text = Language.QConnectRDPFile;
            _mMenToolsQConnectRDPFile.Click += mMenToolsQConnectRDPFile_Click;
        }

        public void ApplyLanguage()
        {
            Text = Language._Tools;
            _mMenToolsSshTransfer.Text = Language.SshFileTransfer;
            _mMenToolsExternalApps.Text = Language.ExternalTool;
            _mMenToolsPortScan.Text = Language.PortScan;
        }

        #region Tools

        private void mMenToolsSSHTransfer_Click(object sender, EventArgs e)
        {
            Windows.Show(WindowType.SSHTransfer);
        }

        private void mMenToolsUVNCSC_Click(object sender, EventArgs e)
        {
            Windows.Show(WindowType.UltraVNCSC);
        }

        private void mMenToolsExternalApps_Click(object sender, EventArgs e)
        {
            Windows.Show(WindowType.ExternalApps);
        }

        private void mMenToolsPortScan_Click(object sender, EventArgs e)
        {
            Windows.Show(WindowType.PortScan);
        }

        private void mMenToolsOptions_Click(object sender, EventArgs e)
        {
            Windows.Show(WindowType.Options);
        }

        private void mMenToolsQConnectRDPFile_Click(object sender, EventArgs e)
        {
            try
            {

                var tempContainer = new ContainerInfo();
                Import.ImportFromFile(tempContainer);
                foreach (var connection in tempContainer.Children)
                {

                    try
                    {
                        connection.Resolution = RDPResolutions.FitToWindow;
                        Runtime.ConnectionInitiator.OpenConnection(connection, Connection.ConnectionInfo.Force.DoNotJump);
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddExceptionStackTrace("mMenToolsQConnectRDPFile_Click() failed.", ex);
                    }

                }

            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddExceptionMessage("Unable to import file.", ex);
            }

        }

        #endregion
    }
}