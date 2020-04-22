using Anda.Fluid.App.Common;
using Anda.Fluid.Domain.FluProgram;
using Anda.Fluid.Infrastructure.Msg;
using Anda.Fluid.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.App
{
    public class LoadFlu
    {
        private static readonly LoadFlu instance = new LoadFlu();
        private LoadFlu() { }
        public static LoadFlu Instance => instance;

        public void OpenFile(IMsgSender sender)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Title = "";
            ofd.Filter = "|*.flu";
            ofd.InitialDirectory = System.IO.Directory.GetCurrentDirectory() + "\\G";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                MsgCenter.Broadcast(Constants.MSG_LOAD_PROGRAM, sender, ofd.FileName);
                Properties.Settings.Default.ProgramName = ofd.FileName;
            }
        }

        public void SaveFile(FluidProgram program)
        {
            if(program == null)
            {
                return;
            }
            if (FluidProgram.CurrentFilePath == string.Empty)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "*.flu|*.*";
                sfd.FileName = program.Name;
                sfd.InitialDirectory = System.IO.Directory.GetCurrentDirectory() + "\\G";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    FluidProgram.CurrentFilePath = FileUtils.FormatFilePath(sfd.FileName, "flu");
                    MsgCenter.Broadcast(Constants.MSG_SAVE_PROGRAM, null, FluidProgram.CurrentFilePath);
                }
            }
            else
            {
                MsgCenter.Broadcast(Constants.MSG_SAVE_PROGRAM, null, FluidProgram.CurrentFilePath);
            }
        }
    }
}
