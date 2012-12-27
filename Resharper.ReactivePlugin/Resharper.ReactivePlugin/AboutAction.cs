namespace Resharper.ReactivePlugin
{
    using System.Windows.Forms;
    using JetBrains.ActionManagement;
    using JetBrains.Application.DataContext;

    [ActionHandler("Resharper.ReactivePlugin.About")]
    public class AboutAction : IActionHandler
    {
        public bool Update(IDataContext context, ActionPresentation presentation, DelegateUpdate nextUpdate)
        {
            // return true or false to enable/disable this action
            return false;
        }

        public void Execute(IDataContext context, DelegateExecute nextExecute)
        {
            MessageBox.Show(
            "Reactive Extensions Plugin for Resharper\nOllie Riches\n\nA Resharper plug-in to help use the Reactive Extension libraries",
            "About Reactive Extensions Plugin for Resharper",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);
        }
    }
}
