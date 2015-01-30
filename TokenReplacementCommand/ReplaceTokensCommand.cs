using Sitecore;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Shell.Framework.Commands;
using System.Collections.Specialized;
namespace MikeRobbins.TokenCommands
{
    public class ReplaceTokensCommand : Command
    {
        public override void Execute(CommandContext context)
        {

            Item item = context.Items[0];





            Context.ClientPage.SendMessage(this, "item:load(id=" + item.ID + ")");



        }

        private void ReplaceTokens(Item item)
        {
            try
            {
                item.Editing.BeginEdit();

                item.Fields.ReadAll();

                foreach (Field field in item.Fields)
                {
                    if (field.Value.Contains("$"))
                    {
                        Sitecore.Data.MasterVariablesReplacer replacer = Sitecore.Configuration.Factory.GetMasterVariablesReplacer();
                        Sitecore.Diagnostics.Assert.IsNotNull(replacer, "replacer");
                        replacer.ReplaceItem(item);
                    }
                }

                item.Editing.AcceptChanges(true, false);
            }
            catch (System.Exception ex)
            {
                item.Editing.CancelEdit();
            }

        }
    }
}
